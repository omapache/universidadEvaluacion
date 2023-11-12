using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IGrado : IGenericRepo<Grado>
{
    public Task<IEnumerable<object>> GradosYNumAsignaturas(); 
    public Task<IEnumerable<object>> GradosYNumAsignaturasMayorA40(); 
    public Task<IEnumerable<object>> GradosConSuma();
}
