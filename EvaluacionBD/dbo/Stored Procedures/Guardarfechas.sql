CREATE PROCEDURE [dbo].[Guardarfechas] 
@inicio datetime,
@fin datetime,
@inicioSuper datetime,
@finSuper datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update fechas 
	set inicio = @inicio,
	fin = @fin,
	inicioSuper = @inicioSuper,
	finSuper = @finSuper

END