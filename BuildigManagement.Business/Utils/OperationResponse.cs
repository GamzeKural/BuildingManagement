using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildigManagement.Business.Utils
{
    public class OperationResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Resource { get; set; }
        //public Exception Ex { get; set; }


        public static OperationResponse<T> CreateSuccesResponse(T result)
        {
            return new OperationResponse<T>
            {
                Success = true,
                Resource = result
            };
        }

        public static OperationResponse<T> CreateSuccesResponse(string message)
        {
            return new OperationResponse<T>
            {
                Success = true,
                Message = message,
                Resource = default
            };
        }

        public static OperationResponse<T> CreateFailure(string message)
        {
            return new OperationResponse<T>
            {
                Success = false,
                Message = message,
                //Ex = ex
            };
        }
    }
}
