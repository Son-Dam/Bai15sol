using Bai15.Transcripts;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Bai15.Students
{
    class Student: IComparable<Student>
    {
        public static int numStudent = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int StartYear { get; set; }
        public double EntranceScore { get; set; }
        public Transcript transcript;
        public Student(int Id,string Name, string DOB,int StartYear, double EntranceScore,Transcript transcript ) 
        {
            this.Id = Id;
            this.Name = Name;
            this.DOB = DOB;
            this.StartYear = StartYear;
            this.EntranceScore = EntranceScore;
            this.transcript = transcript;
            numStudent++;
        }

        public Student(int Id) {
            this.Id = Id;   
        }

        public Student(Student student) {
            this.Id = student.Id;
            this.Name = student.Name;
            this.DOB = student.DOB;
            this.StartYear = student.StartYear;
            this.EntranceScore = student.EntranceScore;
            this.transcript = student.transcript;
        }

        public void AddTranscriptRecord(string semester, double GPA)
        {
            transcript.AddRecord(semester, GPA);
        }
        public double GetStudentSemGPA(string semester) {
            return transcript.GetSemGPA(semester);
        }



        public int CompareTo(Student? other)
        {
            if(other == null) throw new Exception("Can't compare an object to null.");
            if (other is StudentInService)
            {
                if(this is StudentInService) return other.StartYear.CompareTo(StartYear);
                else return 1;
            }
            if (this is StudentInService) return -1;
            return other.StartYear.CompareTo(StartYear);
            
        }
        
        public bool Is8GPAOrAbove()
        {
            return transcript.Is8GPAOrAbove();
        }
        public override string ToString()
        {
            return $"Student name: {Name}, ID: {Id}, DOB: {DOB}, Start year: {StartYear},Entrance test score:{EntranceScore}\n" +
                $"\t Transcript:{transcript}";
        }
    }
}

