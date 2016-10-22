﻿using Shuttle.Core.Data;
using Shuttle.Sentinel.Query;

namespace Shuttle.Sentinel
{
    public interface IDataStoreQueryFactory
    {
        IQuery Add(DataStore dataStore);
        IQuery Remove(string name);
        IQuery All();
        IQuery Edit(string key, DataStore dataStore);
    }
}