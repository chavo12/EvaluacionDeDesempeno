CREATE PROCEDURE [dbo].[EliminarEmpleado] 

	@idEmpleado int
AS
BEGIN
	SET NOCOUNT ON;
	set xact_abort on

	begin transaction

	delete RespuestasEvaluacion
	from RespuestasEvaluacion r
	inner join Evaluacion e on e.IdEvaluacion = r.IdEvaluacion
	where e.IdEmpleado = @idEmpleado

	delete Evaluacion where IdEmpleado = @idEmpleado
	
	DELETE dbo.ItemsEvaluacion WHERE Nivel = (SELECT numpia FROM dbo.Empleados WHERE IdEmpleado = @idEmpleado)

	delete Empleados where IdEmpleado = @idEmpleado

	commit transaction

END