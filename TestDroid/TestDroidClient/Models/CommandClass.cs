using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDroidClient.Models
{
    class CommandClass
    {
        public CommandClass(long id, string command, DateTime callTime)
        {
            Command = command;
            Id = id;
            CallTime = callTime;
        }

        public string Command { get; set; }
        public long Id { get; set; }
        public DateTime CallTime { get; set; }
    }
}
