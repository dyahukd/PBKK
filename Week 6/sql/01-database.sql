IF DB_ID(N'StudentRegistrationDB') IS NULL
    CREATE DATABASE StudentRegistrationDB;
GO
USE StudentRegistrationDB;
GO
SELECT DB_NAME() AS CurrentDatabase;
