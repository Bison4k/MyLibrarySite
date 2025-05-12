using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.DataBase.Repositories;
using MyLibrary.Models;

namespace MyLibrary.Pages;

public class BooksModel : PageModel
{
    private readonly BookRepository _bookRepository;

    public BooksModel(BookRepository repository)
    {
        _bookRepository = repository;
    }

    public List<Book> Books { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; }

    [BindProperty]
    public BookForm NewBook { get; set; } 

    public async Task<IActionResult> OnGetAsync()
    {
        var allBooks = await _bookRepository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            var filteredBooks = allBooks
                .Where(b =>
                    b.Title.ToLower().Contains(Search.ToLower()) ||
                    b.Author.ToLower().Contains(Search.ToLower())).ToList() ;

            Books = filteredBooks;
            return Page();
        }

        Books = allBooks;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        
        await _bookRepository.CreateAsync(NewBook ?? new BookForm());
        return RedirectToPage();
    }

}