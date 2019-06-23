var elemnets = {
    ids: ["LineID", "ReceiverName"],
    methods: [
        {
            required: true,
            messages: ["请输入线路编号", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入门店地址", "", "", ""]
        }
    ]
};

var LineInfo = {
    init: function () {
        valid.init(elemnets);
        LineInfo.regEvent();
    },

    regEvent: function () {

        $("#ReceiverName").keyup(function () {
            var Name = $("#ReceiverName").val();
            $.ajax({
                type: "post",
                url: "SearchByName",
                data: {
                    name: Name,
                },
                success: function (data) {
                    console.log(data);
                    if (data) {
                        $("#div_items").html("")
                        $("#div_items").show();                        
                        for (var i = 0; i < data.length; i++) {
                            $("#div_items").append("<li class='div_item'>" + data[i].ReceiverName + "</li>");
                        }                        
                    }
                }
            })
            
        })

        //选中
        $("#div_items").click(function () {
            $(".div_item").each(function () {
                $(this).click(function () {
                    $("#ReceiverName").val($(this).html());
                })
            })
        })        

        $("#saveLine").click(function () {
            var lineID = $("#txt_LineID").val();
            var ReceiverName = $("#ReceiverName").val();
            var ID = $("#hid_ID").val();
            $.ajax({
                type: "post",
                url: "Modify",
                data: {
                    lineid: lineID,
                    name: ReceiverName,
                    id:ID
                },
                success: function (msg) {
                    console.log(msg);
                    alert("线路更新成功！");
                    window.location.href = "/Line/Index";
                }
            })
        });
    },    
}

function ModifyLine(id, lineid, receivername) {
    $("#hid_ID").val(id);
    $("#txt_LineID").val(lineid);
    $("#ReceiverName").val(receivername);
}
