using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DocAssistantWebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocAssistantWebApi.Utils.Factories
{
    public abstract class TokenFactory
    {

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

        private static (byte[], string) CreateToken(long userId,string[] roles, string prefix = "")
        {
            var salt = SecurityUtils.GenerateRandomSalt(32);
            
            object info = new
            {
                UserId = userId,
                Roles = roles
            };
            // Add timestamp

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var infoBytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(info, settings));

            var infoStringSignature = SecurityUtils.HashStringHex(infoBytes, salt);
            
            return (salt, prefix + Convert.ToHexString(infoBytes) + "." + infoStringSignature);
        }

        private static (byte[], string) CreateDoctorAccessToken(long userId) => CreateToken(userId, new Roles[]
        {
            Roles.Assistant,
            Roles.Doctor
        }.GetFormattedRoles().ToArray());
        private static (byte[],string) CreateAssistantAccessToken(long userId) => CreateToken(userId, new Roles[]
        {
            Roles.Assistant
        }.GetFormattedRoles().ToArray(),"$");
    }
}