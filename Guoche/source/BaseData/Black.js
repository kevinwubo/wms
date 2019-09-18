var elemnets = {
    ids: ["UnionName"],
    methods: [
        {
            required: true,
            messages: ["请输入黑名单名称", "", "", ""]
        }
    ]
};

var LineInfo = {
    init: function () {
        valid.init(elemnets);
        LineInfo.regEvent();
    },

    regEvent: function () {

        $("#UnionName").keyup(function () {
            searchCarrierInfo();
        })
        $("#UnionName").focus(function () {
            searchCarrierInfo();
        })
        function searchCarrierInfo() {
            var Name = $("#UnionName").val();
            $.ajax({
                type: "post",
                url: "GetCarrierByName",
                data: {
                    name: Name,
                },
                success: function (data) {
                    console.log(data);
                    if (data) {
                        $("#div_items").html("")
                        $("#div_items").show();
                        for (var i = 0; i < data.length; i++) {
                            $("#div_items").append("<li class='div_item' id='" + data[i].CarrierID + "'>" + data[i].CarrierName + "</li>");
                        }
                    }
                }
            })
        }


 
        $("#div_items").on("click", "li", function () {
            var name = $(this).text();
            $("#hid_UnionID").val($(this).attr("id"));
            $('#UnionName').val(name);
        });

        $('#UnionName').blur(function () {
            $('#div_items').hide(500);
        })

        $("#saveBlack").click(function () {//blackid,string type, int unionid, string unionname, string Remark
            var blackid = $("#hid_BlackID").val();
            var type = $("#Type").val();
            var unionid = $("#hid_UnionID").val();
            var unionname = $("#UnionName").val();
            var remark = $("#Remark").val();
            $.ajax({
                type: "post",
                url: "ModifyBlack",
                data: {
                    blackid: blackid,
                    type: type,
                    unionid: unionid,
                    unionname: unionname,
                    remark: remark
                },
                success: function (msg) {
                    console.log(msg);
                    alert("黑名单更新成功！");
                    window.location.href = "/BaseData/BlackList";
                }
            })
        });
    },    
}

function ModifyBlack(blackid, type, unionid, unionname, Remark) {
    $("#hid_BlackID").val(blackid);
    $("#Type").val(type);
    $("#hid_UnionID").val(unionid);
    $("#UnionName").val(unionname);
    $("#Remark").val(Remark);
}
