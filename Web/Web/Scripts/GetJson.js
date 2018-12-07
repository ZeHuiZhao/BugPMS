function getJson(strSelect, TableName, strWhere, order, top) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebService/common.asmx/getdataTable",
        data: "{strSelect:'" + strSelect + "',TableName:'" + TableName + "',strWhere:'" + strWhere + "',order:'" + order + "',top:'" + top + "'}",
        dataType: "json",
        success: function (result) {

            alert(result.d);

            return result.d;


        },
        error: function b(ms) {
            alert("请求失败，请按F5刷新！");
            return;
        }
    });
}