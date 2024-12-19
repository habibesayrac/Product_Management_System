using System.Net;

namespace PMS.DataAccessLayer.ResponseModel
{
    public class ErrorResponseModel<T> : BaseResponseModel
    {
        public ErrorResponseModel(int statusCode, bool status, string message) : base(statusCode, status, message)
        {

        }
        public ErrorResponseModel(int statusCode, bool status) : base(statusCode, status, "Error.")
        {

        }
        public ErrorResponseModel(int statusCode) : base(statusCode, false, "Error.")
        {

        }
        public ErrorResponseModel() : base((int)HttpStatusCode.BadRequest, false, "Error.")
        {

        }
        public ErrorResponseModel(string errorMessage) : base((int)HttpStatusCode.BadRequest, false, "Error.")
        {
            this.ErrorMessage = errorMessage;
        }
        public ErrorResponseModel(string errorMessage, int statusCode, bool status, string message) : base(statusCode, status, message)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}