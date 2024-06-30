using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Core.Data;
using Mini_Core.ViewModel;
using System.Runtime.CompilerServices;

namespace Mini_Core.Controllers
{
    public class Tareas : Controller
    {
        private readonly ApplicationDbContext _context;

        public Tareas(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult FiltrarPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                ModelState.AddModelError("", "La fecha de inicio no puede ser mayor que la fecha de fin.");
                return RedirectToAction("Index"); // Redireccionamos al Index si hay error de fechas
            }

            ViewData["fechaInicio"] = fechaInicio.ToShortDateString();
            ViewData["fechaFin"] = fechaFin.ToShortDateString();

            var tareasFiltradas = _context.Tareas
                                        .Where(t => t.EstadoProgreso == "En progreso" &&
                                                    t.FechadeInicio <= fechaFin &&
                                                    t.FechadeInicio.AddDays(t.tiempoestimado) >= fechaInicio)
                                        .Select(t => new TareasVM
                                        {
                                            Id = t.Id,
                                            Nombredelatarea = t.Nombredelatarea,
                                            FechadeInicio = t.FechadeInicio,
                                            tiempoestimado = t.tiempoestimado,
                                            EstadoProgreso = t.EstadoProgreso,
                                            ProyectoName = t.proyecto.Name,
                                            EmpleadoName = t.empleado.Name
                                        })
                                        .ToList();

            foreach (var tarea in tareasFiltradas)
            {
                DateTime fechaEstimadaFinal = tarea.FechadeInicio.AddDays(tarea.tiempoestimado);
                if (fechaEstimadaFinal < fechaFin)
                {
                    int diasRetraso = Math.Abs((fechaEstimadaFinal - fechaFin).Days);
                    tarea.EstadoProgreso += $" (Retrasado por {diasRetraso} días)";
                }
            }

            return View("Index", tareasFiltradas);
        }


        public IActionResult Index()
                {
                    var tareas = _context.Tareas
                                        .Select(t => new TareasVM
                                        {
                                            Id = t.Id,
                                            Nombredelatarea = t.Nombredelatarea,
                                            FechadeInicio = t.FechadeInicio,
                                            tiempoestimado = t.tiempoestimado,
                                            EstadoProgreso = t.EstadoProgreso,
                                            ProyectoName = t.proyecto.Name,
                                            EmpleadoName = t.empleado.Name
                                        })
                                        .ToList();

                    return View(tareas);

                }
            }
        }
