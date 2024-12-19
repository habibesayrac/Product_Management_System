namespace PMS.DataAccessLayer.ResponseModel
{
    public interface IBaseResponseModel
    {
        public int StatusCode { get; set; }

        //HttpStatusCode mi olmalı
        public bool Status { get; set; }
        public string Messages { get; set; }
    }
}