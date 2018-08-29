using System;
using System.Threading.Tasks;
using PatternApproach.Utils;

namespace PatternApproach.StepPattern
{
    public abstract class Step<TResponse> : Step
    {
        internal abstract TResponse ProcessStep();

        private StepResponse OnStepCompleted(TResponse stepResponseData)
        {
            return new StepResponse<TResponse>
            {
                HasExecutionErrors = false,
                ErrorMessage = string.Empty,
                Result = stepResponseData
            };
        }
        
        internal override async Task<StepResponse> ExecuteAsync() => await ExceptionHandler.ExecuteAsync(ProcessStep, OnStepError, OnStepCompleted);
    }

    public abstract class Step<TResponse, TLastResponse> : Step<TResponse>
    {
        private TLastResponse _previousStepResult;

        public TLastResponse PreviousStepResult
        {
            get
            {
                if (_previousStepResult == null)
                    throw new ArgumentException("Previous step returned null or is not defined");

                return _previousStepResult;
            }
            set => _previousStepResult = value;
        }

        internal override void SetPreviousStepReponse(StepResponse currentStepResponse) => PreviousStepResult = ((StepResponse<TLastResponse>) currentStepResponse).Result;
    }

    public abstract class Step
    {
        public StepResponse StepExecutedOk() => new StepResponse
        {
            HasExecutionErrors = false,
            ErrorMessage = string.Empty
        };

        internal StepResponse OnStepError(Exception ex) => new StepResponse
        {
            HasExecutionErrors = true,
            ErrorMessage = ex.ToString(),
            InnerException = ex
        };
        
        internal abstract Task<StepResponse> ExecuteAsync();

        internal virtual void SetPreviousStepReponse(StepResponse currentStepResponse) { }
    }
}