# DevConnect - Sistema de Autenticación Blazor

## 🚀 Sistema Implementado

Se ha implementado un sistema completo de autenticación en Blazor Server que integra con la API CQRS de DevConnect.

### ✅ Componentes Implementados

#### **Backend (API)**

- ✅ Patrón CQRS con Commands y Queries
- ✅ JWT Bearer Authentication
- ✅ Role-based Authorization
- ✅ Endpoints de autenticación y gestión de roles

#### **Frontend (Blazor)**

- ✅ Sistema de autenticación con JWT
- ✅ Login y registro de usuarios
- ✅ Persistencia de token en localStorage
- ✅ Estado de autenticación global
- ✅ Vista temporal para testing de roles
- ✅ Navegación con autenticación
- ✅ **Integración con DTOs de Shared**
- ✅ **HttpClient configurado como WeatherApiClient**

### 📁 Archivos Creados

#### **Servicios:**

- ~~`Services/AuthDtos.cs`~~ - Eliminado, se usan DTOs de Shared
- `Services/ITokenStorage.cs` - Gestión de tokens en localStorage
- `Services/JwtAuthenticationStateProvider.cs` - Proveedor de estado de autenticación
- `Services/AuthApiService.cs` - Cliente HTTP para API (configurado como WeatherApiClient)

#### **Páginas:**
- `Components/Pages/Login.razor` - Vista de inicio de sesión
- `Components/Pages/Register.razor` - Vista de registro
- `Components/Pages/RolesTest.razor` - Vista temporal para testing

#### **Layout:**
- Actualizado `Components/Layout/NavMenu.razor` - Navegación con autenticación
- Actualizado `Components/App.razor` - Configuración de autenticación

#### **Configuración:**
- Actualizado `Program.cs` - Registro de servicios de autenticación

## 🔧 Cómo Usar

### 1. **Iniciar los Servicios**

```bash
# Terminal 1: API
cd "c:\dev\DevConnect\DevConnect\src\Api\DevConnect.ApiService"
dotnet run

# Terminal 2: Web
cd "c:\dev\DevConnect\DevConnect\src\Front\DevConnect.Web"  
dotnet run
```

### 2. **URLs de la Aplicación**

- **Blazor Web**: `http://localhost:5000` (o el puerto que se asigne)
- **API**: `http://localhost:5257`

### 3. **Flujo de Testing**

#### **Registro de Usuario:**
1. Ve a `/register`
2. Registra: `admin@devconnect.com` / `Password123!`
3. El sistema te loguea automáticamente

#### **Login:**
1. Ve a `/login`
2. Usa las credenciales registradas
3. Serás redirigido al home

#### **Test de Roles:**
1. Estando logueado, ve a `/roles`
2. Haz clic en "Get Available Roles" para probar el endpoint
3. Usa "Change User Role" para cambiar roles (solo admins)

### 4. **Usuarios de Prueba Sugeridos**

```json
// Admin User
{
  "email": "admin@devconnect.com",
  "password": "Password123!"
}

// Regular User  
{
  "email": "user@devconnect.com",
  "password": "Password123!"
}
```

## 🛠 Funcionalidades

### **Sistema de Autenticación:**
- ✅ Registro automático con JWT
- ✅ Login con validación
- ✅ Logout con limpieza de token
- ✅ Persistencia de sesión en localStorage
- ✅ Estado global de autenticación

### **Seguridad:**
- ✅ JWT Bearer tokens
- ✅ Authorization basada en roles
- ✅ Rutas protegidas con `<AuthorizeView>`
- ✅ Headers de autorización automáticos

### **Testing de API:**
- ✅ Vista temporal `/roles` para probar endpoints
- ✅ Test de `GET /api/user-roles/available-roles`
- ✅ Test de `PUT /api/user-roles/change-role`
- ✅ Manejo de errores y estados de carga

## 🎯 Próximos Pasos

1. **Mejorar UX:** Agregar toast notifications
2. **Validaciones:** Mejorar validaciones de formularios
3. **Seguridad:** Implementar refresh tokens
4. **Testing:** Agregar tests unitarios
5. **Cleanup:** Remover vista temporal de roles

## 📋 Comandos de Testing

### **Con la API de testing HTTP:**
```http
# Ver archivo: src/Api/DevConnect.ApiService/cqrs-requests.http
POST http://localhost:5257/api/auth/register
POST http://localhost:5257/api/auth/login  
GET http://localhost:5257/api/user-roles/available-roles
PUT http://localhost:5257/api/user-roles/change-role
```

### **Con la interfaz Blazor:**
1. Navega a las páginas usando el menú
2. Usa los formularios para interactuar
3. Observa los cambios de estado en tiempo real

## ✨ Características Implementadas

- **Responsive Design** con Bootstrap
- **Loading States** en botones y operaciones
- **Error Handling** con mensajes claros
- **Navigation Integration** con estado de auth
- **Role-based UI** que muestra/oculta elementos según permisos
- **Clean Architecture** siguiendo principios SOLID

¡El sistema está listo para usar y probar! 🎉
