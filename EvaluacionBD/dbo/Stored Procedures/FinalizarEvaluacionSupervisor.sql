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