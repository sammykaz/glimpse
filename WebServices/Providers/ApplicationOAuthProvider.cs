using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebServices.Models;
using Glimpse.Core.Contracts.Services;
using Plugin.RestClient;
using Glimpse.Core.Services.General;

namespace WebServices.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        private Vendor currentVendor;
        RestClient<Vendor> restClient = new RestClient<Vendor>();

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            currentVendor = await restClient.GetByKeyword(context.UserName, true);

            //context.SetError("invalid_grant", currentVendor.Email + "pass: " + context.Password + "encrypted pass: " + currentVendor.Password);
            //return;
            //string password = context.Password;
            //string salt = currentVendor.Salt;
            //string encryptedPassword = Cryptography.EncryptAes(password, salt);

            if (currentVendor!=null){
                if (context.Password == currentVendor.Password) {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    identity.AddClaim(new Claim("username", "user"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, ""+ currentVendor.VendorId));
                    identity.AddClaim(new Claim(ClaimTypes.Sid, ""+ currentVendor.VendorId));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}