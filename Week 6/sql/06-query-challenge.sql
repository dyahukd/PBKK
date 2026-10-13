USE StudentRegistrationDB;
GO
-- Mahasiswa Teknik Informatika
SELECT s.NIM, s.Name, p.ProgramName
FROM dbo.Students AS s
INNER JOIN dbo.Programs AS p ON s.ProgramId = p.ProgramId
WHERE p.ProgramCode = 'TI';
GO
-- Total mahasiswa
SELECT COUNT(*) AS TotalMahasiswa FROM dbo.Students;
GO
-- Jumlah mahasiswa per program studi, termasuk prodi dengan 0 mahasiswa
SELECT p.ProgramName, COUNT(s.StudentId) AS JumlahMahasiswa
FROM dbo.Programs AS p
LEFT JOIN dbo.Students AS s ON p.ProgramId = s.ProgramId
GROUP BY p.ProgramId, p.ProgramName
ORDER BY p.ProgramId;
GO
