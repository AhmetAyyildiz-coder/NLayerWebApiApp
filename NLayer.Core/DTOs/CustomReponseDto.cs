using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomReponseDto<T>
    {
        public T Data { get; set; }

        public List<String> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode{ get; set; }

        //Factory design pattern  
        //static factor methods
        public static CustomReponseDto<T> Success(int statusCode, T data)
            => new CustomReponseDto<T> { Data = data, Errors = null, StatusCode = statusCode };

        public static CustomReponseDto<T> Success(int statusCode)
            => new CustomReponseDto<T> { StatusCode = statusCode };

        public static CustomReponseDto<T> Fail(int statusCode , List<String> errors)
            => new CustomReponseDto<T> {  Errors = errors, StatusCode = statusCode };

        public static CustomReponseDto<T> Fail(int statusCode,string error)
           => new CustomReponseDto<T> { Errors = new List<string>() { error}, StatusCode = statusCode };
    }
}
