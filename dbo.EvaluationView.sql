CREATE VIEW [dbo].[EvaluationView]
	AS SELECT  a.*,s.EvaluationDate,s.GroupId,s.ObtainedMarks FROM[ProjectA].[dbo].[Evaluation] as a JOIN GroupEvaluation s ON a.Id = s.EvaluationId
