using Microsoft.AspNetCore.Mvc;

namespace ParkIstra.Libraries.ASP
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
        public ObjectResult? ObjectResult { get; set; }
        public T? Data { get; set; }
    }
}
