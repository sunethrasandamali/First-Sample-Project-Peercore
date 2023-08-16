using Sample_CRUD_Application.DataService.BaseClasses;
using Sample_CRUD_Application.DataService.Repository;
using Sample_CRUD_Application.Model;
using System.Collections.Generic;

namespace Sample_CRUD_Application.AppService
{
    public class StudentService
    {
        private readonly StudentDataRepository _studentdataRepository;

        public StudentService(StudentDataRepository studentdataRepository)
        {
            _studentdataRepository = studentdataRepository;
        }

        public List<StudentDataModel> GetAllStudents()
        {
            return _studentdataRepository.GetAllStudents();
        }

        public void CreateStudent(StudentDataModel student)
        {
             _studentdataRepository.CreateStudent(student);
        }

        public void UpdateStudent(int studentID, StudentDataModel student)
        {
             _studentdataRepository.UpdateStudent(studentID, student);
        }

        public void DeleteStudent(int studentID)
        {
             _studentdataRepository.DeleteStudent(studentID);
        }
    }
}