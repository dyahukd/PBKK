USE StudentRegistrationDB;
GO
IF OBJECT_ID(N'dbo.Programs', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Programs
    (
        ProgramId INT IDENTITY(1,1) CONSTRAINT PK_Programs PRIMARY KEY,
        ProgramCode VARCHAR(20) NOT NULL CONSTRAINT UQ_Programs_ProgramCode UNIQUE,
        ProgramName VARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Programs_IsActive DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Programs_CreatedAt DEFAULT SYSDATETIME()
    );
END;
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Programs)
BEGIN
    INSERT INTO dbo.Programs (ProgramCode, ProgramName)
    VALUES ('TI','Teknik Informatika'),
           ('SI','Sistem Informasi'),
           ('MNJ','Manajemen'),
           ('TE','Teknik Elektro'),
           ('DKV','Desain Komunikasi Visual');
END;
GO
SELECT * FROM dbo.Programs;
