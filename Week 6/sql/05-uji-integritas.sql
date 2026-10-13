USE StudentRegistrationDB;
GO
-- Uji FOREIGN KEY: perintah ini diharapkan gagal karena ProgramId 99 tidak tersedia.
BEGIN TRY
    INSERT INTO dbo.Students (NIM, Name, ProgramId)
    VALUES ('TEST-FK-99', 'Uji Foreign Key', 99);
END TRY
BEGIN CATCH
    SELECT 'Uji FOREIGN KEY ditolak sesuai harapan' AS Hasil, ERROR_NUMBER() AS ErrorNumber,
           ERROR_MESSAGE() AS Detail;
END CATCH;
GO
-- Uji UNIQUE: perintah ini diharapkan gagal karena NIM 20231001 sudah ada.
BEGIN TRY
    INSERT INTO dbo.Students (NIM, Name, ProgramId)
    VALUES ('20231001', 'Mahasiswa Duplikat', 1);
END TRY
BEGIN CATCH
    SELECT 'Uji UNIQUE ditolak sesuai harapan' AS Hasil, ERROR_NUMBER() AS ErrorNumber,
           ERROR_MESSAGE() AS Detail;
END CATCH;
GO
