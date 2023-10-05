using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace LogsAllianceApi.Authorization
{
    public class AuthorizationToken : IAuthorizationRequirement
    {
        public IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorizationToken(IConfiguration config)
        {
            Configuration = config;
        }
    }
    public class LagAuthorization :  AuthorizationHandler<AuthorizationToken>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       AuthorizationToken requirement)
        {
            //var httpMethod = _httpContextAccessor.HttpContext.Request;


            string Baseurl = requirement.Configuration["URL:Identity"];
            string PrefijoUrl = requirement.Configuration["URL:IdentityPrefix"];
            try
            {
                var httpContext = context.Resource as HttpContext;
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (!string.IsNullOrEmpty(token))
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    using (HttpResponseMessage Res = await client.GetAsync(PrefijoUrl + "Authorization"))
                    {
                        //Checking the response is successful or not which is sent using HttpClient  
                        if (Res.IsSuccessStatusCode)
                        {
                            //Luis De Andrade. 04/01/2018. Obtenemos los datos del token.
                            var handler = new JwtSecurityTokenHandler();
                            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                            //Obtenemos el idUsuario del token

                            string idUsuario = tokenS.Claims.Where(x => x.Type == "idUsuario").Select(x => x.Value).FirstOrDefault();
                            //Si no hay usuario no completamos la autenticacion
                            if (!string.IsNullOrEmpty(idUsuario))
                            {
                                context.Succeed(requirement);
                                await Task.CompletedTask;
                            }
                        }
                    }
                }

                await Task.CompletedTask;
            }
            catch
            {
                await Task.CompletedTask;
            }
        }
    }
}
