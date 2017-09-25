CREATE procedure [dbo].[GetEstadoTipoEvaluacion] --1018
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	select 'Responsabilidad' as Nombre,isnull(dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Responsabilidad'),0) as completado
	from TipoEvaluacion t
	inner join ItemsEvaluacion i on i.idTipoEvalucion = t.IdTipoEvaluacion
	inner join RespuestasEvaluacion r on r.IdItem = i.IdItem
	where r.IdEvaluacion = @IdEvaluacion
	union
	select 'Competencia' as Nombre,isnull(dbo.EstadoTipoEvaluacion(@IdEvaluacion,'Competencia'),0) as completado
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