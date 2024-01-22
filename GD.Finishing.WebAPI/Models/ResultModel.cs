namespace GD.Finishing.WebAPI.Models
{
    public class ResultModel<T>
    {

        public ResultModel() => Message = string.Empty;

        public ResultModel(T data) => Data = data;

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }
        public T? Data { get; set; }
        public int ResultType { get; set; }

    }
}
