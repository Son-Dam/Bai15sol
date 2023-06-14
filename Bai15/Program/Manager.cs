using Bai15.Exceptions;
using Bai15.Departments;
using Bai15.Students;
using Bai15.Enums;
using System.Globalization;
using Bai15.Transcripts;


namespace Bai15.Program
{
    class Manager
    {
        public delegate void ValidateFunc<T>(string input, out T output);

        public List<Department> departments;
        public List<Student> students;
        public Manager() {
            departments = new List<Department>();
            students = new List<Student>();
        }
        public void Run()
        {
            bool quitting = false;
        start:
            Console.WriteLine("Hello, this is the student manager.\n" +
                "Please choose one of the folllowing actions to continue:\n" +
                "Type Add or 0 to add a new student into the database\n" +
                "Type Remove or 1 to remove student by Id\n" +
                "Type Quit or 2 to quit the program.");

            string input;

        ReadUserAction:

            ReadData(ValidateUserAction, out UserAction action,
                "Employee Type must be Add,Find or Quit. Please re-enter valid action:");
            
            switch (action)
            {
                case UserAction.Add:
                    AddStudent();
                    break;
                case UserAction.Print:
                    Print();
                    break;
                case UserAction.Quit:
                    quitting = true;
                    Console.WriteLine("Exitting program...");
                    return;

            }
            if (!quitting) goto start;
        }
        private void AddStudent(Student student, string departmentName)
        {
            foreach (Department d in departments)
            {
                if (d.Name == departmentName)
                {
                    d.AddStudent(student);
                    
                    return;
                }
            }
            Department department = new Department(departmentName);
            department.AddStudent(student);
            departments.Add(department);
        }
        public void AddStudent(Student student, List<string> departmentNames)
        {   
            students.Add(student);
            foreach(string departmentName in departmentNames)
            {
                
                AddStudent(student, departmentName);
            }
            
        }
        public void AddStudent()
        {
            //Read Id
            Console.WriteLine("Enter student ID:");
            ReadData(ValidateId, out int ID, "ID must be number only. Please re-enter ID:");
            
            //Read name
            Console.WriteLine("Please enter your name (10-50 characters):");
            ReadData(ValidateName, out string Name, "Name must be 10-50 characters long. Please re-enter valid name:");

            //Read DOB
            Console.WriteLine("Enter student's birthday:");
            ReadData(ValidateDateTime, out string DOB, "Please re-enter birthday in (dd/mm/yyyy) format:");

            //Read Start Year
            Console.WriteLine("Enter student's start year:");
            ReadData(ValidateInt, out int StartYear, "Please re-enter a valid number for start year:");
            
            //Read entrance score
            Console.WriteLine("Enter student's entrance score:");
            ReadData(ValidateScore, out double EntranceScore, "Please re-enter valid entrance score:");
            
            //Read Transcript
            Console.WriteLine("Enter student's number of records in transcript:");
            ReadData(ValidateInt, out int numRecord, "Please re-enter valid integer:");
                
            Transcript transcript = new();
            for(int i = 0; i<numRecord; i++)
            {
                Console.WriteLine("Semester:");
                string sem = Console.ReadLine();
                Console.WriteLine("GPA:");
                ReadData(ValidateScore, out double GPA, "Please re-enter valid GPA:");
                transcript.AddRecord(sem, GPA);
            }

            //Read student type
            Console.WriteLine("Is the student in service? (Type Yes or No)");
            StudentIsInService studentIsInService;
        ReadStudentType:
            try
            {
                string input = Console.ReadLine();
                if (!Enum.TryParse(input, true, out studentIsInService))
                    throw new Exception("Invalid input");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please enter yes or no only:");
                goto ReadStudentType;
            }
            Student student;
            switch (studentIsInService)
            {
                case StudentIsInService.Yes:
                    Console.WriteLine("Enter student'ss associate training location:");
                    string location = Console.ReadLine();
                    student = new StudentInService(ID,Name,DOB,StartYear,EntranceScore,transcript,location);
                    break;
                default:
                    student = new Student(ID, Name, DOB, StartYear, EntranceScore, transcript);
                    break;
            }
            

            Console.WriteLine("Enter number of departments that student enrolled in:");
            ReadData(ValidateInt, out int numDepartment, "Please re-enter valid integer:");
            List<string> departmentEnrolled = new List<string>();
            for(int i = 0; i < numDepartment; i++)
            {
                Console.WriteLine($"Enter department #{i}'s name:");
                departmentEnrolled.Add(Console.ReadLine());
            }
            students.Add(student);
            AddStudent(student, departmentEnrolled);
            Console.WriteLine("student added successfully!");
        }

