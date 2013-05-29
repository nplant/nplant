using System;
using System.Runtime.Serialization;

namespace NPlant.Core
{
    [Serializable]
    public class NPlantException : Exception
    {
        public NPlantException()
        {
        }

        public NPlantException(string message) : base(message)
        {
        }

        public NPlantException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NPlantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}