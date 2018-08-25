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
			//throw new Exception("Error from step 2");
            return new AnotherSampleResponse { LastName = "My lastName" };
        }
    }
}