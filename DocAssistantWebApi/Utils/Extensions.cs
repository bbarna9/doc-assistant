using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocAssistantWebApi.Filters;
using Microsoft.OpenApi.Extensions;

namespace DocAssistantWebApi.Utils
{
    public static class Extensions
    {
        public static IEnumerable<string> GetFormattedRoles(this Roles[] roles) => roles.Select(role => role.GetDisplayName().ToLower());
    }
}