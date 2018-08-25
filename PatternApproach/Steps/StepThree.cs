using System;
using PatternApproach.Models;
using PatternApproach.StepPattern;

namespace PatternApproach.Steps
{
    public class StepThree : Step<StepResponse, AnotherSampleResponse>
    {
        internal override StepResponse ProcessStep()
        {
            Console.WriteLine($"Hello from step three: {PreviousStepResult.LastName}");

            return StepExecutedOk;
        }
    }
}