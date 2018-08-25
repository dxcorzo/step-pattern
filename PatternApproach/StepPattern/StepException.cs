using System;

namespace PatternApproach.StepPattern
{
    public class StepException : Exception
    {
        public StepException(string message, Exception innerException) : base(message, innerException) { }
    }
}