using AgendaTCI.Domains;

namespace AgendaTCI.Interfaces
{
    public interface IAgendaRepository
    {
        Task<List<Agenda>> ListarAsync();

        Task<Agenda?> BuscarPorIdAsync(Guid id);

        Task<Agenda?> BuscarPorNomeAsync(string nome);

        Task<Agenda?> BuscarPorEmailAsync(string email);

        Task<Agenda?> BuscarPorCPFAsync(string cpf);

        Task<Agenda?> BuscarPorTelefoneAsync(string telefone);

        Task<bool> EmailExisteAsync(string email);

        Task<bool> CPFExisteAsync(string cpf);

        Task CadastrarAsync(Agenda agenda);

        Task AtualizarAsync(Guid id, Agenda agenda);

        Task DeletarAsync(Guid id);
    }
}