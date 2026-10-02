using Microsoft.AspNetCore.Mvc;
using Practica2Api.Models;

namespace Practica2Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private static readonly List<Producto> _productos = new()
    {
        new Producto { Id = 1, Nombre = "Teclado", Precio = 85000m, Stock = 10 },
        new Producto { Id = 2, Nombre = "Mouse", Precio = 45000m, Stock = 25 },
        new Producto { Id = 3, Nombre = "Monitor", Precio = 650000m, Stock = 5 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Producto>> ObtenerTodos()
    {
        return Ok(_productos);
    }

    [HttpGet("{id}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto is null) return NotFound();
        return Ok(producto);
    }

    [HttpPost]
    public ActionResult<Producto> Crear(Producto nuevo)
    {
        nuevo.Id = _productos.Count == 0 ? 1 : _productos.Max(p => p.Id) + 1;
        _productos.Add(nuevo);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, Producto datos)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto is null) return NotFound();

        producto.Nombre = datos.Nombre;
        producto.Precio = datos.Precio;
        producto.Stock = datos.Stock;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto is null) return NotFound();

        _productos.Remove(producto);
        return NoContent();
    }
}