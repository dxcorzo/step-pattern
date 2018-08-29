using System;
using System.Threading.Tasks;
using PatternApproach.StepPattern;

namespace PatternApproach.Utils
{
    public class ExceptionHandler
    {
        public static void Execute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex}");
            }
        }

        public static StepResponse Execute<T>(Func<T> code, Func<Exception, StepResponse> onErrorHandler, Func<T, StepResponse> onCompleteHandler)
        {
            if (code == null)
            {
                throw new ArgumentException("code param is null");
            }

            try
            {
                var returnData = code.Invoke();
                return onCompleteHandler.Invoke(returnData);
            }
            catch (Exception ex)
            {
                return onErrorHandler.Invoke(ex);
            }
        }

        public static async Task<StepResponse> ExecuteAsync<T>(Func<T> code, Func<Exception, StepResponse> onErrorHandler, Func<T, StepResponse> onCompleteHandler)
        {
            if (code == null)
            {
                throw new ArgumentException("code param is null");
            }

            try
            {
                var returnData = await Task.Run(code);
                return onCompleteHandler.Invoke(returnData);
            }
            catch (Exception ex)
            {
                return onErrorHandler.Invoke(ex);
            }

        }

    }
}