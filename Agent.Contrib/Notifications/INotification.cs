using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Notifications
{
    public interface INotification
    {
        string Text { get; set; }
        string Type { get; set; }
        DateTime TimeStamp { get; set; }
        
    }
}
