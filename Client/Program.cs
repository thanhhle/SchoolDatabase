using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;

namespace Client
{
    public class Program
    {
        private static IBusinessLayer unitOfWork = new BusinessLayer.BusinessLayer();
        public static void Main(string[] args)
        {
            bool stop = false;

            while (!stop)
            {
                unitOfWork = new BusinessLayer.BusinessLayer();
                DisplayMenu();
                int choice = ReadChoice();
                Console.WriteLine("Selected: {0}\n", choice);
                int i = 0;

                if (choice == i)
                {
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                    return;
                }

                #region Standard
                else if (choice == ++i) AddStandard();
                else if (choice == ++i) UpdateStandard();
                else if (choice == ++i) RemoveStandard();
                else if (choice == ++i) SearchStandardByID();
                else if (choice == ++i) SearchStandardByName();
                else if (choice == ++i) GetAllStandards();
                else if (choice == ++i) GetStandardAndAllStudents();
                else if (choice == ++i) GetStandardAndAllTeachers();
                #endregion

                #region Student
                else if (choice == ++i) AddStudent();
                else if (choice == ++i) UpdateStudent();
                else if (choice == ++i) RemoveStudent();
                else if (choice == ++i) SearchStudentByID();
                else if (choice == ++i) SearchStudentByName();
                else if (choice == ++i) GetAllStudents();
                else if (choice == ++i) GetStudentAndAllCourses();
                #endregion

                #region Teacher
                else if (choice == ++i) AddTeacher();
                else if (choice == ++i) UpdateTeacher();
                else if (choice == ++i) RemoveTeacher();
                else if (choice == ++i) SearchTeacherByID();
                else if (choice == ++i) SearchTeacherByName();
                else if (choice == ++i) GetAllTeachers();
                else if (choice == ++i) GetTeacherAndAllCourses();
                #endregion

                #region Course
                else if (choice == ++i) AddCourse();
                else if (choice == ++i) UpdateCourse();
                else if (choice == ++i) RemoveCourse();
                else if (choice == ++i) SearchCourseByID();
                else if (choice == ++i) SearchCourseByName();
                else if (choice == ++i) GetAllCourses();
                else if (choice == ++i) GetTeacherOfCourse();
                else if (choice == ++i) GetAllStudentsInCourse();
                #endregion

                #region Student Address
                else if (choice == ++i) AddStudentAddress();
                else if (choice == ++i) UpdateStudentAddress();
                else if (choice == ++i) RemoveStudentAddress();
                else if (choice == ++i) GetStudentAddressByStudent();
                else if (choice == ++i) GetAllStudentAddresses();
                #endregion

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadLine();
            }

        }

        #region Menu
        private static void DisplayMenu()
        {
            string output = "-------------- Repository Program --------------\n\n";
            string[] attributes = { "Standard", "Student", "Teacher", "Course", "Student Address" };
            int option = 1;
            foreach (string s in attributes)
            {
                output += String.Format("[            {0}            ]\n", s);
                output += String.Format("{0}) Add {1}\n", option++, s);
                output += String.Format("{0}) Update {1}\n", option++, s);
                output += String.Format("{0}) Remove {1}\n", option++, s);

                if (s != "Student Address")
                {
                    output += String.Format("{0}) Search for {1} by ID\n", option++, s);
                    output += String.Format("{0}) Search for {1} by Name\n", option++, s);
                    output += String.Format("{0}) Get all {1}s\n", option++, s);
                }

                if (s == "Standard")
                {
                    output += String.Format("{0}) Get Standard and all Students\n", option++);
                    output += String.Format("{0}) Get Standard and all Teachers\n\n", option++);
                }
                else if (s == "Student")
                {
                    output += String.Format("{0}) Get Student and all Courses\n\n", option++);
                }
                else if (s == "Teacher")
                {
                    output += String.Format("{0}) Get Teacher and all Courses\n\n", option++);
                }
                else if (s == "Course")
                {
                    output += String.Format("{0}) Get Teacher of Course\n", option++);
                    output += String.Format("{0}) Get All Students in Courses\n\n", option++);
                }
                else if (s == "Student Address")
                {
                    output += String.Format("{0}) Get Student Address by Student Name or ID\n", option++);
                    output += String.Format("{0}) Get All Student Addresses\n\n", option++);
                }
            }

            output += "\n------------------------------------------------\n";
            output += "0) Exit\n\n";
            output += "- Select an option: ";
            Console.Write(output);
        }

