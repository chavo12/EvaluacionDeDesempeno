CREATE TABLE [dbo].[RespuestasEvaluacion] (
    [idRespuesta]       INT           IDENTITY (1, 1) NOT NULL,
    [IdEvaluacion]      INT           NOT NULL,
    [IdItem]            INT           NOT NULL,
    [Valor]             VARCHAR (MAX) NULL,
    [ValorSupervisor]   VARCHAR (MAX) NULL,
    [escrito]           VARCHAR (MAX) NULL,
    [escritoSupervisor] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_RespuestasEvaluacion] PRIMARY KEY CLUSTERED ([idRespuesta] ASC),
    CONSTRAINT [FK_RespuestasEvaluacion_ItemsEvaluacion] FOREIGN KEY ([IdItem]) REFERENCES [dbo].[ItemsEvaluacion] ([IdItem])
);


GO
create TRIGGER [dbo].[Respuestas_Update]
   ON  [dbo].[RespuestasEvaluacion] 
   AFTER update
AS 
BEGIN
	
	SET NOCOUNT ON;

   
		INSERT INTO [dbo].[RespuestasEvaluacionAudit]
           ([idRespuesta]
           ,[idEvaluacion]
           ,[idItem]
           ,[valor]
           ,[valorSupervisor]
           ,[escrito]
           ,[escritoSupervisor]
           ,[FechaModif])
		   SELECT d.[idRespuesta]
      ,d.[IdEvaluacion]
      ,d.[IdItem]
      ,d.[Valor]
      ,d.[ValorSupervisor]
      ,d.[escrito]
      ,d.[escritoSupervisor]
	 ,getdate()
  FROM Deleted D
  

END
GO
create TRIGGER [dbo].[Respuestas_Delete]
   ON  [dbo].[RespuestasEvaluacion] 
   AFTER delete
AS 
BEGIN
	
	SET NOCOUNT ON;

   
		INSERT INTO [dbo].[RespuestasEvaluacionAudit]
           ([idRespuesta]
           ,[idEvaluacion]
           ,[idItem]
           ,[valor]
           ,[valorSupervisor]
           ,[escrito]
           ,[escritoSupervisor]
           ,[FechaModif])
		   SELECT d.[idRespuesta]
      ,d.[IdEvaluacion]
      ,d.[IdItem]
      ,d.[Valor]
      ,d.[ValorSupervisor]
      ,d.[escrito]
      ,d.[escritoSupervisor]
	 ,getdate()
  FROM Deleted D
 

END