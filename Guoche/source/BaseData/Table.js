/**下面是操作表格的方法*/
/**
 * 为table指定行添加一行
     * table的行从0开始
     * trHtml 添加行的html代码
 */
function addTr(trHtml) {
    //获取table最后一行 $("#fineTable tr:last")
    //获取table第一行 $("#fineTable tr").eq(0)
    //获取table倒数第二行 $("#fineTable tr").eq(-2)
    var $tr = $("#fineTable tr:last");
    if ($tr.size() == 0) {
        alert("指定的table或对应的行数不存在！");
        return;
    }
    $tr.after(trHtml);
}

/**删除一行*/
function delTr($this, id) {
    jQuery.ajax({
        url: "/Contact/Remove",
        data: { cid: id },
        type: "post",
        success: function (result) {
            if (result) {
                $($this).parent().parent().remove();
            }
        }
    });

}

/**生成一行
    * 这一行的写法就是：先写一个 return ''，然后将 <tr>......</tr> 一个 tr 中的内容直接复制过来即可。注意每个 tr 都应该带上对应的方法
    */
function oneTr() {
    return '<tr>\n' +
        '        <td><input type="text" id="contractName" class="form-control"></td>\n' +
        '        <td><input type="text" id="contractPhone" class="form-control "></td>\n' +
        '        <td><input type="text" id="contractTelephone" class="form-control "></td>\n' +
        '        <td><input type="text" id="contractEmail" class="form-control "></td>\n' +
        '        <td><input type="text" id="description" class="form-control "></td>\n' +
        '        <td><input type="hidden" id="contactID" value="0" /><span style="cursor:pointer" onclick="delTr(this)">移除</span></td>\n' +
        '    </tr>';
}