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