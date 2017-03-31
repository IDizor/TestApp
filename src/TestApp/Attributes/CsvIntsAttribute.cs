using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Attributes
{
    /// <summary>
    /// CSV integers array attribute.
    /// </summary>
    /// <seealso cref="TestApp.Attributes.CsvArrayBaseAttribute" />
    public class CsvIntsAttribute : CsvArrayBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvIntsAttribute"/> class.
        /// </summary>
        /// <param name="paramName">The parameter-to-parse name.</param>
        public CsvIntsAttribute(string paramName) : base(paramName)
        {
        }

        /// <summary>
        /// Parameter processing action.
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                context.ActionArguments[this.paramName] = base
                    .ParseParamValue(context)
                    .Select(int.Parse)
                    .ToArray();
            }
            catch
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
