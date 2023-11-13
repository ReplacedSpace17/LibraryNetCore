using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Libreria3.Context;
using Libreria3.Models;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly LibreriaContext _context;

    public AuthorsController(LibreriaContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromBody] Author author)
    {
        // Validaciones u lógica adicional si es necesario

        _context.Authors.Add(author);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpGet]
    public IActionResult GetAllAuthors()
    {
        var authors = _context.Authors.ToList();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        var author = _context.Authors.Find(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }
}
