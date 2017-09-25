CREATE procedure [dbo].[ModificarValorRespuesta] 
	@idRespuesta int,
	@valor varchar(max) = null,
	@valorSupervisor varchar(max) = null,
	@escrito varchar(max) = null,
	@escritoSupervisor varchar(max) = null

AS
BEGIN
	SET NOCOUNT ON;




	update RespuestasEvaluacion set Valor = case when @valor is null or e.estado <> 'Autoevaluación' then valor else @valor end, 
			ValorSupervisor = case when @valorSupervisor is null or e.Estado <> 'Enviado al Supervisor' then ValorSupervisor else @valorSupervisor end
			,escrito = case when @escrito is null or e.estado <> 'Autoevaluación' then escrito else @escrito end
			,escritoSupervisor = case when @escritoSupervisor is null or e.Estado <> 'Enviado al Supervisor' then escritoSupervisor else @escritoSupervisor end
		from RespuestasEvaluacion r 
		inner join Evaluacion e on e.IdEvaluacion = r.IdEvaluacion
	where idRespuesta = @idRespuesta

END