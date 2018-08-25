using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace GarminExport.Auth
{
    public class AuthService
    {
        public Session Session { get; private set; }
        private RestClient SSOClient { get; set; }
        private RestClient ConnectClient { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

        public AuthService(string userName, string password)
        {
            Session = new Session();
            SSOClient = new RestClient("https://sso.garmin.com")
            {
                CookieContainer = Session.Cookies,
                FollowRedirects = true
            };
            ConnectClient = new RestClient("http://connect.garmin.com/")
            {
                CookieContainer = Session.Cookies,
                FollowRedirects = true
            };
            UserName = userName;
            Password = password;
        }

        public bool SignIn()
        {
   
            try
            {
                var signInResponse = PostLogin(null);
                var ticketUrl = ParseServiceTicketUrl(signInResponse);
                return ProcessTicket(ticketUrl);
            }
            catch
            {
                return false;
            }
        }

        private bool ProcessTicket(string ticketUrl)
        {
            //http:\/\/connect.garmin.com\/post-auth\/login?ticket=ST-xyz
            var ticketUrlFormatted = ticketUrl.Replace(@"\/", @"/");
            var ticketId = ticketUrlFormatted.Substring(ticketUrlFormatted.LastIndexOf("ticket=") + 7);
            var uri = new Uri("https://connect.garmin.com/modern/?ticket=" + ticketId);
            var client = new RestClient(uri.Scheme + "://" + uri.Host)
            {
                CookieContainer = Session.Cookies
            };
            var loginRequest = new RestRequest(uri.PathAndQuery, Method.GET);
            var response = client.Execute(loginRequest);
            return IsDashboardUri(response.ResponseUri);
        }

        private static bool IsDashboardUri(Uri uri)
        {
            return uri.Host == "connect.garmin.com"
                && uri.LocalPath == "/modern/";
        }


        private string PostLogin(string flowExecutionKey)
        {
            var request = BuildAuthRequest(Method.POST);
            NameValueCollection formData = new NameValueCollection();
            formData.Add("username", UserName);
            formData.Add("password", Password);
            formData.Add("embed", "true");
            formData.Add("_eventId", "submit");

            string formDataStr="";
            foreach (var key in formData.AllKeys)
            {
                formDataStr += key + "=" + formData[key] + "&";
            }
            var formDataBytes = Encoding.UTF8.GetBytes(formDataStr);
            request.AddParameter("application/x-www-form-urlencoded", formDataStr, ParameterType.RequestBody);
            IRestResponse response = SSOClient.Execute(request);
            return response.Content;
        }

        private RestRequest BuildAuthRequest(Method method)
        {
            var authRequest = new RestRequest("sso/login", method);
            authRequest.AddQueryParameter("service", "http://connect.garmin.com/post-auth/login");
            authRequest.AddQueryParameter("clientId", "demo");
            return authRequest;
        }

        private string ParseServiceTicketUrl(string content)
        {
            // var response_url = "http://connect.garmin.com/post-auth/login?ticket=ST-XXXXXX-XXXXXXXXXXXXXXXXXXXX-cas";
            var regex = new Regex("response_url\\s*=\\s*\"(?<url>[^\"]*)\"");
            var match = regex.Match(content);
            if (!match.Success)
                throw new Exception("Servcie ticket URL not found.");

            return match.Groups["url"].Value;
        }
    }
}
