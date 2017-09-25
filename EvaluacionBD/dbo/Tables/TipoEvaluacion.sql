CREATE TABLE [dbo].[TipoEvaluacion] (
    [IdTipoEvaluacion] INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]           VARCHAR (50)  NOT NULL,
    [Descripcion]      VARCHAR (MAX) NULL,
    [Supervisa]        BIT           NULL,
    [Idioma]           VARCHAR (3)   NOT NULL,
    CONSTRAINT [PK_TipoEvaluacion] PRIMARY KEY CLUSTERED ([IdTipoEvaluacion] ASC)
);

