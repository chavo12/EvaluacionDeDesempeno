CREATE procedure [dbo].[GetEmpleadosSupervisados]
	@idSupervisor int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT e.*,dbo.armarNombre(e.Nombre,e.PApellido,e.SApellido)  as nombreCompleto, dbo.armarNombre(s.Nombre,s.PApellido,s.SApellido) as supervisor,v.IdEvaluacion,ISNULL(v.estado,'Autoevaluación') AS estadoEvaluacion,
	 ISNULL(r.Valor,'') AS desempenoGlobal,ISNULL(r.ValorSupervisor,'') AS desempenoGlobalSuper,s.CorreoElectronico AS mailSupervisor
	from Empleados e
	left join Evaluacion v on v.IdEmpleado = e.IdEmpleado
	LEFT JOIN dbo.RespuestasEvaluacion r ON r.IdEvaluacion = v.IdEvaluacion
	LEFT JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem 
	left join Empleados s on s.IdEmpleado = e.SupervisorID
	where e.SupervisorID = @idSupervisor AND i.idTipoEvalucion = 23

END