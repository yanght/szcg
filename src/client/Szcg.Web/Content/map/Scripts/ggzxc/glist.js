
function getdata(result, pcount) {
    $("#dealList").empty();
    var pcolor = "#F0F0F0"
    for (var i = 0; i < pcount; i++) {
        var dsuvilist = "dsuvilist" + i;
        var dsupic = "dsupic" + i;
        $("#dealList").append("<div class='mid_content' id='"+dsuvilist+"'>"
                                        	+"<div class='content_top meishi'>"
                                            + "<span><a href='javascript:void(0)' title=''>" + "设施编号：" + "</a></span> "
                                                    + "<a href='javascript:void(0)'>" + result[i].zds + "</a>"
                                            + "</div>"  
                                            +"<div class='content_left'>"
                                                  + "<a href='javascript:void(0)' target='_top'>"
                                                  + "<img width='30px' height='30px'  id='" + dsupic + "'>"
                                                  +"</a>"
                                            +"</div>"
                                            + "<strong>数量：<span>" + result[i].zxcs+ "</span><br>所在位置："+result[i].adder+"</strong>"
                                            + "</div>")
        var o = document.getElementById(dsuvilist);
        o.re = result[i];
        o.onclick = function () {
             getinfowin(this.re)
         }
         o.onmouseover = function () {
             showMark2(this.re)
         }
         o.onmouseout = function () {
             // showout()
             delpoint();
         }
        var p = document.getElementById(dsupic)
        var cp=result[i].photo;
        if (cp!= undefined) {
            var wpics = cp.split('|');
            var cp0=wpics[0];
            p.src = pathcontext + "/ztphoto/ggzxc/" + cp0;
        }
      
       }
}


