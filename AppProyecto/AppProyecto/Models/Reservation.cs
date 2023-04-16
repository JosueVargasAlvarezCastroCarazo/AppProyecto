using AppProyecto.ModelsDTOs;
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
        public string ItemName { get; set; }


        //crea una reserva nueva
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


        //actualiza la reserva
        public async Task<bool> Update()
        {
            try
            {
                string RouteSufix = string.Format("Reservations/{0}", ReservationId);
                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

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

        //elimina una reserva completamente
        public async Task<bool> Delete()
        {
            try
            {
                string RouteSufix = string.Format("Reservations/{0}", ReservationId);
                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Delete);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                string Serialize = JsonConvert.SerializeObject(this);

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

        //trae una lista de reservas segun fechas de disponibilidad
        public async Task<List<Reservation>> GetList(int UserId,DateTime Start, DateTime End)
        {
            try
            {
                string RouteSufix = "";

                RouteSufix = string.Format("Reservations/Search?UserId={0}&Start={1}&End={2}", UserId, Start.ToString("yyyyMMddHHmmss"), End.ToString("yyyyMMddHHmmss"));
                
                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK ||
                    statusCode == HttpStatusCode.Created ||
                    statusCode == HttpStatusCode.NoContent
                    )
                {

                    return JsonConvert.DeserializeObject<List<Reservation>>(response.Content);
                }
                else
                {
                    return new List<Reservation>();
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }


        //busca una lista de reservas segun un item
        public async Task<List<Reservation>> SearchByItemId(int ItemId)
        {
            try
            {
                string RouteSufix = "";

                RouteSufix = string.Format("Reservations/SearchByItemId?ItemId={0}", ItemId);

                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK ||
                    statusCode == HttpStatusCode.Created ||
                    statusCode == HttpStatusCode.NoContent
                    )
                {

                    return JsonConvert.DeserializeObject<List<Reservation>>(response.Content);
                }
                else
                {
                    return new List<Reservation>();
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }


        //top lista de reservas

        public async Task<List<ReservationCount>> TopItems()
        {
            try
            {
                string RouteSufix = "";

                RouteSufix = string.Format("Reservations/TopItems");

                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK ||
                    statusCode == HttpStatusCode.Created ||
                    statusCode == HttpStatusCode.NoContent
                    )
                {

                    return JsonConvert.DeserializeObject<List<ReservationCount>>(response.Content);
                }
                else
                {
                    return new List<ReservationCount>();
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
