using System;
using System.Collections.Generic;

namespace AgendaTCI.Domains;

public partial class Agenda
{
    public Guid PacienteID { get; set; }

    public string Nome { get; set; } = null!;

    public string? Apelido { get; set; }

    public string CPF { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public DateTime? DataUltimaAlteracao { get; set; }
}
