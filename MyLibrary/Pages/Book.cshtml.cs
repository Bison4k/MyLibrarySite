using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.DataBase.Repositories;
using MyLibrary.Models;

namespace MyLibrary.Pages;

public class BookModel : PageModel
{
    public Book? Book {get; set;}
    
    private readonly BookRepository _bookRepository;

    public BookModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task OnGetAsync(int id )
    {
        Book = await _bookRepository.GetByIdAsync(id);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
        return RedirectToPage("/Books");
    }
}
