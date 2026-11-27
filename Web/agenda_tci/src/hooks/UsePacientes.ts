import { FormEvent, useEffect, useState } from "react";
import { CadastroPaciente, ListagemPaciente, AtualizacaoPaciente } from "../../@types/paciente";
import { Cadastrar, BuscarPorNome, Listar, Atualizar, Deletar } from "../services/api/agendaService";
import { ValidarNome, ValidarCPF, ValidarEmail, ValidarTelefone } from "../utils/validations";

export function usePaciente() {
    const [erro, setErro] = useState("")
    const [modalAberto, setModalAberto] = useState(false);
    const [paciente, setPaciente] = useState<ListagemPaciente[]>([]);
    const [busca, setBusca] = useState("");

    async function listarPaciente() {
        try {
            const lista = await Listar();
            setPaciente(lista)
        } catch (error: any) {
            console.log(error.message)
        }
    }

    useEffect(() => {
        listarPaciente();
    }, [])

    async function buscarPorNome(nome: string) {
        try {
            const lista = nome.trim() ? await BuscarPorNome(nome) : await Listar();
            setPaciente(lista);
        } catch (error: any) {
            setErro(error.message);
        }
    }

    async function cadastrarPaciente(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();

        setErro("");

        const formulario = new FormData(event.currentTarget);

        const dadosCadastro: CadastroPaciente = {
            nome: String(formulario.get("nome") ?? ""),
            apelido: String(formulario.get("apelido") ?? ""),
            cpf: String(formulario.get("cpf") ?? ""),
            email: String(formulario.get("email") ?? ""),
            telefone: String(formulario.get("telefone") ?? "")
        };

        if (!ValidarNome(dadosCadastro.nome)) {
            setErro("Informe um nome com até 75 caracteres.");
            return;
        }

        if (!ValidarCPF(dadosCadastro.cpf)) {
            setErro("O CPF deve ter 11 dígitos.");
            return;
        }

        if (!ValidarEmail(dadosCadastro.email)) {
            setErro("Informe um e-mail válido.");
            return;
        }

        if (!ValidarTelefone(dadosCadastro.telefone)) {
            setErro("Informe um telefone com DDD, ex: (11) 99999-9999.");
            return;
        }

        try {
            await Cadastrar(dadosCadastro);

            const listaAtualizada = await Listar();
            setPaciente(listaAtualizada);

            setModalAberto(false);
        } catch (error: any) {
            setErro(error.message);
        }
    }

    async function atualizarPaciente(id: string, event: FormEvent<HTMLFormElement>) {
        event.preventDefault();

        setErro("");

        const formulario = new FormData(event.currentTarget);

        const dadosAtualizacao: AtualizacaoPaciente = {
            nome: String(formulario.get("nome") ?? ""),
            apelido: String(formulario.get("apelido") ?? ""),
            cpf: String(formulario.get("cpf") ?? ""),
            email: String(formulario.get("email") ?? ""),
            telefone: String(formulario.get("telefone") ?? "")
        };

        if (!ValidarNome(dadosAtualizacao.nome)) {
            setErro("Informe um nome com até 75 caracteres.");
            return;
        }

        if (!ValidarCPF(dadosAtualizacao.cpf)) {
            setErro("O CPF deve ter 11 dígitos.");
            return;
        }

        if (!ValidarEmail(dadosAtualizacao.email)) {
            setErro("Informe um e-mail válido.");
            return;
        }

        if (!ValidarTelefone(dadosAtualizacao.telefone)) {
            setErro("Informe um telefone com DDD, ex: (11) 99999-9999.");
            return;
        }

        try {
            await Atualizar(id, dadosAtualizacao);

            const listaAtualizada = await Listar();
            setPaciente(listaAtualizada);

            setModalAberto(false);
        } catch (error: any) {
            setErro(error.message);
        }
    }

    async function excluirPaciente(id: string) {
        setErro("");

        try {
            await Deletar(id);

            const listaAtualizada = await Listar();
            setPaciente(listaAtualizada);
        } catch (error: any) {
            setErro(error.message);
        }
    }

    return {
        paciente,
        buscarPorNome,
        modalAberto,
        setModalAberto,
        erro,
        setErro,
        cadastrarPaciente,
        atualizarPaciente,
        excluirPaciente
    };
}