using System.Collections.Generic;
using System.Web.Http;
using fisheffect_api.Dao;
using fisheffect_api.Models;

namespace fisheffect_api.Controllers
{
    public class FishForSaleController : ApiController
    {
        // GET api/FishForSale
        public IEnumerable<FishForSale> GetFishForSales(int page = 0)
        {
            return new FishForSaleDao().GetFishFishForSales(page);
        }
    }
}