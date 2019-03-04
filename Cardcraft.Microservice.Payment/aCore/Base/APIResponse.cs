namespace Cardcraft.Microservice.aCore
{
    public interface IAPIResponse
    {
        string Message { get; }
        string ApiStatus { get; }
        bool Success { get; }
    }

    public class APIResponse : IAPIResponse
    {
        public string Message { get; private set; }
        public string ApiStatus { get; private set; }
        public bool Success { get; private set; }

        public APIResponse(bool success, string apiStatus, string message)
        {
            ApiStatus = apiStatus;
            Message = message;
            Success = success;
        }
    }

    public class APIResponse<T> : APIResponse
    {
        public T Data { get; private set; }

        public APIResponse(bool success, string apiStatus, string message, T data)
            : base(success, apiStatus, message)
        {
            Data = data;
        }
    }
}
