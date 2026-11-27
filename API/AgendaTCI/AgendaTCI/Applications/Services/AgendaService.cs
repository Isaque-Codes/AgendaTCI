using AgendaTCI.Applications.Conversoes.ConversaoDTO;
using AgendaTCI.Applications.Validacoes;
using AgendaTCI.Domains;
using AgendaTCI.DTOs.AgendaDto;
using AgendaTCI.Exceptions;
using AgendaTCI.Interfaces;

namespace AgendaTCI.Applications.Services
{
    public class AgendaService
    {
        private readonly IAgendaRepository _repository;

        public AgendaService(IAgendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LerAgendaDto>> ListarAsync()
        {
            List<Agenda> agendas = await _repository.ListarAsync();

            return agendas
                .Select(ConversaoAgendaDto.ToDto)
                .ToList();
        }

        public async Task<LerAgendaDto> BuscarPorIdAsync(Guid id)
        {
            Agenda? agenda = await _repository.BuscarPorIdAsync(id);

            if (agenda == null)
                throw new DomainException("Paciente não encontrado ou inexistente.");

            return ConversaoAgendaDto.ToDto(agenda);
        }

        public async Task<List<LerAgendaDto>> BuscarPorNomeAsync(string nome)
        {
            List<Agenda> agendas = await _repository.BuscarPorNomeAsync(nome.Trim());

            return agendas
                .Select(ConversaoAgendaDto.ToDto)
                .ToList();
        }

        public async Task CadastrarAsync(CadastrarAgendaDto dto)
        {
            if (!ValidarNome.Validar(dto.Nome))
                throw new DomainException("O nome não pode ser nulo e nem ultrapassar 75 caracteres.");

            if (!ValidarCPF.Validar(dto.CPF, out string cpfFormatado))
                throw new DomainException("Informe um CPF válido.");

            if (!ValidarEmail.Validar(dto.Email))
                throw new DomainException("Informe um e-mail válido.");

            if (!ValidarTelefone.Validar(dto.Telefone, out string telefoneFormatado))
                throw new DomainException("Informe um telefone válido.");

            dto.CPF = cpfFormatado;
            dto.Telefone = telefoneFormatado;

            if (await _repository.CPFExisteAsync(dto.CPF))
                throw new DomainException("Já existe um paciente cadastrado com este CPF.");

            if (await _repository.EmailExisteAsync(dto.Email))
                throw new DomainException("Já existe um paciente cadastrado com este e-mail.");

            Agenda agenda = ConversaoAgendaDto.ToEntity(dto);

            await _repository.CadastrarAsync(agenda);
        }

        public async Task AtualizarAsync(Guid id, AtualizarAgendaDto dto)
        {
            if (!ValidarNome.Validar(dto.Nome))
                throw new DomainException("O nome não pode ser nulo e nem ultrapassar 75 caracteres.");

            if (!ValidarCPF.Validar(dto.CPF, out string cpfFormatado))
                throw new DomainException("Informe um CPF válido.");

            if (!ValidarEmail.Validar(dto.Email))
                throw new DomainException("Informe um e-mail válido.");

            if (!ValidarTelefone.Validar(dto.Telefone, out string telefoneFormatado))
                throw new DomainException("Informe um telefone válido.");

            dto.CPF = cpfFormatado;
            dto.Telefone = telefoneFormatado;

            Agenda? agenda = await _repository.BuscarPorIdAsync(id);

            if (agenda == null)
                throw new DomainException("Paciente não encontrado ou inexistente.");

            if (dto.CPF != agenda.CPF &&
                await _repository.CPFExisteAsync(dto.CPF))
                throw new DomainException("Já existe um paciente cadastrado com este CPF.");

            if (dto.Email != agenda.Email &&
                await _repository.EmailExisteAsync(dto.Email))
                throw new DomainException("Já existe um paciente cadastrado com este e-mail.");

            ConversaoAgendaDto.UpdateEntity(dto, agenda);

            await _repository.AtualizarAsync(id, agenda);
        }

        public async Task DeletarAsync(Guid id)
        {
            Agenda? agenda = await _repository.BuscarPorIdAsync(id);

            if (agenda == null)
                throw new DomainException("Paciente não encontrado ou inexistente.");

            await _repository.DeletarAsync(id);
        }
    }
}