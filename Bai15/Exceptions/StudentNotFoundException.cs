using System.Runtime.Serialization;


namespace Bai15.Exceptions
{
    [Serializable]
    class StudentNotFoundException : Exception
    {
        private const string DefaultMessage = "Student doesn't exist!";
        public StudentNotFoundException() : base(DefaultMessage) { }
        public StudentNotFoundException(string message) : base(DefaultMessage + " " + message) { }
        public StudentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected StudentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}