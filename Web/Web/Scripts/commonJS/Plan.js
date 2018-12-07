
$(function () {
    var WebUrlPath = "";
    var pageModel = $("#PlanModal");//模态框
    var PagemodalTitle = $("#PlanModal .modal-title");
    var Modal = $("#Modal");//模态框
    var ModalTitle = $("#Modal .modal-title");
    var hideId = $("#userId");
    
    var ProjectID = $("#ProjectInfo").attr("data-id");
    //提需求
    $("button.ProjectAdd").click(function () {
        var tr = $(this).parent().parent();
        var html = $("#ChangePlanTemplate").html();
        Modal.attr("data-operate", "PlanAdd");
        $("#Modal div.modal-body").html(html);
        ModalTitle.html("提需求");
        Modal.modal("show");

    });
    //修改计划
    $("a.ChangePlan").click(function () {
        var tr = $(this).parent().parent();
        var PlanID = $(this).attr("data-Planid");

        var html = $("#ChangePlanTemplate").html();
        Modal.attr("data-operate", "PlanChange");
        Modal.attr("data-Planid", PlanID);
        
        $("#Modal div.modal-body").html(html);
        ModalTitle.html("修改项目内容");
        //绑定文本框
        var PlanName = tr.find("td span.trContents").text();
      
        var Target = tr.find("td.trTarget").text();
        var ExpectedDate = tr.find("td.ExpectedDate").text();
        var changestr = PlanName + Target + ExpectedDate;
        Modal.attr("changestr", changestr);
        $("#Modal div.modal-body textarea.PlanName").val(PlanName);
        $("#Modal div.modal-body input.Target").val(Target);
        $("#Modal div.modal-body input.ExpectedDate").val(ExpectedDate);
        Modal.modal("show");

    });    

    //修改计划（弹框中的保存按钮）
    $("#btnPlanChange").click(function () {
        var formData = new FormData();
        $(this).attr("disabled", "disabled");
        var tr = Modal.find("div.modal-body");// $(this).parent().parent();
        var operate = Modal.attr("data-operate");
        //判断文件类型
        var filename = Modal.find("input[name='FilePlan']").val();
        var flag = true;
        if (filename != "" && typeof (filename) != "undefined") {
            var point = filename.lastIndexOf(".");
            if (point > -1) {
                var type = filename.substr(point).toLowerCase();
                if (type == ".jpg" || type == ".gif" || type == ".png"
                    || type == ".doc" || type == ".docx" || type == ".pdf"
                    || type == ".xls" || type == ".xlsx" || type == ".txt"
                    || type == ".zip" || type == ".rar") {                    
                    //格式符合
                    var file = Modal.find("input[name='FilePlan']")[0].files[0];
                    formData.append("FilePlan", file);
                }
                else {
                    toastr.warning("附件格式不正确");
                    $(this).removeAttr("disabled");
                    flag = false;
                }
            }
        }
        if (operate == "PlanAdd" && flag) {
            var PlanID = 0;//Modal.attr("data-Planid");

            var PlanName = $(tr).find("textarea.PlanName").val();//计划内容

            var PlanTarget = $(tr).find("input.Target").val();
            var ExpectedDate = $(tr).find("input.ExpectedDate").val();
            if (PlanName == "") {
                toastr.warning("请填写需求/bug描述");
                $(this).removeAttr("disabled");
                tr.addClass("has-error");
            }
            else if (ExpectedDate == "") {
                toastr.warning("请填写期望完成时间");
                $(this).removeAttr("disabled");
                tr.addClass("has-error");
            }
            else {
                var strMath = "zlgw" + new Date().getTime();
                var json = {
                    ProjectID: ProjectID, PlanName: PlanName,
                    PlanTarget: PlanTarget, ExpectedDate: ExpectedDate,
                    strMath: strMath
                };
                formData.append("ProjectID", ProjectID);
                formData.append("PlanName", PlanName);
                formData.append("PlanTarget", PlanTarget);
                formData.append("ExpectedDate", ExpectedDate);
                formData.append("strMath", strMath);

                $.ajax({
                    type: "POST",
                    url: WebUrlPath + '/Json/ProjectPlanAdd',
                    data: formData,
                    //告诉jQuery不要去处理发送的数据
                    processData: false,
                    // 告诉jQuery不要去设置Content-Type请求头
                    contentType: false,
                    success: function (data) {
                        if (data.Result == "success") {
                            toastr.success(data.Message);
                            var url = "window.location.reload();";
                            window.setTimeout(url, 1000);
                        }
                        else {
                            $("#btnPlanChange").removeAttr("disabled");
                            toastr.error(data.Message);
                            tr.addClass("has-error");
                        }
                    },
                    error: function () {
                        toastr.error("网络繁忙请稍后再试");
                    }
                });
            }
        }
        else if (operate == "PlanChange" && flag) {
            var PlanID = Modal.attr("data-Planid");
            var PlanName = $(tr).find("textarea.PlanName").val();//计划内容
            var PlanTarget = $(tr).find("input.Target").val();
            var ExpectedDate = $(tr).find("input.ExpectedDate").val();
            var changestr = Modal.attr("changestr");
            var newchangestr = PlanName + PlanTarget + ExpectedDate;
            if (changestr == newchangestr && (filename == "" || typeof (filename) == "undefined")) {
                toastr.warning("未修改任何内容");
                $(this).removeAttr("disabled");
                Modal.modal("hide");
            }
            else if (PlanName == "") {
                toastr.warning("请填写需求/bug描述");
                $(this).removeAttr("disabled");
                tr.addClass("has-error");
            }
            else {
                var strMath = "zlgw" + new Date().getTime();
                var json = {
                    ProjectID: ProjectID,PlanID:PlanID,
                    PlanName: PlanName, PlanTarget: PlanTarget, ExpectedDate: ExpectedDate,
                    strMath: strMath
                };
                formData.append("ProjectID", ProjectID);
                formData.append("PlanID", PlanID);
                formData.append("PlanName", PlanName);
                formData.append("PlanTarget", PlanTarget);
                formData.append("ExpectedDate", ExpectedDate);
                formData.append("strMath", strMath);
                $.ajax({
                    type: "POST",
                    url: WebUrlPath + '/Json/ProjectPlanChange',
                    data: formData,
                    //告诉jQuery不要去处理发送的数据
                    processData: false,
                    // 告诉jQuery不要去设置Content-Type请求头
                    contentType: false,
                    success: function (data) {
                        if (data.Result == "success") {
                            toastr.success(data.Message);
                            var url = "window.location.reload();";
                            window.setTimeout(url, 1000);
                        }
                        else {
                            $("#btnPlanChange").removeAttr("disabled");
                            toastr.error(data.Message);
                            tr.addClass("has-error");
                        }
                    },
                    error: function () {
                        toastr.error("网络繁忙请稍后再试");
                    }
                });
            }
        }
    });
   
    $("a.delPlan").click(function () {
        var PlanID = $(this).attr("data-Planid");
        var strMath = "zlgw" + new Date().getTime();
        var msg = "删除后不可恢复，确定删除？";
        var confir = confirm(msg);
        if (confir) {
            $.ajax({
                type: "POST",
                url: WebUrlPath+'/Json/ProjectTaskDel',
                data: {PlanID: PlanID, strMath: strMath},
                cache: false,
                success: function (data) {
                    if (data.Result == "success")
                    {
                        toastr.success(data.Message);
                        var url = "window.location.reload();";
                        window.setTimeout(url, 1000);
                    }
                    else {
                        toastr.error(data.Message);
                    }
                },
                error: function () {
                    toastr.error("网络繁忙请稍后再试");
                }
            });
        }
    });
   
    //提交审核按钮
    $("button.ProjectSubmit").click(function () {
        var strMath = "zlgw" + new Date().getTime();
        $.ajax({
            type: "POST",
            url: WebUrlPath + '/Json/ProjectSubmit',
            data: {
                ProjectID: ProjectID, strMath: strMath
            },
            success: function (data) {
                if (data.Result == "success")//新增成功
                {
                    toastr.success(data.Message);
                    var url = "location.href = '" + data.Address + "';";
                    window.setTimeout(url, 1000);
                }
                else {
                    toastr.error(data.Message);
                    tr.addClass("has-error");
                }
            },
            error: function () {
                toastr.error("网络繁忙请稍后再试");
            }
        });
    });
    //审核状态
    $.ajax({
        type: "POST",
        url: WebUrlPath+"/Json/getBaseData",
        data: { TableName: 'ProjectTaskCheckStatus' },
        success: function (result) {
            var pHtml = "<option value=''>审核状态</option>";
            var data = JSON.parse(result);
            for (var i = 0; i < data.length; i++) {
                pHtml += "<option value='" + data[i].KeyID + "' class='" + data[i].ClassName + "'>" + data[i].Name + "</option>"
            }
            $("#ProjectTaskCheckStatusID").html(pHtml);
        },
        error: function b(ms) {
            alert("请求失败，请按F5刷新！");
            return;
        }
    });
    //获取两个日期的周数差或天数差，两天相等则差为1
    function getWeekNum(StartDate, EndDate, flag) {
        if (StartDate != "" && EndDate != "") {
            StartDate = new Date(StartDate);
            EndDate = new Date(EndDate);
            var days = EndDate.getTime() - StartDate.getTime();

            
            if (flag == "day") {
                var day = Math.ceil(days / (1000 * 60 * 60 * 24));
                return day;
            }
            else {//取整
                var day = parseInt(days / (1000 * 60 * 60 * 24));
                var week = Math.ceil(day / 7);
                return week;
            }
        }
        else {
            return 0;
        }
    }
    $(".mc-content").on("click", "#selectAll", function () {//全选反选
        if ($(this).prop("checked")) {
            $("input.CBPlan:checkbox").prop("checked", true); 
        }
        else {
            $("input.CBPlan:checkbox").prop("checked", false);
        }
    });
    //审核通过和不通过
    $(".mc-content").on("click", "button.PlanCheck", function () {
        $(this).attr("disabled", "disabled");
        var operate = $(this).attr("data-operate");
        var strPlanid = "";
        var checkNote = $("#CheckNote").val();
        $("input.CBPlan").each(function () {
            if ($(this).prop("checked")) {//选中了
                var pid = $(this).attr("data-id");
                strPlanid += pid + ",";
            }          
        });
        if (strPlanid != "") {
            if (operate == "checkno" && checkNote == "") {
                toastr.error("请填写审核意见");
                $(this).removeAttr("disabled");
            }
            else if (operate == "checkyes" || operate == "checkno") {
                $.ajax({
                    type: "POST",
                    url: WebUrlPath + "/Json/CheckPlan",
                    data: { operate: operate, checkNote: checkNote, strPlanid: strPlanid,ProjectID:ProjectID },
                    success: function (res) {
                        if (res.Result == "success")
                        {
                            toastr.success(res.Message);
                            //var url = "location.href = '" + res.Address + "';";
                            var url = "window.location.reload();";
                            window.setTimeout(url, 1000);
                        }
                        else {
                            toastr.error(res.Message);
                        }
                    },
                    error: function b(ms) {
                        alert("请求失败，请按F5刷新！");
                        return;
                    }
                });
            }
        }
        else {
            toastr.error("请勾选要审核的项");
            $(this).removeAttr("disabled");
        }
    });


});
