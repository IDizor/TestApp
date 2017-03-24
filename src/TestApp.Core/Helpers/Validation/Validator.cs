using System;
using System.Reflection;

namespace TestApp.Core.Helpers.Validation
{
    /// <summary>
    /// Provides validation methods for business methods parameters.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Throws the exception with specified type when condition is failed.
        /// </summary>
        /// <typeparam name="T">Exception type.</typeparam>
        /// <param name="validCondition">If set to <c>true</c> do not throw an exception.</param>
        /// <param name="messageTemplate">The exception message template.</param>
        /// <param name="parameters">The message template parameters.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void Requires<T>(bool validCondition, string messageTemplate, params object[] parameters) where T : Exception
        {
            if (validCondition)
            {
                return;
            }

            string errorMessage = string.Format(messageTemplate, parameters);

            T exception = null;
            ConstructorInfo constructor = typeof(T).GetTypeInfo()
                .GetConstructor(new Type[] { typeof(string), typeof(string) });

            if (constructor != null)
            {
                if (constructor.GetParameters()[0].Name == "paramName")
                {
                    exception = constructor.Invoke(new object[] { null, errorMessage }) as T;
                }
                else
                {
                    exception = constructor.Invoke(new object[] { errorMessage, null }) as T;
                }
            }
            else
            {
                constructor = typeof(T).GetTypeInfo().GetConstructor(new Type[] { typeof(string) });

                if (constructor != null)
                {
                    exception = constructor.Invoke(new object[] { errorMessage }) as T;
                }
            }

            if (exception == null)
            {
                throw new ArgumentException(errorMessage);
            }

            throw exception;
        }
    }
}
