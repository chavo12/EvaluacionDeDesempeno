CREATE procedure [dbo].[GetEmpleadosSupervisadosEstado]
	@idSupervisor int

AS
BEGIN
	SET NOCOUNT ON;

	select sum(case when ev.Estado = 'Finalizada' then 1 else 0 end) * 100 / count(e.IdEmpleado) as estado
	from Empleados e
	inner join Evaluacion ev on ev.IdEmpleado = e.IdEmpleado
	where e.SupervisorID = @idSupervisor

END