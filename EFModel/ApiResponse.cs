using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{

    public class ApiResponse<T>
    {
       
        public int StatusCode { get; set; }
        public Error ApiError { get; set; }       
        public T Result { get; set; }
    }
}
