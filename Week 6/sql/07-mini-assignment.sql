USE StudentRegistrationDB;
GO
-- Jalankan satu kali. Ganti NIM bila ingin menambahkan data dengan NIM berbeda.
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE NIM = '20231006')
    INSERT INTO dbo.Students (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
    VALUES ('20231006','Nadia Putri',1,'2004-05-14','Jl. Ketintang, Surabaya','081234567801');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE NIM = '20231007')
    INSERT INTO dbo.Students (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
    VALUES ('20231007','Rafi Pratama',2,'2003-12-09','Jl. Manyar, Surabaya','081234567802');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE NIM = '20231008')
    INSERT INTO dbo.Students (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
    VALUES ('20231008','Maya Anindita',4,'2004-08-30','Jl. Keputih, Surabaya','081234567803');
GO
SELECT s.NIM, s.Name, p.ProgramName, s.BirthDate, s.PhoneNumber
FROM dbo.Students AS s
INNER JOIN dbo.Programs AS p ON s.ProgramId = p.ProgramId
ORDER BY s.Name;
