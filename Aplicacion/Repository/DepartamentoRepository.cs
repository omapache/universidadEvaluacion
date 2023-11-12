using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
    public class DepartamentoRepository : GenericRepo<Departamento>, IDepartamento
{
    private readonly ApiContext _context;
    
    public DepartamentoRepository(ApiContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
            .ToListAsync();
    }

    public override async Task<Departamento> GetByIdAsync(int id)
    {
        return await _context.Departamentos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public async Task<IEnumerable<object>> DepartamentoSinProfesores()
    {
        var departamentos = await (
            from d in _context.Departamentos
            where !d.Profesores.Any() 
            select new
            {
                Nombre = d.Nombre
            })
            .ToListAsync();

        return departamentos;
    }
    public async Task<IEnumerable<object>> DepartamentoYProfesor()
    {
        var resultado = await _context.Departamentos
            .Where(d => d.Profesores == d.Profesores) 
            .Select(d => new
            {
                NombreDepartamento = d.Nombre, 
                CantidadProfesores = d.Profesores.Count
            })
            .OrderByDescending(dp => dp.CantidadProfesores)
            .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<object>> SinAsignaturas() 
    {
        var departamentosSinAsignaturas = await _context.Departamentos
            .Where(d => !d.Profesores.Any(p => p.Asignaturas.Any()))
            .Select(d => new
            {
                NombreDepartamento = d.Nombre
            })
            .ToListAsync();

        return departamentosSinAsignaturas;
    }
}