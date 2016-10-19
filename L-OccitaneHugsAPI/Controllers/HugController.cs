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
    /// Hugs controller
    /// </summary>
    [RoutePrefix("api/hug")]
    public class HugController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Hug> hugRepository;

        public HugController()
        {
            hugRepository = unitOfWork.Repository<Hug>();
        }


        /// <summary>
        /// Get all hugs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get()
        {
            var hugs = hugRepository.GetAll();
            return Ok(hugs);
        }


        /// <summary>
        /// Get total of hugs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("total")]
        public IHttpActionResult GetTotal()
        {
            var hugs = hugRepository.GetAll();
            return Ok(hugs.Count());
        }

        /// <summary>
        /// Get total of hugs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("regionsTotals")]
        public IHttpActionResult GetRegionsTotal()
        {
            var hugs = hugRepository.GetAll(x=> x.City, x=> x.City.State, x => x.City.State.Region);

            var totals = from r in hugs
                     orderby r.City.State.Region.Name
                     group r by r.City.State.Region.Name into grp
                     select new { key = grp.Key, cnt = grp.Count() };

            return Ok(totals);
        }


        /// <summary>
        /// Filter hugs and take 150 records
        /// </summary>
        /// <param name="type">creatorId feelingId stateId createDate</param>
        /// <param name="values">value for type param (createDate must have 2 values)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public IHttpActionResult Filter(string type, [FromUri]string[] values)
        {
            var hugs = new List<Hug>();
            if(type == "creatorId")
            {
                hugs =  hugRepository.FindAll(x => x.Identifier == values.FirstOrDefault(), x=> x.City, x=> x.Feeling, x=> x.City.State).Take(150).ToList();
            }
            else if(type == "feelingId")
            {
                hugs =  hugRepository.FindAll(x => x.FeelingId == int.Parse(values.FirstOrDefault()), x => x.City, x => x.Feeling, x => x.City.State).Take(150).ToList();
            }
            else if (type == "stateId")
            {
                hugs =  hugRepository.FindAll(x => x.City.StateId == int.Parse(values.FirstOrDefault()), x => x.City, x => x.Feeling, x => x.City.State).Take(150).ToList();
            }
            else if (type == "createDate")
            {
                hugs = hugRepository.FindAll(x => x.CreateDate.CompareTo(DateTime.Parse(values[0])) >= 0 && x.CreateDate.CompareTo(DateTime.Parse(values[1])) <= 0, x => x.City, x => x.Feeling, x => x.City.State).Take(150).ToList();
            }
            if (hugs == null || hugs.Count == 0)
                hugs= hugRepository.GetAll().Take(150).ToList();


            var returnObject = from a
                               in hugs
                               select new { Id= a.Id, From = a.From, To = a.To, Message = a.Message,
                                   Likes = a.Likes, City = a.City.Name, State = a.City.State.Name, Identifier = a.Identifier};
                               
            return Ok(returnObject);
        }


        /// <summary>
        /// Get hug details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var hug = hugRepository.GetById(id, x=> x.City, x => x.City.State, x => x.Feeling);
            return Created(new
            {
                To = hug.To,
                From = hug.From,
                Message = hug.Message,
                ImageUrl = hug.ImageUrl,
                FeelingName = hug.Feeling.Name,
                CityName= hug.City.Name,
                StateAbbreviation = hug.City.State.Abbreviation,
                CreateDate = hug.CreateDate.ToShortDateString()
            });
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

        /// <summary>
        /// Create a hug
        /// </summary>
        /// <param name="hug"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(Hug hug)
        {
            var feelingRepository = unitOfWork.Repository<Feeling>();
            Feeling feeling = await feelingRepository.FindAsync(x => x.Tags.ToUpper().Contains(hug.Message.ToUpper()));
            if (feeling == null)
                feeling = feelingRepository.GetAll().FirstOrDefault(); 
            if (ModelState.IsValid)
            {
                hug.CityId = hug.CityId;
                hug.FeelingId = feeling.Id;
                hug.Identifier = hug.Identifier;
                hug.Message = hug.Message;
                hug.From = hug.From;
                hug.To = hug.To;
                hug.CreateDate = DateTime.Now.Date;
                hugRepository.Insert(hug);
            }

            hug = hugRepository.GetById(hug.Id, x => x.City, x => x.City.State, x => x.Feeling);
            return Created(new
            {
                To = hug.To,
                From = hug.From,
                Message = hug.Message,
                ImageUrl = hug.ImageUrl,
                FeelingName = hug.Feeling.Name,
                CityName = hug.City.Name,
                StateAbbreviation = hug.City.State.Abbreviation,
                CreateDate = hug.CreateDate.ToShortDateString()
            });
        }

        /// <summary>
        /// Like a hug
        /// </summary>
        /// <param name="id">Hug id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/like")]
        public IHttpActionResult LikeDislike(int id)
        {
            var hug = hugRepository.GetById(id);
            hug.Likes = hug.Likes + 1;
            hugRepository.Update(hug);
            return Ok(hug.Likes);
        }

        /// <summary>
        /// Dislike a hug
        /// </summary>
        /// <param name="id">Hug id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/dislike")]
        public IHttpActionResult Dislike(int id)
        {
            var hug = hugRepository.GetById(id);
            hug.Likes = hug.Likes - 1;
            hugRepository.Update(hug);
            return Ok(hug.Likes);
        }

        /// <summary>
        /// Delete a hug.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
