namespace AgendaTCI.DTOs.AgendaDto
{
    public class CadastrarAgendaDto
    {
        public string Nome { get; set; } = null!;

        public string? Apelido { get; set; }

        public string CPF { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;
    }
}
