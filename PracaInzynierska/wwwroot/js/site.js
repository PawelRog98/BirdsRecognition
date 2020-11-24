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
            //this.on("queuecomplete", function () {
            //    this.on("success", function (file, response) {
            //        var obj = jQuery.parseJSON(response)
            //        console.log(obj);
            //        $("#hiddenValue").val(obj.Prediction);
            //    })
            //    //$("#hiddenValue").val(obj.Prediction);
            //    $('.dz-preview').remove();
            //    this.removeAllFiles;
            //});
            //this.on("successmultiple", function (file, response) {
            //    this.on("queuecomplete", function () {
            //        var obj = jQuery.parseJSON(response)
            //        console.log(obj);
            //        $("#hiddenValue").val(obj.Prediction);
            //    })
            //    //$("#hiddenValue").val(obj.Prediction);
            //    $('.dz-preview').remove();
            //    this.removeAllFiles;
            //});
            this.on("successmultiple", function (file, response) {
                var obj = jQuery.parseJSON(response)
                console.log(obj);
                $.each(obj, function (i, item) {
                    $("#hiddenValue").val(item.Prediction);
                    console.log(item);
                });
                //$("#hiddenValue").val(obj.Prediction);
                $('.dz-preview').remove();
                this.removeAllFiles;
            })
        },

};

//$('#submit').click(function () {
//    var myDropzone = Dropzone.forElement(".dropzone");
//    myDropzone.processQueue();
//});

