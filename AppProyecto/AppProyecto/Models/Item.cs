using AppProyecto.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AppProyecto.Models
{
    public class Item
    {

        public Item()
        {

        }

        public RestRequest Request { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }


        public async Task<bool> Create()
        {
            try
            {
                string RouteSufix = string.Format("items");
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

        public async Task<Item> GetItemById(int id)
        {
            try
            {
                string RouteSufix = string.Format("items/{0}", id);
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
                    //JsonConvert.DeserializeObject<List<User>>(response.Content)[0];
                    return JsonConvert.DeserializeObject<Item>(response.Content);
                }
                else
                {
                    return new Item(); ;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }

        public async Task<bool> Update()
        {
            try
            {
                string RouteSufix = string.Format("items/{0}", ItemId);
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


        public async Task<List<Item>> GetList(bool Active,string search)
        {
            try
            {
                string RouteSufix = "";

                if (string.IsNullOrEmpty(search))
                {
                    RouteSufix = string.Format("items?active={0}", Active);
                }
                else
                {
                    RouteSufix = string.Format("items/Search?active={0}&search={1}", Active, search);
                }

               
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
                    
                    return JsonConvert.DeserializeObject<List<Item>>(response.Content);
                }
                else
                {
                    return new List<Item>();
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