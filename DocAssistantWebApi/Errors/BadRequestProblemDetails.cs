using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocAssistantWebApi.Errors
{
    public class BadRequestProblemDetails
    {
        [JsonProperty("status")]
        public int StatusCode { get; set; }
        
        public string Title { get; set; }
        
        [JsonPropertyName("errors")]
        public IDictionary<string, string[]> Errors { get; }
        
        public BadRequestProblemDetails(IDictionary<string, string[]> errors)
        {
            this.Errors = errors;
        }
    }
}