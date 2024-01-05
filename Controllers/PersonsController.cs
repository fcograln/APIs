using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CSVApi.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace CSVApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IArchivoService _archivoService;
        private readonly ApplicationDbContext _context;

    public PersonsController(IArchivoService archivoService,ApplicationDbContext context)
    {
        _archivoService = archivoService;
        _context= context;
    }


    [HttpGet]
    public IActionResult GetData()
    {
        var personas = _context.Persons.ToList();
        return Ok(personas);
    }
    [HttpPost]
    public IActionResult LoadFile(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("Archivo no válido");

        var result = _archivoService.ProcesarArchivo(archivo);

        if (result)
            return Ok("Datos guardados correctamente");

        return BadRequest("Error al procesar el archivo");
    }

    }

    public interface IArchivoService
{
    bool ProcesarArchivo(IFormFile archivo);
}

public class ArchivoService : IArchivoService
{
    private readonly ApplicationDbContext _context;

    public ArchivoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool ProcesarArchivo(IFormFile archivo)
    {
        // Lógica para leer el archivo CSV y guardar los datos en la base de datos
        // Puedes usar una biblioteca como CsvHelper para facilitar la lectura del CSV

        // Ejemplo de uso de CsvHelper


        using (var reader = new StreamReader(archivo.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Person>().ToList();

            _context.Persons.AddRange(records);
            _context.SaveChanges();
        }

        return true;
    }
}

}