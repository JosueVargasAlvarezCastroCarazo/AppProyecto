using RestSharp;
using System;

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
    }
}