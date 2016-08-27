using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRSample
{
    public class ChatHub : Hub
    {
        //    public void Send(string name, string message)
        //    {
        //        Clients.All.addNewMessageToPage(name, message);
        //    }

        public void AssignWorkflow(string workflow)
        {
            Clients.All.assignWF(workflow);
        }
    }
}