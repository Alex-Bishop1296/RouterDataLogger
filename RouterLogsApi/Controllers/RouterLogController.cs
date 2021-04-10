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
        /// <summary>
        /// Basic GET, returns all entries in the RouterLogs table
        /// </summary>
        /// <returns>IEnumerable<RouterLog> containing all enties in the RouterLogs table</returns>
        [HttpGet]
        public ActionResult <IEnumerable<RouterLog>> GetAllRouterLogs()
        {
            IEnumerable<RouterLog> table = databaseContext.RouterLogs
                                                        .ToList();
            return Ok(table);
        }

        //GET api/routerLog/{id}
        /// <summary>
        /// Basic GET, returns an entry from the RouterLogs table based on it's Id field
        /// </summary>
        /// <param name="id">id the caller is searching for</param>
        /// <returns>Routerlog with inputed Id</returns>
        [HttpGet("{id}")]
        public ActionResult <RouterLog> GetRouterLogById(int id)
        {
            RouterLog IdLog = databaseContext.RouterLogs
                                            .Where(x => x.Id == id)
                                            .FirstOrDefault();
            return Ok(IdLog);
        }
    }
}
