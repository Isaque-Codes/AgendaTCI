export interface ListagemPaciente {
    id: string;
    nome: string;
    apelido: string;
    cpf: string;
    email: string;
    telefone: string;
    dataCadastro: string;
    dataUltimaAlteracao: string;
}

export interface CadastroPaciente {
    nome: string;
    apelido: string;
    cpf: string;
    email: string;
    telefone: string;
}

export interface AtualizacaoPaciente {
    nome: string;
    apelido: string;
    cpf: string;
    email: string;
    telefone: string;
}
