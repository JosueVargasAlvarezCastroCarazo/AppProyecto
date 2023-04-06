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
    public class Reservation
    {


        public Reservation()
        {
            
        }

        public RestRequest Request { get; set; }

        public int ReservationId { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }



        public async Task<bool> Create()
        {
            try
            {
                string RouteSufix = string.Format("Reservations");
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
