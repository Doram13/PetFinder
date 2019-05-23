using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetFinder.Core.Models;
using PetFinder.Service;

namespace PetFinder.Controllers
{
    [ApiController]
    public class ImageApiController : Controller
    {        
            HereApiService _apiServices = new HereApiService();


            // GET: api/<controller>
            [Route("Posts/Details/api/ImageApi")]
            [HttpGet]
            public async Task<IActionResult> GetAsync()
            {
                SeenDetail seen = new SeenDetail() { Map = await _apiServices.GetMapAsync() };
                return Ok(seen);
            }
        }
}