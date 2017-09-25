CREATE TABLE [dbo].[RespuestasEvaluacionAudit] (
    [idRespuestasAudit] INT           IDENTITY (1, 1) NOT NULL,
    [idRespuesta]       INT           NULL,
    [idEvaluacion]      INT           NULL,
    [idItem]            INT           NULL,
    [valor]             VARCHAR (MAX) NULL,
    [valorSupervisor]   VARCHAR (MAX) NULL,
    [escrito]           VARCHAR (MAX) NULL,
    [escritoSupervisor] VARCHAR (MAX) NULL,
    [FechaModif]        DATETIME      NOT NULL,
    CONSTRAINT [PK_RespuestasEvaluacionAudit] PRIMARY KEY CLUSTERED ([idRespuestasAudit] ASC)
);

