using Bai15.Transcripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai15.Students
{
    class StudentInService : Student
    {
        public static int numStudentInService = 0;
        public string AssociateTrainingLocation { get; set; }
        public StudentInService(int Id, string Name, string DOB, int StartYear, double EntranceScore, Transcript transcript, string associateTrainingLocation) : base(Id, Name, DOB, StartYear, EntranceScore, transcript)
        {
            AssociateTrainingLocation = associateTrainingLocation;
            numStudent--;
            numStudentInService++;
        }
        public override string ToString()
        {
            return $"Student name: {Name}, ID: {Id}, DOB: {DOB}, Start year: {StartYear},Entrance test score:{EntranceScore}, Training Location: {AssociateTrainingLocation}\n" +
                $"\t Transcript:{transcript.ToString()}";
        }

    }
}
