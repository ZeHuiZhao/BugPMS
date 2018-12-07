var dropListOne="";
var strSelectOne="";
var TableNameOne="";
var strWhereOne="";
var orderOne="";
var topOne="";
//-------------两个控件分割线---------------
var dropListTwo="";
var strSelectTwo="";
var TableNameTwo = "";
var strWhereTwo = "";
var orderTwo="";
var topTwo = "";
function setParameter(dropListOneT, strSelectOneT, TableNameOneT, strWhereOneT, orderOneT, topOneT, dropListTwoT, strSelectTwoT, TableNameTwoT, strWhereTwoT, orderTwoT, topTwoT)
{
    dropListOne = dropListOneT;//下拉框ID
    strSelectOne = strSelectOneT;//获取的字段
    TableNameOne = TableNameOneT;//表名称
    strWhereOne = strWhereOneT;//where条件
    orderOne = orderOneT;//排序
    topOne = topOneT;//获取条数（为空时候全部获取）
    dropList1 = "#" + dropListOne
    //-------------两个控件分割线---------------
    dropListTwo = dropListTwoT;
    strSelectTwo = strSelectTwoT;
    TableNameTwo = TableNameTwoT;
    strWhereTwo = strWhereTwoT;
    orderTwo = orderTwoT;
    topTwo = topTwoT;
    BindDropDownListOne(dropListOne, strSelectOne, TableNameOne, strWhereOne, orderOne, topOne);//设置变量值之后，绑定第一个下拉框
}


//这个是绑定第一个下拉框函数
function BindDropDownListOne(dropList,strSelect, TableName, strWhere, order, top)
{
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebService/common.asmx/getdataTable",
        data: "{strSelect:'" + strSelect + "',TableName:'" + TableName + "',strWhere:'" + strWhere + "',order:'" + order + "',top:'" + top + "'}",
        dataType: "json",
        success: function (result) {
            //alert(result.d);
            bindSemesterDDL(result.d,dropList);
        },
        error: function b(ms) {
            alert("请求失败，请按F5刷新！");
            return;
        }
    });
}
///绑定数据到第一个下拉框
function bindSemesterDDL(strJson, dropList) {
    var dataArray = eval(strJson);//分割成一个一个的对象
    $("#" + dropList + "").empty();
    $("#" + dropList + "").append($("<option value='" + -1 + "'>---请选择---</option>"));
    for (var i = 0; i < dataArray.length; i++) {
        $("#" + dropList + "").append($("<option value='" + dataArray[i].ID + "'>" + dataArray[i].TrueName + "</option>"));
    }
}
 

//第一个下拉框切换时候
$("#dwnList1").change(function () {
    var ID = $("#" + dropListOne + " option:selected").val();

    var strSelect = strSelectTwo;
    var TableName = TableNameTwo;
    //var strWhere = "" + strWhereTwo + "" + ID + " "; 
    var strWhere = strWhereTwo.replace("#$%&", ID);
    var order = orderTwo;
    var top = topTwo;


    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebService/common.asmx/getdataTable",
        data: "{strSelect:'" + strSelect + "',TableName:'" + TableName + "',strWhere:'" + strWhere + "',order:'" + order + "',top:'" + top + "'}",
        dataType: "json",
        success: function (result) {
            //var select = strSelectTwo.split(",");
            //alert(select[1]);
            bindSemesterDDL2(result.d);
            $("#" + dropListTwo + "").removeAttr("disabled");
        },
        error: function b(ms) {
            alert("请求失败，请F5刷新");
            return;
        }
    });
})

/////绑定数据到第二个下拉框
function bindSemesterDDL2(strJson) {
    if (strJson.length < 3) {
        $("#dwnList2").empty();
        $("#dwnList2").append($("<option value=\"0\">---请选择---</option>"));
    }
    else {
        var dataArray = eval(strJson);//分割成一个一个的对象 
        $("#dwnList2").empty();
        for (var i = 0; i < dataArray.length; i++) {
            $("#dwnList2").append($("<option value='" + dataArray[i].ID + "'>" + dataArray[i].TName + "</option>"));
        }
    }
}



 