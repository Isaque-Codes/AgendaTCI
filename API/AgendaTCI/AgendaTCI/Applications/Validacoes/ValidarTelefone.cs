using System.Text.RegularExpressions;

namespace AgendaTCI.Applications.Validacoes
{
    public static class ValidarTelefone
    {
        public static bool Validar(string? telefone, out string telefoneFormatado) // Retorna true com o numero formatado se ele for valido
        {
            telefoneFormatado = string.Empty;

            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            // Mantem apenas digitos
            string numero = Regex.Replace(telefone, @"\D", "");

            // Garante que o numero comece com 55
            if (numero.Length is 10 or 11)
                numero = "55" + numero;

            // Aceita somente 55 + DDD + 8 digitos para fixo e 9 para celular
            if ((numero.Length != 12 && numero.Length != 13)
                || !numero.StartsWith("55"))
                return false;

            telefoneFormatado = numero;
            return true;
        }
    }
}