using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;

namespace RouterLogsApi.Controllers
{
    [Route("api/routerLogs")]
    [ApiController]
    public class RouterLogController : ControllerBase
    {
        /// <summary>
        /// The Database Context allows us to preform operations from our DB
        /// </summary>
        private RoutersContext databaseContext;

        /// <summary>
        /// Constructor, intializes dbcontext for Routers DB
        /// </summary>
        public RouterLogController()
        {
            databaseContext = new RoutersContext();
        }

        //GET api/routerLog
        [HttpGet]
        public ActionResult <IEnumerable<RouterLog>> GetAllRouterLogs()
        {
            IEnumerable<RouterLog> table = databaseContext.RouterLogs
                                                    .ToList();
            return Ok(table);
        }

        //GET api/routerLog/{id}
        [HttpGet("{id}")]
        public ActionResult <RouterLog> GetRouterLogById(int id)
        {
            RouterLog IdLog = databaseContext.RouterLogs.Where(x => x.Id == id).FirstOrDefault();
            return Ok(IdLog);
        }
    }
}
