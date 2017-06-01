using Carvajal.Turns.Utils.Security;
using Component;
using Data;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AuthorizationServer3
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
                    string Error = new Utils().GetResourceMessages("M27");
                    context.SetError("Invalid _grant", Error);
                    return;
                }

            }
            else
            {
                string Error = new Utils().GetResourceMessages("M7");
                context.SetError("Invalid _grant", Error);
                return;
            }
        }

        public override async Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {

            var accessToken = context.AccessToken;

            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;

            string name = context.Identity.Name;
            string Rol = string.Empty;
            string Status = string.Empty;
            string User = string.Empty;
            string Email = string.Empty;
            string Country = string.Empty;
            string Company = string.Empty;

            var identity = (ClaimsIdentity)context.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var item in claims)
            {
                switch (item.Type)
                {
                    case "Rol":
                        Rol = item.Value;
                        break;
                    case "Status":
                        Status = item.Value;
                        break;
                    case "User":
                        User = item.Value;
                        break;
                    case "Email":
                        Email = item.Value;
                        break;
                    case "Country":
                        Country = item.Value;
                        break;
                    case "Company":
                        Company = item.Value;
                        break;
                    default:
                        break;
                }
            }

            Client ObjectClient = new Client();
            ObjectClient.Token = accessToken;
            ObjectClient.Active = true;
            ObjectClient.User = User;
            ObjectClient.RefreshTokenLifeTime = DateTime.Now.AddMinutes(20);
            ObjectClient.AllowedOrigin = CToken.Instance.Encode(ObjectClient.Token);
            CClient.Instance.SaveClient(ObjectClient);
        }

    }
}