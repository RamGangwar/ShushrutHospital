using System;
using System.Collections.Generic;
using System.Text;

namespace ShushrutEyeHospitalCRM.Models.Wrapper
{
    public class ResponseClass<T>
    {
        public ResponseClass()
        {
        }
        public ResponseClass(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public ResponseClass(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
