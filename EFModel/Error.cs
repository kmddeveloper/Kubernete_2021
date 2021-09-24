using System;
using System.Collections.Generic;
using System.Text;

namespace EFModel
{    
    public class Error
    {
        public string Type { get; set; }     
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Error(Exception ex)
        {
            Type = ex?.GetType().Name;            
            Message = ex?.Message;
            StackTrace = ex?.StackTrace.ToString();
        }
    }
}
