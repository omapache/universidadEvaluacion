using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class MatriculaRepository : GenericRepo<Matricula>, IMatricula
{
    private readonly ApiContext _context;

    public MatriculaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Matricula>> GetAllAsync()
    {
        return await _context.Matriculas
            .ToListAsync();
    }

    public override async Task<Matricula> GetByIdAsync(int id)
    {
        return await _context.Matriculas
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<object>> MatriculadosPorCurso() 
    {
        var resultado = await _context.Matriculas
            .GroupBy(m => m.CursoEscolar.AñoInicio)
            .Select(g => new
            {
                AnioCursoEscolar = g.Key,
                NumAlumnosMatriculados = g.Count()
            })
            .OrderBy(g => g.AnioCursoEscolar)
            .ToListAsync();

        return resultado;
    }
}