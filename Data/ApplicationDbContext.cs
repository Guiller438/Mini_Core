using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mini_Core.Models;
using System;

namespace Mini_Core.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Empleado> Empleados{ get; set; }

        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Tareas> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed para la entidad Person
            builder.Entity<Empleado>().HasData(
                new Empleado
                {
                    id = 1,
                    Name = "Juan",
                    LastName = "Pérez"
                },
                new Empleado
                {
                    id = 2,
                    Name = "María",
                    LastName = "González"
                },
                new Empleado
                {
                    id = 3,
                    Name = "Carlos",
                    LastName = "Martínez"
                },
                new Empleado
                {
                    id=4,
                    Name = "Guillermo",
                    LastName = "Alvarez"
                },
                new Empleado
                {
                    id=5,
                    Name = "Jair",
                    LastName = "Torres",
                }

            );

            builder.Entity<Proyecto>().HasData(
               new Proyecto
               {
                   Id = 1,
                   Name = "Proyecto A"
               },
               new Proyecto
               {
                   Id = 2,
                   Name = "Proyecto B"
               },
               new Proyecto
               {
                   Id = 3,
                   Name = "Proyecto C"
               },
               new Proyecto
               {
                   Id=4,
                   Name="Proyecto E"
               }
           );

            builder.Entity<Tareas>().HasData(
                new Tareas
                {
                    Id = 1,
                    Nombredelatarea = "Tarea 1",
                    FechadeInicio = new DateTime(2024, 3, 1), // Fecha específica
                    tiempoestimado = 5.5,
                    EstadoProgreso = "En progreso",
                    proyectoId = 1,
                    empleadoId = 1
                },
                new Tareas
                {
                    Id = 2,
                    Nombredelatarea = "Tarea 2",
                    FechadeInicio = new DateTime(2024, 2, 2), // Fecha específica
                    tiempoestimado = 7.5,
                    EstadoProgreso = "Completado",
                    proyectoId = 2,
                    empleadoId = 2
                },
                new Tareas
                {
                    Id = 3,
                    Nombredelatarea = "Tarea 3",
                    FechadeInicio = new DateTime(2024, 1, 3), // Fecha específica
                    tiempoestimado = 3.0,
                    EstadoProgreso = "En espera",
                    proyectoId = 1,
                    empleadoId = 3
                },
                new Tareas
                {
                    Id = 4,
                    Nombredelatarea = "Tarea 4",
                    FechadeInicio = new DateTime(2024, 3, 4), // Fecha específica
                    tiempoestimado = 4.5,
                    EstadoProgreso = "En progreso",
                    proyectoId = 4,
                    empleadoId = 1
                },
                new Tareas
                {
                    Id = 5,
                    Nombredelatarea = "Tarea 5",
                    FechadeInicio = new DateTime(2024, 6, 5), // Fecha específica
                    tiempoestimado = 6.0,
                    EstadoProgreso = "Completado",
                    proyectoId = 5,
                    empleadoId = 4
                },
                new Tareas
                {
                    Id = 6,
                    Nombredelatarea = "Tarea 6",
                    FechadeInicio = new DateTime(2024, 5, 6), // Fecha específica
                    tiempoestimado = 2.5,
                    EstadoProgreso = "En progreso",
                    proyectoId = 2,
                    empleadoId = 2
                },
                new Tareas
                {
                    Id = 7,
                    Nombredelatarea = "Tarea 7",
                    FechadeInicio = new DateTime(2024, 2, 7), // Fecha específica
                    tiempoestimado = 8.0,
                    EstadoProgreso = "En espera",
                    proyectoId = 1,
                    empleadoId = 3
                },
                new Tareas
                {
                    Id = 8,
                    Nombredelatarea = "Tarea 8",
                    FechadeInicio = new DateTime(2024, 1, 8), // Fecha específica
                    tiempoestimado = 4.0,
                    EstadoProgreso = "En progreso",
                    proyectoId = 4,
                    empleadoId = 4
                },
                new Tareas
                {
                    Id = 9,
                    Nombredelatarea = "Tarea 9",
                    FechadeInicio = new DateTime(2024, 2, 9), // Fecha específica
                    tiempoestimado = 5.5,
                    EstadoProgreso = "Completado",
                    proyectoId = 2,
                    empleadoId = 1
                },
                new Tareas
                {
                    Id = 10,
                    Nombredelatarea = "Tarea 10",
                    FechadeInicio = new DateTime(2024, 10, 10), // Fecha específica
                    tiempoestimado = 7.0,
                    EstadoProgreso = "En progreso",
                    proyectoId = 3,
                    empleadoId = 2
                }
            );



        }

    }
}
