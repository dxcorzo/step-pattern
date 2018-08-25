using System;

namespace PatternApproach.StepPattern
{
    public class StepResponse
    {
        public string ErrorMessage { get; set; }

        public bool IsSuccessfullyExecuted { get; set; } = false;

        public Exception InnerException { get; set; }

        public static StepResponse FromData<T>(T dataForStep) => new StepResponse<T> { Result = dataForStep };

        public static T ExtractResult<T>(StepResponse data) => ((StepResponse<T>) data).Result;
    }

    public class StepResponse<T> : StepResponse
    {
        public T Result { get; set; }
    }
}