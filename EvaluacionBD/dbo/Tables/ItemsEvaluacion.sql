CREATE TABLE [dbo].[ItemsEvaluacion] (
    [IdItem]          INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion]     VARCHAR (MAX) NOT NULL,
    [idTipoEvalucion] INT           NOT NULL,
    [Nivel]           VARCHAR (50)  NULL,
    [Escrito]         BIT           NULL,
    [Idioma]          VARCHAR (3)   NOT NULL,
    CONSTRAINT [PK_ItemsEvaluacion] PRIMARY KEY CLUSTERED ([IdItem] ASC),
    CONSTRAINT [FK_ItemsEvaluacion_TipoEvaluacion] FOREIGN KEY ([idTipoEvalucion]) REFERENCES [dbo].[TipoEvaluacion] ([IdTipoEvaluacion])
);

