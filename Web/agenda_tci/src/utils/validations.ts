import { RemoverNaoNumericos } from "./masks";

export function ValidarNome(nome: string): boolean {
    return nome.trim().length > 0 && nome.trim().length <= 75;
}

export function ValidarCPF(cpf: string): boolean {
    return RemoverNaoNumericos(cpf).length === 11;
}

// Retorna true se seguir o formato texto@dominio.extensao (sem espaços e sem @ repetido)
export function ValidarEmail(email: string): boolean {
    return (
        email.trim().length > 0 &&
        email.length <= 254 &&
        /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)
    );
}

export function ValidarTelefone(telefone: string): boolean {
    let numero = RemoverNaoNumericos(telefone);

    if (numero.length === 10 || numero.length === 11) {
        numero = "55" + numero;
    }

    return (numero.length === 12 || numero.length === 13) && numero.startsWith("55");
}