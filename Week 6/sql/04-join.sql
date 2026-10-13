USE StudentRegistrationDB;
GO
SELECT s.StudentId, s.NIM, s.Name, s.ProgramId, p.ProgramCode, p.ProgramName,
       s.BirthDate, s.Address, s.PhoneNumber, s.CreatedAt, s.UpdatedAt
FROM dbo.Students AS s
INNER JOIN dbo.Programs AS p ON s.ProgramId = p.ProgramId
ORDER BY s.StudentId;
