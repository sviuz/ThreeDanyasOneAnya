using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSetup.Database
{
    public class Log
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
