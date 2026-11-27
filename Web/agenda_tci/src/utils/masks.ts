// Remove tudo que nao eh digito
export function RemoverNaoNumericos(valor: string) {
    return valor.replace(/\D/g, "");
}

export function FormatarCPF(valor: string) { // Formata o CPF enquanto o usuario digita para 000.000.000-00
    // Limita a 11 digitos
    const cpf = RemoverNaoNumericos(valor).slice(0, 11);

    return cpf
        // Coloca 1 ponto a cada 3 digitos    
        .replace(/(\d{3})(\d)/, "$1.$2")
        .replace(/(\d{3})(\d)/, "$1.$2")
        .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
}

// Formata o telefone enquanto o usuario digita para (00) 0000-0000 ou (00) 00000-0000
export function FormatarTelefone(valor: string) {
    // Somente numeros, sem o 55 do pais no começo, limitado a 11 digitos (DDD + número)
    const telefone = RemoverNaoNumericos(valor)
        .replace(/^55/, "")
        .slice(0, 11);

    // Ate 10 digitos: telefone fixo, (00) 0000-0000
    if (telefone.length <= 10) {
        return telefone
            // Coloca o DDD entre parenteses e um espaco depois
            .replace(/(\d{2})(\d)/, "($1) $2")
            // Coloca o traco antes dos ultimos 4 digitos
            .replace(/(\d{4})(\d{1,4})$/, "$1-$2");
    }

    // 11 digitos: celular, (00) 00000-0000
    return telefone
        .replace(/(\d{2})(\d)/, "($1) $2")
        // Coloca o traco antes dos ultimos 4 digitos, com 5 digitos antes dele
        .replace(/(\d{5})(\d{1,4})$/, "$1-$2");
}