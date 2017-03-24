using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestApp.Extensions
{
    /// <summary>
    /// JSON respone object with 'data' and 'error' fields.
    /// </summary>
    public class JsonResponse
    {
        public object Data { get; set; }
        public object Error { get; set; }
    }

    /// <summary>
    /// Extends MVC controller class with JsonSuccess and JsonError methods.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Prepares JSON response object for success case.
        /// </summary>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="data">The data to return.</param>
        /// <param name="camelCase">If set to <c>true</c> convert fields names to camel case.</param>
        /// <param name="ignoreReferenceLoop">Tf set to <c>true</c> ignore reference loop.</param>
        /// <returns>The JSON response object with no error.</returns>
        public static JsonResult JsonSuccess(this Controller controller, object data = null,
            bool camelCase = true, bool ignoreReferenceLoop = false)
        {
            var serializerSettings = new JsonSerializerSettings();

            if (camelCase)
            {
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (ignoreReferenceLoop)
            {
                serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            return controller.Json(
                new JsonResponse
                {
                    Data = data == null
                        ? new { Success = true }
                        : data,
                    Error = null
                },
                serializerSettings);
        }

        /// <summary>
        /// Prepares JSON response object for exception case.
        /// </summary>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="camelCase">If set to <c>true</c> convert fields names to camel case.</param>
        /// <returns>The JSON response object with error field populated.</returns>
        public static JsonResult JsonError(this Controller controller, string errorMessage, bool camelCase = true)
        {
            var serializerSettings = new JsonSerializerSettings();

            if (camelCase)
            {
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return controller.Json(new JsonResponse
            {
                Data = null,
                Error = new
                {
                    Code = 0,
                    Message = errorMessage
                }
            },
            serializerSettings);
        }
    }
}
