﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOperations
{
    public abstract class AbstractDataProvider
    {
        public abstract void Initialize();
        public abstract void SendToQueue();


    }
}
