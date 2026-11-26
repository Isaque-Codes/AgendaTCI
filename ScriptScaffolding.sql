CREATE DATABASE TCI;
GO

USE TCI;
GO

CREATE TABLE Agenda
(
    PacienteID UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT DF_Agenda_PacienteID DEFAULT NEWSEQUENTIALID(),

    Nome NVARCHAR(75) NOT NULL,
    Apelido NVARCHAR(50) NULL,
    CPF CHAR(11) NOT NULL,
    Email VARCHAR(254) NOT NULL,
    Telefone VARCHAR(13) NOT NULL,

    DataCadastro DATETIME2(0) NOT NULL
        CONSTRAINT DF_Agenda_DataCadastro DEFAULT SYSUTCDATETIME(),

    DataUltimaAlteracao DATETIME2(0) NULL,

    CONSTRAINT PK_Agenda
        PRIMARY KEY (PacienteID),

    CONSTRAINT UQ_Agenda_CPF
        UNIQUE (CPF),

    CONSTRAINT UQ_Agenda_Email
        UNIQUE (Email),

    CONSTRAINT CK_Agenda_CPF_Formato
        CHECK
        (
            LEN(CPF) = 11
            AND CPF NOT LIKE '%[^0-9]%'
        ),

    CONSTRAINT CK_Agenda_Telefone_BR
        CHECK
        (
            LEN(Telefone) IN (12, 13)
            AND Telefone LIKE '55%'
            AND Telefone NOT LIKE '%[^0-9]%'
        )
);
GO

CREATE TRIGGER TR_Agenda_AtualizaDataAlteracao
ON Agenda
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Garante que nao haja acionamento repetido por conta de nova atualizacao decorrente da propria trigger
    IF TRIGGER_NESTLEVEL() > 1
        RETURN;

    UPDATE A
    SET DataUltimaAlteracao = SYSUTCDATETIME()
    FROM Agenda AS A
    INNER JOIN inserted AS I
        ON I.PacienteID = A.PacienteID;
END;
GO