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
    
}