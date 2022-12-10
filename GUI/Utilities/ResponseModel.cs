using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace GUI.Utilities
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public dynamic Data { get; set; }
        public static ResponseModel Success(dynamic payload, string message, HttpStatusCode statusCode)
        {
            return new ResponseModel
            {
                StatusCode = (int)statusCode,
                Message = message,
                Status = ReasonPhrases.GetReasonPhrase((int)statusCode),
                Data = payload
            };
        }
        public static ResponseModel Success(string message, HttpStatusCode statusCode)
        {
            return new ResponseModel
            {
                StatusCode = (int)statusCode,
                Message = message,
                Status = ReasonPhrases.GetReasonPhrase((int)statusCode),
            };
        }
        public static ResponseModel NotFound(string message, HttpStatusCode statusCode)
        {
            return new ResponseModel
            {
                StatusCode = (int)statusCode,
                Message = message,
                Status = ReasonPhrases.GetReasonPhrase((int)statusCode)
            };
        }
        public static ResponseModel Failure(dynamic payload, string message, HttpStatusCode statusCode)
        {
            return new ResponseModel
            {
                StatusCode = (int)statusCode,
                Status = ReasonPhrases.GetReasonPhrase((int)statusCode),
                Message = message,
                Data = payload
            };
        }
        public static ResponseModel Failure(string message, HttpStatusCode statusCode)
        {
            return new ResponseModel
            {
                StatusCode = (int)statusCode,
                Status = ReasonPhrases.GetReasonPhrase((int)statusCode),
                Message = message
            };
        }
    }
}
