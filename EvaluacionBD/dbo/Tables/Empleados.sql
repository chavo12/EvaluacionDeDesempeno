﻿CREATE TABLE [dbo].[Empleados] (
    [IdEmpleado]        INT           IDENTITY (1, 1) NOT NULL,
    [EmpleadoId]        VARCHAR (50)  NOT NULL,
    [Nombre]            VARCHAR (100) NOT NULL,
    [PApellido]         VARCHAR (50)  NOT NULL,
    [SApellido]         VARCHAR (50)  NULL,
    [Cargo]             VARCHAR (200) NULL,
    [Departamento]      VARCHAR (200) NULL,
    [Negocio]           VARCHAR (50)  NULL,
    [TipoEmpleado]      VARCHAR (50)  NULL,
    [RazonSocial]       VARCHAR (100) NULL,
    [Ingreso]           VARCHAR (50)  NULL,
    [Nacimiento]        VARCHAR (50)  NULL,
    [TipoEvaluacion]    VARCHAR (50)  NULL,
    [Pais]              VARCHAR (50)  NULL,
    [CorreoElectronico] VARCHAR (100) NULL,
    [SupervisorID]      INT           NULL,
    [Localidad]         VARCHAR (100) NULL,
    [CiudadLocalidad]   VARCHAR (100) NULL,
    [Nivel]             VARCHAR (50)  NULL,
    [NumPia]            VARCHAR (100) NULL,
    [Rol]               VARCHAR (100) NULL,
    [clave] VARCHAR(MAX) NULL, 
    [resetClave] UNIQUEIDENTIFIER NULL, 
    [fechaResetClave] DATETIME NULL, 
    CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED ([IdEmpleado] ASC),
    CONSTRAINT [FK_Empleados_Empleados] FOREIGN KEY ([SupervisorID]) REFERENCES [dbo].[Empleados] ([IdEmpleado])
);

