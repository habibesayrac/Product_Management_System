namespace PMS.DataAccessLayer.ResponseModel
{
    public class BaseResponseModel : IBaseResponseModel
    {
        public BaseResponseModel(int statusCode, bool status, string message)
        {
            StatusCode = statusCode;
            Status = status;
            Messages = message;
        }
        public BaseResponseModel()
        {

        }

        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Messages { get; set; }
 
    }
}