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

        private void SetContextUserData(long id, IEnumerable<Roles> roles)
        {
            var claims = new List<Claim>();

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role,role.GetDisplayName()));
            
            Context.User.AddIdentity(new ClaimsIdentity(claims));
            Context.Items.Add("Id",id);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.NoResult();
            // TEST
            var claims = new List<Claim>();

            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault();

                if (token[0] == '$')
                {
                    // Assistant
                    var (valid, assistantId, roles) = _assistantAuthService.VerifyAccessToken(token.Substring(1));

                    if (!valid) throw new Exception();

                    // TEST
                    foreach (var role in roles)
                        claims.Add(new Claim(ClaimTypes.Role, role.GetDisplayName()));

                    SetContextUserData(assistantId, roles);
                }
                else
                {
                    // Doctor
                    var (valid, doctorId, roles) = _doctorAuthService.VerifyAccessToken(token);

                    if (!valid) throw new Exception();

                    // TEST
                    foreach (var role in roles)
                        claims.Add(new Claim(ClaimTypes.Role, role.GetDisplayName()));

                    SetContextUserData(doctorId, roles);
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid token");
            }
            
            
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}