# TecnoHogar — Portal de Solicitudes de Servicio Técnico

Portal interno para gestionar solicitudes de instalación, mantenimiento, revisión y fugas de gas. Reemplaza el flujo por WhatsApp/papel con un sistema digital trazable.

**Stack:** ASP.NET Core MVC (.NET 9) · EF Core + SQLite · Bootstrap 5 + AOS

**Demo:** https://evaluacion20262.onrender.com

## Funcionalidades

### Registro de Solicitud
Formulario con `Cliente`, `Teléfono`, `Distrito`, `Tipo de Servicio` y `Descripción`. Validación con `DataAnnotations` y `ModelState`, guardado con `Add` + `SaveChangesAsync`.

### Listado de Solicitudes
Consulta con `OrderByDescending(s => s.FechaRegistro).ToListAsync()`, tabla con badges por tipo de servicio, estadísticas y vista responsive.

## Modelo de Datos

```csharp
public class SolicitudServicio
{
    public int Id { get; set; }
    [Required] public string Cliente { get; set; }
    [Required] public string Telefono { get; set; }
    [Required] public string Distrito { get; set; }
    [Required] public string TipoServicio { get; set; } // Instalación, Mantenimiento, Revisión, Fuga
    public string Descripcion { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
```

`Data/AppDbContext` → `DbSet<SolicitudServicio> Solicitudes` · `appsettings.json` → `Data Source=tecnogas.db` · `Program.cs` aplica migraciones con `Database.Migrate()`.

## Ejecutar Local

```bash
dotnet restore
dotnet ef database update
dotnet run
# http://localhost:5265
# /Solicitudes/Crear  /Solicitudes
```

## Despliegue

**Docker (Render):** `Dockerfile` expone `8080`, `ASPNETCORE_URLS=http://+:8080`.

**Build nativo:** `dotnet publish -c Release -o out` → `dotnet out/evaluacion20262.dll`

## Estructura

```
Controllers/SolicitudesController.cs  # Index + Crear GET/POST
Models/SolicitudServicio.cs
Data/AppDbContext.cs
Migrations/
Views/Home/Index.cshtml
Views/Solicitudes/Crear.cshtml  Index.cshtml
wwwroot/css/site.css  js/site.js
Dockerfile
```

Diseño premium con gradientes, glassmorphism, AOS y micro-animaciones.
