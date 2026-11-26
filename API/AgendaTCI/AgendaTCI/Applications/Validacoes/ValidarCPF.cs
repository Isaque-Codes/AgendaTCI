using System.Text.RegularExpressions;

namespace AgendaTCI.Applications.Validacoes
{
    public static class ValidarCPF
    {
        public static bool Validar(string? cpf, out string cpfFormatado) // Retorna true com o CPF formatado se ele for valido
        {
            // Remove espacos, pontuacao e qualquer caractere que nao seja digito
            cpfFormatado = Regex.Replace(cpf ?? string.Empty, @"\D", "");

            if (cpfFormatado.Length != 11)
            {
                cpfFormatado = string.Empty;
                return false;
            }

            return true;
        }
    }
}