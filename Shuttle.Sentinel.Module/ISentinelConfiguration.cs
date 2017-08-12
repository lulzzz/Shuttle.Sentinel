﻿namespace Shuttle.Sentinel.Module
{
    public interface ISentinelConfiguration
    {
        string InboxWorkQueueUri { get; set; }
        string EndpointName { get; set; }
        string MachineName { get; }
        string BaseDirectory { get; }
        string IPv4Address { get; }
        int HeartbeatIntervalSeconds { get; set; }
    }
}