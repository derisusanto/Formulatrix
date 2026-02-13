namespace Ecommerce.Common.ServiceResult;
public class ServiceResult<T>
{
    public bool Success {get; set;} = true;
    public string Message {get; set;} = string.Empty;
    public T? Data {get; set;}

    public static ServiceResult<T> SuccessResult(T data, string message = "Success")
    {
        return new ServiceResult<T>
        {
            Success = true,
            Message = message,
            Data = data
        };

    }

    public static ServiceResult<T> ErrorResult( string? message = null)
    {
        return new ServiceResult<T>
        {
            Success = false,
            Message = message ?? "Error",
        };
        
    }
}