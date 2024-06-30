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

            // Obtener las tareas básicas incluyendo las entidades relacionadas
            var tareasBasicas = _context.Tareas
                                        .Include(t => t.proyecto) // Incluir el proyecto relacionado
                                        .Include(t => t.empleado) // Incluir el empleado relacionado
                                        .Where(t => t.EstadoProgreso == "En progreso" && t.FechadeInicio <= fechaFin)
                                        .ToList();

            var tareasFiltradas = tareasBasicas
                                     .Where(t => CalcularFechaEstimadaFinal(t.FechadeInicio, t.tiempoestimado) >= fechaInicio)
                                     .Select(t => new TareasVM
                                     {
                                         Id = t.Id,
                                         Nombredelatarea = t.Nombredelatarea,
                                         FechadeInicio = t.FechadeInicio,
                                         tiempoestimado = t.tiempoestimado,
                                         EstadoProgreso = t.EstadoProgreso,
                                         ProyectoName = t.proyecto?.Name, // Verificar si proyecto es null
                                         EmpleadoName = t.empleado?.Name  // Verificar si empleado es null
                                     })
                                     .ToList();

            foreach (var tarea in tareasFiltradas)
            {
                DateTime fechaEstimadaFinal = CalcularFechaEstimadaFinal(tarea.FechadeInicio, tarea.tiempoestimado);

                if (fechaEstimadaFinal < fechaFin)
                {
                    int diasRetraso = ObtenerDiasHabilesEntre(fechaEstimadaFinal, fechaFin);
                    tarea.EstadoProgreso += $" (Retrasado por {diasRetraso} días)";
                }
            }

            return View("Index", tareasFiltradas);
        }





        public static DateTime CalcularFechaEstimadaFinal(DateTime startDate, double totalDays)
        {
            DateTime currentDate = startDate;
            int days = (int)Math.Ceiling(totalDays);

            while (days > 0)
            {
                currentDate = currentDate.AddDays(1);

                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    days--;
                }
            }

            return currentDate;
        }

        public static int ObtenerDiasHabilesEntre(DateTime startDate, DateTime endDate)
        {
            int totalDiasHabiles = 0;
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    totalDiasHabiles++;
                }
                currentDate = currentDate.AddDays(1);
            }

            return totalDiasHabiles;
        }



    }
}

