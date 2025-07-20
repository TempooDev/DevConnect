# 🏗️ DevConnect - Arquitectura Clean Architecture (Sin MediatR)

Este proyecto está estructurado siguiendo los principios de **Clean Architecture**, orientada a **casos de uso** y sin usar MediatR. Se prioriza una separación clara de responsabilidades, mantenibilidad y testabilidad.

---

## 🔄 Flujo de dependencias

Las dependencias fluyen **de fuera hacia adentro**:

[ ApiService ]
↓
[ Infrastructure ]
↓
[ Application ]
↓
[ Domain ]


---

## 🧱 Capas del proyecto

### 📁 DevConnect.Domain

> Núcleo puro del negocio

- Contiene las **entidades**, **objetos de valor**, **enumeraciones** y **lógica de negocio inmutable**.
- No depende de ninguna otra capa ni de frameworks (EF Core, ASP.NET, etc).

**Ejemplos:**
- `User.cs`
- `Email.cs` (Value Object)
- `UserRole.cs` (Enum)

---

### 📁 DevConnect.Application

> Casos de uso y contratos de negocio

- Contiene la **lógica de aplicación**: servicios, casos de uso, interfaces de repositorios.
- Depende de `Domain`.
- Define **qué necesita** el sistema, pero no cómo se implementa.

**Ejemplos:**
- `IUserRepository.cs`
- `RegisterUserService.cs`
- `LoginHandler.cs`

---

### 📁 DevConnect.Infrastructure

> Implementación de servicios y persistencia

- Implementa las interfaces definidas en `Application`.
- Usa herramientas concretas como **Entity Framework**, **HTTP clients**, **file system**, etc.
- Depende de `Application`, `Domain` y `Shared`.

**Ejemplos:**
- `UserRepository.cs` (implementa `IUserRepository`)
- `AppDbContext.cs`
- `EmailService.cs` (envío real de emails)

**Por qué depende de `Domain`:**
Para acceder a las entidades y value objects que persiste o transforma.

**Por qué depende de `Application`:**
Para implementar las interfaces que esta define.

---

### 📁 DevConnect.ApiService

> Punto de entrada de la aplicación (API)

- Contiene los **controladores**, configuración de ASP.NET Core y registro de dependencias.
- **No contiene lógica de negocio**.
- Conecta las capas `Application` e `Infrastructure` mediante **inyección de dependencias**.

**Ejemplos:**
- `UserController.cs`
- `Program.cs`
- `DependencyInjection.cs` (extensión para registrar servicios)

**Por qué depende de `Application`:**
Para invocar casos de uso.

**Por qué depende de `Infrastructure`:**
Para registrar las implementaciones concretas (repositorios, servicios, EF, etc).

---

## ✅ Principios respetados

- **Inversión de dependencias (DIP):** `Application` depende de interfaces, `Infrastructure` las implementa.
- **Separación de responsabilidades:** Cada capa tiene una función clara.
- **Independencia tecnológica:** Las capas internas no conocen EF Core, ASP.NET ni SQL.
- **Testabilidad:** `Application` puede testearse con mocks/fakes porque no tiene dependencias concretas.

---

## 🚫 Lo que evitamos

- ❌ Acoplar `Application` a `Infrastructure`
- ❌ Acceder a `Infrastructure` desde los controladores
- ❌ Lógica de negocio en la API
- ❌ Llamadas directas a servicios concretos sin interfaces

---

## 🧪 Opcional: Testing

- Puedes añadir un proyecto `DevConnect.Application.Tests` que testee los casos de uso de forma aislada.
- `Infrastructure` se puede testear con integración (EF Core InMemory, etc).

---

## 🗂️ Resumen de referencias entre proyectos

| Proyecto         | Referencia a...                          |
|------------------|-------------------------------------------|
| `Domain`         | Ninguno                                   |
| `Application`    | `Domain`, `Shared`                        |
| `Infrastructure` | `Application`, `Domain`, `Shared`         |
| `ApiService`     | `Application`, `Infrastructure`, `Shared` |

---

## 🧩 Opcional: Capas adicionales

- `DevConnect.Shared`: Clases comunes (DTOs, resultados, errores)
- `DevConnect.Tests`: Proyecto de tests por capas

---

## 📌 Recomendación

Puedes extender esta plantilla para otros proyectos manteniendo la misma estructura. Esto asegura mantenibilidad, escalabilidad y una experiencia coherente para el equipo.

s