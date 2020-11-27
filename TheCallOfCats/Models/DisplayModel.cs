using RestSharp;

namespace TheCallOfCats.Models
{
    public class DisplayModel
    {
        public IRestResponse Response { get; set; }

        public string Url { get; set; }
    }
}