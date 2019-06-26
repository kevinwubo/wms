
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

        //订单配送
        $(".lcQueryItem").on("click", ".sure", function () {
            var dataId = $(this).parent().parent().parent().parent().attr("dataid")
            if (confirm('订单确认开始配送吗？')) {
                $("#hid_orderid").val(dataId);
                $("#myModal").show();
            }
        })

        //关闭配送弹窗
        $("#myModal .myModal_close").click(function () {
            $("#myModal").hide();
        })

        //配送时间提交
        $("#myModal .myModal_sure").click(function () {
            var date = $('#txt_arrivertime').val();
            var orderid = $("#hid_orderid").val();
            if (date) {
                $.ajax({
                    type: "post",
                    url: "/Order/OrderChangeArriverTime",
                    data: {
                        orderid: orderid,
                        arrivertime: date
                    },
                    success: function (msg) {
                        console.log(msg);
                        alert(msg);
                        $("#arrivertime" + orderid).text(date);
                        $("#myModal").hide();
                    }
                })
            } else {
                alert("请选择送达时间");
            }

        })
    }
}


//图片附件查看
function showImg(idss) {
    if (idss != "") {
        $.ajax({
            url: "/Order/searchAttachmentByIDs",
            type: 'POST',
            async: false,
            data: { ids: idss },
            success: function (data) {
                console.log(data);
                if (!!data) {
                    for (var i = 0; i < data.length; i++) {
                        if (i == 0) {
                            $(".docs-pictures").append(" <img data-original='" + data[i].FileName + "' style='display: none;' id='top1' src='" + data[i].FilePath + "' >");
                        }
                        else {
                            $(".docs-pictures").append(" <img data-original='" + data[i].FileName + "' style='display: none;' src='" + data[i].FilePath + "' >");
                        }
                    }                 
                    $('.docs-pictures').viewer("data-original");
                    $('#top1').click();
                }
            }
        });
    }
}