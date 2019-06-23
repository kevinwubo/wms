var elemnets = {
    ids: ["StorageID", "ProductDate", "SaveInventoryJson"],
    methods: [
        {
            required: true,
            messages: ["请选择仓库名称", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入入库时间", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择需要入库的商品", "", "", ""]
        }
    ]
};

var inventoryLoss = {
    init: function () {
        valid.init(elemnets);
        inventoryLoss.regEvent();
    },

    regEvent: function () {      

        $("#save").click(function () {

            var formParams = {};
            var params = {
                listGoods: []
            };
            $("#parentTbody").find("tr").each(function () {
                var tdArr = $(this).children();

                //StorageID,InventoryDate
                //GoodsID,CustomerID,Quantity,BatchNumber,ProductDate,UnitPrice,Remark,
                var goodsid = tdArr.eq(0).find('input').val();//商品ID
                var goodsno = tdArr.eq(1).find('input').val();//商品编号 -
                var goodsname = tdArr.eq(2).find('input').val();//商品名称 -
                var customerid = tdArr.eq(3).find('input').eq(0).val();//商品所属 
                var quantity = tdArr.eq(4).find('input').val();//数量
                var batchnumber = tdArr.eq(5).find('input').val();//批次号
                var productdate = tdArr.eq(6).find('input').val();//生产日期
                var unitprice = tdArr.eq(7).find('input').val();//单价
                var remark = tdArr.eq(8).find('input').val();//备注
                //var exdate = tdArr.eq(4).find('input').val();//保质期 -
                if (name != "Head") {
                    formParams = {
                        "GoodsID": goodsid,
                        "CustomerID": customerid,
                        "Quantity": quantity,
                        "BatchNumber": batchnumber,
                        "ProductDate": productdate,
                        "UnitPrice": unitprice,
                        "Remark": remark,
                    };
                    params.listGoods.push(formParams);
                }

            });

            $("#SaveInventoryJson").val(JSON.stringify(params));

            if (valid.validate()) {
                //, string ,string 
                $.ajax({
                    url: "SaveLossInventory",
                    type: 'POST',
                    async: false,
                    data: { StorageID: $("#StorageID").val(), productDate: $("#ProductDate").val(), json: $("#SaveInventoryJson").val() },
                    success: function (data) {
                        if (data == "T") {
                            alert("入库操作成功");
                            window.location.reload();
                        }
                        else {
                            alert("修改失败，请联系技术人员");
                        }
                    }
                });

            }
        });

        //商品选择器
        $("#select_goods").click(function () {
            var obj = window;
            obj.name = "我是父窗口";  //子窗口获得的是这个
            var openUrl = "/Inventory/GoodsSelect";//弹出窗口的url
            var iWidth = 1200; //弹出窗口的宽度;
            var iHeight = 600; //弹出窗口的高度;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            myWindow = window.open(openUrl, "Child", "height=" + iHeight + ", width=" + iWidth + ", top=" + iTop + ", left=" + iLeft + ",alwaysRaised=yes,z-look=yes,location=yes");
            myWindow.focus(); //置顶
        });

        //根据仓库赛选 库位区域
        $("#StorageID").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetStorageAreaNo",
                    type: 'POST',
                    async: false,
                    data: { sid: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#StorageAreaNo").html("").append("<option value=''>--请选择仓库库位--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#StorageAreaNo").append("<option value='" + data[i].StorageAreaNo + "'>" + data[i].StorageAreaNo + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#StorageAreaNo").html("").append("<option value=''>--请选择仓库库位--</option>");
            }
        });
    },    
}

/**删除一行*/
function delTr($this, id) {
    $($this).parent().parent().remove();
}

