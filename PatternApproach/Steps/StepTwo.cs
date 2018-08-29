using System;
using PatternApproach.Models;
using PatternApproach.StepPattern;

namespace PatternApproach.Steps
{
    public class StepTwo : Step<AnotherSampleResponse, SampleResponse>
    {
        internal override AnotherSampleResponse ProcessStep()
        {
            Console.WriteLine($"Hello from step two: {PreviousStepResult.Name}");

            return new AnotherSampleResponse { LastName = "My lastName" };
        }
    }
}