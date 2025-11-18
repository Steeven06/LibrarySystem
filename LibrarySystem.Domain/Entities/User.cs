using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Domain.Entities
{
    public class LibraryUser
    {
       
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }

            // Relación 1 -> N
            public List<Loan> Loans { get; set; } = new();
        }
}
