using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

namespace project;

public class CarController : Controller
{
    private readonly ApplciationDbContext _context;
    public CarController(ApplciationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> List()
    {
        List<Car> cars = await _context.Cars.ToListAsync();
        return View(new CarListModel { Cars = cars });
    }

    public IActionResult Create()
    {
        return View(new CarCreateModel() { Categories = new SelectList(_context.Categories.ToList(), "Id", "Name") });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CarCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View("Create", model);
        }
        Car car = new Car() { Model = model.Car.Model, Brand = model.Car.Brand, Description = model.Car.Description, Price = model.Car.Price, Year = model.Car.Year, CategoryId = model.SelectedCategoryId };

        if (model.Car.Image != null)
        {
            string uniqueFileName = GetUniqueFileName(model.Car.Image.FileName);
            string filePath = Path.Combine("wwwroot/images", uniqueFileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                model.Car.Image.CopyTo(fs);
            }
            car.Image = uniqueFileName;
        }

        await _context.Cars.AddAsync(car);
        _context.SaveChanges();
        return RedirectToAction("List");
    }

    private string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Car? car = await _context.Cars.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(new CarDetailsModel { Car = car });
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Car? car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(new CarDeleteModel { Car = car });
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(CarDeleteModel model)
    {
        Car? car = await _context.Cars.FindAsync(model.Car.Id);
        if (car == null)
        {
            return NotFound();
        }
        System.IO.File.Delete(Path.Combine("wwwroot/images", car.Image));
        _context.Cars.Remove(car);
        _context.SaveChanges();
        return RedirectToAction("List");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Car? car = await _context.Cars.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(new CarEditModel
        {
            Id = car.Id,
            ImageUrl = car.Image,
            Car = new CarEditModule { Brand = car.Brand, Model = car.Model, Description = car.Description, Price = car.Price, Year = car.Year },
            Categories = new SelectList(_context.Categories.ToList(), "Id", "Name"),
            SelectedCategoryId = car.CategoryId
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(CarEditModel model)
    {
        Car? car = await _context.Cars.FindAsync(model.Id);
        if (!ModelState.IsValid)
        {
            model.ImageUrl = car?.Image ?? "";
            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View("Edit", model);
        }
        if (car == null)
        {
            return NotFound();
        }
        car.Model = model.Car.Model;
        car.Brand = model.Car.Brand;
        car.Description = model.Car.Description;
        car.Price = model.Car.Price;
        car.Year = model.Car.Year;
        car.CategoryId = model.SelectedCategoryId;

        if (model.Car.Image != null)
        {
            string uniqueFileName = GetUniqueFileName(model.Car.Image.FileName);
            string filePath = Path.Combine("wwwroot/images", uniqueFileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                model.Car.Image.CopyTo(fs);
            }
            System.IO.File.Delete(Path.Combine("wwwroot/images", car.Image));
            car.Image = uniqueFileName;
        }

        _context.SaveChanges();
        return RedirectToAction("List");
    }
}
