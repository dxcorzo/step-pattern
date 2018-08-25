using System;
using PatternApproach.Models;
using PatternApproach.StepPattern;

namespace PatternApproach.Steps
{
    public class StepFour : Step<AnotherSampleResponse>
    {
        internal override AnotherSampleResponse ProcessStep()
        {
            Console.WriteLine("Hello from step four");
            //throw new Exception("Error from step 4");

            return new AnotherSampleResponse { LastName = "Response for S4" };
        }
    }
}