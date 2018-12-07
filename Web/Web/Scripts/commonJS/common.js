//去除多余的分割符，例如：",a",结果为"a"
//a：字符串
//splitC：分隔符
function dropRsplit(a, splitC) {
    var tmpA = a.split(splitC);
    var tmpObj = new Array();
    for (var i = 0; i < tmpA.length; i++) {
        if (tmpA[i] != "") {
            tmpObj.push(tmpA[i]);
        }

    }
    return tmpObj.join(splitC);
}

$("#nowYear").html(new Date().getFullYear());


 