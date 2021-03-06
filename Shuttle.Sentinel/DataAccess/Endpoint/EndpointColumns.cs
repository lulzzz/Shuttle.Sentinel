﻿using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Sentinel.DataAccess
{
    public class EndpointColumns
    {
        public static MappedColumn<string> EndpointName = new MappedColumn<string>("EndpointName", DbType.AnsiString);
        public static MappedColumn<string> MachineName = new MappedColumn<string>("MachineName", DbType.AnsiString);
        public static MappedColumn<string> BaseDirectory = new MappedColumn<string>("BaseDirectory", DbType.AnsiString);
        public static MappedColumn<string> EntryAssemblyQualifiedName = new MappedColumn<string>("EntryAssemblyQualifiedName", DbType.AnsiString);
        public static MappedColumn<string> IPv4Address = new MappedColumn<string>("IPv4Address", DbType.AnsiString);
        public static MappedColumn<string> InboxWorkQueueUri = new MappedColumn<string>("InboxWorkQueueUri", DbType.AnsiString);
        public static MappedColumn<string> ControlInboxWorkQueueUri = new MappedColumn<string>("ControlInboxWorkQueueUri", DbType.AnsiString);
    }
}