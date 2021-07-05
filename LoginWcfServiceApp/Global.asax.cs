using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace LoginWcfServiceApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*Сюда нужно добавить ссылку на веб-приложение, чтобы CORS не мешал*/
            var allowedOrigins = new[] { "http://foo.example", "http://bar.example", "https://localhost:44354", "https://localhost:44397" };
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            var origin = request.Headers["Origin"];

            if (origin != null && allowedOrigins.Any(x => x == origin))
            {
                response.AddHeader("Access-Control-Allow-Origin", origin);
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type, X-Requested-With");
                response.AddHeader("Access-Control-Allow-Credentials", "true");
                if (request.HttpMethod == "OPTIONS")
                {
                    response.End();
                }
            }
        }
            protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}