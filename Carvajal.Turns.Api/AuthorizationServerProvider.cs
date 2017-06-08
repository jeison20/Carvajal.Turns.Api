using Component;
using Data;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Carvajal.Turns.Api
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            Users User = CUsers.Instance.SearchUser(context.UserName, context.Password);
            if (User != null)
            {
                if (User.Status)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, User.Name));
                    identity.AddClaim(new Claim("Rol", User.FkRole_Identifier));
                    identity.AddClaim(new Claim("Status", User.Status.ToString()));
                    identity.AddClaim(new Claim("User", User.PkIdentifier));
                    identity.AddClaim(new Claim("Email", User.Email));
                    identity.AddClaim(new Claim("Country", User.FkCountries_Identifier.ToString()));
                    identity.AddClaim(new Claim("Company", User.FkCompanies_Identifier));
                    context.Validated(identity);
                }
                else
                {
                    string Error = new Utils.Security.Utils().GetResourceMessages("M27");
                    context.SetError("Invalid _grant", Error);
                }
            }
            else
            {
                string Error = new Utils.Security.Utils().GetResourceMessages("M7");
                context.SetError("Invalid _grant", Error);
            }
        }

        public override async Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var accessToken = context.AccessToken;

            string User = string.Empty;

            var identity = context.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var item in claims)
            {
                if (item.Type.Equals("User"))
                    User = item.Value;
            }

            CClient.Instance.InactiveTokenVigentes(User);
            Client ObjectClient = new Client();
            ObjectClient.Token = accessToken;
            ObjectClient.Active = true;
            ObjectClient.User = User;
            ObjectClient.RefreshTokenLifeTime = DateTime.Now.AddMinutes(20);
            ObjectClient.AllowedOrigin = CClient.Instance.Encode(ObjectClient.Token);
            CClient.Instance.SaveClient(ObjectClient);
        }
    }
}