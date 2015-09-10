﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOperations
{
    public interface IDataProvider
    {
        IDisposable SubscribeToObservable(Action method);

    }
}
