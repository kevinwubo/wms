var elemnets = {
    ids: ["Name", "Code", "sltCity", "ChargeFee", "ServerFee", "ParkFee", "Address", "PayType", "StartTime", "EndTime"],
    methods: [
        {
            required: true,
            messages: ["请输入名称", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入代码", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择城市", "", "", ""]
        },
        {
            required: true,
            rule: /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/,
            messages: ["请输入充电费", "", "", "请输入正确的金额(XX.XX)"]
        },
        {
            required: true,
            rule: /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/,
            messages: ["请输入服务费", "", "", "请输入正确的金额(XX.XX)"]
        },
        {
            required: true,
            rule: /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/,
            messages: ["请输入停车费", "", "", "请输入正确的金额(XX.XX)"]
        },
        {
            required: true,
            messages: ["请输入地址", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择支付方式", "", "", ""]
        },
        {
            required: false,
            rule: /^[0-2]{0,1}\d{1}:{1}[0-5]{1}[0-9]{1}$/,
            messages: ["", "", "", "请输入正确的营业时间（XX:XX）"]
        },
        {
            required: false,
            rule: /^[0-2]{0,1}\d{1}:{1}[0-5]{1}[0-9]{1}$/,
            messages: ["", "", "", "请输入正确的营业时间（XX:XX）"]
        }
    ]
};
var chargeBase = {
    init: function () {
        valid.init(elemnets);
        chargeBase.regEvent();
        chargeBase.initCity();
    },

    regEvent: function () {
        $("#sltProvince").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetCity",
                    type: 'POST',
                    async: false,
                    data: { pid: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#sltCity").html("").append("<option value=''>--城市--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#sltCity").append("<option value='" + data[i].CityID + "'>" + data[i].CityName + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#sltCity").html("").append("<option value=''>--城市--</option>");
            }
        });

        $("#save").click(function () {
            if (valid.validate()) {
                $("#baseForm").submit();
            }
        });

        $("input[name=pType]").click(function () {
            var codes = [];
            $("input[name=pType]:checked").each(function (index, item) { codes.push($(item).val()) });
            $("#PayType").val(codes.join(","));
        })


    },

    initCity: function () {
        var pid = $("#provinceid").val(), cid = $("#cityid").val();
        $("#sltProvince").val(pid).trigger("change");
        $("#sltCity").val(cid);
    }
}