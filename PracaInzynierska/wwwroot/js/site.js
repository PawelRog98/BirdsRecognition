// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myParamName() {
    return "files";
}
    Dropzone.options.dropzoneForm = {
        paramName: myParamName,
        maxFilesize: 25,
        maxFiles: 5,
        acceptedFiles: "image/*",
        parallelUploads: 5,
        dictMaxFilesExceeded: "Custom max files msg",
        timeout: 100000,
        uploadMultiple: true,
        autoProcessQueue: false,
        addRemoveLinks: true,

        
        init: function () {
            var button = document.querySelector("#myButton")
            myDropzone = this;

            button.addEventListener("click", function () {
                myDropzone.processQueue();
            });
            this.on("queuecomplete", function () {
                //var res = JSON.parse(data.xhr.responseText);
                //getPrediction();
                
                //var customVal = $("#hiddenValue").data("value");
                //$("@ViewBag.Result.Prediction").val()
                
            });
            this.on("successmultiple", function (file, response) {
                var obj = jQuery.parseJSON(response)
                console.log(obj);
                $('#Table tbody').empty();
                $.each(obj, function (i, item) {
                    $("tbody").append($("<tr>"));
                    appendElement = $("tbody tr").last();
                    appendElement.append($("<td>").html(obj[item.Prediction]));
                    var rows = '<tr class="Prediction">' + "<td>" +'<img class="img-fluid rounded image" src="'+ item.Image +'">'+ "</td>" + "<td>" + item.PredictionResult + "</td>" + "<td>" + item.Score + "%</td>" + "</tr>";
                    $('#Table tbody').append(rows);
                    console.log(item);
                });
                $('.dz-preview').remove();
                this.removeAllFiles;
            })
        },

};


