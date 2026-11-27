
import { ListagemPaciente, CadastroPaciente, AtualizacaoPaciente } from "../../../@types/paciente";
import { api } from "./api";

export async function Listar(): Promise<ListagemPaciente[]> {
    try {
        const response = await api.get("");

        return response.data;
    } catch (error: any) {
        throw new Error(error.response.data);
    }
}

export async function BuscarPorNome(nome: string): Promise<ListagemPaciente[]> {
    try {
        const response = await api.get("/buscar/nome", { params: { nome } });
        return response.data;
    } catch (error: any) {
        throw new Error(error.response?.data);
    }
}

export async function Cadastrar(dadosCadastro: CadastroPaciente) {
    try {
        await api.post("", dadosCadastro);
    }
    catch (error: any) {
        throw new Error(error.response.data);
    }
}

export async function Atualizar(id: string, dadosAtualizacao: AtualizacaoPaciente) {
    try {
        await api.put(`/${id}`, dadosAtualizacao);
    }
    catch (error: any) {
        throw new Error(error.response.data);
    }
}

export async function Deletar(id: string) {
    try {
        await api.delete(`${id}`);
    }
    catch (error: any) {
        throw new Error(error.response.data)
    }
}