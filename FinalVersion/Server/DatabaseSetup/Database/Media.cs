using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSetup.Database
{
    public class Media
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Type Type { get; set; }
        public int? TypeId { get; set; }
    }
}
