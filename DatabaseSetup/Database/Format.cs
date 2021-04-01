using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSetup.Database
{
    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public Type Type { get; set; }
        public int? TypeId { get; set; }
    }
}
