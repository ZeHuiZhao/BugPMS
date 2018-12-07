/******************************
【说明】：1、需要先引用jquery，然后再引用gantt.css和本js
2、初始化时指定任意一个div，比如   <div id="demo"> </div>，然后初始化参数（demo示例详见demo.html）
 opts = {
        title: "这是甘特图的标题",
        dataLeft: dataLeft,//含ID,标题，内容，起止时间，标识flag
        dataUser: dataUser,
        dataCustom: dataCustom,
        showCheckbox: false,
        showEdit: true,
        showDel: false
    };
    dataLeft中第一行是标题，dataLeft中"data-color":"#f00" 可为甘特图对应行的图形部分指定颜色,StageName和ProjectName相同的列会自动合并（行合并）
    dataUser和dataCustom中第一列是人员职责，第二列是人员姓名，其他列是工作内容
    //生成甘特图
    $("#table").gantt(opts);
**********************************/
jQuery.fn.gantt = function (opts) {
    //json中data-StartDate，data-EndDate，data-Content是必须的，而且必须是大小写一致的
    //StageName和ProjectName会自动去除重复
    opts = jQuery.extend({
        title:"工作进度",
        dataLeft: "",//含ID,标题，内容，起止时间，标识flag
        dataUser: "",
        dataCustom: "",
        showCheckbox: true,
        showEdit: false,
        showDel:false,

        callback: function () { return false; }
    }, opts || {});

    return this.each(function () {
        //获取两个日期的周数差或天数差，两天相等则差为1
        function getWeekNum(StartDate, EndDate, flag) {
            StartDate = new Date(StartDate);
            EndDate = new Date(EndDate);
            var days = EndDate.getTime() - StartDate.getTime();
            
            var day = parseInt(days / (1000 * 60 * 60 * 24));
            if (flag == "day") {
                return day+1;
            }
            else {//取整
                var week = Math.ceil(day / 7);
                return week;
            }
        }
        function DayAdd(Dates, days) {//日期加减
            Dates = Dates.getTime() + 1000 * 60 * 60 * 24 * days;
            Dates = new Date(Dates);
            return Dates;
        }
        //将周数1，2，3转换为第一周，第二周这样的形式
        function getWeekName(weekNum,flag) {
            var res="";
            weekNum = weekNum.toString();
            var ary0 = ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九"];
            var ary1 = ["", "十", "百", "千"];
            var ary2 = ["", "万", "亿", "兆"];
            var ary = [];
            for (var i = weekNum.length; i >= 0; i--) {
                ary.push(weekNum[i])
            }
            var zero = "";
            var newary = "";
            ary = ary.join("");
            var i4 = -1
            for (var i = 0; i < ary.length; i++) {
                if (i % 4 == 0) { //首先判断万级单位，每隔四个字符就让万级单位数组索引号递增  
                    i4++;
                    newary = ary2[i4] + newary; //将万级单位存入该字符的读法中去，它肯定是放在当前字符读法的末尾，所以首先将它叠加入$r中，  
                    zero = ""; //在万级单位位置的“0”肯定是不用的读的，所以设置零的读法为空  

                }
                //关于0的处理与判断。  
                if (ary[i] == '0') { //如果读出的字符是“0”，执行如下判断这个“0”是否读作“零”  
                    switch (i % 4) {
                        case 0:
                            break;
                            //如果位置索引能被4整除，表示它所处位置是万级单位位置，这个位置的0的读法在前面就已经设置好了，所以这里直接跳过  
                        case 1:
                        case 2:
                        case 3:
                            if (ary[i - 1] != '0') {
                                zero = "零"
                            }
                            ; //如果不被4整除，那么都执行这段判断代码：如果它的下一位数字（针对当前字符串来说是上一个字符，因为之前执行了反转）也是0，那么跳过，否则读作“零”  
                            break;

                    }

                    newary = zero + newary;
                    zero = '';
                }
                else { //如果不是“0”  
                    newary = ary0[parseInt(ary[i])] + ary1[i % 4] + newary; //就将该当字符转换成数值型,并作为数组ary0的索引号,以得到与之对应的中文读法，其后再跟上它的的一级单位（空、十、百还是千）最后再加上前面已存入的读法内容。  
                }

            }
            if (newary.indexOf("零") == 0) {
                newary = newary.substr(1);
            }//处理前面的0  
            if (newary.indexOf("一十") == 0)//将第一十二周这种替换为十二周
            {
                newary = newary.substr(1);
            }
            res = newary;
            if (flag != "no")
            {
                res = "第" + newary + "周";
            }
            return res;
        }

        function DateFormat(now, mask) {
            var d = now;
            var zeroize = function (value, length) {
                if (!length) length = 2;
                value = String(value);
                for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {
                    zeros += '0';
                }
                return zeros + value;
            };

            return mask.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMstT])\1?|[lLZ])\b/g, function ($0) {
                d = new Date(d);
                switch ($0) {
                    case 'd': return d.getDate();
                    case 'dd': return zeroize(d.getDate());
                    case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][d.getDay()];
                    case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][d.getDay()];
                    case 'M': return d.getMonth() + 1;
                    case 'MM': return zeroize(d.getMonth() + 1);
                    case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][d.getMonth()];
                    case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][d.getMonth()];
                    case 'yy': return String(d.getFullYear()).substr(2);
                    case 'yyyy': return d.getFullYear();
                    case 'h': return d.getHours() % 12 || 12;
                    case 'hh': return zeroize(d.getHours() % 12 || 12);
                    case 'H': return d.getHours();
                    case 'HH': return zeroize(d.getHours());
                    case 'm': return d.getMinutes();
                    case 'mm': return zeroize(d.getMinutes());
                    case 's': return d.getSeconds();
                    case 'ss': return zeroize(d.getSeconds());
                    case 'l': return zeroize(d.getMilliseconds(), 3);
                    case 'L': var m = d.getMilliseconds();
                        if (m > 99) m = Math.round(m / 10);
                        return zeroize(m);
                    case 'tt': return d.getHours() < 12 ? 'am' : 'pm';
                    case 'TT': return d.getHours() < 12 ? 'AM' : 'PM';
                    case 'Z': return d.toUTCString().match(/[A-Z]+$/);
                        // Return quoted strings with the surrounding quotes removed
                    default: return $0.substr(1, $0.length - 2);
                }
            });
        };

        function GetNumByObjAndName(Name, Val, object) {//对象必须连续，否则就终止
            console.log(object);
            var len = object.length;
            var Num = 0;
            var flag = -1;
            for (var i = 0; i < len; i++) {
                var trLine = object[i];
                for (var item in trLine) {
                    if (Name == item.toString().toLowerCase()) {
                        var value = trLine[item];
                        if (value == Val && value != "") {
                            if (i == flag + 1 || flag == -1) {
                                Num++;
                                flag = i;
                            }
                            else {
                                flag = 0;
                            }
                            break;
                        }
                    }
                }
            }
            return Num;
        }
        //绑定左侧表格
        function BindHtml() {
            $this.find("th.ganttHeadOne").html(opts.title);;
            //第一列为标题（表头）
            var Left = dataLeft.length;
            var html = "<table border='0' cellpadding='0' cellspacing='0'>";
            var LeftLen = 0;
            var Stage = "";
            var Project = "";
            var No=1;
            for (var i = 0; i < dataLeft.length; i++) {
                var trLine = dataLeft[i];
                if (i == 0) {
                    var trLineLength = getJsonObjLength(trLine);
                    if (opts.showCheckbox) {
                        trLineLength++;
                    }
                    //html += "<tr><th class='center' colspan='" + trLineLength + "'>" + opts.title + "</th></tr>";
                    html += "<tr  "
                    var strTd = "";
                    if (opts.showCheckbox) {
                        strTd += "<th class='ganttth doubleHeight'></th>";
                    }                   
                    for (var item in trLine)
                    {
                        var value = trLine[item];
                        if (item.indexOf("data-") == -1) {
                            strTd += "<th  class='ganttth doubleHeight'>　" + value + "　</th>";
                            LeftLen++;                            
                        }
                        else {
                            html += " " + item + "='" + value + "'";                          
                        }                      
                    }                   
                    html += " >" + strTd + "</tr><tr></tr>";
                }
                else {
                    html += "<tr "
                    var strTd = "";
                    
                    if (opts.showCheckbox) {
                        strTd += "<td><input type='checkbox' /></td>";
                    }
                    for (var item in trLine) {
                        var value = trLine[item];
                        if (item.indexOf("data-") == -1) {
                            var itemName=item.toString().toLowerCase();
                            //if (value != "" && (itemName == "stagename" || itemName == "projectname"))
                            if (value != "" && itemName == "projectname")
                            {
                                if (Project == value && itemName == "projectname") {

                                }
                                else {
                                    var Num = GetNumByObjAndName(itemName, value, dataLeft);
                                    strTd += "<td rowspan='" + Num + "' class='center'>" + getWeekName(No,"no") + "</td><td rowspan='" + Num + "'>" + value + "</td>";
                                    Project = value;
                                    No++;
                                }
                            }
                            else{
                                strTd += "<td>" + value + "</td>";
                            }
                        }
                        else  {
                            html += " " + item + "='" + value + "'";
                            if (item.toString().toLowerCase() == "data-startdate") {
                                minDate = CompareDate(minDate, value,"min");
                            }
                            if (item.toString().toLowerCase() == "data-enddate") {
                                maxDate = CompareDate(maxDate, value,"max");
                            }
                        }
                        
                    }
                    html += " >" + strTd + "</tr>";
                   
                }
            }
            html += "</table>";
            $this.find("tr td.Ganttrow1 div").html(html);
        }
        //获取json对象的长度（个数）
        function getJsonObjLength(jsonObj) {
            var Length = 0;
            for (var item in jsonObj) {
                if (item.indexOf("data-") == -1) {
                    Length++;
                }
            }
            return Length;
        }

        //对比两个时间，根据flag返回较大或者较小的一个
        function CompareDate(dateold, datenew, flag) {
            if (datenew != "") {
                var dateold = new Date(dateold);
                var datenew = new Date(datenew);

                if (flag == "min") {
                    if (dateold.getTime() > datenew.getTime()) {
                        return datenew;
                    } else {
                        return dateold;
                    }
                }
                else {
                    if (dateold.getTime() > datenew.getTime()) {
                        return dateold;
                    } else {
                        return datenew;
                    }
                }
            }
            else {
                return dateold;
            }
        }

        function BindDateTitle() {//生成工作时间的表头
            var tdNum = WeekNum * 7;
            var trOne = "";//"<tr><th colspan='" + tdNum + "' class='center'>工作时间</th></tr>";
            var html = "<table border='0' cellpadding='0' cellspacing='0'>" +trOne + "<tr>";
            var htmltwo = "";
            
            for (var i = 0; i < WeekNum; i++)
            {
                html += "<th colspan='7' class='ganttth'>" + getWeekName(i + 1) + "</th>";
            }
            html+="</tr><tr>"
            for (var i = 0; i < tdNum; i++) {//需要计算这是几号
                if (i == 0) {
                    htmltwo += "<th width='50' class='ganttth' title='" + DateFormat(minDate, "yyyy-MM-dd") + "'>　" + DateFormat(minDate, "d") + "　</th>";
                }
                else {
                    var bgColorClass = "";
                    if (i % 7 == 5 || i % 7 == 6) {
                        bgColorClass = " weekEnd";
                    }
                    var newDate = DayAdd(minDate, i);
                    htmltwo += "<th width='50' class='ganttth" + bgColorClass + "' title='" + DateFormat(newDate, "yyyy-MM-dd") + "'>　" + DateFormat(newDate, "d") + "　</th>";
                }
            }
            html += htmltwo + "</tr>";
            $this.find("tr td.Ganttrow2 div").html(html);
        }

        function BindDateContent() {//生成工作时间的甘特图,若结束时间小于于开始时间，则不输出
            var tdNum = WeekNum * 7;
            var html = "";
            
            for (var i = 1; i < dataLeft.length; i++) {
                html = "<tr ";
                var colorClass = "";
                var trLine = dataLeft[i];
                for (var item in trLine) {
                    var value = trLine[item];
                    if (item.indexOf("data-") > -1) {
                        html += " " + item + "='" + value + "'";
                    }
                    if (item.toString().toLowerCase() == "data-startdate") {
                        minDate = CompareDate(minDate, value, "min");
                    }
                    if (item.toString().toLowerCase() == "data-enddate") {
                        maxDate = CompareDate(maxDate, value, "max");
                    }
                    if (item.toString().toLowerCase() == "data-color") {
                        if (value != "")
                        {
                            colorClass = " style='background-color:"+value+";'";
                        }
                    }
                }
                html += " >";
                var StartDate = trLine["data-StartDate"];
                var EndDate = trLine["data-EndDate"];
                var Content = trLine["Content"];
                if (typeof (StartDate) == "undefined" || typeof (EndDate) == "undefined") {
                    console.log("属性非法，请查看BindDateContent");
                }
                else {//获取到起始时间和截至时间
                    if (StartDate == "" || EndDate == "") {
                        for (var j = 0; j < tdNum; j++) {//需要计算这是几号
                            var bgColorClass = "";
                            if (j % 7 == 5 || j % 7 == 6) {
                                bgColorClass = " weekEnd";
                            }
                            html += "<td class='" + bgColorClass + "'></td>";
                        }
                    }
                    else {
                        StartDate = new Date(StartDate);
                        EndDate = new Date(EndDate);
                        var StartNum = getWeekNum(minDate, StartDate, "day");
                        var DayNum = getWeekNum(StartDate, EndDate, "day");
                        var flag = false;
                        for (var j = 0; j < tdNum; j++) {//需要计算这是几号     
                            var bgColorClass = "";
                            if (j % 7 == 5 || j % 7 == 6) {
                                bgColorClass = " weekEnd";
                            }
                            if (StartNum == (j + 1) && DayNum > 0) {
                                var msg = "工作内容：" + Content + "，时间：" + DateFormat(StartDate, "MM-dd") + "至" + DateFormat(EndDate, "MM-dd")
                                html += "<td colspan='" + (DayNum) + "' class='tdGanttTime tdGantt' " + colorClass + " title='" + msg + "'></td>";
                                j = (j + DayNum-1);
                                //flag = true;
                            }
                            //else if (flag) {

                            //    html += "<td class='" + bgColorClass + "'><div class='Ganttcontent'>" + Content + "</span></td>";
                            //    flag = false;
                            //}
                            else {
                                html += "<td class='" + bgColorClass + "' ></td>";
                            }
                        }
                    }
                }
                html += "</tr>";
                $this.find("tr td.Ganttrow2 div table").append(html);
            }
        }

        function BindUser(dataobject,user) {
            var len = dataobject.length;
            var html = "<table border='0' cellpadding='0' cellspacing='0'>";
            var LeftLen = 0;
            var Stage = "";
            var Project = "";
            for (var i = 0; i < len; i++) {
                var trLine = dataobject[i];
                html += "<tr ";
                var strTd = "";
                for (var item in trLine) {
                    var value = trLine[item];
                    if (item.indexOf("data-") == -1) {
                        if (i > 1) {
                            strTd += "<td>" + value + "</td>";
                        }
                        else {
                            strTd += "<th class='ganttth'>　" + value + "　</th>";
                        }
                    }
                    else {
                        html += " " + item + "='" + value + "'";
                    }

                }
                html += " >" + strTd + "</tr>";
            }
            html += "</table>";
            if (user == "user") {
                $this.find("tr td.Ganttrow3 div").html(html);
            }
            else if (user == "custom") {
                $this.find("tr td.Ganttrow4 div").html(html);
            }
        }
        function BindOptrate() {
            var len = dataLeft.length;
            var html = "<table border='0' cellpadding='0' cellspacing='0'><tr><th class='ganttth doubleHeight'>　操作　</th></tr>";
            var LeftLen = 0;
            var Stage = "";
            var Project = "";
            for (var i = 1; i < len; i++) {
                var trLine = dataLeft[i];
                html += "<tr ";
                var strTd = "";
                for (var item in trLine) {
                    var value = trLine[item];
                    if (item.indexOf("data-") > -1) {
                        html += " " + item + "='" + value + "'";
                    }
                }
                strTd += "<td class='center'><a href='#' class='ganttEdit' title='编辑'><i class='icoEdit'></i></a></td>";
                html += " >" + strTd + "</tr>";
            }
            html += "</table>";
            $this.find("tr td.Ganttrow5 div").html(html);

        }
       //获取到对象以便调用
        var $this = $(this);
        $this.html(
            " <table id='gantt_jsc' class='gantt_jsc' border='0' cellpadding='0' cellspacing='0'>"
            + "<tr><th class='ganttHead ganttHeadOne' colspan='2'>工作内容</th> "
            + "<th class='ganttHead' colspan='2'>责任人工作分解</th>"
            + "<th class='ganttHead operateHeight' rowspan='2'></th></tr>"
            + "<tr><th class='ganttHead'>工作内容</th><th class='ganttHead'>工作时间</th>"
            + "<th class='ganttHead'>　中力方　</th><th class='ganttHead'></th></tr>"
            + "<tr><td class='Ganttrow Ganttrow1 noborder'><div></div></td>"
            + "<td class='Ganttrow Ganttrow2 noborder'><div class='ganttScoll'></div></td>"
            + "<td class='Ganttrow Ganttrow3 noborder'><div></div></td>"
            + "<td class='Ganttrow Ganttrow4 noborder'><div></div></td>"
            + "<td class='Ganttrow Ganttrow5 noborder'><div></div></td></tr> </table>");
        var minDate = "2100-1-1";
        var maxDate = "1900-1-1";

        
        //绑定左侧列表
        BindHtml();
        var WeekStart = minDate.getDay();//获取到这天是周几,如果不是周一，那么就减到周一
        var WeekEnd = maxDate.getDay();//不是周日就加到周日
        //将起始日期设置到第一周的周一，将结束时间设置到最后一周的周末
        if (WeekStart != 1)
        {
            minDate = DayAdd(minDate, 1 - WeekStart);
        }
        if (WeekEnd != 7) {
            maxDate = DayAdd(maxDate, 7 - WeekEnd);
        }
        //为防止超出边界，最后一周加一周
        //maxDate = DayAdd(maxDate, 7);

        var WeekNum = getWeekNum(minDate, maxDate,"week");
        BindDateTitle();
        BindDateContent();
        //绑定用户
        BindUser(dataUser, "user");
        BindUser(dataCustom, "custom");
        //绑定操作列
        if (opts.showEdit) {
            BindOptrate();
        }
    });
};