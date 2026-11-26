using System.ComponentModel.DataAnnotations;

namespace AgendaTCI.Applications.Validacoes
{
    public static class ValidarEmail
    {
        public static bool Validar(string? email) // Retorna true se o email for valido
        {
            if (string.IsNullOrWhiteSpace(email) || email.Length > 254) // Impede nulos e overflow no banco
                return false;

            // Valida o formato do email
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}