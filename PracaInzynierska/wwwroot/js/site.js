// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#impPrev').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}

    Dropzone.options.dropzoneForm = {
        paramName: "files",
        maxFilesize: 15,
        maxFiles: 5,
        acceptedFiles: "image/*",
        parallelUploads: 100,
        dictMaxFilesExceeded: "Custom max files msg",
        timeout: 100000,
        uploadMultiple: false,
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
            this.on("success", function (file, response) {
                var obj = jQuery.parseJSON(response)
                console.log(obj);
                $("#hiddenValue").val(obj.Prediction);
            })
        },

};

//$('#submit').click(function () {
//    var myDropzone = Dropzone.forElement(".dropzone");
//    myDropzone.processQueue();
//});

