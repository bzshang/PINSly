﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class AdhocEventsDTO
    {
        public string WebID { get; set; }
        public IList<EventDTO> Items { get; set; }
    }
}
