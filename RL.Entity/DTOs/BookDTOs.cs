using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Entity.DTOs
{
    public class BookRequest
    {
        public string Title { get; set; } = default;
        public string FirstName { get; set; } = default;
        public string LastName { get; set; } = default;
        public string Publisher { get; set; } = default;
        public int TotalCopies { get; set; } = default;
        public string Type { get; set; } = default;
        public string ISBN { get; set; } = default;
        public string Category { get; set; } = default;
    }

    public class BookUpdateRequest : BookRequest
    { 
        public int Id { get; set; }
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = default;
        public string Publisher { get; set; } = default;
        public string Author { get; set; } = default;
        public string Type { get; set; } = default;
        public string ISBN { get; set; } = default;
        public string Category { get; set; } = default;
        public string Avalible { get; set; } = default;
    }
}
