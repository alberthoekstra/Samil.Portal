using System;

namespace Samil.Power
{
    public static class SamilHelperExtensions
    {

        public static T TryGetValues<T>(this SamilHelper helper, Func<SamilHelper, T> func, int tryCount = 5)
        {
            Exception exception = null;
            
            for (var i = 0; i < tryCount; i++)
            {
                try
                {
                    return func.Invoke(helper);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }

            throw exception;
        }
    }
}
