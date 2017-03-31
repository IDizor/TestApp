using System;

using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Attributes
{
    /// <summary>
    /// Base CSV array attribute.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public abstract class CsvArrayBaseAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The parameter-to-parse name.
        /// </summary>
        protected readonly string paramName;

        /// <summary>
        /// Gets or sets the CSV separator char.
        /// </summary>
        public char Separator { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvArrayBaseAttribute"/> class.
        /// </summary>
        /// <param name="paramName">The parameter-to-parse name.</param>
        public CsvArrayBaseAttribute(string paramName)
        {
            this.paramName = paramName;
            this.Separator = ',';
        }
        #endregion

        #region Abstract_Methods        
        /// <summary>
        /// Parameter processing action.
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public abstract override void OnActionExecuting(ActionExecutingContext context);
        #endregion

        #region Protected_Methods        
        /// <summary>
        /// Parses the query string parameter value.
        /// </summary>
        /// <param name="context">The action context.</param>
        /// <returns>The array of strings.</returns>
        protected string[] ParseParamValue(ActionExecutingContext context)
        {
            string[] result = Array.Empty<string>();

            if (context.ActionArguments.ContainsKey(this.paramName))
            {
                string parameters = string.Empty;

                if (context.RouteData.Values.ContainsKey(this.paramName))
                {
                    parameters = (string)context.RouteData.Values[this.paramName];
                }
                else
                {
                    parameters = context.HttpContext.Request.Query[this.paramName];
                }

                if (!string.IsNullOrEmpty(parameters))
                {
                    result = parameters.Split(this.Separator);
                }
            }

            return result;
        }
        #endregion
    }
}
