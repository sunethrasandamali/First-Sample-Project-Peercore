using Sample_CRUD_Application.DataService.BaseClasses;
using Sample_CRUD_Application.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sample_CRUD_Application.DataService.Repository
{
    public class StudentDataRepository
    {
        private readonly BaseDataService _dataAccess;

        public StudentDataRepository(BaseDataService dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<StudentDataModel> GetAllStudents()
        {
            List<StudentDataModel> students = new List<StudentDataModel>();

            using (SqlConnection connection = _dataAccess.GetDBConnection())
            {
                connection.Open();
                string query = "select StudentID,FirstName,LastName,ContactNo,Email,DOB from dbo.Students";  

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                   
                        while (reader.Read())
                        {
                            StudentDataModel student = new StudentDataModel
                            {
                                StudentID = Convert.ToInt32(reader["StudentId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"])
                                
                            };

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        public void CreateStudent(StudentDataModel student)
        {
            using (SqlConnection connection = _dataAccess.GetDBConnection())
            {
                connection.Open();
                string query = "INSERT INTO dbo.Students (FirstName, LastName, ContactNo, Email, DOB) " +
                               "VALUES (@FirstName, @LastName, @ContactNo, @Email, @DOB)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@DOB", student.DOB);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateStudent(int studentID, StudentDataModel student)
        {
            using (SqlConnection connection = _dataAccess.GetDBConnection())
            {
                connection.Open();
                string query = "UPDATE dbo.Students " +
                               "SET FirstName = @FirstName, LastName = @LastName, ContactNo = @ContactNo, Email = @Email, DOB = @DOB " +
                               "WHERE StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@DOB", student.DOB);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteStudent(int studentID)
        {
            using (SqlConnection connection = _dataAccess.GetDBConnection())
            {
                connection.Open();
                string query = "DELETE FROM dbo.Students WHERE StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}