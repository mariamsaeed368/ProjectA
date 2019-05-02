﻿CREATE VIEW [dbo].[View]
	AS Select a.*,p.AdvisorId,p.AdvisorRole,h.StudentId from Project as a Join ProjectAdvisor as p on p.ProjectId=a.Id join GroupProject as s on s.ProjectId=a.Id join GroupStudent as h on h.GroupId=s.GroupId