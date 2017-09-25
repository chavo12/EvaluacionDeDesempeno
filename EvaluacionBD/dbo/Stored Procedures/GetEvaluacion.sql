CREATE procedure [dbo].[GetEvaluacion] --2,'20161212','20161231','20170101','20170112'
	@IdEmpleado INT = null,
	@inicio datetime = null,
	@fin datetime = null,
	@inicioSupervisor datetime = null,
	@finSupervisor DATETIME = null,
	@idEvaluacion INT = null
	

AS
BEGIN
	declare @idioma varchar(3),@nivel varchar(50)

	SET NOCOUNT ON;
	set xact_abort on

	IF (@idEvaluacion IS NULL)
	Begin
		if not exists(select 1 from Evaluacion where IdEmpleado = @IdEmpleado)
		begin
			begin transaction

			select @idioma = case when e.Pais = 'Brasil' then 'por' else 'es' end,@nivel = e.NumPia
			from Empleados e
			where e.IdEmpleado = @IdEmpleado

			select @inicio = inicio, @fin = fin, @inicioSupervisor = inicioSuper, @finSupervisor = @finSupervisor from fechas

			insert Evaluacion(IdEmpleado,Estado,Inicio,Fin,FechaEstado,InicioSupervisor,FinSupervisor)
			values (@IdEmpleado,'Autoevaluación',@inicio,@fin,getdate(),@inicioSupervisor,@finSupervisor)
			
			set @idEvaluacion = SCOPE_IDENTITY()

			insert RespuestasEvaluacion(IdEvaluacion,IdItem)
			select @idEvaluacion,i.IdItem
			from ItemsEvaluacion i
			INNER JOIN dbo.TipoEvaluacion t ON t.IdTipoEvaluacion = i.idTipoEvalucion
			where (i.Nivel = @nivel or i.Nivel is null)
			--where (i.Nivel = @nivel or i.Nivel is null) AND ((@idioma = 'es' AND i.idTipoEvalucion NOT IN (24,25,26,27,28,29,30,31,33)) OR (@idioma = 'por' AND idTipoEvalucion NOT IN (2,5,8,11,14,17,20,21,32)))

			commit transaction
		end
	
	
		SELECT [IdEvaluacion]
      ,[IdEmpleado]
      ,[Estado]
      ,[Inicio]
      ,[Fin]
      ,[FechaEstado]
      ,[InicioSupervisor]
      ,[FinSupervisor]
		from Evaluacion 
		where IdEmpleado = @IdEmpleado
	
	END
	ELSE
	Begin
		SELECT [IdEvaluacion]
      ,[IdEmpleado]
      ,[Estado]
      ,[Inicio]
      ,[Fin]
      ,[FechaEstado]
      ,[InicioSupervisor]
      ,[FinSupervisor]
		from Evaluacion 
		WHERE idEvaluacion = @idEvaluacion
	end

   
END