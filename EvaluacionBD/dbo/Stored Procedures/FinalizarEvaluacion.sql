CREATE procedure [dbo].[FinalizarEvaluacion] 
	@IdEvaluacion int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @result VARCHAR(MAX)

	select @result = 'La responsabilidad "' + i.Descripcion + '" no está completa. Solo escribió ' + CONVERT(VARCHAR,LEN(ISNULL(r.escrito,''))) + ' caracteres' from RespuestasEvaluacion r
					INNER JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem  
					where r.IdEvaluacion = @IdEvaluacion AND
					(
						( i.idTipoEvalucion = 1 AND (r.escrito IS NULL or LEN(r.escrito) < 10))
						)

	IF @result IS null
		BEGIN
		
			SELECT @result = 'La Competencia "' + i.Descripcion + '" no está completa.' from RespuestasEvaluacion r
					INNER JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem  
					where r.IdEvaluacion = @IdEvaluacion AND
					(
						(i.idTipoEvalucion NOT IN (1,22,23) and valor is null)
					)
		
			if @result IS null
			BEGIN
				select @result = 'La Oportunidad de Mejora no está completa. Solo escribió ' + CONVERT(VARCHAR,LEN(ISNULL(r.escrito,''))) + ' caracteres' from RespuestasEvaluacion r
					INNER JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem  
					where r.IdEvaluacion = @IdEvaluacion AND
					(
						
						(i.idTipoEvalucion = 22 AND (r.escrito IS NULL or LEN(r.escrito) < 50))
					) 
				if @result IS null
					BEGIN
						select @result = 'El Desempeño Global no está completo. Solo escribió ' + CONVERT(VARCHAR,LEN(ISNULL(r.escrito,''))) + ' caracteres' from RespuestasEvaluacion r
									INNER JOIN dbo.ItemsEvaluacion i ON i.IdItem = r.IdItem  
									where r.IdEvaluacion = @IdEvaluacion AND
									(
										(i.idTipoEvalucion = 23 AND (r.escrito IS NULL or LEN(r.escrito) < 30 OR r.Valor IS null))
									)
						if @result IS NULL update Evaluacion set Estado = 'Enviado al Supervisor' where IdEvaluacion = @IdEvaluacion
						ELSE -- desp 
							raiserror(@result,17,1)
					END
					ELSE -- oport
					BEGIN
						raiserror(@result,17,1)
					END
			END
			ELSE --comp
			BEGIN
				raiserror(@result,17,1)
			END
		END
		ELSE -- resp
		BEGIN
			raiserror(@result,17,1)
		END
	
END