        private static int ReadChoice()
        {
            int choice = GetNumber();

            while (choice < 0 && choice > 13)
            {
                Console.Write("Please enter number from 0 - 13: ");
                choice = GetNumber();
            }

            return choice;
        }
        #endregion

        #region Standard
        private static void AddStandard()
        {
            Console.WriteLine("-----------------Add Standard-----------------");            
            Console.WriteLine("[optional] Enter Standard Name:");
            string name = Console.ReadLine();

            Standard standard = new Standard();
            if (!string.IsNullOrWhiteSpace(name))
            {
                standard.StandardName = name;
            }
            else
            {
                Console.WriteLine("Standard Name is null");
            }

            Console.WriteLine("\n[optional] Enter Standard Description:");
            string description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                standard.Description = description;
            }
            else
            {
                Console.WriteLine("Standard Description is null");
            }

            unitOfWork.AddStandard(standard);
            
            Console.WriteLine("\nStandard has been added!");
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
            Display(standard);
        }

        private static void UpdateStandard()
        {
            Console.WriteLine("----------------Update Standard----------------");            
            Console.WriteLine("Enter Standard ID or Standard Name to modify:");
            string input = Console.ReadLine();

            Standard standard;
            if (int.TryParse(input, out int id))
            {
                standard = unitOfWork.GetStandardByID(id);
            }
            else
            {
                standard = unitOfWork.GetStandardByName(input);
            }

            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else
            {
                Console.WriteLine("\nUpdating Standard...");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);

                Console.WriteLine("\n[optional] Enter Standard Name to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || name == standard.StandardName)
                {
                    Console.WriteLine("Standard Name is kepted the same: {0}", standard.StandardName);
                }
                else if (name == "null")
                {
                    standard.StandardName = String.Empty;
                }
                else
                {
                    standard.StandardName = name;
                }


                Console.WriteLine("\n[optional] Enter Standard Description to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description) || description == standard.Description)
                {
                    Console.WriteLine("Standard Description is kepted the same: {0}", standard.Description);
                }
                else if (description == "null")
                {
                    standard.Description = String.Empty;
                }
                else
                {
                    standard.Description = description;
                }

                unitOfWork.UpdateStandard(standard);
           
                Console.WriteLine("\nStandard has been updated!");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
            }
        }

        private static void RemoveStandard()
        {
            Console.WriteLine("----------------Remove Standard----------------");            

            Console.WriteLine("Enter Standard ID or Standard Name to get the Standard to be removed:");
            string input = Console.ReadLine();

            Standard standard;
            if (int.TryParse(input, out int id))
            {
                standard = unitOfWork.GetStandardByID(id);
            }
            else
            {
                standard = unitOfWork.GetStandardByName(input);
            }

            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else
            {
                foreach(Student student in standard.Students)
                {
                    student.StandardId = null;
                }

                foreach (Teacher teacher in standard.Teachers)
                {
                    teacher.StandardId = null;
                }

                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
                unitOfWork.RemoveStandard(standard);
                
                Console.WriteLine("\nStandard has been removed!");        
            }
        }

        private static void SearchStandardByID()
        {
            Console.WriteLine("-------------Search Standard By ID-------------");            
            
            Console.WriteLine("Enter Standard ID to search:");
            int id = GetNumber();

            Standard standard = unitOfWork.GetStandardByID(id);
            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
            }
        }

