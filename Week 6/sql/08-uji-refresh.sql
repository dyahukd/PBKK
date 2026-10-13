USE StudentRegistrationDB;
GO
-- Jalankan saat aplikasi sedang terbuka, lalu klik Refresh Data.
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE NIM = '20231009')
    INSERT INTO dbo.Students (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
    VALUES ('20231009','Uji Refresh',1,'2004-02-20','Jl. Teknik, Surabaya','081234567809');
SELECT * FROM dbo.Students WHERE NIM = '20231009';
