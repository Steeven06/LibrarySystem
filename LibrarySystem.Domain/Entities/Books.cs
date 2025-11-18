using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LibrarySystem.Domain.Entities
{
    public class Books
    {
        
            public Guid Id { get; set; }
            public string Title { get; set; }

            public string ISBN { get; set; }
            public int Quantity { get; set; }  // Total de ejemplares
            public int AvailableQuantity { get; set; } // Libros disponibles

            // Relaciones
            public Guid AuthorId { get; set; }
            public Author Author { get; set; }

            public Guid CategoryId { get; set; }
            public Category Category { get; set; }

            // Relación con préstamos
            public List<Loan> Loans { get; set; } = new();
        

    }
}