        private static void SearchStandardByName()
        {
            Console.WriteLine("------------Search Standard By Name------------");            
            
            Console.WriteLine("Enter Standard Name to search:");
            string name = Console.ReadLine();

            Standard standard = unitOfWork.GetStandardByName(name);
            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
            }
        }

        private static void GetAllStandards()
        {
            Console.WriteLine("------------Get All Standards------------");            
            
            IEnumerable < Standard > standards = unitOfWork.GetAllStandards();
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
            foreach (Standard standard in standards)
            {
                Display(standard);
            }
        }

        private static void GetStandardAndAllStudents()
        {
            Console.WriteLine("-------Get Standard and All Students-------");            
            
            Console.Write("Enter Standard ID or Standard Name to get Students: ");
            string input = Console.ReadLine();

            Standard standard;
            if (int.TryParse(input, out int id))
            {
                standard = unitOfWork.GetStandardByID(id);
            }
            else
            {
                standard = unitOfWork.GetStandardByName(input);
            }

            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else if (standard.Students == null || standard.Students.Count == 0)
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
                Console.WriteLine("\nThis Standard has no Students!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
                Console.WriteLine("\nThis Standard contains {0} Student(s):", standard.Students.Count);
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                foreach (Student student in standard.Students)
                {
                    Display(student);
                }
            }
        }

        private static void GetStandardAndAllTeachers()
        {
            Console.WriteLine("-------Get Standard and All Teachers-------");           
            
            Console.Write("Enter Standard ID or Standard Name to get Teachers: ");
            string input = Console.ReadLine();

            Standard standard;
            if (int.TryParse(input, out int id))
            {
                standard = unitOfWork.GetStandardByID(id);
            }
            else
            {
                standard = unitOfWork.GetStandardByName(input);
            }

            if (standard == null)
            {
                Console.WriteLine("\nStandard not found!");
            }
            else if (standard.Teachers == null || standard.Teachers.Count == 0)
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
                Console.WriteLine("\nThis Standard has no Teachers!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Standard Name", "Description");
                Display(standard);
                Console.WriteLine("\nThis Standard contains {0} Teacher(s):", standard.Teachers.Count);
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                foreach (Teacher teacher in standard.Teachers)
                {
                    Display(teacher);
                }
            }
        }

        #endregion

        #region Student
        private static void AddStudent()
        {
            Console.WriteLine("-----------------Add Student-----------------");            
            
            Console.WriteLine("[optional] Enter Student Name:");
            Student student = new Student();
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                student.StudentName = name;
            }
            else
            {
                Console.WriteLine("Student Name is null");
            }

            Console.WriteLine("\n[optional] Enter Standard ID:");
            string standardID = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(standardID))
            {
                try
                {
                    Int32.Parse(standardID);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Invalid input format. Please enter again: ");
                    standardID = Console.ReadLine();
                }

            }

            if (string.IsNullOrWhiteSpace(standardID))
            {
                Console.WriteLine("Standard ID is null");
            }
            else
            {
                student.StandardId = Int32.Parse(standardID);
            }

            unitOfWork.AddStudent(student);
            
            Console.WriteLine("\nStudent has been added!");
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
            Display(student);
        }

        private static void UpdateStudent()
        {
            Console.WriteLine("----------------Update Student----------------");            
            
            Console.WriteLine("Enter Student ID or Student Name to modify:");
            string input = Console.ReadLine();

            Student student;
            if (int.TryParse(input, out int id))
            {
                student = unitOfWork.GetStudentByID(id);
            }
            else
            {
                student = unitOfWork.GetStudentByName(input);
            }

            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\nUpdating Student...");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);

                #region Set Name
                Console.WriteLine("\n[optional] Enter Student Name to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || name == student.StudentName)
                {
                    Console.WriteLine("Student Name is kepted the same: {0}", student.StudentName);
                }
                else if (name == "null")
                {
                    student.StudentName = String.Empty;
                }
                else
                {
                    student.StudentName = name;
                }
                #endregion

                #region Set StandardID
                Console.WriteLine("\n[optional] Enter Standard ID to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string standardID = Console.ReadLine();

                while (!string.IsNullOrWhiteSpace(standardID) && standardID != "null")
                {
                    try
                    {
                        Int32.Parse(standardID);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input format. Please enter again: ");
                        standardID = Console.ReadLine();
                    }

                }

                if (string.IsNullOrWhiteSpace(standardID) || Int32.Parse(standardID) == student.StandardId)
                {
                    Console.WriteLine("Standard ID is kepted the same: {0}", student.StandardId);
                }
                else if (standardID == "null")
                {
                    student.StandardId = null;
                }
                else
                {
                    student.StandardId = Int32.Parse(standardID);
                }

                unitOfWork.UpdateStudent(student);
                
                Console.WriteLine("\nStudent has been updated!");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);
                #endregion
            }
        }

        private static void RemoveStudent()
        {
            Console.WriteLine("----------------Remove Student----------------");            
           
            Console.WriteLine("Enter Student ID or Student Name to remove:");
            string input = Console.ReadLine();

            Student student;
            if (int.TryParse(input, out int id))
            {
                student = unitOfWork.GetStudentByID(id);
            }
            else
            {
                student = unitOfWork.GetStudentByName(input);
            }

            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);
                unitOfWork.RemoveStudent(student);
                
                Console.WriteLine("\nStudent has been removed!");
            }
        }

        private static void SearchStudentByID()
        {
            Console.WriteLine("-------------Search Student By ID-------------");            
            
            Console.WriteLine("Enter Student ID to search:");
            int id = GetNumber();

            Student student = unitOfWork.GetStudentByID(id);
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);
            }
        }

        private static void SearchStudentByName()
        {
            Console.WriteLine("-------------Search Student By Name-------------");            
            
            Console.WriteLine("Enter Student Name to search:");
            string name = Console.ReadLine();

            Student student = unitOfWork.GetStudentByName(name);
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);
            }
        }

        private static void GetAllStudents()
        {
            Console.WriteLine("------------Get All Students------------");            
            IEnumerable < Student > students = unitOfWork.GetAllStudents();
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
            foreach (Student student in students)
            {
                Display(student);
            }
        }

        private static void GetStudentAndAllCourses()
        {
            Console.WriteLine("-------Get Student and All Courses-------");            
            
            Console.Write("Enter Student ID or Student Name to get Courses: ");
            string input = Console.ReadLine();

            Student student;
            if (int.TryParse(input, out int id))
            {
                student = unitOfWork.GetStudentByID(id);
            }
            else
            {
                student = unitOfWork.GetStudentByName(input);
            }

            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else if (student.Courses == null || student.Courses.Count == 0)
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Student Name", "Description");
                Display(student);
                Console.WriteLine("\nThis Student has no Courses!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Student Name", "Description");
                Display(student);
                Console.WriteLine("\nThis Student contains {0} Course(s):", student.Courses.Count);
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                foreach (Course course in student.Courses)
                {
                    Display(course);
                }
            }
        }
        #endregion

        #region Teacher
        private static void AddTeacher()
        {
            Console.WriteLine("-----------------Add Teacher-----------------");            
            
            Console.WriteLine("[optional] Enter Teacher Name:");

            Teacher teacher = new Teacher();
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                teacher.TeacherName = name;
            }
            else
            {
                Console.WriteLine("Teacher Name is null");
            }

            Console.WriteLine("\n[optional] Enter Standard ID:");
            string standardID = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(standardID))
            {
                try
                {
                    Int32.Parse(standardID);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Invalid input format. Please enter again: ");
                    standardID = Console.ReadLine();
                }
            }

            if (string.IsNullOrWhiteSpace(standardID))
            {
                Console.WriteLine("Standard ID is null");
            }
            else
            {
                teacher.StandardId = Int32.Parse(standardID);
            }

            unitOfWork.AddTeacher(teacher);
            
            Console.WriteLine("\nTeacher has been added!");
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
            Display(teacher);
        }

        private static void UpdateTeacher()
        {
            Console.WriteLine("----------------Update Teacher----------------");            
            
            Console.WriteLine("Enter Teacher ID or Teacher Name to modify:");
            string input = Console.ReadLine();

            Teacher teacher;
            if (int.TryParse(input, out int id))
            {
                teacher = unitOfWork.GetTeacherByID(id);
            }
            else
            {
                teacher = unitOfWork.GetTeacherByName(input);
            }

            if (teacher == null)
            {
                Console.WriteLine("\nTeacher not found!");
            }
            else
            {
                Console.WriteLine("\nUpdating Teacher...");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);

                #region Set Name
                Console.WriteLine("\n[optional] Enter Teacher Name to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || name == teacher.TeacherName)
                {
                    Console.WriteLine("Teacher Name is kepted the same: {0}", teacher.TeacherName);
                }
                else if (name == "null")
                {
                    teacher.TeacherName = String.Empty;
                }
                else
                {
                    teacher.TeacherName = name;
                }
                #endregion

                #region Set StandardID
                Console.WriteLine("\n[optional] Enter Standard ID to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string standardID = Console.ReadLine();

                while (!string.IsNullOrWhiteSpace(standardID) && standardID != "null")
                {
                    try
                    {
                        Int32.Parse(standardID);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input format. Please enter again: ");
                        standardID = Console.ReadLine();
                    }

                }

                if (string.IsNullOrWhiteSpace(standardID) || Int32.Parse(standardID) == teacher.StandardId)
                {
                    Console.WriteLine("Standard ID is kepted the same: {0}", teacher.StandardId);
                }
                else if (standardID == "null")
                {
                    teacher.StandardId = null;
                }
                else
                {
                    teacher.StandardId = Int32.Parse(standardID);
                }

                unitOfWork.UpdateTeacher(teacher);
                
                Console.WriteLine("\nTeacher has been updated!");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
                #endregion
            }
        }

        private static void RemoveTeacher()
        {
            Console.WriteLine("----------------Remove Teacher----------------");            
            
            Console.WriteLine("Enter Teacher ID or Teacher Name to remove:");
            string input = Console.ReadLine();

            Teacher teacher;
            if (int.TryParse(input, out int id))
            {
                teacher = unitOfWork.GetTeacherByID(id);
            }
            else
            {
                teacher = unitOfWork.GetTeacherByName(input);
            }

            if (teacher == null)
            {
                Console.WriteLine("\nTeacher not found!");
            }
            else
            {
                foreach(Course course in teacher.Courses)
                {
                    course.TeacherId = null;
                }

                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
                unitOfWork.RemoveTeacher(teacher);
                
                Console.WriteLine("\nTeacher has been removed!");
            }
        }

        private static void SearchTeacherByID()
        {
            Console.WriteLine("-------------Search Teacher By ID-------------");            
            
            Console.WriteLine("Enter Teacher ID to search:");
            int id = GetNumber();

            Teacher teacher = unitOfWork.GetTeacherByID(id);
            if (teacher == null)
            {
                Console.WriteLine("\nTeacher not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
            }
        }

        private static void SearchTeacherByName()
        {
            Console.WriteLine("-------------Search Teacher By Name-------------");            
            
            Console.WriteLine("Enter Teacher Name to search:");
            string name = Console.ReadLine();

            Teacher teacher = unitOfWork.GetTeacherByName(name);
            if (teacher == null)
            {
                Console.WriteLine("\nTeacher not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
            }
        }

        private static void GetAllTeachers()
        {
            Console.WriteLine("------------Get All Teachers------------");            
            
            IEnumerable < Teacher > teachers = unitOfWork.GetAllTeachers();
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
            foreach (Teacher teacher in teachers)
            {
                Display(teacher);
            }
        }

        private static void GetTeacherAndAllCourses()
        {
            Console.WriteLine("-------Get Teacher and All Courses-------");            
            
            Console.Write("Enter Teacher ID or Teacher Name to get Courses: ");
            string input = Console.ReadLine();

            Teacher teacher;
            if (int.TryParse(input, out int id))
            {
                teacher = unitOfWork.GetTeacherByID(id);
            }
            else
            {
                teacher = unitOfWork.GetTeacherByName(input);
            }

            if (teacher == null)
            {
                Console.WriteLine("\nTeacher not found!");
            }
            else if (teacher.Courses == null || teacher.Courses.Count == 0)
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
                Console.WriteLine("\nThis Teacher has no Courses!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                Display(teacher);
                Console.WriteLine("\nThis Teacher contains {0} Course(s):", teacher.Courses.Count);
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                foreach (Course course in teacher.Courses)
                {
                    Display(course);
                }
            }
        }
        #endregion

        #region Course
        private static void AddCourse()
        {
            Console.WriteLine("-----------------Add Course-----------------");            

            Console.WriteLine("[optional] Enter Course Name:");
            Course course = new Course();
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                course.CourseName = name;
            }
            else
            {
                Console.WriteLine("Course Name is null");
            }

            Console.WriteLine("\n[optional] Enter Teacher ID:");
            string teacherID = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(teacherID))
            {
                try
                {
                    Int32.Parse(teacherID);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Invalid input format. Please enter again: ");
                    teacherID = Console.ReadLine();
                }

            }

            if (string.IsNullOrWhiteSpace(teacherID))
            {
                Console.WriteLine("Teacher ID is null");
            }
            else
            {
                course.TeacherId = Int32.Parse(teacherID);
            }

            //SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            unitOfWork.AddCourse(course);
            
            Console.WriteLine("\nCourse has been added!");
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
            Display(course);
        }

        private static void UpdateCourse()
        {
            Console.WriteLine("----------------Update Course----------------");            

            Console.WriteLine("Enter Course ID or Course Name to modify:");
            string input = Console.ReadLine();

            Course course;
            if (int.TryParse(input, out int id))
            {
                course = unitOfWork.GetCourseByID(id);
            }
            else
            {
                course = unitOfWork.GetCourseByName(input);
            }

            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                Console.WriteLine("\nUpdating Course...");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);

                #region Set Name
                Console.WriteLine("\n[optional] Enter Course Name to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || name == course.CourseName)
                {
                    Console.WriteLine("Course Name is kepted the same: {0}", course.CourseName);
                }
                else if (name == "null")
                {
                    course.CourseName = String.Empty;
                }
                else
                {
                    course.CourseName = name;
                }
                #endregion

                #region Set TeacherID
                Console.WriteLine("\n[optional] Enter Teacher ID to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                string teacherID = Console.ReadLine();

                while (!string.IsNullOrWhiteSpace(teacherID) && teacherID != "null")
                {
                    try
                    {
                        Int32.Parse(teacherID);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input format. Please enter again: ");
                        teacherID = Console.ReadLine();
                    }

                }

                if (string.IsNullOrWhiteSpace(teacherID) || Int32.Parse(teacherID) == course.TeacherId)
                {
                    Console.WriteLine("Teacher ID is kepted the same: {0}", course.TeacherId);
                }
                else if (teacherID == "null")
                {
                    course.TeacherId = null;
                }
                else
                {
                    course.TeacherId = Int32.Parse(teacherID);
                }

                unitOfWork.UpdateCourse(course);
                
                Console.WriteLine("\nCourse has been updated!");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);
                #endregion
            }
        }

        private static void RemoveCourse()
        {
            Console.WriteLine("----------------Remove Course----------------");            

            Console.WriteLine("Enter Course ID or Course Name to remove:");
            string input = Console.ReadLine();

            Course course;
            if (int.TryParse(input, out int id))
            {
                course = unitOfWork.GetCourseByID(id);
            }
            else
            {
                course = unitOfWork.GetCourseByName(input);
            }

            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                foreach (Student student in course.Students)
                {
                    student.Courses.Remove(course);
                }

                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);
                unitOfWork.RemoveCourse(course);
                
                Console.WriteLine("\nCourse has been removed!");              
            }
        }

        private static void SearchCourseByID()
        {
            Console.WriteLine("-------------Search Course By ID-------------");            

            Console.WriteLine("Enter Course ID to search:");
            int id = GetNumber();

            Course course = unitOfWork.GetCourseByID(id);
            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);
            }
        }

        private static void SearchCourseByName()
        {
            Console.WriteLine("-------------Search Course By Name-------------");            

            Console.WriteLine("Enter Course Name to search:");
            string name = Console.ReadLine();

            Course course = unitOfWork.GetCourseByName(name);
            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);
            }
        }

        private static void GetAllCourses()
        {
            Console.WriteLine("------------Get All Courses------------");            

            IEnumerable < Course > courses = unitOfWork.GetAllCourses();
            Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
            foreach (Course course in courses)
            {
                Display(course);
            }
        }

        private static void GetTeacherOfCourse()
        {
            Console.WriteLine("----------------Get Teacher Of Course----------------");            

            Console.WriteLine("Enter Course ID or Course Name to get Teacher:");
            string input = Console.ReadLine();

            Course course;
            if (int.TryParse(input, out int id))
            {
                course = unitOfWork.GetCourseByID(id);
            }
            else
            {
                course = unitOfWork.GetCourseByName(input);
            }

            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                Console.WriteLine("\nCourse Details:");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);

                if (course.Teacher == null)
                {
                    Console.WriteLine("\nTeacher not found!");
                }
                else
                {
                    Console.WriteLine("\nTeacher Details:");
                    Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Teacher Name", "Standard ID");
                    Display(course.Teacher);
                }
            }
        }

        private static void GetAllStudentsInCourse()
        {
            Console.WriteLine("----------------Get All Students In Course----------------");            

            Console.WriteLine("Enter Course ID or Course Name to get all Students:");
            string input = Console.ReadLine();

            Course course;
            if (int.TryParse(input, out int id))
            {
                course = unitOfWork.GetCourseByID(id);
            }
            else
            {
                course = unitOfWork.GetCourseByName(input);
            }

            if (course == null)
            {
                Console.WriteLine("\nCourse not found!");
            }
            else
            {
                Console.WriteLine("\n{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
                Display(course);

                if (course.Students == null || course.Students.Count == 0)
                {
                    Console.WriteLine("\nThis Course has no Students!");
                }
                else
                {
                    Console.WriteLine("\nThis Course contains {0} Student(s):", course.Students.Count);
                    foreach (Student student in course.Students)
                    {
                        Console.WriteLine("- " + student.StudentName);
                    }
                }
            }
        }
        #endregion

        #region StudentAddress
        private static void AddStudentAddress()
        {
            Console.WriteLine("----------------Add Student Address----------------");            

            Student student = InputStudent();
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\nStudent Details:");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);

                if (student.StudentAddress != null)
                {
                    Console.WriteLine("The Student Address of this Student is already added.\nEach Student cannot has more than one Student Address.");
                }
                else
                {
                    StudentAddress address = new StudentAddress() { StudentID = student.StudentID };

                    Console.WriteLine("\n[optional] Enter Address 1:");
                    string address1 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(address1))
                    {
                        Console.WriteLine("Address 1 cannot be null. Please enter valid value.");
                        address1 = Console.ReadLine();
                    }
                    address.Address1 = address1;

                    Console.WriteLine("\n[optional] Enter Address 2:");
                    string address2 = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(address2))
                    {
                        address.Address2 = address2;
                    }
                    else
                    {
                        Console.WriteLine("Address 2 is null");
                    }

                    Console.WriteLine("\n[optional] Enter City:");
                    string city = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(city))
                    {
                        Console.WriteLine("City cannot be null. Please enter valid value.");
                        city = Console.ReadLine();
                    }
                    address.City = city;

                    Console.WriteLine("\n[optional] Enter State:");
                    string state = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(state))
                    {
                        Console.WriteLine("State cannot be null. Please enter valid value.");
                        state = Console.ReadLine();
                    }
                    address.State = state;

                    student.StudentAddress = address;
                    unitOfWork.AddStudentAddress(address);
                    
                    Console.WriteLine("\nStudent Address has been added!");
                    Display(student.StudentAddress);
                }
            }
        }

        private static void UpdateStudentAddress()
        {
            Console.WriteLine("----------------Update Student Address----------------");            

            Student student = InputStudent();
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\nStudent Details:");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);

                StudentAddress address = unitOfWork.GetStudentAddressByStudentID(student.StudentID);
                if (address == null)
                {
                    Console.WriteLine("\nThe Student Address is null");

                }
                else
                {
                    Console.WriteLine("\nUpdating Student Address...");
                    Display(address);

                    Console.WriteLine("\n[optional] Enter Address 1 to be updated. Press Enter to keep it the same:");
                    string address1 = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(address1))
                    {
                        address.Address1 = address1;
                    }
                    else
                    {
                        Console.WriteLine("Address 1 is kept the same: {0}\n", address.Address1);
                    }

                    Console.WriteLine("\n[optional] Enter Address 2 to be updated. Press Enter to keep it the same or enter \"null\" to set it to NULL:");
                    string address2 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(address2))
                    {
                        Console.WriteLine("Address 2 is kept the same: {0}\n", address.Address2);
                    }
                    else if (address2 == "null")
                    {
                        Console.WriteLine("Address 2 is set to NULL");
                        address.Address2 = null;
                    }
                    else
                    {
                        address.Address2 = address2;
                    }

                    Console.WriteLine("\n[optional] Enter City to be updated. Press Enter to keep it the same:");
                    string city = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(city))
                    {
                        address.City = city;
                    }
                    else
                    {
                        Console.WriteLine("City is kept the same: {0}\n", address.City);
                    }

                    Console.WriteLine("\n[optional] Enter State to be updated. Press Enter to keep it the same:");
                    string state = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(state))
                    {
                        address.State = state;
                    }
                    else
                    {
                        Console.WriteLine("State is kept the same: {0}\n", address.State);
                    }

                    unitOfWork.UpdateStudentAddress(address);
                    
                    Console.WriteLine("\nStudent Address has been updated!");
                    Display(student.StudentAddress);
                }
            }
        }

        private static void RemoveStudentAddress()
        {
            Console.WriteLine("----------------Remove Student Address----------------");            

            Student student = InputStudent();
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\nStudent Details:");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);

                StudentAddress address = unitOfWork.GetStudentAddressByStudentID(student.StudentID);
                if (address != null)
                {
                    Console.WriteLine("\nStudent Address:");
                    Display(address);
                    unitOfWork.RemoveStudentAddress(address);
                    
                    Console.WriteLine("\nStudent Address has been removed!");
                }
                else
                {
                    Console.WriteLine("\nStudent Address not found!");
                }
            }
        }

        private static void GetStudentAddressByStudent()
        {
            Console.WriteLine("----------------Get Student Address----------------");            

            Student student = InputStudent();
            if (student == null)
            {
                Console.WriteLine("\nStudent not found!");
            }
            else
            {
                Console.WriteLine("\nStudent Details:");
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(student);

                StudentAddress address = student.StudentAddress;
                if (address == null)
                {
                    Console.WriteLine("\nStudent Address not found!");
                }
                else
                {
                    Console.WriteLine("\nStudent Address:");
                    Display(address);
                }
            }
        }

        private static void GetAllStudentAddresses()
        {

            Console.WriteLine("------------Get All Student Addresses ------------");            

            IEnumerable < StudentAddress > addresses = unitOfWork.GetAllStudentAddresses();
            //Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Course Name", "Teacher ID");
            foreach (StudentAddress address in addresses)
            {
                Console.WriteLine("{0,-10}{1,-20}{2}", "ID", "Student Name", "Standard ID");
                Display(unitOfWork.GetStudentByID(address.StudentID));
                Display(address);
                Console.WriteLine();
            }
        }
        #endregion

        #region SupportedMethods
        private static int GetNumber()
        {
            try
            {
                return Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Write("Invalid input format. Please enter a number: ");
                return GetNumber();
            }
        }

        private static void Display(Student student)
        {
            string strStandardID = String.Empty;
            if (student.StandardId.HasValue)
            {
                strStandardID = student.StandardId.ToString();
            }
            Console.WriteLine("{0,-10}{1,-20}{2}", student.StudentID, student.StudentName, strStandardID);
        }

        private static void Display(Standard standard)
        {
            Console.WriteLine("{0,-10}{1,-20}{2}", standard.StandardId, standard.StandardName, standard.Description);
        }

        private static void Display(Teacher teacher)
        {
            string strStandardID = String.Empty;
            if (teacher.StandardId.HasValue)
            {
                strStandardID = teacher.StandardId.ToString();
            }
            Console.WriteLine("{0,-10}{1,-20}{2}", teacher.TeacherId, teacher.TeacherName, strStandardID);
        }

        private static void Display(Course course)
        {
            string strTeacherID = String.Empty;
            if (course.TeacherId.HasValue)
            {
                strTeacherID = course.TeacherId.ToString();
            }
            Console.WriteLine("{0,-10}{1,-20}{2}", course.CourseId, course.CourseName, strTeacherID);
        }

        private static void Display(StudentAddress address)
        {
            string address2 = String.Empty;
            if (!string.IsNullOrEmpty(address.Address2))
            {
                address2 = address.Address2 + "\n";
            }
            Console.WriteLine("{0}\n{1}{2}, {3}", address.Address1, address2, address.City, address.State);
        }

        private static Student InputStudent()
        {
            Console.WriteLine("Enter Student ID or Student Name to get Student Address:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                return unitOfWork.GetStudentByID(id);
            }
            else
            {
                return unitOfWork.GetStudentByName(input);
            }
        }
        #endregion
    }
}
