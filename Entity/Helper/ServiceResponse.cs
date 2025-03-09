namespace Entity.Helper;

public class ServiceResponse<T>
{
    public bool Success { get; set;}
    public string Message { get; set;}
    public T Data { get; set;}

    public ServiceResponse(){
        Success = true;
    }
}
