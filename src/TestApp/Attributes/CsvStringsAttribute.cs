using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Attributes
{
    /// <summary>
    /// CSV strings array attribute.
    /// </summary>
    /// <seealso cref="TestApp.Attributes.CsvArrayBaseAttribute" />
    public class CsvStringsAttribute : CsvArrayBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvStringsAttribute"/> class.
        /// </summary>
        /// <param name="paramName">The parameter-to-parse name.</param>
        public CsvStringsAttribute(string paramName) : base(paramName)
        {
        }

        /// <summary>
        /// Parameter processing action.
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments[this.paramName] = base.ParseParamValue(context);
        }
    }
}
