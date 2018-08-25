using System;
using PatternApproach.Models;
using PatternApproach.StepPattern;

namespace PatternApproach.Steps
{
    public class StepOne : Step<SampleResponse, SampleResponse>
    {
        internal override SampleResponse ProcessStep()
        {
            //Console.WriteLine($"Hello from step one: {PreviousStepResult.Name}");
            Console.WriteLine($"Hello from step one");
            
            return new SampleResponse { Name = "My Name" };
        }
    }
}