# TrebolService

Para realizar la instalacion, se requiere restaurar un backup de la base de datos. con los nombres que usted desee.. 
    Base/TrebolAcademicService.bak


Se procede a actualizar la cadena de conexion de la base de datos en appsettings.json con nombre Local

Despues de importar el archivo Postman/Afinidades.postman_collection.json
Se procedera a actualizar la url base del proyecto postman para poder ejecutar los diferentes procesos.



Primero debe de realizar una solicitud de ingreso con los datos del estudiante
ruta: {{host}}/api/ingresoes


{
  "id": 0,
  "fechaop": "2023-01-22T22:06:09.838Z",
  "status":"",
  "nombre": "Carlos ",
  "apellido": "Arzua",
  "identificacion": "789123",
  "edad": 34,
  "motivo": "",
  "idAfinidad": 2
}


Despues ejecutar el proceso de validacion de ingreso y asignacion de Gremorio

{{host}}/api/ingresoes/procesar


Podra ver las asignaciones realizadas
{{host}}/api/GrimorioAsignadoes


Postdata:

No tuve mucho tiempo para verificar, ya que tuve que trabajar el dia de hoy por que uno de nuestros servidores de reliablesite, estuvo caido por mas de 5 horas, asi que tuvimos que migrar varias cosas a otro servidor

Saludos