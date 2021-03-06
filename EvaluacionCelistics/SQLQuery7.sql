USE [prueba]
GO
/****** Object:  StoredProcedure [dbo].[BorrarEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[BorrarEvaluacion]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	set xact_abort on 

	begin transaction
		
		delete RespuestasEvaluacion where IdEvaluacion = @IdEvaluacion
		delete Evaluacion where IdEvaluacion = @IdEvaluacion

	commit transaction

END


GO
/****** Object:  StoredProcedure [dbo].[EditarEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[EditarEvaluacion]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	set xact_abort on 

	update Evaluacion set Estado = 'Autoevaluación' where IdEvaluacion = @IdEvaluacion

END


GO
/****** Object:  StoredProcedure [dbo].[FinalizarEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[FinalizarEvaluacion]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	if not exists(select 1 from RespuestasEvaluacion where IdEvaluacion = @IdEvaluacion and valor is null)
		update Evaluacion set Estado = 'Enviado al Supervisor' where IdEvaluacion = @IdEvaluacion
	else raiserror('Debe completar toda la evalucación antes de finalizarla',17,1)

END


GO
/****** Object:  StoredProcedure [dbo].[FinalizarEvaluacionSupervisor]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[FinalizarEvaluacionSupervisor]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	if not exists(select 1 from RespuestasEvaluacion r 
							inner join ItemsEvaluacion i on i.IdItem = r.IdItem
							inner join TipoEvaluacion t on t.IdTipoEvaluacion = i.idTipoEvalucion
							where IdEvaluacion = @IdEvaluacion and ValorSupervisor is null and t.Supervisa = 1)
		update Evaluacion set Estado = 'Finalizada' where IdEvaluacion = @IdEvaluacion
	else raiserror('Debe supervisar toda la evalucación antes de finalizarla',17,1)

END


GO
/****** Object:  StoredProcedure [dbo].[GetEmpleado]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEmpleado] --@CorreoElectronico='amontoya'
	@IdEmpleado int = null,
	@CorreoElectronico varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select e.*,dbo.armarNombre(e.Nombre,e.PApellido,e.SApellido)  as nombreCompleto, dbo.armarNombre(s.Nombre,s.PApellido,s.SApellido) as supervisor
	from Empleados e
	left join Empleados s on s.IdEmpleado = e.SupervisorID
	where (@IdEmpleado is null or e.IdEmpleado = @IdEmpleado) and (@CorreoElectronico is null or e.EmpleadoId = @CorreoElectronico)

END


GO
/****** Object:  StoredProcedure [dbo].[GetEmpleadoAdmin]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEmpleadoAdmin] --@CorreoElectronico='amontoya'
	@IdAdmin int = null,
	@Pais varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select e.*,dbo.armarNombre(e.Nombre,e.PApellido,e.SApellido)  as nombreCompleto, dbo.armarNombre(s.Nombre,s.PApellido,s.SApellido) as supervisor
	from Empleados e
	left join Empleados s on s.IdEmpleado = e.SupervisorID
	where (@Pais is null or e.Pais = @Pais) and e.IdEmpleado <> @IdAdmin

END


GO
/****** Object:  StoredProcedure [dbo].[GetEmpleadosSupervisados]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEmpleadosSupervisados]
	@idSupervisor int

AS
BEGIN
	SET NOCOUNT ON;

	select e.*, dbo.armarNombre(e.nombre,e.PApellido,e.SApellido) as nombreCompleto, ev.Estado as estadoEvaluacion, ev.IdEvaluacion
	from Empleados e
	inner join Evaluacion ev on ev.IdEmpleado = e.IdEmpleado
	where e.SupervisorID = @idSupervisor

END


GO
/****** Object:  StoredProcedure [dbo].[GetEstadoTipoEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEstadoTipoEvaluacion]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	select 'Responsabilidad' as Nombre,dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Responsabilidad') as completado
	from TipoEvaluacion t
	inner join ItemsEvaluacion i on i.idTipoEvalucion = t.IdTipoEvaluacion
	inner join RespuestasEvaluacion r on r.IdItem = i.IdItem
	where r.IdEvaluacion = @IdEvaluacion
	union
	select 'Competencia' as Nombre,dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Competencia') as completado
	from TipoEvaluacion t
	inner join ItemsEvaluacion i on i.idTipoEvalucion = t.IdTipoEvaluacion
	inner join RespuestasEvaluacion r on r.IdItem = i.IdItem
	where r.IdEvaluacion = @IdEvaluacion
	union
	select 'Oportunidad' as Nombre,dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Oportunidad') as completado
	from TipoEvaluacion t
	inner join ItemsEvaluacion i on i.idTipoEvalucion = t.IdTipoEvaluacion
	inner join RespuestasEvaluacion r on r.IdItem = i.IdItem
	where r.IdEvaluacion = @IdEvaluacion
	union
	select 'Desempeno' as Nombre,dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Desempeno') as completado
	from TipoEvaluacion t
	inner join ItemsEvaluacion i on i.idTipoEvalucion = t.IdTipoEvaluacion
	inner join RespuestasEvaluacion r on r.IdItem = i.IdItem
	where r.IdEvaluacion = @IdEvaluacion


END


GO
/****** Object:  StoredProcedure [dbo].[GetEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEvaluacion] --2,'20161212','20161231','20170101','20170112'
	@IdEmpleado int,
	@inicio datetime,
	@fin datetime,
	@inicioSupervisor datetime,
	@finSupervisor datetime

AS
BEGIN
	declare @idEvaluacion int, @idioma varchar(3),@nivel varchar(50)

	SET NOCOUNT ON;
	set xact_abort on


	if not exists(select 1 from Evaluacion where IdEmpleado = @IdEmpleado)
	begin
		begin transaction

		select @idioma = case when e.Pais = 'Brasil' then 'por' else 'es' end,@nivel = e.Nivel
		from Empleados e

		insert Evaluacion(IdEmpleado,Estado,Inicio,Fin,FechaEstado,InicioSupervisor,FinSupervisor)
		values (@IdEmpleado,'Autoevaluación',@inicio,@fin,getdate(),@inicioSupervisor,@finSupervisor)
		
		set @idEvaluacion = SCOPE_IDENTITY()

		insert RespuestasEvaluacion(IdEvaluacion,IdItem)
		select @idEvaluacion,i.IdItem
		from ItemsEvaluacion i
		where i.Idioma = @idioma and (i.Nivel = @nivel or i.Nivel is null)

		commit transaction
	end
	
	select *
	from Evaluacion 
	where IdEmpleado = @IdEmpleado

   
END


GO
/****** Object:  StoredProcedure [dbo].[GetRespuestasEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetRespuestasEvaluacion] 
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	select r.*, i.Descripcion as item,i.Escrito,i.idTipoEvalucion as idTipoEvaluacion,t.Supervisa,t.Nombre as TipoEvaluacion, t.Descripcion AS TipoEvaluacionDescrip
	from RespuestasEvaluacion r
	inner join ItemsEvaluacion i on i.IdItem = r.IdItem
	inner join TipoEvaluacion t on t.IdTipoEvaluacion = i.idTipoEvalucion
	where r.IdEvaluacion = @IdEvaluacion

END


GO
/****** Object:  StoredProcedure [dbo].[GuardarEmpleado]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
      @Ingreso datetime,
      @Negocio varchar(50),
      @Pais varchar(50),
      @CorreoElectronico varchar(100),
      @SupervisorID int,
      @Nivel varchar(50),
      @Rol varchar(50)
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
           ,[Rol])
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
           ,@Ingreso
           ,null
           ,null
           ,@Pais
           ,@CorreoElectronico
           ,@SupervisorID
           ,null
           ,null
           ,@Nivel
           ,@NumPia
           ,@Rol)

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
		  ,[Ingreso] = @Ingreso
		  ,[Pais] = @Pais
		  ,[CorreoElectronico] = @CorreoElectronico
		  ,[SupervisorID] = @SupervisorID
		  ,[Nivel] = @Nivel
		  ,[NumPia] = @NumPia
		  ,[Rol] = @Rol
	 WHERE IdEmpleado = @idEmpleado
		
	end

END

GO
/****** Object:  StoredProcedure [dbo].[ModificarFechaEvaluacion]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ModificarFechaEvaluacion] 
@idEvaluacion int,
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
	where IdEvaluacion = @idEvaluacion

END

GO
/****** Object:  StoredProcedure [dbo].[ModificarValorRespuesta]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ModificarValorRespuesta] 
	@idRespuesta int,
	@valor varchar(max) = null,
	@valorSupervisor varchar(max) = null,
	@escrito varchar(max) = null,
	@escritoSupervisor varchar(max) = null

AS
BEGIN
	SET NOCOUNT ON;

	update RespuestasEvaluacion set Valor = case when @valor is null then valor else @valor end, 
			ValorSupervisor = case when @valorSupervisor is null then ValorSupervisor else @valorSupervisor end
			,escrito = case when @valor is null then escrito else @escrito end
			,escritoSupervisor = case when @valorSupervisor is null then escritoSupervisor else @escritoSupervisor end
	where idRespuesta = @idRespuesta

END


GO
/****** Object:  StoredProcedure [dbo].[SetLog]    Script Date: 12/12/2016 17:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SetLog]
 @IdEmpleado int,
 @pagina varchar(100),
 @detalle varchar(max) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert Logs(IdEmpleado,Pagina,Detalle,FechayHora)
	values(@IdEmpleado,@pagina,@detalle,getdate())

END


GO
