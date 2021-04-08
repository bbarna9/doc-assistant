using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;

namespace DocAssistantWebApi.Services.Auth.AuthHandler
{
    public class AuthTokenHandler : AuthenticationHandler<AuthTokenSchemeOptions>
    {
        public  IAuthService<Doctor> _doctorAuthService { get; set; }
        public  IAuthService<Assistant> _assistantAuthService { get; set; }
        
        public AuthTokenHandler(IAuthService<Doctor> doctorAuthService,IAuthService<Assistant> assistantAuthService,IOptionsMonitor<AuthTokenSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this._doctorAuthService = doctorAuthService;
            this._assistantAuthService = assistantAuthService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.NoResult();

            var claims = new List<Claim>();
            long id;
            Roles userRole = default;

            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault();

                if (token[0] == '$') // if the token starts with $ => assistant
                {
                    // Assistant
                    var (valid, assistantId, roles) = _assistantAuthService.VerifyAccessToken(token.Substring(1));

                    if (!valid) throw new Exception();
                    
                    foreach (var role in roles)
                        claims.Add(new Claim(ClaimTypes.Role, role.GetDisplayName()));

                    id = assistantId;
                    userRole = Roles.Assistant;
                }
                else
                {
                    // Doctor
                    var (valid, doctorId, roles) = _doctorAuthService.VerifyAccessToken(token);

                    if (!valid) throw new Exception();
                    
                    foreach (var role in roles)
                        claims.Add(new Claim(ClaimTypes.Role, role.GetDisplayName()));

                    id = doctorId;
                    userRole = Roles.Doctor;
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid token");
            }
            
            
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            Context.Items.Add("Id",id);
            Context.Items.Add("AccountType",userRole);
            
            return AuthenticateResult.Success(ticket);
        }
    }
}