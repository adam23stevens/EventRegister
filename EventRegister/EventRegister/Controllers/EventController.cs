using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace EventRegister.Controllers
{
    [Route("api/[controller]")]
    public class EventController : ApiController
    {
        private Business.RepoContract.IEventRegRepo repo;
        public EventController(Business.RepoContract.IEventRegRepo _repo)
        {
            repo = _repo;
        }

        [HttpPost]
        public IHttpActionResult RegisterToEvent(Business.RepoContract.Model.RegisterReq req)
        {
            repo.RegisterToEvent(req);

            return Ok();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllEmailsForEvent(Business.RepoContract.Model.EventReq req)
        {
            var emails = await repo.GetRegisterdEmailsForEvent(req);
            return Ok(emails);
        }
    }
}
