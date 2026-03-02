using LibrarySystem.Application.DTOs.Books;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly ICategoryRepository _categoryRepo;

        public BookService(
            IBookRepository bookRepo,
            IAuthorRepository authorRepo,
            ICategoryRepository categoryRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _bookRepo.GetAllAsync();

            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Year = b.Year,
                ImageUrl = b.ImageUrl,
                Quantity = b.Quantity,
                AvailableQuantity = b.AvailableQuantity,
                AuthorFullName = b.Author.FullName,
                CategoryName = b.Category.Name
            }).ToList();
        }
        public async Task<BookDto?> GetByIdAsync(Guid id)
        {
            var b = await _bookRepo.GetByIdAsync(id);

            if (b == null)
                return null;

            return new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Year = b.Year,
                ImageUrl = b.ImageUrl,
                Quantity = b.Quantity,
                AvailableQuantity = b.AvailableQuantity,
                AuthorFullName = b.Author.FullName,
                CategoryName = b.Category.Name
            };
        }


        public async Task<Guid> CreateAsync(CreateBookDto dto)
        {
            var author = await _authorRepo.GetByIdAsync(dto.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            if (category == null) throw new Exception("Category not found");

            var book = new Books
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Year = dto.Year,
                ImageUrl = dto.ImageUrl,
                Quantity = dto.Quantity,
                AvailableQuantity = dto.Quantity,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId
            };

            await _bookRepo.AddAsync(book);
            return book.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateBookDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found");

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.Year = dto.Year;
            book.ImageUrl = dto.ImageUrl;
            book.Quantity = dto.Quantity;

            await _bookRepo.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepo.DeleteAsync(id);
        }
    }
}
