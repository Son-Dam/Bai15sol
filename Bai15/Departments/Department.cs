using Bai15.Students;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bai15.Departments
{
    class Department
    {
        public string Name { get; set; }
        private List<Student> students;
        public Department(string Name) {
            this.Name = Name;
            students = new List<Student>();
            
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        public Student GetValedictorian()
        {
            if (students.Count == 0) throw new Exception("Department has no student");
            return students.MaxBy(x => x.EntranceScore);
        }

        public List<Student> GetStudentInServiceByLocation(string location)
        {
            List<Student> list = new List<Student>(); 
            foreach (Student student in students)
            {
                if(student is StudentInService && (student as StudentInService).AssociateTrainingLocation == location)
                    list.Add(student);
            }
            return list;
        }

        public List<Student> GetStudentWith8GPAOrAbove()
        {
            List<Student> list = new List<Student>();
            foreach (Student student in students)
            {
               if(student.Is8GPAOrAbove()) list.Add(student);
            }
            return list;
        }

        public Dictionary<int,int> GetNumStudentByStartYearStats()
        {
            Dictionary<int,int> dict = new Dictionary<int,int>();
            foreach (Student student in students)
            {
                dict.Add(student.StartYear, dict.GetValueOrDefault(student.StartYear,0)+1);
            }
            return dict;
        }
        public List<Student> GetSortByTypeThenStartYear()
        {
            return students.OrderBy(x => x).ThenByDescending(x=>x.StartYear).ToList();
        }
        public override string ToString()
        {
            return $"Department: {Name}\n" +
                $"\t{string.Join("\n",students)}";
        }
    }
    
}