        public void Print()
        {
            foreach(Department department in departments)
            {
                Console.WriteLine(department.ToString());
            }
        }

        public bool IsNormalStudent(int Id)
        {
            foreach(Student student in students)
            {
                if (student.Id == Id) return !(student is StudentInService);
            }
            throw new StudentNotFoundException();
            
        }

        public Dictionary<Department, Student> GetValedictorians()
        {
            Dictionary<Department, Student> dict = new();
            ;
            foreach(Department department in departments)
            {
               dict.Add(department,department.GetValedictorian());
            }
            return dict;
        }

        public double GetStudentSemGPA(int Id, string semester)
        {
            foreach (Student student in students)
            {
                if (student.Id == Id) return student.GetStudentSemGPA(semester);
            }
            throw new StudentNotFoundException();
        }

        public static int GetNumNormalStudent()
        {
            return Student.numStudent;
        }
        public static int GetNumStudentInService()
        {
            return StudentInService.numStudentInService;
        }

        public Dictionary<Department,List<Student>> GetStudentInServiceByLocation(string location)
        {
            Dictionary<Department, List<Student>> dict = new();
            ;
            foreach (Department department in departments)
            {              
                dict.Add(department, department.GetStudentInServiceByLocation(location));
            }
            return dict;
        }
        public Dictionary<Department,List<Student>> GetStudentWith8GPAOrAbove(){
            Dictionary<Department, List<Student>> dict = new();
            ;
            foreach (Department department in departments)
            {
                dict.Add(department, department.GetStudentWith8GPAOrAbove());
            }
            return dict;
        }
        public Dictionary<Department, Dictionary<int, int>> GetNumStudentByStartYearStats()
        {
            Dictionary<Department, Dictionary<int, int>> dict = new();
            
            foreach (Department department in departments)
            {
                dict.Add(department, department.GetNumStudentByStartYearStats());
            }
            return dict;
        }
        
        public static void ReadData<T>(ValidateFunc<T> validateFunc,out T output,string exceptionMessage)
        {
        start:
            try
            {
                string input = Console.ReadLine();
                validateFunc(input, out output);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(exceptionMessage);
                goto start;
            }
        }


        void ValidateUserAction(string input, out UserAction action)
        {
            if (!Enum.TryParse(input, true, out action))
                throw new Exception("Invalid User Action");
        }

        void ValidateId(string input, out int Id)
        {
            if (!int.TryParse(input, out Id)|| students.Any(x=>x.Id==  int.Parse(input))) throw new Exception("Invalid ID!");
        }
        void ValidateInt(string input, out int num)
        {
            if (!int.TryParse(input, out num)) throw new Exception("Invalid integer!");
        }

        void ValidateName(string input, out string Name)
        {
            if (input.Length < 10) throw new FullNameException("Name too short (less than 10 character)!");
            if (input.Length > 50) throw new FullNameException("Name too long (more than 50 characters)!");
            Name = input;
        }

        void ValidateDateTime(string input, out string DOB)
        {
            if (!DateTime.TryParse(input, CultureInfo.CreateSpecificCulture("fr-FR"), out DateTime datetime))
                throw new BirthDayException("The date time must be enter with (dd/mm/yyyy) format.");
            DOB = input;
        }

        void ValidateScore(string input, out double num)
        {
            if (!double.TryParse(input, out num))
                throw new Exception("The score must be a number (can be decimal).");
        }

    }
}
