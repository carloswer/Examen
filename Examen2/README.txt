

## Ejercicio

Ejercicio Conduent

## Requisitos

El proyecto se desarrollo con ASP.NET MVC en Visual Studio 2019

1. Clonar el proyecto desde git https://github.com/carloswer/Examen
2. Antes de ejecutar el proyecto es necesario realizar una actualizacion de paquetes desde la Consola de Administrador de Paquetes Nuget

update-package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r


## Funcionamiento

1. Para poder visualizar informacion el primer paso debe ser cargar el archivo excel con la informacion desde el boton Importar datos.
2. Se debe seleccionar el archivo e ingresar el numero de veces que se va a rotar la clave al momento de generarla

La informacion se almacenara en SessionStorage del navegador en tiempo de ejecución, la cual se mostrara en una tabla 
y se generara una grafica de barras de las calificaciones de los alumnos.

Se mostrara en pantalla el alumno con mayor calificacion, menor, y un promedio de las calificaciones.

Adicional se mostrara el clima actual de la ciudad

## Construido con 

ASP.NET MVC 
Angular JS
HTML
JS
CSS
BOOSTRAP 3
WEB API

Para la API del Clima se utilizó
https://api.openweathermap.org

Grafica
https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js

