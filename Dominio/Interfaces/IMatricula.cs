using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IMatricula : IGenericRepo<Matricula>
{
    public Task<IEnumerable<object>> MatriculadosPorCurso(); 

}
