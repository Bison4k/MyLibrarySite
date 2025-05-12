using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.Models;
using MyLibrary.DataBase.Repositories;

namespace MyLibrary.Pages;

public class EditModel : PageModel
{
    private readonly BookRepository _bookRepository;

    public EditModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [BindProperty]
    public Book? Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Book = await _bookRepository.GetByIdAsync(id);
        
        if (Book == null)
        {
            return RedirectToPage("/Books");
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || Book == null)
        {
            return Page();
        }

        await _bookRepository.UpdateAsync(Book);
        return RedirectToPage("/Books");
    }
}