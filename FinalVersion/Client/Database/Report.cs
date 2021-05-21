using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Database
{
    [Serializable]
    public class Report
    {
        public int Id { get; set; }
        public User ReportedUser { get; set; }
        public int? ReportedUserId { get; set; }
        public string Reason { get; set; }
        public User Initiator { get; set; }
        public int? InitiatorId { get; set; }
        public DateTime Time { get; set; }
    }
}
