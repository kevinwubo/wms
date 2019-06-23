var elemnets = {
    ids: ["TypeCode", "CustomerID", "GoodsNo", "GoodsName", "BarCode"],
    methods: [
        {
            required: true,
            messages: ["请选择商品类型", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择所属客户", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入商品编号", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入商品名称", "", "", ""]
        }
        ,
        {
            required: true,
            messages: ["请输入商品条码", "", "", ""]
        }
    ]
};

var goodInfo = {
    init: function () {
        valid.init(elemnets);
        goodInfo.regEvent();
    },

    regEvent: function () {      
        $("#goback").click(function () {
            window.location.href = "/Goods/";
        });

        $("#save").click(function () {

            if (valid.validate()) {
                $("#goodsForm").submit();
            }
        });
    },    
}

