﻿
var orderInfo = {
    init: function () {
        orderInfo.regEvent();
    },

    regEvent: function () {      
        //下载配送单
        $("#download_SHD").click(function () {
            var orderids = "";
            $.each($('input:checkbox:checked'), function () {
                orderids = orderids + $(this).val()+",";
            });
            if (orderids == "")
            {
                alert("请勾选需要下载的配送单！");
                return false;
            }
            location.href = "/PdfView/DownloadPdf?ids=" + orderids;            
        });

        //订单详情显示
        $(".lcQueryItem").on("click", ".showDiv", function () {
            if ($(this).attr("isOk") == "true") {
                $(this).parent().parent().parent().parent().find(".lcItem_b").css("display", "block")
                $(this).attr("isOk", "false");
                return;
            } else {
                $(this).parent().parent().parent().parent().find(".lcItem_b").css("display", "none")
                $(this).attr("isOk", "true");
                return;
            }
        })

        //点击上传图片
        $(".lcQueryItem").on("change", ".upfile", function () {
            var formData = new FormData();

            formData.append('file', $(this)[0].files[0]);
            formData.append('dataId', $(this).parent().parent().parent().parent().parent().attr("dataId"));

            var fileType = $(this)[0].files[0].name.split(".");
            fileType = fileType[fileType.length - 1];

            if (fileType == "png" || fileType == "jpg") {
                $.ajax({
                    url: "/Upload/UploadFile/",
                    type: 'post',
                    cache: false,
                    data: formData,
                    processData: false,
                    contentType: false
                }).done(function (res) {
                    if ("" != res) {
                        console.log(res);
                        alert(res);                        
                    }
                }).fail(function (res) {
                    console.log(res);
                });
            } else {
                alert("请上传png,jpg格式的图片")
            }
        })
    }
}


//图片附件查看
function showImg(idss) {
    if (idss != "") {
        $.ajax({
            url: "searchAttachmentByIDs",
            type: 'POST',
            async: false,
            data: { ids: idss },
            success: function (data) {
                if (!!data) {
                    for (var i; i < data.length; i++) {
                        if (i == 0) {
                            $(".docs-pictures").append(" <img data-original='" + data[i].FileName + "' style='display: none;' id='top1' src='" + data[i].FilePath + "' >");
                        }
                        else {
                            $(".docs-pictures").append(" <img data-original='" + data[i].FileName + "' style='display: none;' src='" + data[i].FilePath + "' >");
                        }
                    }
                    $(".docs-pictures").append(" <img data-original='11_37_20190524091717_1' style='display: none;' id='top1' src='http://www.arapnet.com/lcimg//11/37/11_37_20190524091717_XloenQ.jpg' alt='Cuo Na Lake' >");
                    $(".docs-pictures").append(" <img data-original='11_37_20190524091717_2' style='display: none;' src='http://www.arapnet.com/lcimg//11/37/11_37_20190522202033_ILaNZy.jpg' alt='Cuo Na '>");
                    $(".docs-pictures").append(" <img data-original='11_37_20190524091717_3' style='display: none;' src='http://www.arapnet.com/lcimg//11/37/11_37_20190522202026_O0YTiR.jpg' alt='Cuo Lake'>");                    
                    $('.docs-pictures').viewer("data-original");
                    $('#top1').click();
                }
            }
        });
    }
}