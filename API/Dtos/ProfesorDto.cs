using Dominio.Entities;

namespace API.Dtos;
    public class ProfesorDto : BaseEntity
{
    public int IdDepartamentoFk {get; set;}
    public DepartamentoDto DepartamentoDto {get; set;}

    public int IdProfesorFk {get; set;}
    public PersonaDto PersonaDto {get; set;}

}
