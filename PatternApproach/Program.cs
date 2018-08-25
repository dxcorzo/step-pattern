using System;
using PatternApproach.Models;
using PatternApproach.StepPattern;
using PatternApproach.Steps;
using PatternApproach.Utils;

namespace PatternApproach
{
    class Program
    {
        public static void Main(string[] args)
        {
            ExceptionHandler.Execute(() =>
            {
                var dataForStep1 = new SampleResponse { Name = "Data for step one" };

                new StepFlow()
                    .AddStep(new StepOne()).WithData(dataForStep1)
                    .AddStep(new StepTwo())
                    .AddStep(new StepThree())
                    .AddStep(new StepFour())
                    .Run<AnotherSampleResponse>(response =>
                    {
                        Console.WriteLine($"Executed OK: {response.LastName}");
                    }, OnError);
            });
        }

        public static void OnError(Exception generatedException)
        {
            Console.WriteLine($"Executed with errors: {Environment.NewLine} {generatedException}");
        }
    }
}