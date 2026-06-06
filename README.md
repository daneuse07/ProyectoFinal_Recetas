**Chef Recetas** es una aplicación de escritorio desarrollada en **C# (Windows Forms)** bajo el patrón de arquitectura **Modelo-Vista-Controlador (MVC)**. El sistema está diseñado para centralizar y optimizar la administración de recetas culinarias, permitiendo un control estructurado de categorías, ingredientes individuales, pasos detallados de preparación y almacenamiento de imágenes (fotografías) en formato binario directo en la base de datos. 

Características Principales
*   **Gestión Integral de Recetas:** Registro completo que incluye tiempos de preparación, porciones y niveles de dificultad (`fácil`, `media`, `difícil`).
*   **Control de Almacenamiento BLOB:** Carga y lectura dinámica de archivos de imagen tanto para los ingredientes como para el resultado final de los platillos.
*   **Módulo de Autenticación Seguro:** Acceso restringido mediante inicio de sesión con encriptación de contraseñas en SHA-256 a nivel de servidor.
*   **Arquitectura MVC Separada por Capas:** Estructura limpia que independiza la interfaz de usuario (Views), la lógica de negocio (Controllers), el mapeo de objetos (Models) y el acceso a datos (DAL).
*   **Persistencia de Datos Relacional:** Conectividad directa y eficiente con un servidor MySQL mediante el conector nativo `MySql.Data`.

Requisitos de instalación:

Hardware: *Procesador Intel Core i3 o superior. *Memoria RAM de 4 GB o superior. *Espacio libre en disco de al menos 500 MB

Software: *Sistema Operativo Windows 10 o superior. *Visual Studio .NET 8 o Superior instalado. *MySQL Server. *MySQL Workbench.

DESCARGAR EL PROGRAMA RECETAS ESTRELLA Esta aplicación es un recetario de recetas donde puede ver los ingredientes, categorías y recetas junto con la información de cada una de ellas. Se puede descargar por medio del código de snl. Para que el usuario al que vaya dirigido el codigo solo necesite abrirlo en visual studio para poder ejecutarse Estos son los archivos que se van a descargar cuando inicies, abre el tercero para poder ver el código.

1. Descargar el proyecto Recetas Estrella.

2. Descargar el archivo de la base de datos proporcionado.

3. Instalar MySQL Server y MySQL Workbench.

4. Ejecutar el script de la base de datos en MySQL.

5. Abrir la solución del proyecto en Visual Studio. 6. Verificar la cadena de conexión a la base de datos.

6. Compilar y ejecutar la aplicación.

7. Iniciar sesión con una cuenta registrada o con la cuenta de administrador.

En el explorador de soluciones podrás ver los códigos de cada parte del código para así poder hacer modificaciones si es necesario. También es necesario descargar la base de datos del programa esta se ejecutará en MySQL

Una vez que se tengan estas dos partes funcionando ya se podrá usar la aplicación.


Video de referencia de uso: https://youtu.be/O4_nyyX341k?si=e6xa_d6t6HGBrK1h


Capturas del Sistema:

Login inicial
<img width="367" height="331" alt="Captura de pantalla 2026-06-04 102401" src="https://github.com/user-attachments/assets/2787d175-3acb-4534-92a6-1baa390ab21e" />

<img width="369" height="332" alt="Captura de pantalla 2026-06-04 102552" src="https://github.com/user-attachments/assets/e0cce7e5-5002-4e7c-9e9d-172b82df798d" />

Registro
<img width="336" height="330" alt="Captura de pantalla 2026-06-04 172742" src="https://github.com/user-attachments/assets/81897f47-6638-4fb7-bd76-ac8d631baa29" />

<img width="1365" height="714" alt="Captura de pantalla 2026-06-05 195302" src="https://github.com/user-attachments/assets/38b1bc7e-ceef-4b8d-8fa3-914bb3d0077c" />

<img width="1365" height="714" alt="Captura de pantalla 2026-06-05 195315" src="https://github.com/user-attachments/assets/735f7069-587b-45e7-acf0-cbaadcdf6210" />

<img width="1365" height="718" alt="Captura de pantalla 2026-06-05 195330" src="https://github.com/user-attachments/assets/81982a01-6aee-4e8a-a761-4fd1b7b28777" />

<img width="743" height="373" alt="Captura de pantalla 2026-06-05 195349" src="https://github.com/user-attachments/assets/90e5b6c9-9b87-4ef3-823a-866d32cbee6e" />

<img width="1364" height="716" alt="Captura de pantalla 2026-06-05 195402" src="https://github.com/user-attachments/assets/16197992-a21b-402c-8436-a8b6c7c55be7" />

---

Integrantes del Equipo
El desarrollo e implementación de este proyecto fue realizado por los siguientes integrantes:

*   *Chavira Ruiz* Jazmín
*   *Durán Tolentino* Amanda Nohemí
*   *García Espinoza* Andrea
*   *Romero Ramírez* Alexander
*   *Villalobos Villarreal* Naiby Adriana
