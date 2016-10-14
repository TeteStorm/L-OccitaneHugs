﻿using L_OccitaneHugsData;
using L_OccitaneHugsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/hugs
        [HttpGet]
        [Route("filter")]
        public IHttpActionResult Filter(string type, string value)
        {
            var hugs = new List<Hug>();
            if(type == "creatorId")
            {
                hugs = hugs = hugRepository.FindAll(x => x.Identifier == value).Take(150).ToList();
            }
            else if(type == "feeling")
            {

            }
            else if (type == "state")
            {

            }
            else if (type == "createDate")
            {

            }
            hugs = hugs ?? hugRepository.GetAll().Take(150).ToList();

            return Ok(hugs);
        }

        // GET: api/hug/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var hug = hugRepository.GetById(id);
            return Ok(hug);
        }
        
        // sync version
        //// POST: api/hug
        //[HttpPost]
        ////[Route("create")]
        //public IHttpActionResult Post(Hug hug)
        //{
        //    var feelingRepository = unitOfWork.Repository<Feeling>();
        //    var getFeeling = feelingRepository.Find(x => x.Tags.ToUpper().Contains(hug.Message.ToUpper()));
        //    if (getFeeling == null)
        //        getFeeling = feelingRepository.GetAll().FirstOrDefault();


        //    if (ModelState.IsValid)
        //    {
        //        hug.CityId = 1;
        //        hug.FeelingId = getFeeling.Id;
        //        hug.Identifier = "tania@trin.ca";
        //        hug.Message = "Hello";
        //        hug.From = "Tania";
        //        hug.To = "Maria";
        //        hugRepository.Insert(hug);
        //    }

        //    return Created( new {
        //              Message = hug.Message,
        //              FeelingName = getFeeling.Name
        //            });
        //}

        // POST: api/hug


        [HttpPost]
        //[Route("create")]
        public async Task<IHttpActionResult> PostAsync(Hug hug)
        {
            var feelingRepository = unitOfWork.Repository<Feeling>();
            Feeling feeling = await feelingRepository.FindAsync(x => x.Tags.ToUpper().Contains(hug.Message.ToUpper()));
            if (feeling == null)
                feeling = feelingRepository.GetAll().FirstOrDefault(); 
            if (ModelState.IsValid)
            {
                hug.CityId = 1;
                hug.FeelingId = feeling.Id;
                hug.Identifier = hug.Identifier;
                hug.Message = hug.Message;
                hug.From = hug.From;
                hug.To = hug.To;
                hug.CreateDate = DateTime.Now.Date;
                hugRepository.Insert(hug);
            }

            return Created(new
            {
                Message = hug.Message,
                FeelingName = feeling.Name,
                CreateDate = hug.CreateDate.ToShortDateString()
            });
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
