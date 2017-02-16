function getinfowin(evt) {
    // var evtgr = evt.attributes;
    var evtgr = evt.ID;
    if (evtgr == undefined) {
        getinfowincc(evt.graphic.attributes)
    }
    else {
        getinfowincc(evt)
    }
}
function getinfowincc(result) {
    if (result == undefined) {
        return;
    }
    var ypics = new Array(); //存储已处理的图片数组
    var infoWindows = map.infoWindow;
    var photo = result.photo;
    var POINT_X = result.POINT_X;
    var POINT_Y = result.POINT_Y;
    var status;
    var cldate;

    var content = getcontent(result);
    infoWindows.setContent(content);
    infoWindows.setTitle("公共设施");

    if (photo != undefined) {
        document.getElementById("clhimg").style.cursor = "default";
        var ephotos = photo;
        var ypics = ephotos.split(','); //存储已处理的图片
        var clhimg = document.getElementById("clhimg");
        clhimg.src = pathcontext + "/ztphoto/ggzxc/" + ypics[0];
    }
    var myPoint = new esri.geometry.Point(POINT_X, POINT_Y);
    map.infoWindow.show(map.toScreen(myPoint), map.getInfoWindowAnchor(map.toScreen(myPoint)));
}
function getcontent(result) {
    var content =
                "<div >"+
                    "<div style='font-size:13px;'>" +
                             "<table >" +
                                "<tr>" +
                                      "<td class='infotdname'>" +  "站点编号：" + "</td>" +
                                      "<td class='infotddata'>" + result.zds + "</td>" +
                                "</tr>" +
                                  "<tr>" +
                                     "<td class='infotdname'>" + "数量：" + "</td>" +
                                      "<td class='infotddata'>" + result.zxcs + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                     "<td class='infotdname'>" + "所在位置：" + "</td>" +
                                     "<td class='infotddata'>" + result.adder + "</td>" +
                                "</tr>" +
                         "</table>" +
                  "</div>" +
                  "<div style=';margin-left:60px;width:211px;margin-top:3px;border:1px solid #8D8881;background-color:#ffffff '>" +
                             " <a href='#infoimage' rel='facebox'><img  id='clhimg'  style='margin:5px 5px 5px 5px;height:150px;width:200px'  alt=''/></a>" +
                  "</div>" +
                  "<div id='infoimage' style='display:none'>"+
                         "<image id='dispimg'></image>"+
                "</div>"+
            "</div>"
    return content;
}