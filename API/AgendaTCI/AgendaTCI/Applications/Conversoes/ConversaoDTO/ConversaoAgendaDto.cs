using AgendaTCI.Domains;
using AgendaTCI.DTOs.AgendaDto;

namespace AgendaTCI.Applications.Conversoes.ConversaoDTO
{
    public static class ConversaoAgendaDto
    {
        public static LerAgendaDto ToDto(Agenda entity)
        {
            return new LerAgendaDto
            {
                Id = entity.PacienteID,
                Nome = entity.Nome,
                Apelido = entity.Apelido,
                CPF = entity.CPF,
                Email = entity.Email,
                Telefone = entity.Telefone,
                DataCadastro = entity.DataCadastro,
                DataUltimaAlteracao = entity.DataUltimaAlteracao
            };
        }

        public static Agenda ToEntity(CadastrarAgendaDto dto)
        {
            return new Agenda
            {
                Nome = dto.Nome,
                Apelido = dto.Apelido,
                CPF = dto.CPF,
                Email = dto.Email,
                Telefone = dto.Telefone
            };
        }

        public static void UpdateEntity(AtualizarAgendaDto dto, Agenda entity)
        {
            entity.Nome = dto.Nome;
            entity.Apelido = dto.Apelido;
            entity.CPF = dto.CPF;
            entity.Email = dto.Email;
            entity.Telefone = dto.Telefone;
        }
    }
}