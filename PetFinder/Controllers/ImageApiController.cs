using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetFinder.Core.Models;
using PetFinder.Service;

namespace PetFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageApiController : Controller
    {        
            HereApiService _apiServices = new HereApiService();


            // GET: api/<controller>
            [HttpGet]
            public async Task<IActionResult> GetAsync()
            {
                SeenDetail seen = new SeenDetail() { Map = await _apiServices.GetMapAsync() };
                return Ok(seen);
            }
        }
}