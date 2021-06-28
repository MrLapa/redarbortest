namespace Redarbor.Api.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T response)
        {
            Response = response;
        }

        public T Response { get; set; }
    }
}
