using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork  : IUnitOfWork, IDisposable
{
    private readonly ApiContext _context;

    private RolRepository _Rol;
    private UsuarioRepository _usuarios;
    private AsignaturaRepository _asignaturas;
    private CursoEscolarRepository _cursosEscolares;
    private DepartamentoRepository _Departamentos;
    private GradoRepository _grados;
    private MatriculaRepository _matriculas;
    private PersonaRepository _personas;
    private ProfesorRepository _profesores;

    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }
    
    public IRol Roles
    {
        get{
            if(_Rol== null)
            {
                _Rol= new RolRepository(_context);
            }
            return _Rol;
        }
    }
    
    public IUsuario Usuarios
    {
        get{
            if(_usuarios== null)
            {
                _usuarios= new UsuarioRepository(_context);
            }
            return _usuarios;
        }
    }

    public IAsignatura Asignaturas 
    {
        get{
            if(_asignaturas== null)
            {
                _asignaturas= new AsignaturaRepository(_context);
            }
            return _asignaturas;
        }
    }

    public ICursoEscolar CursoEscolares 
    {
        get{
            if(_cursosEscolares== null)
            {
                _cursosEscolares= new CursoEscolarRepository(_context);
            }
            return _cursosEscolares;
        }
    }

    public IDepartamento Departamentos
    {
        get{
            if(_Departamentos== null)
            {
                _Departamentos= new DepartamentoRepository(_context);
            }
            return _Departamentos;
        }
    }

    public IGrado Grados 
    {
        get{
            if(_grados== null)
            {
                _grados= new GradoRepository(_context);
            }
            return _grados;
        }
    }

    public IMatricula Matriculas 
    {
        get{
            if(_matriculas== null)
            {
                _matriculas= new MatriculaRepository(_context);
            }
            return _matriculas;
        }
    }

    public IPersona Personas 
    {
        get{
            if(_personas== null)
            {
                _personas= new PersonaRepository(_context);
            }
            return _personas;
        }
    }

    public IProfesor Profesores 
    {
        get{
            if(_profesores== null)
            {
                _profesores= new ProfesorRepository(_context);
            }
            return _profesores;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
