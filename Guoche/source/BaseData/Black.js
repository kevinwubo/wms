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
            var status = $("#Status").prop("checked") ? 1 : 0;//$("#Status").val();
            var subStatus = $("#SubStatus").prop("checked") ? 1 : 0;//$("#SubStatus").val();
            var cardCode = $("#CardCode").val();
            var remark = $("#Remark").val();

            $.ajax({
                type: "post",
                url: "ModifyBlack",
                data: {
                    blackid: blackid,
                    type: type,
                    unionid: unionid,
                    unionname: unionname,
                    status: status,
                    subStatus: subStatus,
                    cardCode: cardCode,
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

function ModifyBlack(blackid, type, unionid, unionname,status,substatus,cardCode, Remark) {
    $("#hid_BlackID").val(blackid);
    $("#Type").val(type);
    $("#hid_UnionID").val(unionid);
    $("#UnionName").val(unionname);

    if (substatus == "1") {
        $("#SubStatus").attr("checked", true);//勾选
    }
    else {
        $("#SubStatus").attr("checked", false);//勾选
    }

    if (status == "1") {
        $("#Status").attr("checked", true);//勾选
    }
    else {
        $("#Status").attr("checked", false);//勾选
    }
    
    
    $("#CardCode").val(cardCode);
    $("#Remark").val(Remark);
}


function DeleteBlack(id) {
    var msg = "您确定要删除吗?\n请确认！";
    if (confirm(msg) == true) {
        $.ajax({
            type: "post",
            url: "/BaseData/DeleteBlack",
            data: {
                bid: id,
            },
            success: function (data) {
                console.log(data);
                if (data) {
                    alert("黑名单删除成功!");
                    window.location.href = "/BaseData/BlackList";

                }
            }
        })
    } else {
        return false;
    }
}