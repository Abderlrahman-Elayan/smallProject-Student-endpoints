using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestRestAPI.Models.DTOs
{
    public class ApiResponse<TData>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TData? Data { get; set; }
        public object? Errors { get; set; }
        public DateTime? TimeStamp { get; set; }

        public static ApiResponse<TData> Response(bool success, int statusCode, string message, TData? Data = default, object? errors = null)
        {
            return new ApiResponse<TData>()
            {
                Success = success,
                StatusCode = statusCode,
                Message = message,
                Data = Data,
                Errors = errors,
                TimeStamp = DateTime.UtcNow
            };
        }



        public static ApiResponse<TData> Ok(TData data, string message)
             => Response(true, 200, message, data);

        public static ApiResponse<TData> CreatedAt(TData data, string message)
             => Response(true, 201, message, data);

        public static ApiResponse<TData> NoContent(string message = "Operation Completed Successfully")
             => Response(true, 204, message);


        public static ApiResponse<TData> NotFound(string message = "Resourse Not Found")
            => Response(false, 404, message);



        public static ApiResponse<TData> BadRequest(string message, object? error = null)
            => Response(false, 400, message, errors: error);


        public static ApiResponse<TData> Conflict(string message)
            => Response(false, 409, message);

        public static ApiResponse<TData> Error(int statusCode,string message,object? error = null)
           => Response(false, statusCode, message,errors:error);


    }
}
