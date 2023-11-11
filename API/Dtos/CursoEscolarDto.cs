using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace API.Dtos;
    public class CursoEscolarDto  :BaseEntity
{
    public DateOnly AñoInicio {get;set;}
    public DateOnly AñoFin {get;set;}

}
