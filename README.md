# SchoolDatabase
An application written in C#


## Description
This application involves Repository Pattern and Unit of Work with Entity Framework
The database SchoolDB  has has different tables, stored procedures and views.
- Entities: Standard, Teacher, Student, Course, StudentAddress
- Relationships:
  - One-to-One: Student and StudentAddress have a one-to-one relationship eg. Student has zero or one StudentAddress.
  - One-to-Many: Standard and Teacher have a one-to-many relationship eg. many Teachers can be associate with one Standard.
  - Many-to-Many: Student and Course have a many-to-many relationship using StudentCourse table where StudentCourse table includes StudentId and CourseId. So one student can join many courses and one course also can have many students.

Create an application that allows user to practice CRUD operations on the database.

## Menu of options
```menu
[            Standard            ]
1) Add Standard
2) Update Standard
3) Remove Standard
4) Search for Standard by ID
5) Search for Standard by Name
6) Get all Standards
7) Get Standard and all Students
8) Get Standard and all Teachers

[            Student            ]
9) Add Student
10) Update Student
11) Remove Student
12) Search for Student by ID
13) Search for Student by Name
14) Get all Students
15) Get Student and all Courses

[            Teacher            ]
16) Add Teacher
17) Update Teacher
18) Remove Teacher
19) Search for Teacher by ID
20) Search for Teacher by Name
21) Get all Teachers
22) Get Teacher and all Courses

[            Course            ]
23) Add Course
24) Update Course
25) Remove Course
26) Search for Course by ID
27) Search for Course by Name
28) Get all Courses
29) Get Teacher of Course
30) Get All Students in Courses

[            Student Address            ]
31) Add Student Address
32) Update Student Address
33) Remove Student Address
34) Get Student Address by Student Name or ID
35) Get All Student Addresses
```

