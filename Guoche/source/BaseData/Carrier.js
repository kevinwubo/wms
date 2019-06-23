var elemnets = {
    ids: ["CarrierName", "CarrierShortName", "CarrierNo"],
    methods: [
        {
            required: true,
            messages: ["请输入承运商名称", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入承运商简称", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入承运商编号", "", "", ""]
        }
    ]
};

var carrierInfo = {
    init: function () {
        valid.init(elemnets);
        carrierInfo.regEvent();        
    },

    regEvent: function () {      
        $("#goback").click(function () {
            window.location.href = "/Carrier/";
        });

        $("#save").click(function () {

            var formParams = {};
            var params = {
                listContact: []
            };
            $("#fineTable").find("tr").each(function () {
                var tdArr = $(this).children();
                var name = tdArr.eq(0).find('input').val();//姓名
                var mobile = tdArr.eq(1).find('input').val();//手机
                var tel = tdArr.eq(2).find('input').val();//座机
                var email = tdArr.eq(3).find('input').val();//Email
                var remark = tdArr.eq(4).find('input').val();//备注
                var id = tdArr.eq(5).find('input').val();//联系人ID
                if (name != "Head") {
                    formParams = {
                        "ContactName": name,
                        "Mobile": mobile,
                        "Telephone": tel,
                        "Email": email,
                        "Remark": remark,
                        "ContactID": id
                    };
                    params.listContact.push(formParams);
                }

            });
            $("#ContactJson").val(JSON.stringify(params));

            if (valid.validate()) {
                $("#CarrierForm").submit();
            }
        });
    },    
}

