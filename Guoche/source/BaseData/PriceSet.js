var elemnets = {
    ids: ["StorageID", "CustomerID", "CarrierID", "ReceivingType"],
    methods: [
        {
            required: true,
            messages: ["请选择出库仓", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择所属客户", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择承运商", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择收货方类型", "", "", ""]
        }
    ]
};

var priceSetInfo = {
    init: function () {
        valid.init(elemnets);
        priceSetInfo.regEvent();
        priceSetInfo.initReceiver();
    },

    regEvent: function () {

        $("#ReceivingType").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetReceiver",
                    type: 'POST',
                    async: false,
                    data: { type: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#ReceivingID").html("").append("<option value=''>--请选择收货方--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#ReceivingID").append("<option value='" + data[i].ReceiverID + "'>" + data[i].ReceiverName + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#ReceivingID").html("").append("<option value=''>--请选择收货方--</option>");
            }
        });

        $("#goback").click(function () {
            window.location.href = "/PriceSet/";
        });

        $("#save").click(function () {

            if (valid.validate()) {
                $("#priceSetForm").submit();
            }
        });
    },
    initReceiver: function () {
        var pid = $("#hid_ReceivingType").val(), cid = $("#hid_ReceivingID").val();
        $("#ReceivingType").val(pid).trigger("change");
        $("#ReceivingID").val(cid);
    }
}

