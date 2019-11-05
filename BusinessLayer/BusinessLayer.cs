using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentAddressRepository _studentAddressRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public BusinessLayer()
        {
            _standardRepository = new StandardRepository();
            _studentRepository = new StudentRepository();
            _studentAddressRepository = new StudentAddressRepository();
            _teacherRepository = new TeacherRepository();
            _courseRepository = new CourseRepository();
        }

        #region Standard
        public IEnumerable<Standard> GetAllStandards()
        {
            return _standardRepository.GetAll();
        }

        public Standard GetStandardByID(int id)
        {
            return _standardRepository.GetById(id);
        }

        public Standard GetStandardByName(string name)
        {
            try
            {
                return _standardRepository.GetSingle(
                    s => s.StandardName.Equals(name),
                    s => s.Students);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public void AddStandard(Standard standard)
        {
            _standardRepository.Insert(standard);
        }

        public void UpdateStandard(Standard standard)
        {
            _standardRepository.Update(standard);
        }

        public void RemoveStandard(Standard standard)
        {
            _standardRepository.Delete(standard);
        }
        #endregion


        #region Student
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public Student GetStudentByID(int id)
        {
            return _studentRepository.GetById(id);
        }

        public Student GetStudentByName(string name)
        {
            try
            {
                return _studentRepository.GetSingle(
                    s => s.StudentName.Equals(name),
                    s => s.Courses);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public void AddStudent(Student student)
        {
            _studentRepository.Insert(student);
        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.Update(student);
        }

        public void RemoveStudent(Student student)
        {
            _studentRepository.Delete(student);
        }
        #endregion


        #region StudentAddress
        public IEnumerable<StudentAddress> GetAllStudentAddresses()
        {
            return _studentAddressRepository.GetAll();
        }

        public StudentAddress GetStudentAddressByStudentID(int id)
        {
            return _studentAddressRepository.GetSingle(
                s => s.StudentID == id,
                s => s.Student);
        }

        public void AddStudentAddress(StudentAddress studentAddress)
        {
            _studentAddressRepository.Insert(studentAddress);
        }

        public void UpdateStudentAddress(StudentAddress studentAddress)
        {
            _studentAddressRepository.Update(studentAddress);
        }

        public void RemoveStudentAddress(StudentAddress studentAddress)
        {
            _studentAddressRepository.Delete(studentAddress);
        }

        #endregion


        #region Teacher
        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAll();
        }

        public Teacher GetTeacherByID(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public Teacher GetTeacherByName(string name)
        {
            try
            {
                return _teacherRepository.GetSingle(
                    s => s.TeacherName.Equals(name),
                    s => s.Courses);
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.Insert(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            _teacherRepository.Delete(teacher);
        }
        #endregion


        #region Course
        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Course GetCourseByID(int id)
        {
            return _courseRepository.GetById(id);
        }

        public Course GetCourseByName(string name)
        {
            try
            {
                return _courseRepository.GetSingle(
                    s => s.CourseName.Equals(name),
                    s => s.Students);
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public void AddCourse(Course course)
        {
            _courseRepository.Insert(course);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }

        public void RemoveCourse(Course course)
        {
            _courseRepository.Delete(course);
        }
        #endregion
    }
}