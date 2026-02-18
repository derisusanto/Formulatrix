namespace Ecommerce.Common.ServiceResult;
public class ServiceResult<T>
{
    public bool Success {get; set;} = true;
    public string Message {get; set;} = string.Empty;
    public T? Data {get; set;}
    public int Status {get; set;}

    public static ServiceResult<T> SuccessResult(
    T data, 
    string message = "Success",
    int status = 200
    )
    {
        return new ServiceResult<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Status = status

        };

    }

    public static ServiceResult<T> ErrorResult( string? message = null,int status = 400)
    {
        return new ServiceResult<T>
        {
            Success = false,
            Message = message ?? "Error",
            Status = status
        };
        
    }
}