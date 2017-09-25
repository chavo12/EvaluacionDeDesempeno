create procedure [dbo].[EditarEvaluacion]
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;

	set xact_abort on 

	update Evaluacion set Estado = 'Autoevaluación' where IdEvaluacion = @IdEvaluacion

END