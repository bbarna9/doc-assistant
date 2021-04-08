using System;
using Newtonsoft.Json;

namespace DocAssistantWebApi.Errors
{
    public class GenericRequestException : Exception
    {
        [JsonProperty("status")] public int StatusCode { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("error")] public string Error { get; set; }
        
        public GenericRequestException() {}

        public GenericRequestException(int statusCode, string title, string error)
        {
            this.StatusCode = statusCode;
            this.Title = title;
            this.Error = error;
        }
    }
}