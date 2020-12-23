// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function filesParam() {
    return "files";
}
Dropzone.options.dropzoneForm = {
        paramName: filesParam,
        maxFilesize: 25,
        maxFiles: 4,
        acceptedFiles: "image/*",
        parallelUploads: 5,
        dictMaxFilesExceeded: "Maximum number of pictures is 4",
        timeout: 180000,
        uploadMultiple: true,
        autoProcessQueue: false,
        addRemoveLinks: true,

        
        init: function () {
            var button = document.querySelector("#recognizeButton")
            myDropzone = this;

            button.addEventListener("click", function () {
                myDropzone.processQueue();
            });
            this.on("successmultiple", function (file, response) {
                var obj = jQuery.parseJSON(response)
                $('#Table tbody').empty();
                $.each(obj, function (i, item) {
                    $("tbody").append($("<tr>"));
                    appendElement = $("tbody tr").last();
                    appendElement.append($("<td>").html(obj[item.Prediction]));
                    if (item.Score > 50) {
                        var rows = '<tr class="Prediction">' + "<td>" +
                            '<img class="img-fluid rounded image" src="' +
                            item.Image + '">' +
                            "</td>" + "<td><b>" + item.Label +
                            "</br></br></b>" + item.PredictionResult +
                            "</td>" + "<td> <b>SCORE:    </b>" +
                            item.Score + "%</td>" + "</tr>";
                    }
                    else {
                        var rows = '<tr class="Prediction">' + "<td>" +
                            '<img class="img-fluid rounded image" src="' +
                            item.Image + '">' + "</td>" +
                            '<td class="noBird">NO BIRD HAS BEEN RECOGNIZED &#10008;</td>' +
                            "<td> </td>" + "</tr>";
                    }
                    $('#Table tbody').append(rows);
                });
            })
        },

};

