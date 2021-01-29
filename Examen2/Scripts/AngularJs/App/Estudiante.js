app.controller("EstudiantesController", ["$rootScope", "$scope", "$http", "$window", function ($rootScope, $scope, $http, $window) {

    $scope.Datos = [];
    $scope.listaEstudiantes = [];

    $scope.obtenerEstudiantes = function () {
        $http.post("/Home/obtenerEstudiantes").then(function (dataResult) {
            $scope.listaEstudiantes = dataResult.data.listaEstudiantes;
            $scope.totalEstudiantes = dataResult.data.listaEstudiantes.length;
            console.log($scope.listaEstudiantes);
            $scope.MostrarVista = 0;
        }, function (error) {
            console.log(error);
        }).finally(function () {

        });
    }

    $scope.subirArchivoImportacion = function () {
        $('#myModalImportarArchivo').modal("show");
    }

    $scope.graficarCalificaciones = function () {
        graficaBarrasCalificaciones($scope.listaEstudiantes, $scope.totalEstudiantes);
        $scope.MostrarVista = 1;
    }

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
            data: datos,            
            barColors: ['#00a65a', '#DB3535'],
            xkey: 'y',
            animate: true,
            ykeys: ['a'],
            labels: ['Total'],
            hideHover: 'auto',
            parseTime: false,
            resize: true,
            redraw: true
        });

    } catch (err) {
        console.log(err);
        return false;
    }

    $('#bar-chart-calificaciones').resize(function () {
        morrilsBar.redraw();
    });
}