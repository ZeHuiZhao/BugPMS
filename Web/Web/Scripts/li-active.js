
var url = window.location.href;

/**处理url，兼容带参数和有#的url add by lx 2016-12-23*/
var href1 = url.split('?');
if (href1.length > 1) {
    url = href1[0];
}
var href2 = url.split('#');
if (href2.length > 1) {
    url = href2[0];
}
/**处理url，兼容带参数和有#的url add by lx 2016-12-23*/

$("#side-menu li a").each(function () {//遍历所有a标签
    var datali = $(this).attr("href");
    if (typeof (datali) != "undefined" && datali != "") {
        var index = url.toLowerCase().indexOf(datali.toLowerCase());
        if (index != -1) {
            $(this).parent().addClass("active");
            $(this).parent().parent().parent().addClass("active");
        }
    }
});
$("#side-menu li").each(function () {
    var datali = $(this).attr("data-li");
    if (typeof (datali) != "undefined" && datali != "") {
        var index = url.toLowerCase().indexOf(datali.toLowerCase());
        if (index != -1) {
            $(this).addClass("active");
        }
    }
});

//防止重复选取 ，如果有，去除链接长的那个
var activea = "";
$("#side-menu li li[class='active'] a").each(function () {//遍历所有a标签   
    var datali = $(this).attr("href");
    if (typeof (datali) != "undefined" && datali != "") {
        if (datali != "#")
        {
            if (activea == "")
            {
                activea = datali;
            }
            var datalen = datali.length;
            var alen = activea.length;
            //等于的情况不处理
            if (alen > datalen) {
                $(this).parent().removeClass("active");
            }
            else if (alen < datalen) {
                $("#side-menu li a[href='" + activea + "']").parent().removeClass("active");
            }
        }
    }
});
