using Dominio.Entities;

namespace API.Dtos;
    public class MatriculaDto : BaseEntity
{
    public int IdAlumnoFk { get; set; }
    public PersonaDto PersonaDto {get; set;}
    public int IdAsignaturaFk {get; set;}
    public AsignaturaDto AsignaturaDto {get; set;}
    public int IdCursoEscolarFk {get; set;}
    public CursoEscolarDto CursoEscolarDto {get; set;}
}
