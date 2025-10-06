#  Library -- Sistema de Gestión de Biblioteca

Proyecto ASP.NET Core MVC para gestionar libros, usuarios y préstamos.

##  Tecnologías usadas

-   **ASP.NET Core MVC (C#)**\
-   **Entity Framework Core (Code First)**\
-   **MySQL** como base de datos\
-   **Bootstrap** para estilos

------------------------------------------------------------------------

##  Estructura principal

    Library/
     ├── Controllers/
     │   ├── BookController.cs
     │   ├── UserController.cs
     │   └── LoanController.cs
     |   └── History.cs
     
     │
     ├── Models/
     │   ├── Book.cs
     │   ├── User.cs
     │   ├── Loan.cs
     │   └── History.cs
     │
     ├── Infrastructure/
     │   └── AppDbContext.cs
     │
     ├── Views/
     │   ├── Book/
     │   ├── User/
     │   └── Loan/
     │
     └── wwwroot/

------------------------------------------------------------------------

##  Funcionalidades

###  Gestión de Libros

-   Registrar libros con: **Título, Autor, Código, Ejemplares
    Disponibles**
-   Validar que el código sea **único**
-   Listar todos los libros disponibles
-   Actualizar cantidad disponible al prestar o devolver un libro

###  Gestión de Usuarios

-   Registrar, editar y listar usuarios
-   Cada usuario puede tener múltiples préstamos activos

### Préstamos (Loans)

-   Crear un préstamo solo si hay ejemplares disponibles
-   Disminuir el stock al prestar
-   Incrementar stock al devolver
-   Historial de préstamos por usuario o por libro

###  Historial (History)

-   Registro automático cada vez que un usuario devuelve un libro

------------------------------------------------------------------------

##  Configuración

1.  Crear base de datos en MySQL (por ejemplo `librarydb`)

2.  En el archivo `appsettings.json`, configurar la cadena de conexión:

    ``` json
    "ConnectionStrings": {
        "DefaultConnection": "server=localhost;database=librarydb;user=root;password=tu_clave;"
    }
    ```

3.  Ejecutar los comandos:

    ``` bash
    dotnet ef migrations add Initial
    dotnet ef database update
    ```

4.  Compilar y ejecutar:

    ``` bash
    dotnet build
    dotnet run
    ```

------------------------------------------------------------------------

##  Notas

-   Se utiliza `TempData` para mostrar mensajes temporales (como errores
    o confirmaciones)
-   Todos los controladores usan `AppDbContext` desde `Infrastructure`
-   El proyecto está estructurado siguiendo el patrón MVC

------------------------------------------------------------------------

