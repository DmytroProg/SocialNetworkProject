using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Helpers
{
    public class OnlineStatus
    {
        public Status status;
        public enum Status
        {
            Online,
            Offline,
        }
        public DateTime LastLoggedIn;
        public OnlineStatus(DateTime dateTime)
        {
            status = Status.Offline;
            LastLoggedIn = dateTime;
        }
        public OnlineStatus()
        {
            status = Status.Offline;
            LastLoggedIn = DateTime.UtcNow;
        }
    }
}
