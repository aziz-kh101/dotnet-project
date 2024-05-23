using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

namespace project;

public class CategoryController : Controller
{
    private readonly ApplciationDbContext _context;
    public CategoryController(ApplciationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> List()
    {
        List<Category> categories = await _context.Categories.ToListAsync();
        return View(new CategoryListModel { Categories = categories });
    }

    public IActionResult Create()
    {
        return View(new CategoryCreateModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CategoryCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Create", model);
        }
        await _context.Categories.AddAsync(new Category() { Name = model.Name, Description = model.Description });
        _context.SaveChanges();
        return RedirectToAction("List");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(new CategoryEditModel { Id = category.Id, Name = category.Name, Description = category.Description });
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(CategoryEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", model);
        }
        Category? category = await _context.Categories.FindAsync(model.Id);
        Console.WriteLine(category);
        if (category == null)
        {
            return NotFound();
        }
        category.Name = model.Name;
        category.Description = model.Description;
        _context.SaveChanges();
        return RedirectToAction("List");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(new CategoryDeleteModel { Id = category.Id, Name = category.Name, Description = category.Description });
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(CategoryDeleteModel model)
    {
        Category? category = await _context.Categories.FindAsync(model.Id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return RedirectToAction("List");
    }
}
