app.controller("EstudiantesController", ["$rootScope", "$scope", "$http", "$window", function ($rootScope, $scope, $http, $window) {

    $scope.Datos = [];
    $scope.listaEstudiantes = [];
    $scope.totalEstudiantes = 0;

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

    $scope.GuardarListaSessionStorage = function () {
        var listadoEstudiantes = sessionStorage.getItem("listaEstudiantes");
        var lista = JSON.parse(listadoEstudiantes)
        $scope.listaEstudiantes = lista;
        $scope.totalEstudiantes = $scope.listaEstudiantes.length;

        $scope.analizarCalificaciones();
    }

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

    $scope.subirArchivoImportacion = function () {
        $('#txtVecesIterar').val("");
        $('#myModalImportarArchivo').modal("show");
    }

    $scope.graficarCalificaciones = function () {
        graficaBarrasCalificaciones($scope.listaEstudiantes, $scope.totalEstudiantes);
        $('#tabGraficaCalificaciones').addClass('active');
        $('#itemBarGrafica').addClass('active');
    }

    //$scope.refreshChart = function () {
    //    $("#contenedorGrafica").load();
    //}

    $scope.VolverInicio = function () {
        $scope.MostrarVista = 0;
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
            redraw: true
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