# TecnoHogar — Portal de Solicitudes de Servicio Técnico
### Evaluación Continua 1 — 2026-2 | .NET MVC + EF Core + SQLite | GitHub Flow + Render

Portal interno de **TecnoHogar** (instalación, mantenimiento, revisión y fugas de gas) que reemplaza los pedidos por WhatsApp/papel. Dos operaciones: **Insert (registro)** y **Select (listado)** persistidas en **SQLite** con **EF Core**.

**Repo:** `evaluacion20262` | **Stack:** ASP.NET Core MVC (.NET 9, compatible con .NET 10), EF Core 9 + SQLite, Bootstrap 5 + AOS + animaciones premium.

---

## URLs Entregables

| Entregable | URL |
|---|---|
| **Repositorio GitHub** (con ramas, PRs y merges visibles) | `https://github.com/<tu-usuario>/evaluacion20262` → Reemplazar tras `git push` |
| **Aplicación en Render** (URL pública) | `https://evaluacion20262.onrender.com` → Se genera al desplegar en Render |

> Después de hacer push y deploy, actualiza estas URLs. Instrucciones abajo.

---

## Flujo Git/GitHub Evaluado (5/5)

Ramas mínimas exigidas:

- `main` — producción
- `develop` — integración
- `feature/modelo-sqlite` → Pregunta 1
- `feature/registro-solicitud` → Pregunta 2
- `feature/listado-solicitudes` → Pregunta 3

**Historial realizado:**

```bash
feat: inicializar proyecto base ASP.NET Core MVC (.NET 9)
feat: configurar EF Core con SQLite, entidad SolicitudServicio y migracion inicial  (feature/modelo-sqlite)
Merge pull request #1: feature/modelo-sqlite -> develop
feat: implementar registro de solicitudes (Insert) con validacion y SaveChanges   (feature/registro-solicitud)
Merge pull request #2: feature/registro-solicitud -> develop
feat: implementar listado de solicitudes (Select) con LINQ ToListAsync y tabla ordenada (feature/listado-solicitudes)
Merge pull request #3: feature/listado-solicitudes -> develop
feat: UI premium animada, responsive y profesional (TecnoHogar)
Merge develop -> main   (final)
```

Verificar: `git log --oneline --graph --all` (historial ordenado, merges --no-ff).

**Cómo replicar en GitHub (PRs visibles):**

1. Crear repo vacío en GitHub `evaluacion20262` (sin init).
2. `git remote add origin https://github.com/<tu-usuario>/evaluacion20262.git`
3. `git push -u origin main && git push -u origin develop`
4. Para cada feature: `git push -u origin feature/...` → en GitHub: *Compare & pull request* → base `develop` → *Create PR* → *Merge pull request* (3 PRs).
5. Final: PR `develop` → `main` → Merge.
6. Las URLs de PRs quedarán visibles en la pestaña *Pull requests*.

---

## Modelo de Datos

**Entidad `SolicitudServicio`** (`Models/SolicitudServicio.cs`):

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

**DbContext** `Data/AppDbContext.cs` → `DbSet<SolicitudServicio> Solicitudes`

**appsettings.json:**
```json
"ConnectionStrings": { "DefaultConnection": "Data Source=tecnogas.db" }
```

**Program.cs:**
```csharp
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(conn));
db.Database.Migrate(); // aplica migraciones al iniciar (Render)
```

Migración: `dotnet ef migrations add Inicial` → crea `tecnogas.db` y tabla `Solicitudes`.

---

## Funcionalidades

### Registro (Insert) — `Solicitudes/Crear`
- Formulario con `Cliente, Telefono, Distrito, TipoServicio (select), Descripcion` + validaciones `[Required]` y `ModelState`
- `Add + SaveChangesAsync`, `FechaRegistro = DateTime.Now`, `TempData` + redirect
- Validación cliente con `_ValidationScriptsPartial`

### Listado (Select) — `Solicitudes/Index`
- `await _context.Solicitudes.OrderByDescending(s => s.FechaRegistro).ToListAsync()`
- Tabla premium con badges por tipo, orden por fecha, stats (total / emergencias / hoy / distritos), vista mobile cards y empty-state
- Refleja inmediatamente los inserts de la Pregunta 2

---

## Ejecutar Local

```bash
dotnet restore
dotnet ef database update   # o deja que Program.cs haga Migrate
dotnet run                  # https://localhost:7137 / http://localhost:5265
# Crear: /Solicitudes/Crear  |  Listar: /Solicitudes
```

---

## Despliegue en Render

### Opción A: Docker (recomendado)

Render → *New → Web Service* → conectar repo GitHub → **Environment: Docker** → Dockerfile en raíz.

**Variables de entorno en Render:**

| Key | Value |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ASPNETCORE_URLS` | `http://+:8080` |
| `ConnectionStrings__DefaultConnection` | `Data Source=/app/tecnogas.db` (o dejar `tecnogas.db` — SQLite relativo) |

Dockerfile ya expone `8080` (requerido por Render). El `Database.Migrate()` garantiza creación de BD en el container.

> Render filesystem es efímero: SQLite se resetea en cada deploy. Para persistencia real usar *Disk* o Postgres; aquí se cumple el requisito académico.

### Opción B: Build nativo (sin Docker)

- **Build Command:** `dotnet publish -c Release -o out`
- **Start Command:** `dotnet out/evaluacion20262.dll`
- **Environment:** `DOTNET` 9

### Verificación post-deploy

- `https://<tu-app>.onrender.com/Solicitudes/Crear` → registrar → éxito
- `https://<tu-app>.onrender.com/Solicitudes` → listado con registro reciente

---

## Estructura

```
evaluacion20262/
├── Controllers/SolicitudesController.cs (Index ToListAsync + Crear GET/POST)
├── Models/SolicitudServicio.cs
├── Data/AppDbContext.cs
├── Migrations/ (Inicial)
├── Views/Home/Index.cshtml (landing premium)
├── Views/Solicitudes/Crear.cshtml + Index.cshtml
├── wwwroot/css/site.css (animaciones, glassmorphism, gradients)
├── wwwroot/js/site.js (contadores, tilt 3D)
├── Dockerfile + .dockerignore
├── appsettings.json (ConnectionStrings)
└── Program.cs (UseSqlite + Migrate)
```

---

## Créditos

Diseño premium: gradientes, glassmorphism, AOS scroll, floating cards, badges por tipo de servicio, micro-animaciones y tilt 3D. Interactivo y totalmente responsive.

**Autor:** Estudiante EC1 — 2026-2
