# GimnasioApp — Sistema de Control de Membresías

Aplicación web desarrollada en **ASP.NET Core 10 MVC** con arquitectura de 5 capas.

---

## Arquitectura

```
GimnasioApp.Model   → Entidad Membresia (tabla SQL)
GimnasioApp.DA      → DbContext + Patrón Repositorio (EF Core)
GimnasioApp.BL      → Lógica de negocio y cálculos
GimnasioApp.API     → REST API con autenticación por API Key
GimnasioApp.UI      → Interfaz web MVC que consume la API vía HttpClient
```

---

## Requisitos previos

- Visual Studio 2022/2026 (o VS Code con extensión C#)
- .NET 10 SDK
- SQL Server Express con instancia `.\SQLEXPRESS`
- Postman (para probar los endpoints de la API)

---


### 1. URL de la API (UI)

Edita `GimnasioApp.UI/appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001",
    "ApiKey": "gimnasio-api-key-2026"
  }
}
```

El `BaseUrl` debe coincidir con el puerto que arrancó la API (revisa `GimnasioApp.API/Properties/launchSettings.json`).

---

## Ejecución

### En Visual Studio

1. Click derecho en la solución → **Propiedades**
2. En **Startup Projects** selecciona **Multiple startup projects**
3. Pon ambos (`GimnasioApp.API` y `GimnasioApp.UI`) en **Start**
4. Presiona **F5**

### En terminal (dos terminales separadas)

```bash
# Terminal 1 — API
cd GimnasioApp.API
dotnet run

# Terminal 2 — UI
cd GimnasioApp.UI
dotnet run
```

---


## API Key

Todas las rutas de la API requieren el header:

```
X-Api-Key: gimnasio-api-key-2026
```

Las pruebas de los endpoints se realizan exclusivamente con **Postman** (no se expone Swagger/OpenAPI). Ejemplo de configuración en Postman:

| Endpoint | Método | URL |
|----------|--------|-----|
| Listar todas | GET | `https://localhost:7001/api/membresias?identificacion=` |
| Activas | GET | `https://localhost:7001/api/membresias/activas?identificacion=` |
| Vencidas | GET | `https://localhost:7001/api/membresias/vencidas?identificacion=` |
| Detalle | GET | `https://localhost:7001/api/membresias/{id}` |
| Agregar | POST | `https://localhost:7001/api/membresias` |
| Editar | PUT | `https://localhost:7001/api/membresias/{id}` |

En cada solicitud, agrega en **Headers** la clave `X-Api-Key` con el valor `gimnasio-api-key-2026`.

---
