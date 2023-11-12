using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IProfesor : IGenericRepo<Profesor>
{
    public Task<IEnumerable<object>> ProfesoresSinAsignatura();
    public Task<IEnumerable<object>> NombresDeDepartamentos();

}
