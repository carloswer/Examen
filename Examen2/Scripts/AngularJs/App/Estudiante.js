app.controller("EstudiantesController", ["$rootScope", "$scope", "$http", "$window", function ($rootScope, $scope, $http, $window) {

    $scope.listaEstudiantes = [];
    $scope.totalEstudiantes = 0;

    //Si hay informacion almacenada, obtiene la lista de estudiantes y la muestra en pantalla
    $scope.obtenerEstudiantes = function () {

        var listadoEstudiantes = sessionStorage.getItem("listaEstudiantes");

        if (listadoEstudiantes != null && listadoEstudiantes != undefined) {
            var lista = JSON.parse(listadoEstudiantes)
            $scope.listaEstudiantes = lista;
            $scope.totalEstudiantes = $scope.listaEstudiantes.length;

            $scope.analizarCalificaciones();
        }

        $scope.verClima();
    }

    //Obtiene la lista de estudiantes guardada en SessionStorage para mostrarla en pantalla
    $scope.GuardarListaSessionStorage = function () {
        var listadoEstudiantes = sessionStorage.getItem("listaEstudiantes");
        var lista = JSON.parse(listadoEstudiantes)
        $scope.listaEstudiantes = lista;
        $scope.totalEstudiantes = $scope.listaEstudiantes.length;

        $scope.analizarCalificaciones();
    }

    //Envia la lista de estudiantes al controlador para verificar cual es el estudiante con calificacion mayor, menor y el promedio
    $scope.analizarCalificaciones = function () {

        $http.post("/Home/analizarCalificaciones", $scope.listaEstudiantes).then(function (dataResult) {
            $scope.MejorEstudiante = dataResult.data.estudianteMejor;
            $scope.PeorEstudiante = dataResult.data.estudiantePeor;
            $scope.PromedioCalificaciones = dataResult.data.promedio;

        }, function (error) {
            console.log(error);
        }).finally(function () {
            $scope.graficarCalificaciones();
        });
    }

    //API DEL CLIMA
    $scope.verClima = function () {

        var city_Id = "4013704";
        var API_KEY = "85629cf1eb25797e1214d0552b942c43";

        var url = "https://api.openweathermap.org/data/2.5/weather?lang=es&units=metric" + "&id=" + city_Id +  "&appid=" + API_KEY;
        $http.get(url).then(function (response) {
            console.log(response);
            $scope.NombreCiudad = response.data.name;
            $scope.Temperatura = response.data.main.temp;

        });
    }

    //Evento del boton para abrir modal donde se subira el archivo
    $scope.subirArchivoImportacion = function () {
        $('#txtVecesIterar').val("");
        $("#FileUpload").val('');
        $('#myModalImportarArchivo').modal("show");
    }

    //Grafica la calificacion de los estudiantes
    $scope.graficarCalificaciones = function () {
        graficaBarrasCalificaciones($scope.listaEstudiantes, $scope.totalEstudiantes);
    }


}]);


function graficaBarrasCalificaciones(estudiantes, total) {    
    $('#bar-chart-calificaciones').html("");

    var datos = [];

    for (i = 0; i < total; i++) {
        datos.push({ y: estudiantes[i].Nombres, a: estudiantes[i].Calificacion });
    }

    try {
        var morrilsBar = new Morris.Bar({
            element: 'bar-chart-calificaciones',
            resize: true,
            data: datos,            
            barColors: ['#00a65a', '#DB3535'],
            xkey: 'y',
            ykeys: ['a'],
            labels: ['Calificación'],
            hideHover: 'auto',
            redraw: true,
            xLabelAngle: 60,
            
        });

        morrilsBar.redraw();

    } catch (err) {
        console.log(err);
        return false;
    }

    $('#bar-chart-calificaciones').resize(function () {
        morrilsBar.redraw();
        window.refre
    });

}