using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetFinder.Service
{
    public class HereApiService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string HereMapAPIUrl = @"http://image.maps.api.here.com/mia/1.6/mapview?app_id=qhCsia5d6HnA5Frnca3B&app_code=ICFZtQFrarakCvUj2FdFfg";

        public async Task<byte[]> GetMapAsync()
        {
            try
            {
                return await client.GetByteArrayAsync(HereMapAPIUrl);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
