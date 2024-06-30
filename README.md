# Proyecto ASP.NET MVC: Filtrado por Fechas

Este proyecto es un ejemplo de una aplicación ASP.NET MVC que muestra cómo filtrar y calcular datos dentro de un rango de fechas. A continuación se explican los pasos para crear este proyecto desde cero, así como los enlaces de referencia y contacto.

## Descripción

En este proyecto, se crea una aplicación web que permite gestionar tareas y filtrarlas por un rango de fechas. También se calcula si las tareas están retrasadas y muestra cuántos días de retraso tienen.

## Requisitos

- Visual Studio 2019 o superior
- .NET Core 3.1 o superior
- SQL Server o cualquier base de datos compatible con Entity Framework

## Instalación

1. Clona el repositorio en tu máquina local:

    ```sh
    git clone https://github.com/feli203040/Video-Explicativo-C-.NET.git
    ```

2. Abre el proyecto en Visual Studio.

3. Configura la cadena de conexión en `appsettings.json` para tu base de datos:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=nombre_de_tu_base_de_datos;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*"
    }
    ```

4. Restaura las dependencias y construye el proyecto:

    ```sh
    dotnet restore
    dotnet build
    ```

5. Aplica las migraciones para configurar la base de datos:

    ```sh
    dotnet ef database update
    ```

6. Ejecuta la aplicación:

    ```sh
    dotnet run
    ```

## Uso

Al iniciar la aplicación, se mostrará la lista de tareas filtradas por un rango de fechas predeterminado. Puedes cambiar el rango de fechas utilizando el formulario de filtrado.

### Filtrado por Fechas

- Selecciona las fechas de inicio y fin en el formulario y presiona "Filtrar" para actualizar la lista de tareas.

### Cálculo de Retrasos

- La aplicación calcula si una tarea está retrasada con respecto a la fecha final seleccionada y muestra los días de retraso.

## Estructura del Proyecto

- **Controllers/TareasController.cs:** Controlador que maneja la lógica del filtrado de tareas.
- **Models/Tarea.cs:** Modelo que representa una tarea.
- **Views/Tareas/Index.cshtml:** Vista que muestra la lista de tareas y el formulario de filtrado.
- **appsettings.json:** Archivo de configuración para la cadena de conexión de la base de datos.

## Referencias

- [Documentación de ASP.NET MVC](https://docs.microsoft.com/en-us/dotnet/framework/)
- [Repositorio en GitHub]([(https://github.com/Guiller438/Mini_Core.git)])
- [Enlace del Proyecto]()

## Contactos

- **Email Institucional:** guillermo.alvarez@udla.edu.ec

## Video Tutorial

En este video se explica lo básico para crear un proyecto MVC .NET desde cero y adicionalmente se muestra cómo filtrar y calcular datos dentro de un rango de fechas.

- [Video en YouTube]()
