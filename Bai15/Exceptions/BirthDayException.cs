
using System.Runtime.Serialization;


namespace Bai15.Exceptions
{
    [Serializable]
    class BirthDayException : Exception
    {
        private const string DefaultMessage = "Invalid Birth Day!";
        public BirthDayException() : base(DefaultMessage) { }
        public BirthDayException(string message) : base(DefaultMessage + " " + message) { }
        public BirthDayException(string message, Exception innerException) : base(message, innerException) { }
        protected BirthDayException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
