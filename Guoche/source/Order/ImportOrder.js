var elemnets = {
    ids: ["orderDetailJson"],
    methods: [
        //{
        //    required: true,
        //    messages: ["请选择仓库名称", "", "", ""]
        //},
        //{
        //    required: true,
        //    messages: ["请输入入库时间", "", "", ""]
        //},
        {
            required: true,
            messages: ["请选择商品", "", "", ""]
        }
    ]
};

var orderInfo = {
    init: function () {
        valid.init(elemnets);
        orderInfo.regEvent();
    },

    regEvent: function () {

        //下载送货单
        $("#OrderDownload_SHD").click(function () {
            var shdids = $("#hid_SHDids").val();
            if (shdids == "") {
                alert("请先生成订单！");
                return false;
            }
            if (shdids != "") {
                location.href = "/PdfView/DownloadImportPdf?ids=" + shdids + "&type=SHD";//送货单
            }
        });

        //下载补损单
        $("#OrderDownload_BSD").click(function () {
            var bsdids = $("#hid_BSDids").val();
            if (bsdids == "") {
                alert("请先生成订单！");
                return false;
            }
            if (bsdids != "") {
                location.href = "/PdfView/DownloadImportPdf?ids=" + bsdids + "&type=BSD";//补损单
            }
        });

        //Excel导入
        $(".lcQueryItem").on("change", ".upfile", function () {
            var orderSource = $("#hid_OrderSource").val();
            var formData = new FormData();
            formData.append('file', $(this)[0].files[0]);
            formData.append('importSource', orderSource);
            formData.append('token', $("#hid_Token").val());
            var fileType = $(this)[0].files[0].name.split(".");
            fileType = fileType[fileType.length - 1];

            if (fileType == "xls" || fileType == "xlsx") {
                $.ajax({
                    url: "OrderImportData",
                    type: 'post',
                    cache: false,
                    data: formData,
                    processData: false,
                    contentType: false
                }).done(function (data) {
                    if ("" != data) {
                        console.log(data);
                        $("#orderImportList").append("");
                        for (var i = 0; i < data.length; i++) {
                            var html = "";
                            var css = data[i].GoodsID < 1 ? "color:red" : "";
                            if (orderSource == "Costa") {
                                var ccss = data[i].CustomerID < 1 ? "color:red" : "";
                                html += "<tr>"
                                html += "<td>" + data[i].GoodsNo + "</td><td style='" + css + "'>" + data[i].GoodsName + ":" + data[i].GoodsID + "</td><td>" + data[i].GoodsModel + "</td><td>" + data[i].Units + "</td><td>" + data[i].Quantity + "</td><td>" + data[i].ShopNo + "</td><td style='" + ccss + "'>" + data[i].ShopName + "==" + data[i].CustomerID + "</td>";
                                html += "<td>" + data[i].OrderNo + "</td><td>" + data[i].OrderDate + "</td><td>" + data[i].YyDate + "</td><td>" + data[i].Remark + "</td>";
                                html += "</tr>"
                            }
                            else if (orderSource == "Regular") {
                                html += "<tr>"
                                html += "<td>" + data[i].orderDate + "</td><td>" + data[i].sendDate + "</td><td>" + data[i].sendStorageName + "</td><td>" + data[i].Temp + "</td><td>" + data[i].CarrierName + "</td><td>" + data[i].CustomerName + "</td><td>" + data[i].ReceiverName + "</td>";
                                html += "<td style='" + css + "'>" + data[i].GoodsName + ":" + data[i].GoodsID + "</td><td>" + data[i].Quantity + "</td><td>" + data[i].Units + "</td><td>" + data[i].Remark + "</td>";
                                html += "</tr>"
                            }
                            else {
                                var ccss = data[i].CustomerID < 1 ? "color:red" : "";
                                html += "<tr>"
                                html += "<td>" + data[i].ImportType + "</td><td>" + data[i].OrderDate + data[i].YyDate + "</td><td>" + data[i].CustomerName + "</td><td  style='" + ccss + "'>" + data[i].ShopName + "==" + data[i].CustomerID + "</td><td>" + data[i].OrderNo + "</td><td style='" + css + "'>" + data[i].GoodsName + ":" + data[i].GoodsID + "</td><td>" + data[i].Units + "</td>";
                                html += "<td>" + data[i].Quantity + "</td><td>" + data[i].Address + "</td><td>" + data[i].BarCode + "</td><td>" + data[i].SalesMan + "</td><td>" + data[i].PromotionMan + "</td><td>" + data[i].Remark + "</td>";
                                html += "</tr>"
                            }
                            $("#orderImportList").append(html);
                        }
                    }
                }).fail(function (res) {
                    alert("fail");
                    console.log(res);
                });
            } else {
                alert("请上传png,jpg格式的图片")
            }
        })

        //订单生成
        $("#OrderGeneration").click(function () {
            var orderType = $("#orderType").val();
            if (orderType == "") {
                alert("请选择要生成的订单类型。");
                return false;
            }
            if (confirm("确定要生成" + $("#orderType").find("option:selected").text() + "订单?")) {
                $.ajax({
                    type: "post",
                    url: "GenerateOrder",
                    data: {
                        token: $("#hid_Token").val(),
                        ordersource: $("#hid_OrderSource").val(),
                        orderType: orderType
                    },
                    success: function (data) {
                        console.log(data);
                        if (data) {
                            if (data.SHDIds && data.SHDIds != "") {
                                $("#hid_SHDids").val(data.SHDIds);
                                $("#OrderDownload_SHD").show()
                            }
                            if (data.BSDIds&&data.BSDIds != "") {
                                $("#hid_BSDids").val(data.BSDIds);
                                $("#OrderDownload_BSD").show()
                            }
                            alert("订单生成成功！");
                            
                        }
                    }
                })
            }
        })
    },

}
