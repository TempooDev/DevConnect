# DevConnect - Documentación

Esta carpeta contiene toda la documentación del proyecto DevConnect.

## 📚 Documentación Disponible

### 🏗️ Arquitectura

- **[Architecture.md](./Architecture.md)** - Documentación de la arquitectura general del sistema

### 🔐 Autenticación

- **[BLAZOR_AUTH_GUIDE.md](./BLAZOR_AUTH_GUIDE.md)** - Guía completa del sistema de autenticación Blazor con CQRS

## 🚀 Inicio Rápido

Para comenzar con el proyecto:

1. **Leer la arquitectura:** [Architecture.md](./Architecture.md)
2. **Configurar autenticación:** [BLAZOR_AUTH_GUIDE.md](./BLAZOR_AUTH_GUIDE.md)
3. **Ejecutar el sistema:**

   ```bash
   # API
   cd src/Api/DevConnect.ApiService
   dotnet run

   # Blazor Web
   cd src/Front/DevConnect.Web
   dotnet run
   ```

## 📋 Contenido por Tema

### Backend/API

- Patrón CQRS implementado
- JWT Bearer Authentication
- Role-based Authorization
- Clean Architecture

### Frontend/Blazor

- Sistema de autenticación completo
- Integración con localStorage
- Estados de autenticación global
- Navegación contextual

### Testing

- Endpoints HTTP de prueba
- Vista temporal de roles
- Flujos de autenticación completos

---

**Nota:** Esta documentación se actualiza continuamente conforme el proyecto evoluciona.
