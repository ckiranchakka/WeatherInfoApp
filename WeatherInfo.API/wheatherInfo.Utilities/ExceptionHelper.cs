using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace wheatherInfo.Utilities
{
    public class ExceptionHelper : ControllerBase
    {
        public IActionResult GetResponseStatus(Exception ex)
        {
            string exceptionType = ex.GetType().Name;
            switch (exceptionType)
            {
                //ArgumentNullException
                case ExceptionTypes.ArgumentNullException:
                    return IdentifyReturnCode(400, new Exception(Messages.ErrArugNullEx));
                //AggregateException
                case ExceptionTypes.AggregateException:
                    return IdentifyReturnCode(400, new Exception(Messages.ErrAggregateException));
                //IndexOutOfRangeException
                case ExceptionTypes.IndexOutOfRangeException:
                    return IdentifyReturnCode(500, new Exception(Messages.DefaultMessage));

                //FormatException
                case ExceptionTypes.FormatException:
                    return IdentifyReturnCode(500, new Exception(Messages.DefaultMessage));

              
                //Default Bad Reqest
                default:
                    return IdentifyReturnCode(400, ex);
            }
        }

     
        private IActionResult IdentifyReturnCode(int errorCode, Exception ex)
        {
            if (errorCode == 500)
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);

            else
                return BadRequest(ex);
        }
    }

    internal class ExceptionTypes
    {
        internal const string ArgumentException = "ArgumentException";
        internal const string ArgumentNullException = "ArgumentNullException";
        internal const string AggregateException = "AggregateException";
        internal const string ArgumentOutOfRangeException = "ArgumentOutOfRangeException";
        internal const string IndexOutOfRangeException = "IndexOutOfRangeException";
        internal const string FormatException = "FormatException";
        internal const string SqlException = "SqlException";
    }

    internal class Messages
    {
        internal static string DefaultMessage = "An unhandled Error Occured. Please contact System Admin.";       
        #region C# Error Messages
        internal static string ErrArugNullEx = "One or more required values are passed as null";
        internal static string ErrAggregateException = "One or more required values are invalid";
        #endregion
    }
}
