CREATE PROCEDURE [dbo].[ModificarFechaEvaluacion] 
@idEvaluacion int = null,
@inicio datetime,
@inicioSupervisor datetime,
@fin datetime,
@finSupervisor datetime
AS
BEGIN
	SET NOCOUNT ON;

	update Evaluacion
	set Inicio = @inicio,
	InicioSupervisor = @inicioSupervisor,
	fin = @fin,
	FinSupervisor = @finSupervisor
	where (@idEvaluacion is null or IdEvaluacion = @idEvaluacion)

END