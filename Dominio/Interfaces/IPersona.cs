using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IPersona : IGenericRepo<Persona>
{
    Task<IEnumerable<object>> ListadoAlumnos();
    Task<IEnumerable<object>> ListadoAlumnosNoTelefono();
    Task<IEnumerable<object>> Nacio1999();
    Task<IEnumerable<object>> ProfesorNoTelefono();
    Task<IEnumerable<object>> AlumnasIngenieríaInformática();
    Task<IEnumerable<object>> AsignaturasIngenieríaInformática();
    Task<IEnumerable<object>> ProfeDep();
    Task<IEnumerable<object>> ListProfeDep();
    Task<IEnumerable<object>> Matriculado20182019();
    Task<object> ProfConOSinDepartamento();
    Task<int> TotalAlumnas();
    public Task<object> AlumnosHombresNac1999();
    public Task<IEnumerable<object>> ProfEnCadaDepartamento();
    public Task<IEnumerable<object>> DepartamentoYProfesor();
    public Task<IEnumerable<object>> AsignaturaPorProfesor();
    public Task<object> AlumnoMaJoven();
    public Task<IEnumerable<object>> ProfesoresSinDepartamentos();
    public Task<IEnumerable<object>> DepartamentosSinProfesor();
    public Task<IEnumerable<object>> ProfesorConDeptPeroSinAsignatura();
}
