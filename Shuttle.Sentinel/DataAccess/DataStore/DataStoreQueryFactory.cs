﻿using Shuttle.Core.Data;
using Shuttle.Sentinel.Query;

namespace Shuttle.Sentinel
{
    public class DataStoreQueryFactory : IDataStoreQueryFactory
    {
        public IQuery Add(DataStore dataStore)
        {
            return RawQuery.Create(@"
if not exists (select null from DataStore where Name = @Name)
    insert into DataStore 
    (
        Name,
        ConnectionString,
        ProviderName
    ) 
    values 
    (
        @Name,
        @ConnectionString,
        @ProviderName
    )
")
                .AddParameterValue(DataStoreColumns.Name, dataStore.Name)
                .AddParameterValue(DataStoreColumns.ConnectionString, dataStore.ConnectionString)
                .AddParameterValue(DataStoreColumns.ProviderName, dataStore.ProviderName);
        }

        public IQuery Remove(string name)
        {
            return RawQuery.Create(
                @"delete from DataStore where Name = @Name")
                .AddParameterValue(DataStoreColumns.Name, name);
        }

        public IQuery All()
        {
            return RawQuery.Create(@"select Name, ConnectionString, ProviderName from DataStore order by Name");
        }

        public IQuery Edit(string key, DataStore dataStore)
        {
            return RawQuery.Create(@"
update DataStore set
    Name = @Name,
    ConnectionString = @ConnectionString,
    ProviderName = @ProviderName
where
    Name = @Key
")
                .AddParameterValue(DataStoreColumns.Key, key)
                .AddParameterValue(DataStoreColumns.Name, dataStore.Name)
                .AddParameterValue(DataStoreColumns.ConnectionString, dataStore.ConnectionString)
                .AddParameterValue(DataStoreColumns.ProviderName, dataStore.ProviderName);
        }
    }
}