using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

// The service uses EF Core to perform CRUD operations on the database

namespace ContosoPizza.Services;

public class PizzaService
{
    // class-level field for PizzaContext
    private readonly PizzaContext _context;
    // when the PizzaService instance is created, a PizzaContext is injected into the constructor
    public PizzaService(PizzaContext context)
    {
        // assign parameter context to field _context
       _context = context; 
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
        .AsNoTracking()
        .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
        .Include(p => p.Toppings)
        .Include(p => p.Sauce)
        .AsNoTracking()
        .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();

        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var toppingToAdd = _context.Toppings.Find(ToppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var sauceToUpdate = _context.Sauces.Find(SauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }
}