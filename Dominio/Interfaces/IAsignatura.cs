using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IAsignatura : IGenericRepo<Asignatura>
{
    Task<IEnumerable<object>> AsignaturasprimerCuatrimestre();
    Task<IEnumerable<object>> ListadoAsignaturas();

}
