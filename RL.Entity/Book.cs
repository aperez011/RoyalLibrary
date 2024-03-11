using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Entity
{
    public class Book : BaseProperties
    {
        public string FirstName { get; set; } = default;
        public string LastName { get; set; } = default;
        public int TotalCopies { get; set; } = default;
        public int CopiesInUse { get; set; } = default;
        public string Type { get; set; } = default;
        public string ISBN { get; set; } = default;
        public string Category { get; set; } = default;
    }
}
