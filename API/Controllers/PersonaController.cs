using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class PersonaController  : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public PersonaController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var entidad = await unitofwork.Personas.GetAllAsync();
        return mapper.Map<List<PersonaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonaDto>> Get(int id)
    {
        var entidad = await unitofwork.Personas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<PersonaDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PersonaDto entidadDto)
    {
        var entidad = this.mapper.Map<Persona>(entidadDto);
        this.unitofwork.Personas.Add(entidad);
        await unitofwork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        entidadDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = entidadDto.Id}, entidadDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody]PersonaDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Persona>(entidadDto);
        unitofwork.Personas.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Personas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Personas.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    [HttpGet("consulta1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ListadoAlumnos()
    {
        var entidad = await unitofwork.Personas.ListadoAlumnos();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ListadoAlumnosNoTelefono()
    {
        var entidad = await unitofwork.Personas.ListadoAlumnosNoTelefono();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta3")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Nacio1999()
    {
        var entidad = await unitofwork.Personas.Nacio1999();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta4")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ProfesorNoTelefono()
    {
        var entidad = await unitofwork.Personas.ProfesorNoTelefono();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    
    [HttpGet("consulta6")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> AlumnasIngenieríaInformática()
    {
        var entidad = await unitofwork.Personas.AlumnasIngenieríaInformática();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta7")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> AsignaturasIngenieríaInformática()
    {
        var entidad = await unitofwork.Personas.AsignaturasIngenieríaInformática();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta8")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ProfeDep()
    {
        var entidad = await unitofwork.Personas.ProfeDep();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta10")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ListProfeDep()
    {
        var entidad = await unitofwork.Personas.ListProfeDep();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta11")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Matriculado20182019()
    {
        var entidad = await unitofwork.Personas.Matriculado20182019();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta12")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ProfConOSinDepartamento()
    {
        var entidad = await unitofwork.Personas.ProfConOSinDepartamento();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta17")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TotalAlumnas()
    {
        var entidad = await unitofwork.Personas.TotalAlumnas();
        var dto = mapper.Map<int>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta18")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> AlumnosHombresNac1999()
    {
        var personas = await unitofwork.Personas.AlumnosHombresNac1999();
        return Ok(personas);
    }

    [HttpGet("consulta19")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ProfEnCadaDepartamento()
    {
        var Persona = await unitofwork.Personas.ProfEnCadaDepartamento();
        return mapper.Map<List<object>>(Persona);
    }

    [HttpGet("consulta20")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> DepartamentoYProfesor()
    {
        var Persona = await unitofwork.Personas.DepartamentoYProfesor();
        return mapper.Map<List<object>>(Persona);
    }
    
    [HttpGet("consulta25")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> AsignaturaPorProfesor()
    {
        var Persona = await unitofwork.Personas.AsignaturaPorProfesor();
        return mapper.Map<List<object>>(Persona);
    }

    
    [HttpGet("consulta26")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> AlumnoMaJoven()
    {
        var personas = await unitofwork.Personas.AlumnoMaJoven();
        return Ok(personas);
    }

    
    [HttpGet("consulta27")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ProfesoresSinDepartamentos()
    {
        var personas = await unitofwork.Personas.ProfesoresSinDepartamentos();
        return Ok(personas);
    }
    
    [HttpGet("consulta28")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> DepartamentosSinProfesor()
    {
        var Persona = await unitofwork.Personas.DepartamentosSinProfesor();
        return mapper.Map<List<object>>(Persona);
    }

    
    [HttpGet("consulta29")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ProfesorConDeptPeroSinAsignatura()
    {
        var Persona = await unitofwork.Personas.ProfesorConDeptPeroSinAsignatura();
        return mapper.Map<List<object>>(Persona);
    }
}