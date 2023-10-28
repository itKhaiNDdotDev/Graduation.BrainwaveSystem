using Microsoft.AspNetCore.SignalR;

namespace Graduation.BrainwaveSystem.APIs.Common
{
    public class NotificationHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public string GetConnectionUser()
        {
            return Context.UserIdentifier ?? "Error";
        }

        public NotificationHub() { }
    }
}
