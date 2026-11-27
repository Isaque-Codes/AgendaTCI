using AgendaTCI.Contexts;
using AgendaTCI.Domains;
using AgendaTCI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaTCI.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {

        private readonly TCIContext _context;

        public AgendaRepository(TCIContext context)
        {
            _context = context;
        }

        public Task<List<Agenda>> ListarAsync()
        {
            return _context.Agenda.AsNoTracking()
                .OrderByDescending(a => a.DataCadastro)
                .ToListAsync();
        }

        public Task<Agenda?> BuscarPorIdAsync(Guid id)
        {
            return _context.Agenda.AsNoTracking()
                .OrderByDescending(a => a.DataCadastro)
                .FirstOrDefaultAsync(a => a.PacienteID == id);
        }

        public Task<List<Agenda>> BuscarPorNomeAsync(string nome)
        {
            return _context.Agenda.AsNoTracking()
                .Where(a => a.Nome.StartsWith(nome))
                .OrderBy(a => a.Nome)
                .ToListAsync();
        }

        public Task<bool> EmailExisteAsync(string email)
        {
            return _context.Agenda
                .AnyAsync(a => a.Email == email);
        }

        public Task<bool> CPFExisteAsync(string cpf)
        {
            return _context.Agenda
                .AnyAsync(a => a.CPF == cpf);
        }

        public async Task CadastrarAsync(Agenda agenda)
        {
            await _context.Agenda.AddAsync(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, Agenda agenda)
        {
            var agendaBanco = await _context.Agenda.FindAsync(id);

            if (agendaBanco == null)
                return;

            agendaBanco.Nome = agenda.Nome;
            agendaBanco.Apelido = agenda.Apelido;
            agendaBanco.CPF = agenda.CPF;
            agendaBanco.Email = agenda.Email;
            agendaBanco.Telefone = agenda.Telefone;
            agendaBanco.DataUltimaAlteracao = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var agendaBanco = await _context.Agenda.FindAsync(id);

            if (agendaBanco is null)
                return;

            _context.Agenda.Remove(agendaBanco);
            await _context.SaveChangesAsync();
        }
    }
}