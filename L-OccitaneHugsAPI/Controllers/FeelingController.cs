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

        /// <summary>
        /// Dislike a hug
        /// </summary>
        /// <param name="id">Hug id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/addTags")]
        public IHttpActionResult AddTags(int id, string tags)
        {
            var feeling = feelingRepository.GetById(id);
            feeling.Tags = $"{feeling.Tags} {tags}";
            feelingRepository.Update(feeling);
            return Ok(feeling);
        }

        /// <summary>
        /// Create a hug
        /// </summary>
        /// <param name="feeling"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(Feeling feelingIn)
        {
            var feelingRepository = unitOfWork.Repository<Feeling>();
            Feeling feeling = await feelingRepository.FindAsync(x => (x.Id == feelingIn.Id || x.Name == feelingIn.Name));
            if (!ModelState.IsValid)
                return BadRequest("The model are invalid");
            if (feeling == null)
            {
                feelingRepository.Insert(feelingIn);
            }

            return Created("", new
            {
                feelingIn.Name,
                feelingIn.Tags
            });
        }


    }
}
