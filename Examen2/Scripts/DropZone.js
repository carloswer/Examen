Dropzone.options.dropzoneJsEstudiantes = {
    accept: function (file, done) {
        var thumbnail = $('.dropzone .dz-preview.dz-file-preview .dz-image:last');
        var ext = file.name.split('.').pop();
        switch (ext) {
            case 'pdf':
                thumbnail.css('background', 'url(/Content/img/pdf.png');
                break;
            case 'doc':
            case 'docx':
                thumbnail.css('background', 'url(/Content/img/docx.png');
                break;
            case 'xls':
            case 'xlsx':
                thumbnail.css('background', 'url(/Content/img/xls.jpg');
                break;
            case 'ppt':
            case 'pptx':
                thumbnail.css('background', 'url(/Content/img/ppt.jpg');
                break;
            case 'xml':
                thumbnail.css('background', 'url(/Content/img/xml.png');
                break;
            case 'txt':
                thumbnail.css('background', 'url(/Content/img/txt.png');
                break;
        }

        var size = (file.size / 1024) / 1024;
        if (size > 20) {
            $.notify('No se pudo agregar. </br> El archivo excede el limite de 20 MB', 'error');
            this.removeFile(file);
            return;
        }

        done();
    },
    addRemoveLinks: true,
    autoProcessQueue: false,
    maxFiles: 60,
    parallelUploads: 60,
    init: function () {
        var myDropzone = this;
        //this.on("addedfile", function (file) { alert("Archivo agregado") });
        $('#btnCargarArchivosDesglose').on('click', function (e) {
            e.preventDefault();
            myDropzone.processQueue();
            document.body.style.cursor = "progress";
        });

        this.on('complete', function (data) {
            var jsonResult = JSON.parse(data.xhr.response);

            document.body.style.cursor = "context-menu";
            $.notify(jsonResult["message"], jsonResult["tipo"]);
            console.log(jsonResult["message"]);
            this.removeAllFiles();
        });

        this.on("queuecomplete", function () {
            $('#btnRecargarArbolArchivosDesglose').click();
        });
    }
};