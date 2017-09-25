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