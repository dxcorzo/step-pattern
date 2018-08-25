using System;
using PatternApproach.Utils;

namespace PatternApproach.StepPattern
{
    public abstract class Step<TResponse> : Step
    {
        internal abstract TResponse ProcessStep();

        internal StepResponse<TResponse> OnStepCompleted(TResponse stepResponseData) => new StepResponse<TResponse>
        {
            IsSuccessfullyExecuted = true,
            ErrorMessage = string.Empty,
            Result = stepResponseData
        };

        internal override StepResponse Execute() => ExceptionHandler.Execute(ProcessStep, OnStepError, OnStepCompleted);
    }

    public abstract class Step<TResponse, TLastResponse> : Step<TResponse>
    {
        private TLastResponse _previousStepResult;

        public TLastResponse PreviousStepResult
        {
            get
            {
                if (_previousStepResult == null)
                {
                    throw new ArgumentException("Previous step returned null or is not defined");
                }

                return _previousStepResult;
            }
            set => _previousStepResult = value;
        }

        internal override void SetPreviousStepReponse(StepResponse currentStepResponse)
        {
            PreviousStepResult = ((StepResponse<TLastResponse>) currentStepResponse).Result;
        }
    }

    public abstract class Step
    {
        public StepResponse StepExecutedOk => new StepResponse
        {
            IsSuccessfullyExecuted = true,
            ErrorMessage = string.Empty
        };

        internal StepResponse OnStepError(Exception ex) => new StepResponse
        {
            IsSuccessfullyExecuted = false,
            ErrorMessage = ex.ToString(),
            InnerException = ex
        };
        
        internal abstract StepResponse Execute();

        internal virtual void SetPreviousStepReponse(StepResponse currentStepResponse) { }
    }
}