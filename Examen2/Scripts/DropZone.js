Dropzone.options.dropzoneJsEstudiantes = {

    addedfile: function (file) {
        file.previewTemplate = createElement(this.options.previewTemplate);
        this.previewsContainer.appendChild(file.previewTemplate);
        //file.previewTemplate.querySelector(".filename span").textContent = file.name;
        return ($('input[id=result]').val(file.name));
    }
    //url: 'Home/ImportarDatos',
    //addRemoveLinks: true,
    //autoProcessQueue: false,
    //maxFiles: 60,
    //parallelUploads: 60,
    //init: function () {
    //    var myDropzone = this;
    //    $('#btnCargarArchivosDesglose').on('click', function (e) {
    //        e.preventDefault();
    //        myDropzone.processQueue();
    //        document.body.style.cursor = "progress";
    //    });

    //    this.on('complete', function (data) {
    //        var jsonResult = JSON.parse(data.xhr.response);

    //        document.body.style.cursor = "context-menu";
    //        $.notify(jsonResult["message"], jsonResult["tipo"]);
    //        console.log(jsonResult["message"]);
    //        this.removeAllFiles();
    //    });

    //    this.on("queuecomplete", function () {
    //        $('#btnRecargarArbolArchivosDesglose').click();
    //    });
    //}
};