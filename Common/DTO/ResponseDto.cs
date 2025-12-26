namespace Common.DTO;

public class ResponseDto
{
    public bool Success { get; set; }
    public ErrorCode? ErrorCode { get; set; }
    public string Message { get; set; }
    public object Data { get; set; } // дополнительные данные при успехе
}
