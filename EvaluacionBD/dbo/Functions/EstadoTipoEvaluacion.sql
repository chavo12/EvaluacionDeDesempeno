CREATE FUNCTION [dbo].[EstadoTipoEvaluacion]
(
	@IdEvaluacion int,
	@tipoEvaluacion varchar(max)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result int

	if (@tipoEvaluacion <> 'Competencia')
	begin
	select @result = sum(case when (i.idTipoEvalucion = 23 and r.Valor is not null and r.escrito is not NULL AND LEN(r.escrito)>= 30) or (i.idTipoEvalucion = 22 and r.escrito is not NULL AND LEN(r.escrito)>= 50) or (i.idTipoEvalucion = 1 and r.escrito is not NULL AND LEN(r.escrito)>= 10) or (i.idTipoEvalucion not in (1,22,23) and valor is not null) then 1 else 0 end) * 100 / count(r.idRespuesta)
	from RespuestasEvaluacion r
	inner join ItemsEvaluacion i on i.IdItem = r.IdItem
	where r.IdEvaluacion = @IdEvaluacion and i.idTipoEvalucion = (case when @tipoEvaluacion = 'Responsabilidad' then '1' else case when @tipoEvaluacion = 'Oportunidad' then '22' else '23' end end)
	end
	else
	begin
	select @result = sum(case when r.Valor is not null then 1 else 0 end) * 100 / count(r.idRespuesta)
	from RespuestasEvaluacion r
	inner join ItemsEvaluacion i on i.IdItem = r.IdItem
	where r.IdEvaluacion = @IdEvaluacion and i.idTipoEvalucion in (2,5,8,11,14,17,20,21,24,25,26,27,28,29,30,31,32,33)
	end

	-- Return the result of the function
	RETURN @result

END