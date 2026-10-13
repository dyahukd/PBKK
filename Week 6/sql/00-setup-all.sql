IF DB_ID(N'StudentRegistrationDB') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE StudentRegistrationDB');
END;
GO

USE StudentRegistrationDB;
GO

IF OBJECT_ID(N'dbo.Programs', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Programs
    (
        ProgramId INT IDENTITY(1,1)
            CONSTRAINT PK_Programs PRIMARY KEY,
        ProgramCode VARCHAR(20) NOT NULL
            CONSTRAINT UQ_Programs_ProgramCode UNIQUE,
        ProgramName VARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL
            CONSTRAINT DF_Programs_IsActive DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL
            CONSTRAINT DF_Programs_CreatedAt DEFAULT SYSDATETIME()
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Programs)
BEGIN
    INSERT INTO dbo.Programs (ProgramCode, ProgramName)
    VALUES
        ('TI', 'Teknik Informatika'),
        ('SI', 'Sistem Informasi'),
        ('MNJ', 'Manajemen'),
        ('TE', 'Teknik Elektro'),
        ('DKV', 'Desain Komunikasi Visual');
END;
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentId INT IDENTITY(1,1)
            CONSTRAINT PK_Students PRIMARY KEY,
        NIM VARCHAR(20) NOT NULL
            CONSTRAINT UQ_Students_NIM UNIQUE,
        Name VARCHAR(100) NOT NULL,
        ProgramId INT NOT NULL,
        BirthDate DATE NULL,
        Address VARCHAR(250) NULL,
        PhoneNumber VARCHAR(20) NULL,
        CreatedAt DATETIME2 NOT NULL
            CONSTRAINT DF_Students_CreatedAt DEFAULT SYSDATETIME(),
        UpdatedAt DATETIME2 NULL,
        CONSTRAINT FK_Students_Programs
            FOREIGN KEY (ProgramId) REFERENCES dbo.Programs(ProgramId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Students)
BEGIN
    INSERT INTO dbo.Students
        (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
    VALUES
        ('20231001', 'Ahmad Rizki', 1, '2003-03-12', 'Jl. Merdeka No. 123, Surabaya', '081234567890'),
        ('20231002', 'Siti Aisyah', 2, '2004-07-21', 'Jl. Diponegoro No. 45, Surabaya', '082233445566'),
        ('20231003', 'Budi Santoso', 1, '2003-11-05', 'Jl. Ahmad Yani No. 10, Surabaya', '081355667788'),
        ('20231004', 'Citra Lestari', 3, '2004-01-18', 'Jl. Darmo No. 20, Surabaya', '089900112233'),
        ('20231005', 'Dimas Prayoga', 4, '2003-09-27', 'Jl. Raya ITS, Surabaya', '081288776655');
END;
GO

-- Verifikasi awal database dan relasi.
SELECT DB_NAME() AS CurrentDatabase;
SELECT * FROM dbo.Programs ORDER BY ProgramId;
SELECT s.StudentId, s.NIM, s.Name, p.ProgramCode, p.ProgramName,
       s.BirthDate, s.Address, s.PhoneNumber, s.CreatedAt, s.UpdatedAt
FROM dbo.Students AS s
INNER JOIN dbo.Programs AS p ON s.ProgramId = p.ProgramId
ORDER BY s.StudentId;
