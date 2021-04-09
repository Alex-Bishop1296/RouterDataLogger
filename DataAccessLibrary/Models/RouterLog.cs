﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RouterLog
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
    }
}
