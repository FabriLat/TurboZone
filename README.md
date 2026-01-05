TurboZone API

¡Bienvenidos a TurboZone! Una API RESTful pensada para los apasionados de los autos deportivos, que permite gestionar usuarios, vehículos, imágenes y comentarios con un diseño escalable y buenas prácticas de desarrollo.

Descripción:


TurboZone es un proyecto personal donde puse en práctica conceptos avanzados de backend. La API incluye:


Gestión de usuarios con roles (Client, Moderator, SysAdmin).
Administración de vehículos con soporte para hasta 6 imágenes por vehículo (optimizadas para devolver una por consulta).
Creación y manejo de comentarios asociados a vehículos.

Actualizacion TurboZone v2:

Se incluyeron nuevas funcionalidades para enriquecer la experiencia del usuario y el detalle de los vehículos:

  -Especificaciones de vehículos: ahora cada vehículo puede contar con información técnica detallada como motor, torque, potencia, consumo, aceleración, cantidad de puertas, asientos y peso.
  
  -Features de vehículos: se agregó un sistema de características predefinidas que se muestran en una lista con checkboxes al momento de crear un vehículo, permitiendo seleccionar las features disponibles.
  
  -Sistema de likes: los usuarios autenticados pueden dar y quitar like a los vehículos.
  
  -Registro de visitas: se implementó un sistema de conteo de visualizaciones por vehículo, incluyendo visitas anónimas y de usuarios autenticados.



Tecnologías:


Lenguaje: C# con ASP.NET Core 

ORM: Entity Framework Core (enfoque Code First)

Base de datos: MySQL

Autenticación: JWT con políticas de autorización por rol



Arquitectura:


Clean Architecture: Organizada en capas (Domain, Application, Infrastructure, Web) para un código modular y mantenible.


Patrones de diseño:

DTO: Respuestas dinámicas según el rol del usuario.

Repository genérico: Acceso a datos flexible con tipos genéricos.

Dependency Injection: Desacoplamiento con inyección de dependencias.


Características principales:


Rutas RESTful

Seguridad: Autenticación JWT y políticas de autorización para clientes, moderadores y administradores.

Documentación: Endpoints documentados con swagger. 

Gestión de imágenes: Hasta 6 imágenes por vehículo, optimizadas en consultas.

Code First: Entidades modeladas en código y base generada con migraciones de EF.
