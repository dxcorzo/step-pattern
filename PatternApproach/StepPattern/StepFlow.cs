using System;
using System.Collections.Generic;

namespace PatternApproach.StepPattern
{
    public class StepFlow
    {
        private readonly Queue<Step> _steps;

        public StepFlow() => _steps = new Queue<Step>();

        internal StepFlow AddStep(Step stepToAdd)
        {
            _steps.Enqueue(stepToAdd);
            return this;
        }

        public StepFlow WithData<T>(T dataForStep)
        {
            _steps.Peek().SetPreviousStepReponse(StepResponse.FromData(dataForStep));
            return this;
        }

        public void Run() => ProcessQueue<StepResponse>();

        public void Run<T>(Action<T> onSuccess, Action<Exception> onError) => ProcessQueue(onSuccess, onError);

        private void ProcessQueue<T>(Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            var currentStep = _steps.Dequeue();

            var currentStepResponse = currentStep.Execute();

            if (currentStepResponse.HasExecutionErrors)
            {
                if (onError != null)
                {
                    onError.Invoke(currentStepResponse.InnerException);
                    return;
                }

                throw new StepException($"Error executing step {currentStep.GetType()}", currentStepResponse.InnerException);
            }

            if (_steps.Count == 0)
            {
                onSuccess?.Invoke(StepResponse.ExtractResult<T>(currentStepResponse));
                return;
            }

            _steps.Peek().SetPreviousStepReponse(currentStepResponse);

            ProcessQueue(onSuccess, onError);
        }
        
    }
}