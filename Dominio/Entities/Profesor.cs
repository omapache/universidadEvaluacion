namespace Dominio.Entities;
public class Profesor : BaseEntity
{
    public int IdProfesorFk {get; set;}
    public Persona Persona {get; set;}
    public int IdDepartamentoFk {get; set;}
    public Departamento Departamento {get; set;}



    public ICollection<Asignatura> Asignaturas {get; set;}
}
