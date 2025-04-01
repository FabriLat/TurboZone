TurboZone API

¡Bienvenidos a TurboZone! Una API RESTful pensada para los apasionados de los autos deportivos, que permite gestionar usuarios, vehículos, imágenes y comentarios con un diseño escalable y buenas prácticas de desarrollo.

Descripción:


TurboZone es un proyecto personal donde puse en práctica conceptos avanzados de backend. La API incluye:


Gestión de usuarios con roles (Client, Moderator, SysAdmin).
Administración de vehículos con soporte para hasta 6 imágenes por vehículo (optimizadas para devolver una por consulta).
Creación y manejo de comentarios asociados a vehículos.
El desarrollo se llevó a cabo en la rama development con un historial detallado, mientras que main contiene la versión "final" en un solo commit.



Tecnologías:


Lenguaje: C# con ASP.NET Core 

ORM: Entity Framework Core (enfoque Code First)

Base de datos: MySQL (migrada desde SQLite)

Autenticación: JWT con políticas de autorización por rol



Arquitectura:


Clean Architecture: Organizada en capas (Domain, Application, Infrastructure, Web) para un código modular y mantenible.


Patrones de diseño:

DTO: Respuestas dinámicas según el rol del usuario.

Repository genérico: Acceso a datos flexible con tipos genéricos.

Dependency Injection: Desacoplamiento con inyección de dependencias (AddScoped).


Características principales:


Rutas RESTful: Ejemplos: /api/users/{id}, /api/vehicles.

Seguridad: Autenticación JWT y políticas de autorización (ClientOnly, ModeratorAndSysAdmin, SysAdminOnly).

Gestión de imágenes: Hasta 6 imágenes por vehículo, optimizadas en consultas.

Code First: Entidades modeladas en código y base generada con migraciones de EF.
