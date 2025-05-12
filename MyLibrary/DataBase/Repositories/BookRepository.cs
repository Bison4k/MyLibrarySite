using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.DataBase.Repositories;

public class BookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }
    
    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }
    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task CreateAsync(BookForm bookForm)
    {
        var book = new Book()
        {
            Title = bookForm.Title,
            Author = bookForm.Author,
            Year = bookForm.Year,
            Genre = bookForm.Genre,
            IsRead = bookForm.IsRead,
        };
        
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        
        if (dbBook != null)
        {
            dbBook.Title = book.Title;
            dbBook.Author = book.Author;
            dbBook.Year = book.Year;
            dbBook.Genre = book.Genre;
            dbBook.IsRead = book.IsRead;
            
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
} 
