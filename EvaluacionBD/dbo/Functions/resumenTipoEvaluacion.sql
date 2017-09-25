CREATE FUNCTION [dbo].[resumenTipoEvaluacion]
(
	@idevaluacion int,
	@idTipoEvaluacion int,
	@supervisor bit
)
RETURNS int
AS
BEGIN
	DECLARE @result int,@total int, @cant int


	select @total = case when @supervisor = 1 then sum(convert(int,isnull(r.ValorSupervisor,0))) else sum(convert(int,isnull(r.Valor,0))) end, @cant = count(1)
	from RespuestasEvaluacion r
	inner join ItemsEvaluacion i on i.IdItem = r.IdItem
	where r.IdEvaluacion = @idevaluacion and i.idTipoEvalucion = @idTipoEvaluacion

	set @result = @total / @cant

	RETURN @result

END