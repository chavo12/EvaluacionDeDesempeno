CREATE PROCEDURE [dbo].[GuardarEmpleado] 
	@idEmpleado int = null,
    @EmpleadoId varchar(50),
    @Nombre varchar(100),
    @PApellido varchar(50),
      @SApellido varchar(50) = null,
      @Cargo varchar(100),
      @Departamento varchar(100),
      @TipoEmpleado varchar(100),
      @NumPia varchar(100),
      @Ingreso DATETIME =  null,
      @Negocio varchar(50),
      @Pais varchar(50),
      @CorreoElectronico varchar(100),
      @SupervisorID int,
      @Nivel varchar(50),
      @Rol varchar(50),
      @inicio DATETIME = NULL,
      @inicioSuper DATETIME = NULL,
      @finSuper DATETIME = NULL,
      @fin DATETIME = NULL,
      @clave varchar(max) = null,
      @resetClave uniqueidentifier = null,
      @fechaResetClave datetime = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (@idEmpleado is null or @idEmpleado = 0)
	begin
		
		INSERT INTO [dbo].[Empleados]
           ([EmpleadoId]
           ,[Nombre]
           ,[PApellido]
           ,[SApellido]
           ,[Cargo]
           ,[Departamento]
           ,[Negocio]
           ,[TipoEmpleado]
           ,[RazonSocial]
           ,[Ingreso]
           ,[Nacimiento]
           ,[TipoEvaluacion]
           ,[Pais]
           ,[CorreoElectronico]
           ,[SupervisorID]
           ,[Localidad]
           ,[CiudadLocalidad]
           ,[Nivel]
           ,[NumPia]
           ,[Rol]
		     ,clave
	   ,resetClave
	   ,fechaResetClave)
     VALUES
           (@EmpleadoId
           ,@Nombre
           ,@PApellido
           ,@SApellido
           ,@Cargo
           ,@Departamento
           ,@Negocio
           ,@TipoEmpleado
           ,null
           ,isnull(@Ingreso,getdate())
           ,null
           ,null
           ,@Pais
           ,@CorreoElectronico
           ,@SupervisorID
           ,null
           ,null
           ,@Nivel
           ,@NumPia
           ,@Rol
		     ,@clave
	   ,@resetClave
	   ,@fechaResetClave)

	end
	else
	begin

	UPDATE [dbo].[Empleados]
	   SET [EmpleadoId] = @EmpleadoId
		  ,[Nombre] = @Nombre
		  ,[PApellido] = @PApellido
		  ,[SApellido] = @SApellido
		  ,[Cargo] = @Cargo
		  ,[Departamento] = @Departamento
		  ,[Negocio] = @Negocio
		  ,[TipoEmpleado] = @TipoEmpleado
		  ,[Ingreso] =  CASE WHEN @Ingreso IS NULL THEN Ingreso ELSE @Ingreso end
		  ,[Pais] = @Pais
		  ,[CorreoElectronico] = @CorreoElectronico
		  ,[SupervisorID] = @SupervisorID
		  ,[Nivel] = @Nivel
		  ,[NumPia] = @NumPia
		  ,[Rol] = @Rol
		   ,clave = @clave
		  ,resetClave = @resetClave
		  ,fechaResetClave = @fechaResetClave
	 WHERE IdEmpleado = @idEmpleado
		
	UPDATE evaluacion
	SET inicio = (CASE WHEN @inicio is NULL THEN inicio ELSE @inicio END),
		Fin = (CASE WHEN @fin is NULL THEN Fin ELSE @fin END),
		InicioSupervisor = (CASE WHEN @inicioSuper is NULL THEN InicioSupervisor ELSE @inicioSuper END),
		FinSupervisor = (CASE WHEN @finSuper is NULL THEN FinSupervisor ELSE @finSuper END)
		WHERE idEmpleado = @idEmpleado
		
	end

END