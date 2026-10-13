using Microsoft.Data.SqlClient;
using StudentRegistration.Models;

namespace StudentRegistration.Repositories;

public class StudentRepository
{
    private readonly string _connectionString =
        @"Server=.\SQLEXPRESS;Database=StudentRegistrationDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public List<Student> GetAll()
    {
        var students = new List<Student>();

        const string sql = """
            SELECT s.StudentId, s.NIM, s.Name, s.ProgramId, p.ProgramName,
                   s.BirthDate, s.Address, s.PhoneNumber, s.CreatedAt, s.UpdatedAt
            FROM Students AS s
            INNER JOIN Programs AS p ON s.ProgramId = p.ProgramId
            ORDER BY s.StudentId;
            """;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        connection.Open();

        using var reader = command.ExecuteReader();
        int studentId = reader.GetOrdinal("StudentId");
        int nim = reader.GetOrdinal("NIM");
        int name = reader.GetOrdinal("Name");
        int programId = reader.GetOrdinal("ProgramId");
        int programName = reader.GetOrdinal("ProgramName");
        int birthDate = reader.GetOrdinal("BirthDate");
        int address = reader.GetOrdinal("Address");
        int phone = reader.GetOrdinal("PhoneNumber");
        int createdAt = reader.GetOrdinal("CreatedAt");
        int updatedAt = reader.GetOrdinal("UpdatedAt");

        while (reader.Read())
        {
            students.Add(new Student
            {
                StudentId = reader.GetInt32(studentId),
                NIM = reader.GetString(nim),
                Name = reader.GetString(name),
                ProgramId = reader.GetInt32(programId),
                ProgramName = reader.GetString(programName),
                BirthDate = reader.IsDBNull(birthDate) ? null : reader.GetDateTime(birthDate),
                Address = reader.IsDBNull(address) ? string.Empty : reader.GetString(address),
                PhoneNumber = reader.IsDBNull(phone) ? string.Empty : reader.GetString(phone),
                CreatedAt = reader.GetDateTime(createdAt),
                UpdatedAt = reader.IsDBNull(updatedAt) ? null : reader.GetDateTime(updatedAt)
            });
        }

        return students;
    }

    public int GetTotal()
    {
        const string sql = "SELECT COUNT(*) FROM Students;";
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }
}
