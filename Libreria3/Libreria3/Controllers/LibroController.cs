using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
  // Asegúrate de ajustar el espacio de nombres
using Libreria3.Context;
using Libreria3.Models;
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("api/books")]

public class BooksController : ControllerBase
{
    private readonly LibreriaContext _context;

    public BooksController(LibreriaContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] Book book)
    {
        // Validaciones u lógica adicional si es necesario

        _context.Books.Add(book);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }


    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}
