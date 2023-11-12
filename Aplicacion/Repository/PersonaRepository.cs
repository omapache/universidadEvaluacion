using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class PersonaRepository : GenericRepo<Persona>, IPersona
{
    private readonly ApiContext _context;

    public PersonaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
            .ToListAsync();
    }

    public override async Task<Persona> GetByIdAsync(int id)
    {
        return await _context.Personas
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<object>> ListadoAlumnos()
    {
        var entidad = await _context.Personas
            .Where(p => p.Tipo == Persona.EnumValoresTipo.Alumno)
            .OrderBy(p => p.Apellido1)
            .ThenBy(p => p.Apellido2)
            .ThenBy(p => p.Nombre)
            .Select(p => new
            {
                PrimerApellido = p.Apellido1,
                SegundoApellido = p.Apellido2,
                Nombre = p.Nombre
            })
            .ToListAsync();

        return entidad;
    }
    public async Task<IEnumerable<object>> ListadoAlumnosNoTelefono()
    {
        var Movimiento = await (
           from p in _context.Personas
           where p.Telefono == null
           where p.Tipo == Persona.EnumValoresTipo.Alumno
           select new
           {
               PrimerApellido = p.Apellido1,
               SegundoApellido = p.Apellido2,
               Nombre = p.Nombre
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> Nacio1999()
    {
        var Movimiento = await (
           from p in _context.Personas
           where p.FechaNacimiento.Year == 1999
           select new
           {
               PrimerApellido = p.Apellido1,
               SegundoApellido = p.Apellido2,
               Nombre = p.Nombre
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> ProfesorNoTelefono()
    {
        var Movimiento = await (
           from p in _context.Personas
           where p.Tipo == Persona.EnumValoresTipo.Profesor
           where p.Nit.ToLower().Contains("k")
           select new
           {
               PrimerApellido = p.Apellido1,
               SegundoApellido = p.Apellido2,
               Nombre = p.Nombre
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }

    public async Task<IEnumerable<object>> AlumnasIngenieríaInformática()
    {
        var Movimiento = await (
           from m in _context.Matriculas
           join p in _context.Personas on m.IdAlumnoFk equals p.Id
           join a in _context.Asignaturas on m.IdAsignaturaFk equals a.Id
           join g in _context.Grados on a.IdGradoFk equals g.Id
           where p.Sexo == Persona.EnumValoresSexo.M
           where g.Nombre.Contains("Grado en Ingeniería Informática (Plan 2015)")
           select new
           {
               nif = p.Nit,
               Nombre = p.Nombre,
               PrimerApellido = p.Apellido1,
               SegundoApellido = p.Apellido2,

           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }

    public async Task<IEnumerable<object>> AsignaturasIngenieríaInformática()
    {
        var Movimiento = await (
           from a in _context.Asignaturas
           join g in _context.Grados on a.IdGradoFk equals g.Id
           where g.Nombre.Contains("Grado en Ingeniería Informática (Plan 2015)")
           select new
           {
               Asignatura = a.Nombre,
               curso = a.Curso,
               creditos = a.Creditos
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }

    public async Task<IEnumerable<object>> ProfeDep()
    {
        var Movimiento = await (
           from pr in _context.Profesores
           join d in _context.Departamentos on pr.IdDepartamentoFk equals d.Id
           join p in _context.Personas on pr.IdProfesorFk equals p.Id
           where p.Tipo == Persona.EnumValoresTipo.Profesor
           select new
           {
               primerApellido = p.Apellido1,
               segundoApellido = p.Apellido2,
               nombre = p.Nombre,
               nombreDepartamento = d.Nombre
           }).Distinct()
           .OrderBy(p => p.primerApellido)
           .ThenBy(p => p.segundoApellido)
           .ThenBy(p => p.nombre)
           .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> ListProfeDep()
    {
        var Movimiento = await (
           from a in _context.Asignaturas
           join pr in _context.Profesores on a.IdProfesorFk equals pr.Id
           join p in _context.Personas on pr.IdProfesorFk equals p.Id
           join d in _context.Departamentos on pr.IdDepartamentoFk equals d.Id
           join g in _context.Grados on a.IdGradoFk equals g.Id
           where g.Nombre.Contains("Grado en Ingeniería Informática (Plan 2015)")
           where p.Tipo == Persona.EnumValoresTipo.Profesor
           select new
           {
               nombreDepartamento = d.Nombre
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }
    public async Task<IEnumerable<object>> Matriculado20182019()
    {
        var Movimiento = await (
           from m in _context.Matriculas
           join p in _context.Personas on m.IdAlumnoFk equals p.Id
           join c in _context.CursoEscolares on m.IdCursoEscolarFk equals c.Id
           join a in _context.Asignaturas on m.IdAsignaturaFk equals a.Id
           where c.AñoInicio == 2018
           where c.AñoFin == 2019
           where p.Tipo == Persona.EnumValoresTipo.Alumno
           select new
           {
               nombre = p.Nombre,
               nif = p.Nit
           }).Distinct()
           .ToListAsync();

        return Movimiento;
    }

    public async Task<object> ProfConOSinDepartamento()
    {
        var profesores = await (
             from pr in _context.Profesores
             join p in _context.Personas on pr.IdProfesorFk equals p.Id into personaGroup
             from persona in personaGroup.DefaultIfEmpty()
             join d in _context.Departamentos on pr.IdDepartamentoFk equals d.Id into departamentoGroup
             from departamento in departamentoGroup.DefaultIfEmpty()
             where persona.Tipo == Persona.EnumValoresTipo.Profesor
             orderby departamento != null ? departamento.Nombre : "Sin departamento", persona.Apellido1, persona.Apellido2, persona.Nombre
             select new
             {
                 NombreDepartamento = departamento != null ? departamento.Nombre : "Sin departamento",
                 PrimerApellido = persona.Apellido1,
                 SegundoApellido = persona.Apellido2,
                 Nombre = persona.Nombre
             })
             .ToListAsync();

        return profesores;
    }
    public async Task<int> TotalAlumnas()
    {
        int totalAlumnas = await (
            from p in _context.Personas
            where p.Tipo == Persona.EnumValoresTipo.Alumno
            select p
        ).CountAsync();

        return totalAlumnas;
    }
    public async Task<object> AlumnosHombresNac1999()
    {
        int totalAlumnos = await _context.Personas
            .CountAsync(p => p.Tipo == Persona.EnumValoresTipo.Alumno && p.Sexo == Persona.EnumValoresSexo.H && p.FechaNacimiento.Year == 1999);
        var retorno = new
        {
            totalAlumnos = totalAlumnos
        };
        return retorno;
    }

    public async Task<IEnumerable<object>> ProfEnCadaDepartamento()
    {
        return await (_context.Profesores
            .Include(p => p.Departamento)
            .GroupBy(p => new
            {
                DepartamentoNombre = p.Departamento.Nombre,
            })
            .Select(group => new
            {
                DepartamentoNombre = group.Key.DepartamentoNombre,
                CantidadProf = group.Count(),
            }).OrderByDescending(x => x.CantidadProf)
            .ToListAsync());
    }

    public async Task<IEnumerable<object>> AsignaturaPorProfesor() //consulta 25
    {
        var resultado = await _context.Profesores
            .Where(d => d.Asignaturas == d.Asignaturas)
            .Select(d => new
            {
                Id = d.IdProfesorFk,
                Nombre = d.Persona.Nombre,
                PrimerApellido = d.Persona.Apellido1,
                SegundoApellido = d.Persona.Apellido2,
                CantidadAsignaturas = d.Asignaturas.Count
            })
            .OrderByDescending(dp => dp.CantidadAsignaturas)
            .ToListAsync();
        return resultado;
    }

    public async Task<object> AlumnoMaJoven() //consulta 26
    {
        return await (
            _context.Personas
            .OrderByDescending(x => x.FechaNacimiento)
            .Select(p => new
            {
                p.Nombre,
                p.Apellido1,
                p.Apellido2,
                p.Telefono,
                p.FechaNacimiento
            })
        ).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<object>> ProfesoresSinDepartamentos() // 27
    {
        var profesoresSinDepartamento = await _context.Personas
            .Where(p => p.Tipo == Persona.EnumValoresTipo.Profesor && p.Profesores.All(pf => pf.Departamento == null))
            .Select(p => new
            {
                NombreProfesor = $"{p.Nombre} {p.Apellido1} {p.Apellido2}",
                Departamento = "Sin asignar"
            })
            .ToListAsync();

        return profesoresSinDepartamento;
    }

    public async Task<IEnumerable<object>> DepartamentosSinProfesor() //28
    {
        var resultado = await _context.Departamentos
            .Where(d => !d.Profesores.Any())
            .Select(d => new
            {
                Id = d.Id,
                NombreDepartamento = d.Nombre,
                CantidadProfesores = d.Profesores.Count
            })
            .OrderByDescending(dp => dp.CantidadProfesores)
            .ToListAsync();

        return resultado;
    }

    
    public async Task<IEnumerable<object>> ProfesorConDeptPeroSinAsignatura() //29
    {
        var resultado = await _context.Profesores
            .Where(d => d.Departamento != null)
            .Where(d => !d.Asignaturas.Any())
            .Select(d => new
            {
                Id = d.Id,
                NombreProfesor = d.Persona.Nombre
            })
            .OrderByDescending(dp => dp.NombreProfesor)
            .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<object>> DepartamentoYProfesor()
    {
        var resultado = await _context.Departamentos
            .Where(d => d.Profesores == d.Profesores)
            .Select(d => new
            {
                NombreDepartamento = d.Nombre,
                CantidadProfesores = d.Profesores.Count
            })
            .OrderByDescending(dp => dp.CantidadProfesores)
            .ToListAsync();

        return resultado;
    }
}
