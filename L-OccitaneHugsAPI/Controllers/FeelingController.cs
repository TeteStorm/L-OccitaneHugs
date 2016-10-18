using L_OccitaneHugsData;
using L_OccitaneHugsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace L_OccitaneHugsAPI.Controllers
{
    /// <summary>
    /// Feeling controller 
    /// </summary>
    [RoutePrefix("api/feeling")]
    public class FeelingController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Feeling> feelingRepository;


        public FeelingController()
        {
            feelingRepository = unitOfWork.Repository<Feeling>();
        }


        /// <summary>
        /// Get all feelings
        /// </summary>
        /// <returns></returns>
        // GET: api/localization/states
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var feelings = feelingRepository.GetAll();
            var objectReturn = from c
                             in feelings
                               select new
                               {
                                   Id = c.Id,
                                   Name = c.Name
                               };
            return Ok(feelings);
        }

    }
}
