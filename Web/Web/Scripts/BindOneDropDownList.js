//这个是单个下拉框绑定的时间函数
function BindDropDownList(dropList,strSelect, TableName, strWhere, order, top)
{
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebService/common.asmx/getdataTable",
        data: "{strSelect:'" + strSelect + "',TableName:'" + TableName + "',strWhere:'" + strWhere + "',order:'" + order + "',top:'" + top + "'}",
        dataType: "json",
        success: function (result) {
           bindSemesterDDL(result.d,dropList);
        },
        error: function b(ms) {
            alert("请求失败，请按F5刷新！");
            return;
        }
    });
}
///绑定数据到下拉框
function bindSemesterDDL(strJson, dropList) {
    var dataArray = eval(strJson);//分割成一个一个的对象
    $("#" + dropList + "").empty();
    $("#" + dropList + "").append($("<option value='" + -1 + "'>---请选择---</option>"));
    for (var i = 0; i < dataArray.length; i++) {
        $("#" + dropList + "").append($("<option value='" + dataArray[i].ID + "'>" + dataArray[i].TrueName + "</option>"));
    }
}