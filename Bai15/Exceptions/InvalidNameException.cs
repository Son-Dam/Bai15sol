using System.Runtime.Serialization;


namespace Bai15.Exceptions
{
    [Serializable]
    class InvalidNameException : Exception
    {
        private const string DefaultMessage = "Invalid Full Name!";
        public InvalidNameException() : base(DefaultMessage) { }
        public InvalidNameException(string message) : base(DefaultMessage + " " + message) { }
        public InvalidNameException(string message, Exception innerException) : base(message, innerException) { }
        protected InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}