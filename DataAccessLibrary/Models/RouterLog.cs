using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    /// <summary>
    /// Basic Model for the Router Log table entries
    /// </summary>
    public class RouterLog
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
    }
}
