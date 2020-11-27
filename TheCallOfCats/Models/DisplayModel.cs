using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace TheCallOfCats.Models
{
    public class DisplayModel
    {
        public IRestResponse Response { get; set; }

        public string Url { get; set; }

        public string Content
        {
            get
            {
                try
                {
                    return JObject.Parse(Response.Content).ToString(Formatting.Indented);
                }
                catch (JsonReaderException e)
                {
                    return Response.Content;
                }
            }
        }
    }
}