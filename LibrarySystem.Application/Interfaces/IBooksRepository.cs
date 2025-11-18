using System;
using System.Collections.Generic;
using System.Text;


namespace LibrarySystem.Application.Interfaces
{
    internal interface IBooksRepository
    {
        Task AddBookAsync(Books book);

    }
}
