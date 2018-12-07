/**
*控制input[type='file']文件上传
*add by lx 2016-11-24
*/

var errorWarming = "error";

$.fn.extend({
    AddMessageContol: function (str_message) {
        $(this).after("<label for='uploadimg' class='error'>" + str_message + "</label>");
    },
    RemoveMessageContol: function () {
        $(this).siblings(".error").remove();
    }
});


$(":file").change(function () {
    var all_true = true;;
    var size = $(this).attr("size");
    var ext = $(this).attr("ext");
    var fileType = $(this).attr("fileType");
    var begin_scale = parseFloat($(this).attr("begin_scale"));
    var end_scale = parseFloat($(this).attr("end_scale"));
    var range = 0.1;

    //当有文件时的情况
    if ($(this).val() != "") {
        if (size != undefined) {
            if ((this.files[0].size / 1024) > size) {
                all_true = false;
                $(this).attr("title", "文件大小不能超过" + size + "KB");
                $(this).AddMessageContol("文件大小不能超过" + size + "KB");
            }
        }

        if (ext != undefined) {
            var filepath = $(this).val();
            var extStart = filepath.lastIndexOf(".");
            var current_ext = filepath.substring(extStart, filepath.length).toUpperCase();
            if ((ext.toUpperCase().indexOf(current_ext)) < 0) {
                all_true = false;
                $(this).attr("title", "文件扩展名必须为" + ext);
                $(this).AddMessageContol("文件扩展名必须为" + ext);
            }
        }

        if (this.files[0].size < 5 * 1024 * 1024 && fileType == "img") {//是图片
            var _this = this;
            var fileData = this.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;
                var image = new Image();
                image.onload = function () {
                    var current_scale = image.width / image.height;

                    if (current_scale < begin_scale || current_scale > end_scale) {
                        $(_this).attr("title", "图片宽高比例必须在" + begin_scale.toFixed(1) + "到" + end_scale.toFixed(1) + "之间");
                        $(_this).AddMessageContol("图片宽高比例必须在" + begin_scale.toFixed(1) + "到" + end_scale.toFixed(1) + "之间");
                        $(_this).addClass(errorWarming);
                    }
                };
                image.src = data;
            };
            reader.readAsDataURL(fileData);
        }
    }

    if (all_true) {
        $(this).removeClass(errorWarming);
        $(this).removeAttr("title");
        $(this).RemoveMessageContol();

        doUpload();//上传
    } else {
        $(this).addClass(errorWarming);
    }
});

function doUpload() {
    var formData = new FormData($("#uploadForm")[0]);
    $.ajax({
        url: '/WebService/UploadFile.ashx?ID=2',
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (returndata) {
            $("#imgUrl").val(returndata);
        },
        error: function (returndata) {
            alert(returndata);
        }
    });
}