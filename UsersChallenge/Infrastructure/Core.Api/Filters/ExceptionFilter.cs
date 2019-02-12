using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Exceptions;
using Core.Common.Presentation;
using Core.Logger.Enums;
using Core.Logger.Interfaces;
using Core.Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;

namespace Core.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExceptionFilter(ILogger logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Se encarga de capturar y manejar las excepciones no controladas de la aplicación.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            var inner = actionExecutedContext.Exception.InnerException;
            var exceptionMessage = (inner == null) ? actionExecutedContext.Exception.Message : inner.Message;
            var stack = actionExecutedContext.Exception.StackTrace;

            if (!actionExecutedContext.Exception.Data.Contains("App"))
            {
                actionExecutedContext.Exception.Data.Add("App", "DemoDMA");
                actionExecutedContext.Exception.Data.Add("API", _hostingEnvironment.ApplicationName);
            }
            if (!actionExecutedContext.Exception.Data.Contains("Environment"))
            {
                actionExecutedContext.Exception.Data.Add("Environment", _hostingEnvironment.EnvironmentName);
            }

            if (!_hostingEnvironment.IsDevelopment())
            {
                // Logs the exception
                LogFile logFile = new LogFile(_hostingEnvironment.ApplicationName + "Exceptions", DateTime.Now.ToString("dd-MM-yyyy"));
                _logger.WriteMessageAsync(logFile, new LogMessage<Exception>(actionExecutedContext.Exception, LogLevel.EXCEPTION));
            }

            bool isCustomException = actionExecutedContext.Exception is ICustomException;
            var allErrors = new List<ErrorResult>();

            if (isCustomException)
            {
                var customException = (ICustomException)actionExecutedContext.Exception;
                allErrors = customException.GetErrors().ToList();
            }
            else
            {
                allErrors.Add(new ErrorResult
                {
                    Error = exceptionMessage,
                    Description = stack
                });
            }

            int statusCode = isCustomException
                ? (int)HttpStatusCode.BadRequest
                : (int)HttpStatusCode.InternalServerError;

            var respuesta = new Result(statusCode, allErrors);
            actionExecutedContext.HttpContext.Response.StatusCode = statusCode;

            actionExecutedContext.Result = new JsonResult(respuesta);
            base.OnException(actionExecutedContext);

            Task.FromResult<object>(null);
        }
    }
}
