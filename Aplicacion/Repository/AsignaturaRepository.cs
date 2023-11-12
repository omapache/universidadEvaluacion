using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;


namespace Aplicacion.Repository;
public class AsignaturaRepository : GenericRepo<Asignatura>, IAsignatura
{
    private readonly ApiContext _context;
    
    public AsignaturaRepository(ApiContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<IEnumerable<Asignatura>> GetAllAsync()
    {
        return await _context.Asignaturas
            .ToListAsync();
    }

    public override async Task<Asignatura> GetByIdAsync(int id)
    {
        return await _context.Asignaturas
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public async Task<IEnumerable<object>> AsignaturasprimerCuatrimestre()
    {
         var Movimiento = await (
            from p in _context.Asignaturas
            where p.Cuatrimestre == 1
            where p.Curso == 3
            where p.IdGradoFk == 7
            select new{
                Asignatura = p.Nombre,
            }).Distinct()
            .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> ListadoAsignaturas()
    {
         var Movimiento = await (
            from m in _context.Matriculas
            join c in _context.CursoEscolares on m.IdCursoEscolarFk equals c.Id
            join p in _context.Personas on m.IdAlumnoFk equals p.Id
            
            where p.Nit.Contains("26902806M")

            select new{
                NombreAlumno = p.Nombre,
                añoInicio = c.AñoInicio,
                AñoFin = c.AñoFin,
                Asignatura = (
                            from ma in _context.Matriculas
                            join a in _context.Asignaturas on ma.IdAsignaturaFk equals a.Id
                            where a.Id == m.IdAsignaturaFk
                            select new{
                                        Asignatura = a.Nombre,
                                    }
                ).Distinct().ToList(),
            })
            .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> AsignaturasSinProfesores()
    {
        var asignaturas = await (
            from a in _context.Asignaturas
            where a.IdProfesorFk == null
            select new
            {
                IdAsignatura = a.Id,
                Asignatura = a.Nombre
            })
            .ToListAsync();

        return asignaturas;
    } 
    public async Task<IEnumerable<object>> AsignaturaQueNoSeHayaImpartido()
    {
        var departamentos = await (
            from m in _context.Matriculas
            join a in _context.Asignaturas on m.IdAsignaturaFk equals a.Id
            join p in _context.Profesores on a.IdProfesorFk equals p.Id
            join d in _context.Departamentos on p.IdDepartamentoFk equals d.Id
            select new
            {
                Departamento = d.Nombre,
                AsignaturasNoImpartidas = (
                    from a in _context.Asignaturas
                    where a.IdProfesorFk.HasValue 
                    where !_context.Matriculas.Any(m => m.IdAsignaturaFk == a.Id)
                    select a.Nombre
                ).ToList()
            })
            .Where(depto => depto.AsignaturasNoImpartidas.Any())
            .ToListAsync();

        return departamentos;
    }
    public async Task<IEnumerable<object>> AsignaturaSinProfesor() 
    {
        var resultado = await _context.Asignaturas
            .Where(d => d.IdProfesorFk == null)
            .Select(d => new
            {
                Id = d.Id,
                Asignatura = d.Nombre
            })
            .OrderBy(dp => dp.Id)
            .ToListAsync();

        return resultado;
    }
}