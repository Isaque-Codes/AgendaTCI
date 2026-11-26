namespace AgendaTCI.Applications.Validacoes
{
    public static class ValidarNome
    {
        public static bool Validar(string? nome) // Retorna true se o nome for válido
        {
            return !string.IsNullOrWhiteSpace(nome) // Impede nulos e overflow no banco
                && nome.Trim().Length <= 75;
        }
    }
}