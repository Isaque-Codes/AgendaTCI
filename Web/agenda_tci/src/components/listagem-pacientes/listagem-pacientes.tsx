import { useEffect, useState, type FormEvent } from "react";
import styles from "./listagem-pacientes.module.css";
import { ListagemPaciente, CadastroPaciente, AtualizacaoPaciente } from "../../../@types/paciente";
import { usePaciente } from "@//hooks/UsePacientes";
import { FormatarCPF, FormatarTelefone } from "@//utils/masks";



const ListagemPacientes = () => {

    const {
        paciente,
        buscarPorNome,
        modalAberto,
        setModalAberto,
        erro,
        setErro,
        cadastrarPaciente,
        atualizarPaciente,
        excluirPaciente
    } = usePaciente();

    const [pacienteEmEdicao, setPacienteEmEdicao] = useState<ListagemPaciente | null>(null);

    function confirmarExclusao(item: ListagemPaciente) {
        const confirmou = window.confirm(`Excluir o paciente ${item.nome}?`);

        if (confirmou) {
            excluirPaciente(item.id);
        }
    }

    const LIMITE = 5;
    const [pagina, setPagina] = useState(1);

    const totalPaginas = Math.max(1, Math.ceil(paciente.length / LIMITE));
    const paginaAtual = Math.min(pagina, totalPaginas);
    const inicio = (paginaAtual - 1) * LIMITE;
    const pacientesPagina = paciente.slice(inicio, inicio + LIMITE);

    return (
        <>
            <section className={styles.painel_pacientes}>
                <div className={styles.barra_ferramentas}>
                    <button className={styles.botao_primario} type="button" onClick={() => {
                        setPacienteEmEdicao(null); setErro(""); setModalAberto(true);
                    }}>
                        <span className={styles.icone_adicionar}>+</span>
                        Adicionar novo paciente
                    </button>
                    <div className={styles.campo_busca}>
                        <input
                            type="text" placeholder="Filtrar por nome..."
                            onChange={(event) => {
                                setPagina(1);
                                buscarPorNome(event.target.value);
                            }}
                        />
                        <button type="button">⌕</button>
                    </div>
                </div>

                <div className={styles.tabela_cartao}>
                    <div className={styles.tabela_titulo}>
                        <h2>Lista de pacientes</h2>
                        <span className={styles.contador_registros}>
                            {paciente.length} {paciente.length === 1 ? "cadastro" : "cadastros"}
                        </span>
                    </div>
                    <table className={styles.tabela_pacientes}>
                        <thead>
                            <tr>
                                <th>Nome ↑</th>
                                <th>Apelido</th>
                                <th>CPF</th>
                                <th>E-mail</th>
                                <th>Telefone</th>
                                <th>Data cadastro</th>
                                <th>Última alteração</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {paciente.length > 0 ? (
                                pacientesPagina.map((item) => (
                                    <tr key={item.id}>
                                        <td className={styles.nome_paciente}>{item.nome}</td>
                                        <td>{item.apelido}</td>
                                        <td>{item.cpf}</td>
                                        <td>{item.email}</td>
                                        <td>{item.telefone}</td>
                                        <td>{new Date(item.dataCadastro).toLocaleDateString("pt-BR")}</td>
                                        <td>{item.dataUltimaAlteracao
                                            ? new Date(item.dataUltimaAlteracao).toLocaleDateString("pt-BR")
                                            : "—"}
                                        </td>

                                        <td className={styles.acoes_paciente}>
                                            <button className={styles.botao_icone} type="button" onClick={() => {
                                                setPacienteEmEdicao(item); setErro(""); setModalAberto(true);
                                            }}>
                                                ✎
                                            </button>

                                            <button className={styles.botao_icone} type="button" onClick={() => confirmarExclusao(item)}>
                                                ■
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan={8}>
                                        Nenhum paciente cadastrado.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                    <div className={styles.rodape_tabela}>
                        <span>
                            {paciente.length === 0 ? "Nenhum registro"
                                : `Mostrando ${inicio + 1}–${inicio + pacientesPagina.length} de ${paciente.length} registros`}
                        </span>
                        <div className={styles.paginacao}>
                            <button type="button" onClick={() => setPagina(1)} disabled={paginaAtual === 1}>«</button>
                            <button type="button" onClick={() => setPagina(paginaAtual - 1)} disabled={paginaAtual === 1}>‹</button>
                            <span className={styles.pagina_atual}>{paginaAtual}</span>
                            <span className={styles.paginacao_total}>de {totalPaginas}</span>
                            <button type="button" onClick={() => setPagina(paginaAtual + 1)} disabled={paginaAtual === totalPaginas}>›</button>
                            <button type="button" onClick={() => setPagina(totalPaginas)} disabled={paginaAtual === totalPaginas}>»</button>
                        </div>
                    </div>
                </div>
            </section>

            {modalAberto && (
                <div className={styles.fundo_modal}>
                    <div className={styles.modal_paciente}>
                        <div className={styles.modal_cabecalho}>
                            <div>
                                <span className={styles.sobretitulo}>AGENDA DE PACIENTES</span>
                                <h2>{pacienteEmEdicao ? "Editar paciente" : "Adicionar paciente"}</h2>
                            </div>
                            <button className={styles.botao_fechar} type="button" onClick={() => setModalAberto(false)}>×</button>
                        </div>
                        <form onSubmit={(event) => {
                            if (pacienteEmEdicao) {
                                atualizarPaciente(pacienteEmEdicao.id, event);
                            } else {
                                cadastrarPaciente(event);
                            }
                        }}>
                            <div className={styles.formulario_grade}>
                                <label className={styles.campo_largo}>
                                    <span>Nome completo</span>
                                    <input name="nome" type="text" placeholder="Ex.: Ana Beatriz Silva" defaultValue={pacienteEmEdicao?.nome} />
                                </label>
                                <label>
                                    <span>Apelido</span>
                                    <input name="apelido" type="text" placeholder="Como prefere ser chamado(a)" defaultValue={pacienteEmEdicao?.apelido} />
                                </label>
                                <label>
                                    <span>CPF</span>
                                    <input name="cpf" type="text" placeholder="000.000.000-00"
                                        defaultValue={pacienteEmEdicao ? FormatarCPF(pacienteEmEdicao.cpf) : ""}
                                        onChange={(event) => {
                                            event.currentTarget.value = FormatarCPF(event.currentTarget.value);
                                        }} />
                                </label>
                                <label className={styles.campo_largo}>
                                    <span>E-mail</span>
                                    <input name="email" type="email" placeholder="nome@exemplo.com" defaultValue={pacienteEmEdicao?.email} />
                                </label>
                                <label>
                                    <span>Telefone</span>
                                    <input name="telefone" type="text" placeholder="(11) 99999-9999"
                                        defaultValue={pacienteEmEdicao ? FormatarTelefone(pacienteEmEdicao.telefone) : ""}
                                        onChange={(event) => {
                                            event.currentTarget.value = FormatarTelefone(event.currentTarget.value);
                                        }} />
                                </label>
                            </div>
                            {erro && <p style={{ color: "red" }}>{erro}</p>}
                            <div className={styles.modal_acoes}>
                                <button className={styles.botao_secundario} type="button" onClick={() => { setModalAberto(false); setErro(""); }}>Cancelar</button>
                                <button className={styles.botao_primario} type="submit">{pacienteEmEdicao ? "Salvar alterações" : "Cadastrar paciente"}</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </>
    );
};

export default ListagemPacientes;
