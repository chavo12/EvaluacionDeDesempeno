CREATE procedure [dbo].[GetEmpleado] --@CorreoElectronico='amontoya'
	@IdEmpleado int = null,
	@CorreoElectronico varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select e.*,dbo.armarNombre(e.Nombre,e.PApellido,e.SApellido)  as nombreCompleto, dbo.armarNombre(s.Nombre,s.PApellido,s.SApellido) as supervisor, ev.idEvaluacion AS IdEvaluacion,ev.Estado as estadoEvaluacion
	from Empleados e
	left join Empleados s on s.IdEmpleado = e.SupervisorID
	LEFT JOIN dbo.Evaluacion ev ON ev.IdEmpleado = e.IdEmpleado
	where (@IdEmpleado is null or e.IdEmpleado = @IdEmpleado) and (@CorreoElectronico is null or e.EmpleadoId = @CorreoElectronico)

END