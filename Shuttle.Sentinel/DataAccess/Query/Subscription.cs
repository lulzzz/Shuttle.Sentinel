﻿namespace Shuttle.Sentinel.Query
{
    public class Subscription
    {
        public string MessageType { get; set; }
        public string InboxWorkQueueUri { get; set; }
    }
}