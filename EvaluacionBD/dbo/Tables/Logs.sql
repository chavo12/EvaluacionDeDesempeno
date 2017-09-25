CREATE TABLE [dbo].[Logs] (
    [IdLogs]     INT           IDENTITY (1, 1) NOT NULL,
    [IdEmpleado] INT           NOT NULL,
    [Pagina]     VARCHAR (100) NOT NULL,
    [Detalle]    VARCHAR (MAX) NULL,
    [FechayHora] DATETIME      NOT NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([IdLogs] ASC)
);

