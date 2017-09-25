CREATE TABLE [dbo].[Evaluacion] (
    [IdEvaluacion]     INT          IDENTITY (1, 1) NOT NULL,
    [IdEmpleado]       INT          NOT NULL,
    [Estado]           VARCHAR (50) NOT NULL,
    [Inicio]           DATETIME     NOT NULL,
    [Fin]              DATETIME     NULL,
    [FechaEstado]      DATETIME     NOT NULL,
    [InicioSupervisor] DATETIME     NOT NULL,
    [FinSupervisor]    DATETIME     NULL,
    CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED ([IdEvaluacion] ASC),
    CONSTRAINT [FK_Evaluacion_Empleados] FOREIGN KEY ([IdEmpleado]) REFERENCES [dbo].[Empleados] ([IdEmpleado])
);

