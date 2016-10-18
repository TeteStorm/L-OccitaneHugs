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
    /// Localization controller for get states and cities
    /// </summary>
    [RoutePrefix("api/localization")]
    public class LocalizationController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<City> cityRepository;
        private Repository<State> stateRepository;

        public LocalizationController()
        {
            cityRepository = unitOfWork.Repository<City>();
            stateRepository = unitOfWork.Repository<State>();
        }

        /// <summary>
        /// Get cities by state
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cities")]
        public IHttpActionResult GetCitiesByState(int stateId)
        {
            var cities = cityRepository.FindAll(x => x.StateId == stateId);

            var objectReturn = from c
                               in cities
                               select new
                               {
                                   Id = c.Id,
                                   Name = c.Name
                               };
            return Ok(objectReturn);
        }


        /// <summary>
        /// Get all states
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("states")]
        public IHttpActionResult GetStates()
        {
            var states = stateRepository.GetAll(x => x.Region);
            var objectReturn = from c
                             in states
                               select new
                               {
                                   Id = c.Id,
                                   Name = c.Name
                               };
            return Ok(states);
        }

    }
}