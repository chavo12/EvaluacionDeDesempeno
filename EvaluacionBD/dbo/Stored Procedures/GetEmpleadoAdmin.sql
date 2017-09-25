CREATE procedure [dbo].[GetEmpleadoAdmin] 
	@IdAdmin int = null,
	@Pais varchar(100) = null,
	@inicio datetime = null,
	@fin datetime = null,
	@estado varchar(100) = null,
	@Departamento varchar(100) = null,
	@supervisorId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT e.*,dbo.armarNombre(e.Nombre,e.PApellido,e.SApellido)  as nombreCompleto, dbo.armarNombre(s.Nombre,s.PApellido,s.SApellido) as supervisor,v.IdEvaluacion,ISNULL(v.estado,'Autoevaluación') AS estadoEvaluacion,
	 ISNULL(r.Valor,'') AS desempenoGlobal,ISNULL(r.ValorSupervisor,'') AS desempenoGlobalSuper,s.CorreoElectronico AS mailSupervisor
	from Empleados e
	left join Evaluacion v on v.IdEmpleado = e.IdEmpleado
	LEFT JOIN dbo.RespuestasEvaluacion r ON r.IdEvaluacion = v.IdEvaluacion AND r.IdItem = 50
	LEFT JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem 
	left join Empleados s on s.IdEmpleado = e.SupervisorID
	where (@Pais is null or e.Pais = @Pais) and e.TipoEmpleado <> 'ADMINISTRADOR' and
		(@inicio is null or v.Inicio >= @inicio) and (@fin is null or v.Fin <= @fin) and
		(@estado is null or v.Estado = @estado) and (@Departamento is null or e.Departamento like '%' + @Departamento + '%') and
		(@supervisorId is null or e.SupervisorID = @supervisorId) 

END