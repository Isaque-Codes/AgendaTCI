import styles from "./home.module.css";
import ListagemPacientes from "@//components/listagem-pacientes/listagem-pacientes";

const Home = () => {
    return (
        <>
            <header className={styles.header}>
                <div className={`${styles.container} layout_guide`}>
                    <img src="/imgs/logo.png" alt="Logo da TCI Sistemas" className={styles.logo_empresa} />
                </div>
            </header>
            <main className={styles.pagina_gerenciamento}>
                <div className={styles.cabecalho_pagina}>
                    <span className={styles.sobretitulo}>GESTÃO DE CONTATOS</span>
                </div>
                <ListagemPacientes />
            </main>
        </>
    );
};

export default Home;
