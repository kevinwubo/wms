﻿var elemnets = {
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
        orderInfo.regInit();
    },

    regEvent: function () {      

        //$("#save").click(function () {

        //    var formParams = {};
        //    var params = {
        //        listGoods: []
        //    };
        //    $("#parentTbody").find("tr").each(function () {
        //        var tdArr = $(this).children();
        //        var goodsid = tdArr.eq(0).find('input').val();//商品ID
        //        var goodsno = tdArr.eq(1).find('input').val();//商品编号 -
        //        var goodsname = tdArr.eq(2).find('input').val();//商品名称 -
        //        var customerid = tdArr.eq(3).find('input').eq(0).val();//商品所属 
        //        var quantity = tdArr.eq(4).find('input').val();//数量
        //        var batchnumber = tdArr.eq(5).find('input').val();//批次号
        //        var productdate = tdArr.eq(6).find('input').val();//生产日期
        //        var unitprice = tdArr.eq(7).find('input').val();//单价
        //        var remark = tdArr.eq(8).find('input').val();//备注
        //        //var exdate = tdArr.eq(4).find('input').val();//保质期 -
        //        if (name != "Head") {
        //            formParams = {
        //                "GoodsID": goodsid,
        //                "CustomerID": customerid,
        //                "Quantity": quantity,
        //                "BatchNumber": batchnumber,
        //                "ProductDate": productdate,
        //                "UnitPrice": unitprice,
        //                "Remark": remark,
        //            };
        //            params.listGoods.push(formParams);
        //        }

        //    });

        //    $("#SaveInventoryJson").val(JSON.stringify(params));

        //    if (valid.validate()) {
        //        //, string ,string 
        //        $.ajax({
        //            url: "SaveOrder",
        //            type: 'POST',
        //            async: false,
        //            data: { StorageID: $("#StorageID").val(), productDate: $("#ProductDate").val(), json: $("#SaveInventoryJson").val() },
        //            success: function (data) {
        //                if (data == "T") {
        //                    alert("入库操作成功");
        //                    window.location.reload();
        //                }
        //                else {
        //                    alert("修改失败，请联系技术人员");
        //                }
        //            }
        //        });
        //    }
        //});

        //商品选择器
        $("#select_goods").click(function () {
            var storageID = $("#SendStorageID").val();
            var type = $("#Hid_Type").val();
            var customerid = $("#CustomerID").val();
            var obj = window;
            obj.name = "我是父窗口";  //子窗口获得的是这个
            var openUrl = "OrderGoodsSelect?type=" + type + "&storageID=" + storageID + "&customerid=" + customerid;//弹出窗口的url
            var iWidth = 1200; //弹出窗口的宽度;
            var iHeight = 600; //弹出窗口的高度;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            myWindow = window.open(openUrl, "Child", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft + ",alwaysRaised=yes,z-look=yes,location=yes");
            myWindow.focus(); //置顶
        });

        //订单保存
        $("#save").click(function () {
            var formParams = {};
            var params = {
                listOrderDetail: []
            };
            $("#fineTable").find("tr").each(function () {
                var goodsid, inventoryid, goodsname, goodsno, batchnumber, productdate, exceeddate, units, quantity, weight, totalweight, goodsmodel;
                var tdArr = $(this).children();
                var orderID = $("#OrderID").val();
                var type = $("#Hid_Type").val();
                var title = tdArr.eq(0).find('input').val();
                if (title == "Head") {
                    return;
                }
                //
                if (type == "YSDDA" || type == "YSDDB") {
                    //运输订单A 运输订单B
                    //所属客户	商品名称	保质期	(批次号	生产日期	商品数量)	单位	商品重量	商品总重量   
                    var goodsid = tdArr.eq(0).find('input')[0].value;//商品ID
                    var inventoryid = tdArr.eq(0).find('input')[1].value;//库存id
                    var detailid = tdArr.eq(0).find('input')[2].value;// 订单明细ID;

                    var goodsname = tdArr.eq(2).find('input').val();//商品名称
                    var goodsno = tdArr.eq(10).find('input').val();//商品编号
                    var batchnumber = tdArr.eq(4).find('input').val();//批次号
                    var productdate = tdArr.eq(5).find('input').val();//生产日期
                    var exceeddate = tdArr.eq(3).find('input').val();//到期日期
                    var units = tdArr.eq(7).find('input').val();//单位
                    var quantity = tdArr.eq(6).find('input').val();//数量
                    var weight = tdArr.eq(8).find('input').val();//重量
                    var totalweight = tdArr.eq(9).find('input').val();//总重量
                    var goodsmodel = tdArr.eq(11).find('input').val();//商品规格                    
                }
                else {
                    //仓配订单 调拨订单
                    //商品名称	批次号	生产日期	到期日期	(商品数量)	单位	库存数量	商品重量	商品总重量  
                    var goodsid = tdArr.eq(0).find('input')[0].value;//商品ID
                    var inventoryid = tdArr.eq(0).find('input')[1].value;//库存id
                    var detailid = tdArr.eq(0).find('input')[2].value;// 订单明细ID;

                    var goodsname = tdArr.eq(1).find('input').val();//商品名称
                    var goodsno = tdArr.eq(10).find('input').val();//商品编号
                    var batchnumber = tdArr.eq(2).find('input').val();//批次号
                    var productdate = tdArr.eq(3).find('input').val();//生产日期
                    var exceeddate = tdArr.eq(4).find('input').val();//到期日期
                    var units = tdArr.eq(6).find('input').val();//单位
                    var quantity = tdArr.eq(5).find('input').val();//数量
                    var weight = tdArr.eq(8).find('input').val();//重量
                    var totalweight = tdArr.eq(9).find('input').val();//总重量
                    var goodsmodel = tdArr.eq(11).find('input').val();//商品规格
                }
                //GoodsID,InventoryID,GoodsNo,GoodsName,GoodsModel,Quantity,Units,Weight,TotalWeight,BatchNumber,ProductDate,ExceedDate               
                if (title != "Head") {
                    formParams = {
                        "ID": detailid,
                        "OrderID": orderID,
                        "GoodsID": goodsid,
                        "InventoryID": inventoryid,
                        "GoodsNo": goodsno,
                        "GoodsName": goodsname,
                        "GoodsModel": goodsmodel,
                        "Quantity": quantity,
                        "Units": units,
                        "Weight": weight,
                        "TotalWeight": totalweight,
                        "BatchNumber": batchnumber,
                        "ProductDate": productdate,
                        "ExceedDate": exceeddate,
                    };
                    params.listOrderDetail.push(formParams);
                }

            });
            $("#orderDetailJson").val(JSON.stringify(params));

            if (valid.validate()) {
                $("#OrderForm").submit();
            }
        });

        $("#goback").click(function () {
            window.location.href = "/Order/Index/";
        });

        //筛选联系人信息
        $("#ReceiverID").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetReceiverByGoodsID",
                    type: 'POST',
                    async: false,
                    data: { rid: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#contactName").val(data.ContactName);
                            $("#contactMobile").val(data.Mobile);
                            $("#contactAddress").val(data.Address);
                        }
                    }
                });
            }
            else {
                $("#contactName").val("");
                $("#contactMobile").val("");
                $("#contactAddress").val("");
            }
        });

        //计算价格-弹框
        $("#coumpLayer_price").click(function () {            
            var cusid = $("#CustomerID").val();
            var rid = $("#ReceiverID").val(); ReceiverStorageID
            var ssid = $("#SendStorageID").val();
            var cid = $("#CarrierID").val();
            var otype = $("#Hid_Type").val();

            jQuery.ajax({
                url: "/Order/ComputePrice",
                data: { customerid: cusid, receivingid: rid, sendstorageid: ssid, carrierid: cid, ordertype: otype },
                type: "post",
                success: function (data) {
                    if (data) {
                        if (data != "" && data.length > 0) {
                            for (var i = 0; i < data.length; i++) {
                                $(".psType").append("<option value='" + data[i].PriceSetID + "'>" + data[i].DeliveryModel + "</option>");//配送方式
                                $(".wqType").append("<option value='" + data[i].PriceSetID + "'>" + data[i].TemType + "</option>");//温区
                                $(".other").append("<option value='" + data[i].PriceSetID + "'>" + data[i].Remark + "</option>");//其他
                            }
                            $(".coumpLayer").show();
                        }
                        else {
                            alert("无可用的价格配置！");
                        }
                    }
                }
            });
            
        });
                
        //计算价格-计算
        $(".coumpLayer_sure").click(function () {
            var psid = $(".other").val() != "" ? $(".other").val() : $(".wqType").val();
            jQuery.ajax({
                url: "/Order/GetPriceSetInfoByID",
                data: { pricesetid: psid},
                type: "post",
                success: function (data) {
                    if (data) {
                        if (data != "") {
                            $("#configPrice").val(data.configPrice);//运输应收
                            $("#configHandInAmt").val(data.configHandInAmt);//装卸应收
                            $("#configSortPrice").val(data.configSortPrice);//分拣应收
                            $("#configCosting").val(data.configCosting);//运输应付
                            $("#configHandOutAmt").val(data.configHandOutAmt);//装卸应付
                            $("#configSortCosting").val(data.configSortCosting);//分拣应付
                            $(".coumpLayer").hide();
                        }                        
                    }
                }
            });
            $(".coumpLayer").hide();
        });

        //计算价格-取消
        $(".coumpLayer_back").click(function () {
            $(".coumpLayer").hide();
        });

        
    },
    regInit: function () {
        var type = $("#Hid_Type").val();
        //仓配订单 运输订单B  显示联系人信息+门店信息
        if (type == "CPDD" || type == "YSDDB") {
            $("#choose_Receiver").show(); 
            $("#ReceiverID").show();

            $("#choose_Storage").hide();
            $("#ReceiverStorageID").hide();

            $("#tr_ContactInfo").show();

            $("#ReceiverID").val($("#ReceiverID").val()).trigger("change");
        }
        else {
            //调拨订单 运输订单A显示 +仓库信息
            $("#choose_Receiver").hide();
            $("#ReceiverID").hide();

            $("#choose_Storage").show();
            $("#ReceiverStorageID").show();

            $("#tr_ContactInfo").hide();
        }        
    },
}

/**删除一行*/
function delTr($this, id) {
    if (id) {
        jQuery.ajax({
            url: "/Order/Remove",
            data: { oid: id },
            type: "post",
            success: function (result) {
                if (result) {
                    $($this).parent().parent().remove();
                }
            }
        });
    }
    else {
        $($this).parent().parent().remove();
    }
}

