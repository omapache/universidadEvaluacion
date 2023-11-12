using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IDepartamento : IGenericRepo<Departamento>
{
    Task<IEnumerable<object>> DepartamentoSinProfesores();
    Task<IEnumerable<object>> DepartamentoYProfesor();
    Task<IEnumerable<object>> SinAsignaturas();

}
