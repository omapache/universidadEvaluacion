using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IAsignatura : IGenericRepo<Asignatura>
{
    Task<IEnumerable<object>> AsignaturasprimerCuatrimestre();
    Task<IEnumerable<object>> ListadoAsignaturas();
    Task<IEnumerable<object>> AsignaturasSinProfesores();
    Task<IEnumerable<object>> AsignaturaQueNoSeHayaImpartido();
    Task<IEnumerable<object>> AsignaturaSinProfesor();
}
