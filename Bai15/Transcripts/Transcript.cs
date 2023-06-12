using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace Bai15.Transcripts
{
    class Transcript
    {
        private OrderedDictionary transcript;
        public Transcript() 
        {
            transcript = new OrderedDictionary();
        }

        public void AddRecord(string semester, double GPA)
        {
            transcript.Add(semester, GPA);
        }
        public double GetSemGPA(string semester)
        {
            return (double)transcript[semester];
        }

        internal bool Is8GPAOrAbove()
        {
            return (double)transcript[transcript.Count - 1] > 8.0;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(DictionaryEntry record in transcript) 
            {
                sb.Append(record.Key+ ": "+record.Value+", ");
            }
            sb.Remove(sb.Length-2,2);
            return sb.ToString();
        }



    }
}
