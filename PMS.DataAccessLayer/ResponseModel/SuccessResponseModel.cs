using System.Net;

namespace PMS.DataAccessLayer.ResponseModel
{
    public class SuccessResponseModel<T> : BaseResponseModel
    {
        public SuccessResponseModel(int statusCode, bool status, string message) : base(statusCode, status, message)
        {

        }
        public SuccessResponseModel(int statusCode, bool status) : base(statusCode, status, "Success.")
        {

        }
        public SuccessResponseModel(int statusCode) : base(statusCode, true, "Success.")
        {

        }
        public SuccessResponseModel() : base((int)HttpStatusCode.OK, true, "Success.")
        {

        }
        public SuccessResponseModel(T data) : base((int)HttpStatusCode.OK, true, "Success.")
        {
            this.Data = data;
        }

        public SuccessResponseModel(T data, int statusCode) : base(statusCode, true, "Success.")
        {
            this.Data = data;
        }

        public T Data { get; set; }
    }
}