using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using DocAssistantWebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocAssistantWebApi.Utils.Factories
{
    public abstract class TokenFactory
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
            {NullValueHandling = NullValueHandling.Ignore};

        private static readonly TimeSpan TokenLife = new TimeSpan(7,0,0,0);
        
        public class AccessTokenPayload
        {
            [JsonProperty("userId")]
            public long UserId { get; set; }
            [JsonProperty("roles")]
            public string[] Roles { get; set; }
            [JsonProperty("iat")] 
            public DateTime Issued { get; set; }
            [JsonProperty("exp")]
            public DateTime Expires { get; set; }
        }
        
        public static (byte[],string) CreateAccessToken(Roles tokenType, long userId)
        {
            switch (tokenType)
            {
                case Roles.Doctor:
                    return CreateDoctorAccessToken(userId);
                case Roles.Assistant:
                    return CreateAssistantAccessToken(userId);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private static AccessTokenPayload GetAccessTokenPayload(long userId,Roles [] roles)
        {
            var time = DateTime.Now;
            
            return new AccessTokenPayload()
            {
                UserId = userId,
                Roles = roles.GetFormattedRoles().ToArray(),
                Issued = time,
                Expires = time.Add(TokenLife)
            };
        }
        
        private static (byte[], string) CreateToken(AccessTokenPayload payload, string prefix = "")
        {
            var salt = SecurityUtils.GenerateRandomSalt(32);

            var infoBytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(payload,_serializerSettings));

            var infoStringSignature = SecurityUtils.HashStringHex(infoBytes, salt);
            
            return (salt, prefix + Convert.ToHexString(infoBytes) + "." + infoStringSignature);
        }

        private static (byte[], string) CreateDoctorAccessToken(long userId) => CreateToken(GetAccessTokenPayload(userId,
            new Roles[]{Roles.Assistant, Roles.Doctor}));
        private static (byte[],string) CreateAssistantAccessToken(long userId) => CreateToken(GetAccessTokenPayload(userId,
            new Roles[]{Roles.Assistant}),"$");
    }
}