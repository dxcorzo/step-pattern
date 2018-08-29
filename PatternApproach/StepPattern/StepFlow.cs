using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatternApproach.StepPattern
{
    public class StepFlow
    {
        private readonly ConcurrentQueue<Step> _steps;

        public StepFlow() => _steps = new ConcurrentQueue<Step>();

        internal StepFlow AddStep(Step stepToAdd)
        {
            _steps.Enqueue(stepToAdd);
            return this;
        }

        public StepFlow WithData<T>(T dataForStep)
        {
            _steps.TryPeek(out var nextStep);
            nextStep.SetPreviousStepReponse(StepResponse.FromData(dataForStep));
            return this;
        }

        public async void Run() => await ProcessQueue<StepResponse>();

        public async void Run<T>(Action<T> onSuccess, Action<Exception> onError) => await ProcessQueue(onSuccess, onError);

        private async Task ProcessQueue<T>(Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            _steps.TryDequeue(out var currentStep);

            var currentStepResponse = await currentStep.ExecuteAsync();

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

            _steps.TryPeek(out var nextStep);
            nextStep.SetPreviousStepReponse(currentStepResponse);

            await ProcessQueue(onSuccess, onError);
        }
        
    }
}