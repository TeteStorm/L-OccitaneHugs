using L_OccitaneHugsData;
using L_OccitaneHugsDomain;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace L_OccitaneHugsAPI.Controllers
{
    [RoutePrefix("api/hug")]
    public class HugController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Hug> hugRepository;

        public HugController()
        {
            hugRepository = unitOfWork.Repository<Hug>();
        }
        
        // GET: api/hugs
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get()
        {
            var hugs = hugRepository.GetAll();
            return Ok(hugs);
        }

        // GET: api/hug/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var hug = hugRepository.GetById(id);
            return Ok(hug);
        }

        // POST: api/hug
        [HttpPost]
        //[Route("create")]
        public IHttpActionResult Post(Hug hug)
        {
            if (ModelState.IsValid)
            {
                hug.CityId = 1;
                hug.Identificator = "tania@trin.ca";
                hug.Message = "Hello";
                hug.From = "Tania";
                hug.To = "Maria";
                hugRepository.Insert(hug);
            }

            return Created(hug);
        }

        // PUT: api/hug/5
        [HttpPut]
        [Route("{id}/like")]
        public IHttpActionResult Like(int id)
        {
            var hug = hugRepository.GetById(id);
            hug.Likes = hug.Likes + 1;
            hugRepository.Update(hug);
            return Ok(hug.Likes);
        }

        // DELETE: api/hug/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var hug = hugRepository.GetById(id);
            hugRepository.Delete(hug);
            return Ok();
        }


        protected CreatedNegotiatedContentResult<T> Created<T>(T content, string location = "")
        {
            return Created(location, content);
        }
    }
}
