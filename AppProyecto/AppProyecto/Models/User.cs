using AppProyecto.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppProyecto.Models
{
    public class User
    {
        public User()
        {
           
        }

        public RestRequest Request { get; set; }
        

        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string LoginPassword { get; set; }
        public bool? IsAdmin { get; set; }
        public string Identification { get; set; }
        public bool? Active { get; set; }

        public async Task<User> ValidateLogin()
        {
            try
            {
                string RouteSufix = string.Format("users/LoginUser?identification={0}&password={1}", Identification, LoginPassword);
                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL,Method.Get);

                Request.AddHeader(APIConnection.ApiKeyName,APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK ||
                    statusCode == HttpStatusCode.Created ||
                    statusCode == HttpStatusCode.NoContent
                    )
                {
                    //JsonConvert.DeserializeObject<List<User>>(response.Content)[0];
                    return JsonConvert.DeserializeObject<User>(response.Content);
                }
                else
                {
                    return new User(); ;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }

        public async Task<bool> CreateAccount()
        {
            try
            {
                string RouteSufix = string.Format("users");
                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                string Serialize = JsonConvert.SerializeObject(this);

                Request.AddBody(Serialize, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK || 
                    statusCode == HttpStatusCode.Created || 
                    statusCode == HttpStatusCode.NoContent
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }

    }
}
