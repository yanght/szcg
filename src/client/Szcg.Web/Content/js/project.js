var project = {};

//区域列表
project.getareaList = function getareaList() {
    utils.httpClient("/project/GetAreaList", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each areas as value i}}'
                 + '  <option value="{{value.AreaCode}}">{{value.AreaName}}</option>'
                 + '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("select[name='AreaId']").html(html);

            $("select[name='AreaId']").change(function () {
                var area = $(this).val();
                if (area == "") {
                    $("select[name='SquareId']").html("<option>全部</option>");
                }
                project.getstreetList(area);
            })

        }
        else {
            utils.alert(e.RspMsg);
        }
    });
}

//街道列表
project.getstreetList = function getstreetList(areacode) {
    utils.httpClient("/project/GetStreetList", "post", { areacode: areacode }, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each streets as value i}}'
                 + '  <option value="{{value.StreetCode}}">{{value.StreetName}}</option>'
                 + '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("select[name='StreetId']").html(html);

            $("select[name='StreetId']").change(function () {
                var area = $("select[name='StreetId']:selected").val();
                var street = $(this).val();
                project.getcommounityList(area, street);
            })

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//社区列表
project.getcommounityList = function getcommounityList(areacode, streetcode) {
    utils.httpClient("/project/GetCommunityList", "post", { areacode: areacode, streetcode: streetcode }, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each commoditys as value i}}'
                 + '  <option value="{{value.CommCode}}">{{value.CommName}}</option>'
                 + '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("select[name='SquareId']").html(html);

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//初始化职能部门列表
project.initDepartList = function initDepartList() {
    utils.httpClient("/depart/GetDepartListByAreaCode", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each departs as value i}}'
                + '  <option value="{{value.UserDefinedCode}}">{{value.DepartName}}</option>'
                + '{{/each}}';
            var render = template.compile(source);

            var html = render(data.RspData);

            $("select[name='Depart']").html(html);
        } else {
            utils.alert(data.RspMsg);
        }
    });
}

//部门选择弹框
project.initPropDepartTree = function initPropDepartTree(callback) {
    $("#TargetDepartName").click(function () {

        $.get("/callAcceptance/project/SelectDepart", function (data) {
            var dialog = bootbox.dialog({
                message: data,
                title: "选择部门",
                className: "depart-modal",
                buttons:
                {
                    "save":
                    {
                        "label": "选择",
                        "className": "btn-sm btn-primary",
                        "callback": callback
                    },
                    "button":
                    {
                        "label": "取消",
                        "className": "btn-sm"
                    }
                }
            });

            var setting = {
                view: {
                    dblClickExpand: false,
                    showLine: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                callback: {
                    onClick: function (e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                    }
                }
            };
            utils.httpClient("/depart/GetDutyDepartTree", "post", null, function (data) {
                if (data.RspCode == 1) {

                    zNodes = data.RspData.departs;

                    var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

                } else {
                    utils.alert(data.RspMsg);
                }
            });
        });

    });
}

//初始化大小类选择
project.initProjectClass = function initProjectClass() {
    $("#ProbClass").change(function () {
        var classType = $(this).val();
        utils.httpClient("/project/GetBigClassList", "post", { classType: classType }, function (data) {
            if (classType == "") {
                $("#BigClass").html("<option value=''>全部</option>");
                $("#SmallClass").html("<option value=''>全部</option>");
            }
            if (data.RspCode == 1) {
                var source = '{{each bigclass as value i}}'
               + '  <option value="{{value.BigClassCode}}">{{value.Name}}</option>'
               + '{{/each}}';

                var render = template.compile(source);

                var html = render(data.RspData);

                $("select[name='BigClass']").html(html);

                $("select[name='BigClass']").change(function () {
                    var bigclass = $(this).val();
                    if (bigclass == "") {
                        $("select[name='SmallClass']").html("<option>全部</option>");
                    }
                    project.initSmallClass(classType, bigclass);
                })
            }
        });
    })
}

//初始化小类选择
project.initSmallClass = function initSmallClass(classType, bigclassCode) {
    utils.httpClient("/project/GetSmallClassList", "post", { classType: classType, bigclassCode: bigclassCode }, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each smallclass as value i}}'
           + '  <option value="{{value.SmallCallCode}}">{{value.Name}}</option>'
           + '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("select[name='SmallClass']").html(html);

        }
    });
}

//案卷大小类选择弹框
project.initPropProjectClassTree = function initProjectClassTree(callback) {
    $("#projectclass").click(function () {

        $.get("/callAcceptance/project/SelectProjectClass", function (data) {
            var dialog = bootbox.dialog({
                message: data,
                title: "选择大小类",
                buttons:
                {
                    "save":
                    {
                        "label": "选择",
                        "className": "btn-sm btn-primary",
                        "callback": callback
                    },
                    "button":
                    {
                        "label": "取消",
                        "className": "btn-sm"
                    }
                }
            });
            var setting = {
                view: {
                    dblClickExpand: false,
                    showLine: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                callback: {
                    onClick: function (e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                    }
                }
            };
            utils.httpClient("/project/GetProjectClassTree", "post", null, function (data) {
                if (data.RspCode == 1) {

                    zNodes = data.RspData.projectclass;

                    var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

                } else {
                    utils.alert(data.RspMsg);
                }
            });
        });
    })
}

//区域选择弹框
project.initPropAreaTree = function initPropAreaTree(callback) {

    $("#areaName").click(function () {

        $.get("/callAcceptance/project/SelectArea", function (data) {
            var dialog = bootbox.dialog({
                message: data,
                title: "选择区域街道",
                buttons:
                {
                    "save":
                    {
                        "label": "选择",
                        "className": "btn-sm btn-primary",
                        "callback": callback
                    },
                    "button":
                    {
                        "label": "取消",
                        "className": "btn-sm"
                    }
                }
            });

            var setting = {
                view: {
                    dblClickExpand: false,
                    showLine: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                callback: {
                    onClick: function (e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                    }
                }
            };
            utils.httpClient("/project/GetAreaTree", "post", null, function (data) {
                if (data.RspCode == 1) {

                    zNodes = data.RspData.areaTree;

                    var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

                } else {
                    utils.alert(data.RspMsg);
                }
            });
        });
    })
}

//刷新举报栏待办案卷列表
project.GetDelProjectList = function GetDelProjectList(modelcode, buttoncode, nodeid, table) {

    var json = {
        AreaId: $("select[name='AreaId']").val(),
        StreetId: $("select[name='StreetId']").val(),
        SquareId: $("select[name='SquareId']").val(),
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val(),
        NodeId: nodeid,
        ButtonCode: buttoncode,
    };
    var url = '/project/GetDelProjectList';
    var parm = "?AreaId=" + json.AreaId + "&StreetId=" + json.StreetId + "&SquareId=" + json.SquareId + "&startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode + "&NodeId=" + json.NodeId + "&ButtonCode=" + json.ButtonCode;

    oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取举报栏代办案卷列表
project.initDelProjectTable = function (modelcode, buttoncode, nodeid) {
    var json = {
        AreaId: $("select[name='AreaId']").val(),
        StreetId: $("select[name='StreetId']").val(),
        SquareId: $("select[name='SquareId']").val(),
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val(),
        NodeId: nodeid,
        ButtonCode: modelcode,
    };

    //查询当前页面的操作按钮
    var optionNames = new Array();
    project.GetFlowNodePower(modelcode, function (data) {
        $.each(data, function (index, item) {
            //if (item.ModelCode == modelcode) {
            $.each(item.ChildPowers, function (i, k) {
                optionNames.push(k);
            })
            //}
        })
    })


    var url = '/project/GetDelProjectList';
    var parm = "?AreaId=" + json.AreaId + "&StreetId=" + json.StreetId + "&SquareId=" + json.SquareId + "&startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode + "&NodeId=" + json.NodeId + "&ButtonCode=" + json.ButtonCode;

    var oTable1 =
           $('#projectlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "TimeState", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == 0) {
                              return "正常办理";
                          }
                          if (data == 1) {
                              return "期限将至";
                          }
                          if (data == 2) {
                              return "已经超期";
                          }
                          if (data == -1) {
                              return "还没进入流程";
                          }
                      }
                  },
                   {
                       "data": "IsLock", "sWidth": "5%"
                   },
                  {
                      "data": "IsPress", "mRender": function (data, type, full) {
                          if (data) {
                              return "督办案卷";
                          } else {
                              return "普通案卷";
                          }
                      }
                  },
                  { "data": "ProbSourceName", "sWidth": "5%" },
                  {
                      "data": "PdaIoFlag", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == "01")
                              return "已发送消息";
                          if (data == "11")
                              return "监督员正在核查";
                          if (data == "21" || data == "31")
                              return "已核查完毕";
                          if (data == "01")
                              return "已发送消息";
                          return "未发消息";
                      }
                  },
                  {
                      "data": "ProjName", "mRender": function (data, type, full) {
                          return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '&nodeid=' + full.NodeId + '">' + data + '</a>';
                      }
                  },
                  {
                      "data": "Telephonist", "sWidth": "5%"
                  },
                  { "data": "ProbClassName", "sWidth": "5%" },
                  { "data": "BigClassName" },
                  { "data": "SmallClassName" },
                  {
                      "data": "StartDate", "mRender": function (data, type, full) {
                          return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss")
                      }
                  },
                  { "data": "Street" },
                  { "data": "Square" },
                  { "data": "ProbDesc" },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span>';
                          html += ' </a>';
                          html += '   <div class="inline position-relative">';
                          html += '     <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">';
                          html += '    <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>更多操作';
                          html += '  </button>';
                          html += '  <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">';

                          $.each(optionNames, function (index, item) {
                              html += '  <li onclick=project.operateProject("' + item.ButtonId + '","' + full.Projcode + '","' + item.ButtonCode + '","' + full.NodeId + '","' + full.StartYear + '","' + full.IsEnd + '","' + full.ProbSource + '")>';
                              html += '    <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                              html += '  <span class="blue"><i class="ace-icon fa fa-pencil bigger-130"></i> ' + item.ShowName + '</span>';
                              html += ' </a>';
                              html += ' </li>';
                          })

                          html += '  </ul>';
                          html += '     </div>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $("#projectlistTB").attr("width", "100%");

                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })
                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })

                   var table = oTable1.DataTable();

                   if (table.data() == null || table.data().length == 0) return;

                   var rowdate = table.data()[0];

                   if (rowdate.NodeId != 2) {

                       var column = table.column(6).visible(false);
                   }
                   if (rowdate.NodeId != 10 && rowdate.NodeId != 101 && rowdate.NodeId != 102) {

                       var column = table.column(4).visible(false);
                   }



               }
           });
    return oTable1;
}

//刷新自办件待办案卷列表
project.GetSelfProjectList = function GetSelfProjectList(table) {

    var json = {
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val(),
        Depart: $("#Depart").val()
    };

    var url = '/project/GetZbjProjectList';
    var parm = "?startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode + "&Depart=" + json.Depart;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取自办件案卷列表
project.initSelfProjectTable = function () {

    var json = {
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val(),
        Depart: $("#Depart").val()
    };

    var url = '/project/GetZbjProjectList';
    var parm = "?startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode + "&Depart=" + json.Depart;

    var oTable1 =
           $('#projectlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "TimeState", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == 0) {
                              return "正常办理";
                          }
                          if (data == 1) {
                              return "期限将至";
                          }
                          if (data == 2) {
                              return "已经超期";
                          }
                          if (data == -1) {
                              return "还没进入流程";
                          }
                      }
                  },
                   {
                       "data": "IsLock", "sWidth": "5%"
                   },
                  {
                      "data": "IsPress", "mRender": function (data, type, full) {
                          if (data) {
                              return "督办案卷";
                          } else {
                              return "普通案卷";
                          }
                      }
                  },
                  { "data": "ProbSourceName", "sWidth": "5%" },
                  {
                      "data": "PdaIoFlag", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == "01")
                              return "已发送消息";
                          if (data == "11")
                              return "监督员正在核查";
                          if (data == "21" || data == "31")
                              return "已核查完毕";
                          if (data == "01")
                              return "已发送消息";
                          return "未发消息";
                      }
                  },
                  { "data": "ProjName", "mRender": function (data, type, full) { return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '&nodeid=' + full.NodeId + '">' + data + '</a>'; } },
                  { "data": "Telephonist", "sWidth": "5%" },
                  { "data": "ProbClassName", "sWidth": "5%" },
                  { "data": "BigClassName" },
                  { "data": "SmallClassName" },
                  { "data": "StartDate", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  { "data": "SteptDate", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  { "data": "Street" },
                  { "data": "Square" },
                  { "data": "ProbDesc" },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span>';
                          html += ' </a>';
                          html += '   <div class="inline position-relative">';


                          html += '     </div>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $("#projectlistTB").attr("width", "100%");

                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })
                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })

                   var table = oTable1.DataTable();
                   table.column(0).visible(false);
                   table.column(1).visible(false);
                   table.column(6).visible(false);

               }
           });
    return oTable1;

}

//刷新自办件待办案卷列表
project.GetCDProjectList = function GetSelfProjectList(table) {

    var json = {
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val()
    };

    var url = '/project/GetCDProjectList';
    var parm = "?startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取自办件案卷列表
project.initCDProjectTable = function () {

    var json = {
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        Projcode: $("input[name='Projcode']").val()
    };

    var url = '/project/GetCDProjectList';
    var parm = "?startTime=" + json.startTime + "&endTime=" + json.endTime + "&Projcode=" + json.Projcode;


    var oTable1 =
           $('#projectlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "TimeState", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == 0) {
                              return "正常办理";
                          }
                          if (data == 1) {
                              return "期限将至";
                          }
                          if (data == 2) {
                              return "已经超期";
                          }
                          if (data == -1) {
                              return "还没进入流程";
                          }
                      }
                  },
                   {
                       "data": "IsLock", "sWidth": "5%"
                   },
                  {
                      "data": "IsPress", "mRender": function (data, type, full) {
                          if (data) {
                              return "督办案卷";
                          } else {
                              return "普通案卷";
                          }
                      }
                  },
                  { "data": "ProbSourceName", "sWidth": "5%" },
                  {
                      "data": "PdaIoFlag", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == "01")
                              return "已发送消息";
                          if (data == "11")
                              return "监督员正在核查";
                          if (data == "21" || data == "31")
                              return "已核查完毕";
                          if (data == "01")
                              return "已发送消息";
                          return "未发消息";
                      }
                  },
                  { "data": "ProjName", "mRender": function (data, type, full) { return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '&nodeid=' + full.NodeId + '">' + data + '</a>'; } },
                  { "data": "Telephonist", "sWidth": "5%" },
                  { "data": "ProbClassName", "sWidth": "5%" },
                  { "data": "BigClassName" },
                  { "data": "SmallClassName" },
                  { "data": "StartDate", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  { "data": "Street" },
                  { "data": "Square" },
                  { "data": "ProbDesc" },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span>';
                          html += ' </a>';
                          html += '   <div class="inline position-relative">';


                          html += '     </div>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $("#projectlistTB").attr("width", "100%");

                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })
                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })

                   var table = oTable1.DataTable();
                   table.column(0).visible(false);
                   table.column(1).visible(false);
                   table.column(4).visible(false);
                   table.column(6).visible(false);
                   //if (table.data() == null || table.data().length == 0) return;

                   //var rowdate = table.data()[0];

                   //if (rowdate.NodeId != 2) {

                   //    var column = table.column(6).visible(false);
                   //}
                   //if (rowdate.NodeId != 10 && rowdate.NodeId != 101 && rowdate.NodeId != 102) {

                   //    var column = table.column(4).visible(false);
                   //}

               }
           });
    return oTable1;

}

//刷新自办件待办案卷列表
project.GetQueryProjectList = function GetQueryProjectList(table) {

    var json = {
        ProbSource: $("select[name='ProbSource']").val(),
        ProbClass: $("select[name='ProbClass']").val(),
        AreaId: $("select[name='AreaId']").val(),
        StreetId: $("select[name='StreetId']").val(),
        SquareId: $("select[name='SquareId']").val(),
        DeleteState: $("select[name='DeleteState']").val(),
        BigClass: $("select[name='BigClass']").val(),
        SmallClass: $("select[name='SmallClass']").val(),
        Telephonist: $("select[name='Telephonist']").val(),
        CollCode: $("select[name='CollCode']").val(),
        TargetDepartCode: $("select[name='TargetDepartCode']").val(),
        ProjectState: $("select[name='ProjectState']").val(),
        DoStartTime: $("#DoStartTime").val(),
        Address: $("#Address").val(),
        DoEndTime: $("#DoEndTime").val(),
        StartTime: $("#StartTime").val(),
        EndTime: $("#EndTime").val()
    };

    var url = '/project/QueryProjectList';
    var parm = "?ProbSource=" + json.ProbSource
        + "&ProbClass=" + json.ProbClass
        + "&AreaId=" + json.AreaId
        + "&StreetId=" + json.StreetId
        + "&SquareId=" + json.SquareId
        + "&DeleteState=" + json.DeleteState
        + "&BigClass=" + json.BigClass
        + "&SmallClass=" + json.SmallClass
        + "&Telephonist=" + json.Telephonist
        + "&CollCode=" + json.CollCode
        + "&TargetDepartCode=" + json.TargetDepartCode
        + "&ProjectState=" + json.ProjectState
        + "&DoStartTime=" + json.DoStartTime
        + "&Address=" + json.Address
        + "&DoEndTime=" + json.DoEndTime
        + "&StartTime=" + json.StartTime
        + "&EndTime=" + json.EndTime;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取自办件案卷列表
project.initQueryProjectTable = function () {

    var json = {
        ProbSource: $("select[name='ProbSource']").val(),
        ProbClass: $("select[name='ProbClass']").val(),
        AreaId: $("select[name='AreaId']").val(),
        StreetId: $("select[name='StreetId']").val(),
        SquareId: $("select[name='SquareId']").val(),
        DeleteState: $("select[name='DeleteState']").val(),
        BigClass: $("select[name='BigClass']").val(),
        SmallClass: $("select[name='SmallClass']").val(),
        Telephonist: $("select[name='Telephonist']").val(),
        CollCode: $("select[name='CollCode']").val(),
        TargetDepartCode: $("select[name='TargetDepartCode']").val(),
        ProjectState: $("select[name='ProjectState']").val(),
        DoStartTime: $("#DoStartTime").val(),
        Address: $("#Address").val(),
        DoEndTime: $("#DoEndTime").val(),
        StartTime: $("#StartTime").val(),
        EndTime: $("#EndTime").val()
    };

    var url = '/project/QueryProjectList';
    var parm = "?ProbSource=" + json.ProbSource
        + "&ProbClass=" + json.ProbClass
        + "&AreaId=" + json.AreaId
        + "&StreetId=" + json.StreetId
        + "&SquareId=" + json.SquareId
        + "&DeleteState=" + json.DeleteState
        + "&BigClass=" + json.BigClass
        + "&SmallClass=" + json.SmallClass
        + "&Telephonist=" + json.Telephonist
        + "&CollCode=" + json.CollCode
        + "&TargetDepartCode=" + json.TargetDepartCode
        + "&ProjectState=" + json.ProjectState
        + "&DoStartTime=" + json.DoStartTime
        + "&Address=" + json.Address
        + "&DoEndTime=" + json.DoEndTime
        + "&StartTime=" + json.StartTime
        + "&EndTime=" + json.EndTime;


    var oTable1 =
           $('#projectlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "TimeState", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == 0) {
                              return "正常办理";
                          }
                          if (data == 1) {
                              return "期限将至";
                          }
                          if (data == 2) {
                              return "已经超期";
                          }
                          if (data == -1) {
                              return "还没进入流程";
                          }
                      }
                  },
                   {
                       "data": "IsLock", "sWidth": "5%"
                   },
                  {
                      "data": "IsPress", "mRender": function (data, type, full) {
                          if (data) {
                              return "督办案卷";
                          } else {
                              return "普通案卷";
                          }
                      }
                  },
                  { "data": "ProbSourceName", "sWidth": "5%" },
                  {
                      "data": "PdaIoFlag", "sWidth": "5%", "mRender": function (data, type, full) {
                          if (data == "01")
                              return "已发送消息";
                          if (data == "11")
                              return "监督员正在核查";
                          if (data == "21" || data == "31")
                              return "已核查完毕";
                          if (data == "01")
                              return "已发送消息";
                          return "未发消息";
                      }
                  },
                  { "data": "ProjName", "mRender": function (data, type, full) { return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '&nodeid=' + full.NodeId + '">' + data + '</a>'; } },
                  { "data": "Telephonist", "sWidth": "5%" },
                  { "data": "ProbClassName", "sWidth": "5%" },
                  { "data": "BigClassName" },
                  { "data": "SmallClassName" },
                  { "data": "Street" },
                  { "data": "Square" },
                  { "data": "ProbDesc" },
                    { "data": "StartDate", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span>';
                          html += ' </a>';
                          html += '   <div class="inline position-relative">';


                          html += '     </div>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $("#projectlistTB").attr("width", "100%");

                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })
                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })

                   var table = oTable1.DataTable();
                   table.column(0).visible(false);
                   table.column(1).visible(false);
                   table.column(2).visible(false);
                   table.column(4).visible(false);
                   table.column(6).visible(false);
                   //if (table.data() == null || table.data().length == 0) return;

                   //var rowdate = table.data()[0];

                   //if (rowdate.NodeId != 2) {

                   //    var column = table.column(6).visible(false);
                   //}
                   //if (rowdate.NodeId != 10 && rowdate.NodeId != 101 && rowdate.NodeId != 102) {

                   //    var column = table.column(4).visible(false);
                   //}

               }
           });
    return oTable1;

}

//获取当前页面操作按钮
project.GetFlowNodePower = function GetFlowNodePower(modelcode, callback) {
    utils.httpClient("/account/GetFlowNodePower", "post", { modelcode: modelcode }, function (data) {
        if (data.RspCode == 1) {
            callback(data.RspData.nodepower);
        } else {
            utils.alert(data.RspMsg);
        }

    });
}

//案卷上报
project.projectReport = function projectReport() {

    utils.httpClient("/project/ProjectReport", "post", $("#projectreport").serialize(), function (data) {
        if (data.RspCode == 1) {

            utils.alert1("案卷上报成功!", function () {
                var table = $('#projectlistTB').DataTable();
                table.ajax.reload();
            });

        } else {
            utils.alert1(data.RspMsg);
        }
    });


    $("#cancle").click(function () {
        dialog.dialog("destroy").remove();
    })
}

//案卷上报并批转至登记栏
project.projectPz = function projectPz() {

    utils.httpClient("/project/ProjectApproved", "post", $("#projectreport").serialize(), function (data) {
        if (data.RspCode == 1) {

            utils.alert1("案卷批转成功!", function () {
                var table = $('#projectlistTB').DataTable();
                table.ajax.reload();
            });
        } else {
            utils.alert1(data.RspMsg);
        }
    });
    return false;

}

//查看案卷明细
project.getProjectDetail = function getProjectDetail(projectcode, year, isend, nodeid) {

    var json = {
        projcode: projectcode,
        year: year,
        isend: isend,
        nodeid: nodeid
    };

    utils.httpClient("project/GetProjectDetail", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectdetailtpl', data.RspData);
            document.getElementById('projectdetail').innerHTML = html;

            project.initPropDepartTree(function (e, treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                zTree.expandNode(treeNode);
                $("#departselected").click(function () {
                    $("#TargetDepartName").val(treeNode.name);
                    $("#TargetDepartCode").val(treeNode.id);
                    dialog.dialog("destroy").remove();
                });
            });

            $(".next").click(function () {
                var firstpic = $(this).closest("div").find(".profile-picture").find("a")[0];
                $(this).closest("div").find(".profile-picture").append(firstpic);
            })

            $(".prev").click(function () {
                var lastpic = $(this).closest("div").find(".profile-picture").find("a").last();
                $(this).closest("div").find(".profile-picture").prepend(lastpic);
            })

            $("#sendmessage").click(function () {

                var url = "/CallAcceptance/project/ProjectSendMessage?collcode=" + $(this).attr("collcode") + "&collname=" + $(this).attr("collname");

                $.get(url, function (data) {

                    dialog = $("<div style='margin-bottom:20px;'>" + data + "</div>").appendTo("body").dialog({
                        modal: true,
                        title: "<div class='widget-header widget-header-small'><h4 class='smaller'>向监督员发送消息</h4></div>",
                        title_html: true,
                        width: 400,
                        height: 300,
                        buttons: [
                            {
                                text: "发送",
                                "class": "btn btn-primary btn-xs",
                                click: function () {
                                    var _title = $("#title").val();
                                    var _msgcontent = $("#msgcontent").val();
                                    var _collname = $("#collname").val();
                                    var _collcode = $("#collcode").val();

                                    if (_collname.length == 0 || _collcode.length == 0) {
                                        utils.alert("发送对象不允许为空!"); return false;
                                    }
                                    if (_title.length == 0 || _msgcontent.length == 0) {
                                        utils.alert("消息主题与消息内容不允许为空!"); return false;
                                    }

                                    var json = {
                                        collname: _collname,
                                        title: _title,
                                        msgcontent: _msgcontent,
                                        collcode: _collcode
                                    };

                                    utils.httpClient("/message/SendPDAMsg", "POST", json, function (data) {
                                        if (data.RspCode == 1) {
                                            utils.alert("消息发送成功！");
                                        }
                                        else {
                                            utils.alert1(data.RspMsg);
                                        }
                                    });

                                    $(this).dialog("close");
                                }
                            },
                             {
                                 text: "取消",
                                 "class": "btn btn-xs",
                                 click: function () {
                                     $(this).dialog("close");
                                 }
                             }
                        ]
                    });
                });
            })

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

project.getProjectDetailLA = function getProjectDetailLA(dotype, projectcode, nodeid) {
    var json = {
        dotype: dotype,
        projcode: projectcode,
        nodeid: nodeid
    };

    utils.httpClient("project/GetProjectDetailLA", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectdetailLAtpl', data.RspData);
            document.getElementById('projectdetailLA').innerHTML = html;

            project.initPropDepartTree(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#TargetDepartName").val(treeNode.name);
                    $("#TargetDepartCode").val(treeNode.id);
                    $("#Mobile").val(treeNode.phone);
                }

            });

            $("#FilingType").change(function () {
                utils.httpClient("/project/GetProjectHandleTime", "post", { typecode: $("#ProjectTypeCode").val(), smallcode: $("#SmallClassCode").val(), filingType: $("#FilingType").val(), processtype: $("#ProcessType").val() }, function (data) {
                    $("#handlertime").html(data.RspData.time.split('$')[0]);
                })
            })
            $("#ProcessType").change(function () {
                utils.httpClient("/project/GetProjectHandleTime", "post", { typecode: $("#ProjectTypeCode").val(), smallcode: $("#SmallClassCode").val(), filingType: $("#FilingType").val(), processtype: $("#ProcessType").val() }, function (data) {
                    $("#handlertime").html(data.RspData.time.split('$')[0]);
                })
            })
        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//获取案卷详情（包含巡查员列表 ）
project.getProjectDetailWithCollecter = function getProjectDetailWithCollecter(dotype, projectcode, nodeid) {
    var json = {
        dotype: dotype,
        projcode: projectcode,
        nodeid: nodeid
    };

    utils.httpClient("project/GetProjectDetailWithCollecter", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectdetailwithcollecterLAtpl', data.RspData);
            document.getElementById('projectdetailwithcollecterLA').innerHTML = html;

            var table =
           $('#collecterTb')
           .DataTable({
               "iDisplayLength": 5, //每页显示10条记录
               "dom": 'frtp',
               "sZeroRecords": "对不起，查询不到相关数据！",
               "sEmptyTable": "表中无数据存在！",
               "oLanguage": {
                   "sSearch": "搜索",
                   "oPaginate": {
                       "sFirst": "首页",
                       "sPrevious": "前一页",
                       "sNext": "后一页",
                       "sLast": "尾页"
                   }
               }
           });
            $('#collecterTb tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                    $("#Mobile").val("");
                    $("#CollecterCode").val("");
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                    $("#Mobile").val($(this).attr("_mobile"));
                    $("#CollecterCode").val($(this).attr("_collcode"));
                }



            });

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//案卷登记批转详情
project.getProjectDetailApproved = function getProjectDetailApproved(projectcode, year, isend, nodeid, action, buttoncode) {
    var json = {
        projcode: projectcode,
        year: year,
        isend: isend,
        nodeid: nodeid,
        action: action,
        buttoncode: buttoncode
    };

    utils.httpClient("project/GetProjectDetail", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectapproveddetailtpl', data.RspData);
            document.getElementById('projectapproveddetail').innerHTML = html;

            project.initPropDepartTree(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#TargetDepartName").val(treeNode.name);
                    $("#TargetDepartCode").val(treeNode.id);
                }

            });

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//获取案卷流程
project.getProjectTrace = function (projectcode, year, isend) {
    var json = {
        projcode: projectcode,
        year: year,
        isend: isend
    };

    utils.httpClient("project/ProjectTrace", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projecttracetpl', data.RspData);
            document.getElementById('projecttrace').innerHTML = html;
        }
        else {
            utils.alert(data.RspMsg);
        }
    });

}

//获取监督员列表
project.getCollecters = function getCollecters(projcode, street) {
    utils.httpClient("collector/GetCheckCollecters", "post", { streetcode: street, projcode: projcode }, function (data) {
        if (data.RspCode == 1) {
            var html = template('collecterlisttpl', data.RspData);
            document.getElementById('collecterlist').innerHTML = html;
        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//跳转至案卷登记页
project.operateProject = function operateProject(buttonid, projcode, buttoncode, nodeid, year, isend, probsource) {

    switch (buttonid) {

        case "img_ajbl"://案卷上报

            var url = "/CallAcceptance/project/projectreport?projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷上报",
                    buttons:
                    {
                        "save":
                        {
                            "label": "保存",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#BigClass").length == 0 || $("#SmallClass").length == 0) {
                                    utils.alert("请选择大小类！"); return false;
                                }

                                if ($("#AreaId").length == 0 || $("#StreetId").length == 0 || $("#SquareId").length == 0) {
                                    utils.alert("请选择街道社区！"); return false;
                                }

                                if ($("#Address").length == 0) {
                                    utils.alert("请填写事发位置！"); return false;
                                }

                                if ($("#ProbDesc").length == 0) {
                                    utils.alert("请填写问题描述！"); return false;
                                }


                                project.projectReport();
                            }
                        },
                        "approve":
                          {
                              "label": "批转",
                              "className": "btn-sm btn-primary",
                              "callback": function () {

                                  if ($("#BigClass").length == 0 || $("#SmallClass").length == 0) {
                                      utils.alert("请选择大小类！"); return false;
                                  }

                                  if ($("#AreaId").length == 0 || $("#StreetId").length == 0 || $("#SquareId").length == 0) {
                                      utils.alert("请选择街道社区！"); return false;
                                  }

                                  if ($("#Address").length == 0) {
                                      utils.alert("请填写事发位置！"); return false;
                                  }

                                  if ($("#ProbDesc").length == 0) {
                                      utils.alert("请填写问题描述！"); return false;
                                  }

                                  project.projectPz();
                              }
                          },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "imgZX"://平台受理员案卷注销

            var url = "/CallAcceptance/project/ProjectApprovedView?action=02&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷批转",
                    buttons:
                    {
                        "success":
                        {
                            "label": "注销",
                            "className": "btn-sm btn-primary",
                            "callback": function () {

                                if ($("#Option").length == 0) {
                                    utils.alert("请填写注销意见！"); return false;
                                }

                                utils.httpClient("/project/ProjectAcceptApproved", "post", {
                                    NodeId: nodeid,
                                    ProjectCode: projcode,
                                    CurrentBusiStatus: "02",
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷注销成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_PZ"://平台受理员案卷批转

            var url = "/CallAcceptance/project/ProjectApprovedView?action=03&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷批转",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "批转",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写批转意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectAcceptApproved", "post", {
                                    NodeId: nodeid,
                                    ProjectCode: projcode,
                                    CurrentBusiStatus: "03",
                                    ButtonCode: buttoncode,
                                    TargetDepartCode: $("#TargetDepartCode").val(),
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷批转成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_djht"://平台受理员问题回退

            var url = "/CallAcceptance/project/ProjectApprovedView?action=04&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷批转",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "回退",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写回退意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectAcceptApproved", "post", {
                                    NodeId: nodeid,
                                    ProjectCode: projcode,
                                    CurrentBusiStatus: "04",
                                    ButtonCode: buttoncode,
                                    TargetDepartCode: $("#TargetDepartCode").val(),
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷回退成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_la"://值班长案卷立案

            var url = "/CallAcceptance/project/ProjectLA?dotype=0&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "值班长立案",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "审核",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写值班长意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectFiling", "post", {
                                    ProjectCode: projcode,
                                    ProjectTypeCode: $("#ProjectTypeCode").val(),
                                    TargetDepartCode: $("#TargetDepartCode").val(),
                                    SmallClassCode: $("#SmallClassCode").val(),
                                    ProjectSource: $("#ProjectSource").val(),
                                    FilingType: $("#FilingType").val(),
                                    ProcessType: $("#ProcessType").val(),
                                    Mobile: $("#Mobile").val(),
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷立案成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_ht"://值班长案卷立案回退

            var url = "/CallAcceptance/project/ProjectLA?dotype=2&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "值班长立案",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "回退",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写值班长意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectFilingBack", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷立案回退成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_bla"://值班长案卷立案删除

            var url = "/CallAcceptance/project/ProjectLA?dotype=1&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "值班长立案",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "删除",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写值班长意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectFilingDelete", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷立案删除成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_ajfp"://责任部门任务分派

            var url = "/CallAcceptance/project/ProjectDispatch?dotype=0&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "任务分派",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "任务分派",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写责任部门意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectDispatch", "post", {
                                    ProjectCode: projcode,
                                    TargetDepartCode: $("#TargetDepartCode").val(),
                                    strPQNode: "",
                                    SuperviseName: "",
                                    SuperviseContent: "",
                                    Mobile: $("#Mobile").val(),
                                    IsAcceptNote: "",
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("任务分派成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "imgJGFK"://责任部门结果反馈

            var url = "/CallAcceptance/project/ProjectDispatchRevert?dotype=0&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "任务分派",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "结果反馈",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写责任部门意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectDispathRevert", "post", {
                                    ProjectCode: projcode,
                                    strPQNode: "1",
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("结果反馈成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_zybmht"://专业部门回退

            var url = "/CallAcceptance/project/ProjectDispatchRevert?dotype=1&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "任务分派",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "回退",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").length == 0) {
                                    utils.alert("请填写责任部门意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectDispatchBack", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("部门回退成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_fhczl"://发送核查信息

            utils.httpClient("/project/GetIoFlag", "POST", { projcode: projcode }, function (data) {
                if (data.RspCode == 1) {
                    var strResult = decodeURIComponent(data.RspData.data);
                    strResult = strResult.split('$');

                    var ioflag = strResult[0];
                    if (ioflag == "-9") {
                        strErr = strResult[1];
                        utils.alert("操作出错，请通知系统管理员！\r\n" + strErr);
                        return;
                    }
                    else if (ioflag == "01") {
                        var iftrue = window.confirm('短信息已发送，是否重发?');
                        if (!iftrue)
                            return;
                    }
                    else if (ioflag == "21" || ioflag == "31") {
                        var iftrue = window.confirm('已核查完毕，是否重发核查信息?');
                        if (!iftrue)
                            return;
                        //					    alert('已核查完毕，不需要再次核查，请直接处理！');
                        //					    return;
                    }
                    else if (ioflag == "11") {
                        var iftrue = window.confirm('案卷正在核查，是否重发核查信息?');
                        if (!iftrue)
                            return;
                    }
                    else if (ioflag == "99") {
                        var iftrue = window.confirm('该类型的案卷可以不需要核查，您是否仍要继续核查？');
                        if (!iftrue)
                            return;
                    }

                    var strNodeId = "10";
                    if (ioflag == "21" || ioflag == "31")
                        strNodeId = "102";
                    else if (ioflag == "01" || ioflag == "11")
                        strNodeId = "101";

                    var url = "/CallAcceptance/project/ProjectCheckMessage?projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + strNodeId;

                    $.get(url, function (data) {
                        bootbox.dialog({
                            message: data,
                            title: "发送核查指令",
                            buttons:
                            {
                                "approve":
                                {
                                    "label": "发送",
                                    "className": "btn-sm btn-primary",
                                    "callback": function () {
                                        if ($("#CollecterCode").val() == '') {
                                            utils.alert("请选择监督员！"); return false;
                                        }
                                        if ($("#Option").val() == '') {
                                            utils.alert("请填写消息内容！"); return false;
                                        }

                                        utils.httpClient("/project/ProjectSendCheckMessage", "post", {
                                            ButtonCode: buttoncode,
                                            ProjectCode: projcode,
                                            CollectorCode: $("#CollecterCode").val(),
                                            Message: $("#Option").val(),
                                        }, function (data) {
                                            if (data.RspCode == 1) {
                                                utils.alert1("发送成功！", function () {
                                                    var table = $('#projectlistTB').DataTable();
                                                    table.ajax.reload();
                                                });
                                            } else {
                                                utils.alert1(data.RspMsg);
                                            }
                                        })
                                    }
                                },
                                "button":
                                {
                                    "label": "取消",
                                    "className": "btn-sm"
                                }
                            }
                        });
                    });


                } else {
                    utils.alert1(data.RspMsg);
                }
            })


            break;

        case "img_hcss"://核查属实

            var url = "/CallAcceptance/project/ProjectCheck?CL_IsType=0&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "核查通过",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "通过",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").val() == '') {
                                    utils.alert("请输入批转意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectCheckSuccess", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("核查成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_hcbss"://核查不属实

            var url = "/CallAcceptance/project/ProjectCheck?CL_IsType=1&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "核查不通过",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "不通过",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").val() == '') {
                                    utils.alert("请输入批转意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectCheckFailed", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("核查成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_ja"://案卷结案

            var url = "/CallAcceptance/project/projectend?dotype=0&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷结案",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "结案",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").val() == '') {
                                    utils.alert("请输入值班长意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectClosed", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                    IsRepeatProject: $('input[name="IsRepeatProject"]:checked').val() == "true" ? "1" : "0"
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("结案成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_jaht"://结案回退

            if (probsource == 10) {
                utils.alert("快速上报的案卷，不能回退！");
                return false;
            }

            var url = "/CallAcceptance/project/projectend?dotype=1&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&nodeid=" + nodeid;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷结案",
                    buttons:
                    {
                        "approve":
                        {
                            "label": "回退",
                            "className": "btn-sm btn-primary",
                            "callback": function () {
                                if ($("#Option").val() == '') {
                                    utils.alert("请输入值班长意见！"); return false;
                                }
                                utils.httpClient("/project/ProjectClosedBack", "post", {
                                    ProjectCode: projcode,
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val()
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("会退成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;

        case "img_del"://平台操作员案卷删除

            var url = "/CallAcceptance/project/ProjectApprovedView?action=01&projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;

            $.get(url, function (data) {
                bootbox.dialog({
                    message: data,
                    title: "案卷结案",
                    buttons:
                    {
                        "success":
                        {
                            "label": "删除",
                            "className": "btn-sm btn-primary",
                            "callback": function () {

                                if ($("#Option").length == 0) {
                                    utils.alert("请填写删除意见！"); return false;
                                }

                                utils.httpClient("/project/ProjectAcceptApproved", "post", {
                                    NodeId: nodeid,
                                    ProjectCode: projcode,
                                    CurrentBusiStatus: "01",
                                    ButtonCode: buttoncode,
                                    Option: $("#Option").val(),
                                }, function (data) {
                                    if (data.RspCode == 1) {
                                        utils.alert1("案卷删除成功！", function () {
                                            var table = $('#projectlistTB').DataTable();
                                            table.ajax.reload();
                                        });
                                    } else {
                                        utils.alert1(data.RspMsg);
                                    }
                                })
                            }
                        },
                        "button":
                        {
                            "label": "取消",
                            "className": "btn-sm"
                        }
                    }
                });
            });

            break;
    }
}

//获取案卷上报详情
project.getProjectReportDetail = function getProjectReportDetail(projectcode, year, isend, nodeid) {
    var json = {
        projcode: projectcode,
        year: year,
        isend: isend,
        nodeid: nodeid
    };

    utils.httpClient("project/GetProjectDetail", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectreportdetailtpl', data.RspData.project);
            document.getElementById('projectreportdetail').innerHTML = html;

            project.initPropDepartTree(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#TargetDepartName").val(treeNode.name);
                    $("#TargetDepartCode").val(treeNode.id);
                }

            });

            project.initPropProjectClassTree(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    if (treeNode.level == 0) {
                        $("#TypeName").val(treeNode.name);
                        $("#TypeCode").val(treeNode.id);
                    } else if (treeNode.level == 1) {
                        $("#TypeName").val(treeNode.getParentNode().name);
                        $("#TypeCode").val(treeNode.getParentNode().id);
                        $("#BigClass").val(treeNode.id.split('_')[0]);
                        $("#projectclass").val(treeNode.getParentNode().name + "/" + treeNode.name);
                    } else if (treeNode.level == 2) {
                        $("#BigClass").val(treeNode.getParentNode().id.split('_')[0]);
                        $("#SmallClass").val(treeNode.id);
                        $("#TypeName").val(treeNode.getParentNode().getParentNode().name);
                        $("#TypeCode").val(treeNode.getParentNode().getParentNode().id);
                        $("#projectclass").val(treeNode.getParentNode().getParentNode().name + "/" + treeNode.getParentNode().name + "/" + treeNode.name);
                    }
                }
            })

            project.initPropAreaTree(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#areaName").closest(".form-group").find("input").val("");

                    if (treeNode.level == 0) {
                        $("#areaName").val(treeNode.name);
                        $("#AreaId").val(treeNode.id);
                    } else if (treeNode.level == 1) {
                        $("#AreaId").val(treeNode.getParentNode().id);
                        $("#StreetId").val(treeNode.id);
                        $("#areaName").val(treeNode.getParentNode().name + "/" + treeNode.name);
                    } else if (treeNode.level == 2) {
                        $("#StreetId").val(treeNode.getParentNode().id);
                        $("#SquareId").val(treeNode.id);
                        $("#AreaId").val(treeNode.getParentNode().getParentNode().id);
                        $("#areaName").val(treeNode.getParentNode().getParentNode().name + "/" + treeNode.getParentNode().name + "/" + treeNode.name);
                    }
                }
            })

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

//刷新站内业务消息列表
project.GetMessageList = function GetSelfProjectList(table) {

    var json = {
        userName: $("input[name='userName']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val(),
    };

    var url = '/message/GetMessageList';
    var parm = "?userName=" + json.userName + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取站内业务消息列表
project.initMessageTable = function () {

    var json = {
        userName: $("input[name='userName']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val(),
    };

    var url = '/message/GetMessageList';
    var parm = "?userName=" + json.userName + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime;


    var oTable1 =
           $('#messagelistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "IsRead", "mRender": function (data, type, full) {
                          if (data == 1) {
                              return '<span class="label label-success arrowed-in arrowed-in-right">已读</span>';
                          } else {
                              return '<span class="label label-warning arrowed-in arrowed-in-right">未读</span>';
                          }
                      }
                  },
                  { "data": "MsgTitle" },
                  { "data": "UserName" },
                  { "data": "DepartName" },
                  { "data": "MsgContent" },
                  { "data": "Cu_Date", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagereplay" id="' + full.Id + '" type="' + full.MsgType + '"><i class="ace-icon fa fa-location-arrow  bigger-120"></i>回复</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagedetail" data-url="/CallAcceptance/message/MessageDetail?id=' + full.Id + '&type=' + full.MsgType + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>查看详情</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagedelete" id="' + full.Id + '" type="' + full.MsgType + '"><i class="ace-icon fa fa-trash-o bigger-120"></i>消息删除</span>';
                          html += ' </a>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $(".messagedetail").click(function (e) {
                       utils.dialog(this, "消息详情", 400, 400);
                   })

                   $(".messagereplay").click(function () {

                       var url = "/CallAcceptance/message/MessageDetail?id=" + $(this).attr("id") + "&type=" + $(this).attr("type") + "&option=replay";

                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "消息回复",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "回复",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {

                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写回复内容！"); return false;
                                           }

                                           utils.httpClient("/message/ReplayMessage", "post", {
                                               MsgType: 1,
                                               To_User: $("#Go_User").val(),
                                               MsgTitle: $("#MsgTitle").val(),
                                               MsgContent: $("#MsgContent").val(),
                                           }, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert1("回复成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               } else {
                                                   utils.alert1(data.RspMsg);
                                               }
                                           })
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });
                       });

                   })

                   $(".messagedelete").click(function () {
                       if (window.confirm("确定要删除吗？")) {
                           project.deleteMessage($(this).attr("id"));
                       }
                   })

                   $("#createMessage").click(function () {

                       var url = "/CallAcceptance/message/CreateMessage";

                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "发送平台内Web消息",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "发送",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           if ($("#To_User").val().length == 0) {
                                               utils.alert("请选择收件人！"); return false;
                                           }
                                           if ($("#MsgTitle").val().length == 0) {
                                               utils.alert("请填写消息主题！"); return false;
                                           }
                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写消息内容！"); return false;
                                           }

                                           var json = {
                                               MsgType: $("input[name='MsgType']:checked").val(),
                                               To_User: $("#To_User").val(),
                                               MsgTitle: $("#MsgTitle").val(),
                                               MsgContent: $("#MsgContent").val()
                                           };

                                           utils.httpClient("/message/SendMessage", "POST", json, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert("发送成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               }
                                               else {
                                                   utils.alert(data.RspMsg);
                                               }
                                           });
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });

                           //初始化群组树
                           project.messageGroupTree(function () {
                               var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                               var nodes = zTree.getCheckedNodes(true);
                               if (nodes != null) {
                                   if ($("input[name='MsgType']:checked").val() == "1") {
                                       var names = ""; var codes = "";
                                       $.each(nodes, function (index, item) {
                                           if (item.id.indexOf("aaaa") >= 0) {
                                               names += item.name + ",";
                                               codes += item.id.replace("aaaa", "") + ",";
                                           }
                                       })

                                       $("#toUserName").val(names.substring(0, names.length - 1));
                                       $("#To_User").val(codes.substring(0, codes.length - 1));
                                   } else {
                                       var groupname = ""; var groupcodes = "";
                                       $.each(nodes, function (index, item) {
                                           if (item.level == 2) {
                                               groupname += item.name + ",";
                                               groupcodes += item.id + ",";
                                           }
                                       })
                                       $("#toUserName").val(groupname.substring(0, groupname.length - 1));
                                       $("#To_User").val(groupcodes.substring(0, groupcodes.length - 1));
                                   }
                               }
                           });

                           //清空已选内容
                           $("input[name='MsgType']").change(function () {
                               $("#toUserName").val("");
                               $("#To_User").val("");
                           })

                       });

                   })

                   $("#sendMessage").click(function () {
                       var url = "/CallAcceptance/message/SendMobileMessage";
                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "发送手机短消息",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "发送",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           if ($("#To_User").val().length == 0) {
                                               utils.alert("请选择收件人！"); return false;
                                           }

                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写短信内容！"); return false;
                                           }

                                           var json = {
                                               mobiles: $("#To_User").val(),
                                               content: $("#MsgContent").val()
                                           };

                                           utils.httpClient("/message/SendMobileMessage", "POST", json, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert("发送成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               }
                                               else {
                                                   utils.alert(data.RspMsg);
                                               }
                                           });
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });

                           project.mobileMessageGroupTree(function () {
                               var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                               var nodes = zTree.getCheckedNodes(true);
                               if (nodes != null) {
                                   // if ($("input[name='MsgType']:checked").val() == "1") {
                                   var names = ""; var codes = "";
                                   $.each(nodes, function (index, item) {
                                       if (item.phone != null && item.phone != "") {
                                           names += item.name + ",";
                                           codes += item.phone + ",";
                                       }
                                   })

                                   $("#toUserName").val(codes.substring(0, codes.length - 1));
                                   $("#To_User").val(codes.substring(0, codes.length - 1));
                                   //} else {
                                   //    var groupname = ""; var groupcodes = "";
                                   //    $.each(nodes, function (index, item) {
                                   //        if (item.level == 2) {
                                   //            groupname += item.name + ",";
                                   //            groupcodes += item.id + ",";
                                   //        }
                                   //    })
                                   //    $("#toUserName").val(groupname.substring(0, groupname.length - 1));
                                   //    $("#To_User").val(groupcodes.substring(0, groupcodes.length - 1));
                                   //}
                               }
                           });

                       });
                   })
               }
           });
    return oTable1;

}

//获取站内其他消息列表
project.GetOtherMessageList = function GetSelfProjectList(table) {

    var json = {
        userName: $("input[name='userName']").val(),
        collName: $("input[name='collName']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val(),
    };

    var url = '/message/GetOtherMessageList';
    var parm = "?userName=" + json.userName + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime + "&collName=" + json.collName;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取站内其他消息列表
project.initOtherMessageTable = function () {

    var json = {
        userName: $("input[name='userName']").val(),
        collName: $("input[name='collName']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val(),
    };

    var url = '/message/GetOtherMessageList';
    var parm = "?userName=" + json.userName + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime + "&collName=" + json.collName;


    var oTable1 =
           $('#messagelistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  {
                      "data": "State", "mRender": function (data, type, full) {
                          if (data == 1) {
                              return '<span class="label label-success arrowed-in arrowed-in-right">已读</span>';
                          } else {
                              return '<span class="label label-warning arrowed-in arrowed-in-right">未读</span>';
                          }
                      }
                  },
                  { "data": "UserName" },
                  { "data": "CollName" },
                  { "data": "StreetName" },
                  { "data": "MsgTitle" },
                  { "data": "MsgContent" },
                  { "data": "Cu_Date", "mRender": function (data, type, full) { return utils.getFormatDate(data, "yyyy-mm-dd HH:MM:ss") } },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagereplay" id="' + full.Id + '" type="' + full.MsgType + '"><i class="ace-icon fa fa-location-arrow  bigger-120"></i>回复</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagedetail" data-url="/CallAcceptance/message/MessageDetail?id=' + full.Id + '&type=' + full.MsgType + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>查看详情</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue messagedelete" id="' + full.Id + '" type="' + full.MsgType + '"><i class="ace-icon fa fa-trash-o bigger-120"></i>消息删除</span>';
                          html += ' </a>';
                          html += ' </div>';
                          return html;
                      }
                  }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $(".messagedetail").click(function (e) {
                       utils.dialog(this, "消息详情", 400, 400);
                   })

                   $(".messagereplay").click(function () {

                       var url = "/CallAcceptance/message/MessageDetail?id=" + $(this).attr("id") + "&type=" + $(this).attr("type") + "&option=replay";

                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "消息回复",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "回复",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {

                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写回复内容！"); return false;
                                           }

                                           utils.httpClient("/message/ReplayMessage", "post", {
                                               MsgType: 1,
                                               To_User: $("#Go_User").val(),
                                               MsgTitle: $("#MsgTitle").val(),
                                               MsgContent: $("#MsgContent").val(),
                                           }, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert1("回复成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               } else {
                                                   utils.alert1(data.RspMsg);
                                               }
                                           })
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });
                       });

                   })

                   $(".messagedelete").click(function () {
                       if (window.confirm("确定要删除吗？")) {
                           project.deleteMessage($(this).attr("id"));
                       }
                   })

                   $("#createMessage").click(function () {

                       var url = "/CallAcceptance/message/CreateMessage";

                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "发送平台内Web消息",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "发送",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           if ($("#To_User").val().length == 0) {
                                               utils.alert("请选择收件人！"); return false;
                                           }
                                           if ($("#MsgTitle").val().length == 0) {
                                               utils.alert("请填写消息主题！"); return false;
                                           }
                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写消息内容！"); return false;
                                           }

                                           var json = {
                                               MsgType: $("input[name='MsgType']:checked").val(),
                                               To_User: $("#To_User").val(),
                                               MsgTitle: $("#MsgTitle").val(),
                                               MsgContent: $("#MsgContent").val()
                                           };

                                           utils.httpClient("/message/SendMessage", "POST", json, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert("发送成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               }
                                               else {
                                                   utils.alert(data.RspMsg);
                                               }
                                           });
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });

                           //初始化群组树
                           project.messageGroupTree(function () {
                               var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                               var nodes = zTree.getCheckedNodes(true);
                               if (nodes != null) {
                                   if ($("input[name='MsgType']:checked").val() == "1") {
                                       var names = ""; var codes = "";
                                       $.each(nodes, function (index, item) {
                                           if (item.id.indexOf("aaaa") >= 0) {
                                               names += item.name + ",";
                                               codes += item.id.replace("aaaa", "") + ",";
                                           }
                                       })

                                       $("#toUserName").val(names.substring(0, names.length - 1));
                                       $("#To_User").val(codes.substring(0, codes.length - 1));
                                   } else {
                                       var groupname = ""; var groupcodes = "";
                                       $.each(nodes, function (index, item) {
                                           if (item.level == 2) {
                                               groupname += item.name + ",";
                                               groupcodes += item.id + ",";
                                           }
                                       })
                                       $("#toUserName").val(groupname.substring(0, groupname.length - 1));
                                       $("#To_User").val(groupcodes.substring(0, groupcodes.length - 1));
                                   }
                               }
                           });

                           //清空已选内容
                           $("input[name='MsgType']").change(function () {
                               $("#toUserName").val("");
                               $("#To_User").val("");
                           })

                       });

                   })

                   $("#sendMessage").click(function () {
                       var url = "/CallAcceptance/message/SendMobileMessage";
                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "发送手机短消息",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "发送",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           if ($("#To_User").val().length == 0) {
                                               utils.alert("请选择收件人！"); return false;
                                           }

                                           if ($("#MsgContent").val().length == 0) {
                                               utils.alert("请填写短信内容！"); return false;
                                           }

                                           var json = {
                                               mobiles: $("#To_User").val(),
                                               content: $("#MsgContent").val()
                                           };

                                           utils.httpClient("/message/SendMobileMessage", "POST", json, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert("发送成功！");
                                                   var table = $('#messagelistTB').DataTable();
                                                   table.ajax.reload();
                                               }
                                               else {
                                                   utils.alert(data.RspMsg);
                                               }
                                           });
                                       }
                                   },
                                   "button":
                                   {
                                       "label": "取消",
                                       "className": "btn-sm"
                                   }
                               }
                           });

                           project.mobileMessageGroupTree(function () {
                               var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                               var nodes = zTree.getCheckedNodes(true);
                               if (nodes != null) {
                                   // if ($("input[name='MsgType']:checked").val() == "1") {
                                   var names = ""; var codes = "";
                                   $.each(nodes, function (index, item) {
                                       if (item.phone != null && item.phone != "") {
                                           names += item.name + ",";
                                           codes += item.phone + ",";
                                       }
                                   })

                                   $("#toUserName").val(codes.substring(0, codes.length - 1));
                                   $("#To_User").val(codes.substring(0, codes.length - 1));
                                   //} else {
                                   //    var groupname = ""; var groupcodes = "";
                                   //    $.each(nodes, function (index, item) {
                                   //        if (item.level == 2) {
                                   //            groupname += item.name + ",";
                                   //            groupcodes += item.id + ",";
                                   //        }
                                   //    })
                                   //    $("#toUserName").val(groupname.substring(0, groupname.length - 1));
                                   //    $("#To_User").val(groupcodes.substring(0, groupcodes.length - 1));
                                   //}
                               }
                           });

                       });
                   })
               }
           });
    return oTable1;

}

//获取消息详情
project.getMessageDetail = function (msgId, messageType, option) {

    utils.httpClient("/message/GetMessageInfo", "post", { messageId: msgId, type: messageType, option: option }, function (data) {

        if (data.RspCode == 1) {

            var html = template('messagedetailtpl', data.RspData);

            document.getElementById('messagedetail').innerHTML = html;

        } else {
            utils.alert(data.RspMsg);
        }

    });
}

//删除消息
project.deleteMessage = function (msgId) {

    if (!msgId || msgId.length == 0) {
        utils.alert("请选择要删除的消息！");
        return;
    }

    utils.httpClient("/message/DeleteMsg", "POST", { id: msgId }, function (data) {
        if (data.RspCode == 1) {
            utils.alert("删除成功！");
            var table = $('#messagelistTB').DataTable();
            table.ajax.reload();
        } else {
            utils.alert(data.RspMsg);
        }
    })

}

//消息群组树
project.messageGroupTree = function messageGroupTree(callback) {

    $("#selectgroup").click(function () {
        var url = "";
        if ($("input[name='MsgType']:checked").val() == "1") {
            url = "/message/GetUserTreeList";
        } else {
            url = "/message/GetUserGroupList";
        }
        $.get("/CallAcceptance/message/MessageGroupTree", function (data) {
            bootbox.dialog({
                message: data,
                title: "组织管理",
                buttons:
                {
                    "success":
                    {
                        "label": "选择",
                        "className": "btn-sm btn-primary",
                        "callback": function () {
                            if (callback) {
                                callback();
                            }
                        }
                    },
                    "button":
                    {
                        "label": "取消",
                        "className": "btn-sm"
                    }
                }
            });
            var setting = {
                check: {
                    enable: true
                },
                view: {
                    dblClickExpand: false,
                    showLine: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                callback: {
                    onClick: function (e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                    }
                }
            };
            utils.httpClient(url, "post", null, function (data) {
                if (data.RspCode == 1) {

                    zNodes = data.RspData.groups;

                    var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

                } else {
                    utils.alert(data.RspMsg);
                }
            });
        });

    })
}

//手机短信消息群组树
project.mobileMessageGroupTree = function mobileMessageGroupTree(callback) {
    $("#selectgroup").click(function () {
        var url = "";
        if ($("input[name='MsgType']:checked").val() == "1") {
            url = "/message/GetUserPhoneTreeList";
        } else {
            url = "/message/GetGroupTreeList2";
        }
        $.get("/CallAcceptance/message/MessageGroupTree", function (data) {
            bootbox.dialog({
                message: data,
                title: "组织管理",
                buttons:
                {
                    "success":
                    {
                        "label": "选择",
                        "className": "btn-sm btn-primary",
                        "callback": function () {
                            if (callback) {
                                callback();
                            }
                        }
                    },
                    "button":
                    {
                        "label": "取消",
                        "className": "btn-sm"
                    }
                }
            });
            var setting = {
                check: {
                    enable: true
                },
                view: {
                    dblClickExpand: false,
                    showLine: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                callback: {
                    onClick: function (e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                    }
                }
            };
            utils.httpClient(url, "post", null, function (data) {
                if (data.RspCode == 1) {

                    zNodes = data.RspData.groups;

                    var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

                } else {
                    utils.alert(data.RspMsg);
                }
            });
        });

    })
}



//获取监督员列表
project.GetCollecterList = function GetSelfProjectList(table) {

    var json = {
        streetcode: $("select[name='streetcode']").val(),
        gridcode: $("input[name='gridcode']").val(),
        name: $("input[name='name']").val(),
        loginname: $("input[name='loginname']").val(),
        collmobile: $("input[name='collmobile']").val(),
        isguard: $("select[name='isguard']").val(),
    };

    var url = '/collector/GetCollecterList';
    var parm = "?streetcode=" + json.streetcode + "&gridcode=" + json.gridcode + "&name=" + json.name + "&loginname=" + json.loginname + "&collmobile=" + json.collmobile + "&isguard=" + json.isguard;

    var oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//初始化监督员列表
project.initCollecterTable = function () {
    var json = {
        streetcode: $("select[name='streetcode']").val(),
        gridcode: $("input[name='gridcode']").val(),
        name: $("input[name='name']").val(),
        loginname: $("input[name='loginname']").val(),
        collmobile: $("input[name='collmobile']").val(),
        isguard: $("select[name='isguard']").val(),
    };

    var url = '/collector/GetCollecterList';
    var parm = "?streetcode=" + json.streetcode + "&gridcode=" + json.gridcode + "&name=" + json.name + "&loginname=" + json.loginname + "&collmobile=" + json.collmobile + "&isguard=" + json.isguard;


    var oTable1 =
           $('#collecterlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [

                  { "data": "CollCode", "mRender": function (data, type, full) { return '<div class="checkbox"><label><input name="collecter" type="checkbox" class="ace" value="' + data + ',' + full.CollName + ',' + full.Mobile + '"><span class="lbl"> </span></label></div>' } },
                  {
                      "data": "IsGuard", "mRender": function (data, type, full) {
                          if (data == 0) {
                              return "离线";
                          } else {
                              return "在线";
                          }
                      }
                  },
                  { "data": "IsGPS" },
                  { "data": "CollCode" },
                  { "data": "CollName" },
                  { "data": "GridCode" },
                  { "data": "Mobile" },
                  { "data": "LoginName" },
                  { "data": "StreetName" },
                  { "data": "CommName" },
                  { "data": "Version" }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $('#collecterlistTB tbody').on('click', 'tr', function () {
                       if ($(this).hasClass("selected")) {
                           $(this).removeClass("selected");
                       }
                       else {
                           $(this).addClass("selected").siblings().removeClass("selected");
                       }
                   });

                   $("#jdymessage").click(function () {
                       var collecters = new Array();

                       var checkboxs = $("#collecterlistTB").find("input[name='collecter']:checked");

                       $.each(checkboxs, function (index, item) {
                           var collecter = {};
                           collecter.collcode = $(item).val().split(',')[0];
                           collecter.collname = $(item).val().split(',')[1];
                           collecter.mobile = $(item).val().split(',')[2];
                           collecters.push(collecter);
                       })

                       project.sendMessageToJdy(collecters);

                   })

                   $("#jdymobilemessage").click(function () {

                       var collecters = new Array();

                       var checkboxs = $("#collecterlistTB").find("input[name='collecter']:checked");

                       $.each(checkboxs, function (index, item) {
                           var collecter = {};
                           collecter.collcode = $(item).val().split(',')[0];
                           collecter.collname = $(item).val().split(',')[1];
                           collecter.mobile = $(item).val().split(',')[2];
                           collecters.push(collecter);
                       })

                       project.senMobileMessageToJdy(collecters);


                   })
               }
           });
    return oTable1;

}

project.sendMessageToJdy = function (collecters) {
    if (collecters == null || collecters.length == 0) {
        utils.alert("请选择监督员"); return false;
    }
    var collcodes = '';
    var collnames = '';
    $.each(collecters, function (index, item) {
        collcodes += item.collcode + ",";
        collnames += item.collname + ",";
    })


    var url = "/CallAcceptance/message/SendMessageToJdy?collcodes=" + collcodes.substring(0, collcodes.length - 1) + "&collnames=" + collnames.substring(0, collnames.length - 1);

    $.get(url, function (data) {
        bootbox.dialog({
            message: data,
            title: "向监督员发送消息",
            buttons:
            {
                "success":
                {
                    "label": "发送",
                    "className": "btn-sm btn-primary",
                    "callback": function () {

                        if ($("#title").val().length == 0) {
                            utils.alert("请填写消息主题！"); return false;
                        }

                        if ($("#msgcontent").val().length == 0) {
                            utils.alert("请填写消息内容！"); return false;
                        }

                        var json = {
                            collcode: $("#collcode").val(),
                            msgcontent: $("#msgcontent").val(),
                            title: $("#title").val()
                        };

                        utils.httpClient("/message/SendPDAMsg", "POST", json, function (data) {
                            if (data.RspCode == 1) {
                                utils.alert("发送成功！");
                                var table = $('#collecterlistTB').DataTable();
                                table.ajax.reload();
                            }
                            else {
                                utils.alert(data.RspMsg);
                            }
                        });
                    }
                },
                "button":
                {
                    "label": "取消",
                    "className": "btn-sm"
                }
            }
        });
    });

}

project.senMobileMessageToJdy = function (collecters) {
    if (collecters == null || collecters.length == 0) {
        utils.alert("请选择监督员"); return false;
    }
    var mobiles = '';
    $.each(collecters, function (index, item) {
        if (item.mobile != '') {
            mobiles += item.mobile + ",";
        }
    })

    var url = "/CallAcceptance/message/SendMobileMessageToJdy?phones=" + mobiles.substring(0, mobiles.length - 1);

    $.get(url, function (data) {
        bootbox.dialog({
            message: data,
            title: "向监督员发送短信",
            buttons:
            {
                "success":
                {
                    "label": "发送",
                    "className": "btn-sm btn-primary",
                    "callback": function () {

                        if (mobiles.length == 0) {
                            utils.alert("没有可用手机号码！"); return false;
                        }

                        if ($("#msgcontent").val().length == 0) {
                            utils.alert("请填写短信内容！"); return false;
                        }

                        var json = {
                            mobiles: $("#phones").val(),
                            content: $("#msgcontent").val()
                        };

                        utils.httpClient("/message/SendMobileMessage", "POST", json, function (data) {
                            if (data.RspCode == 1) {
                                utils.alert("发送成功！");
                                var table = $('#collecterlistTB').DataTable();
                                table.ajax.reload();
                            }
                            else {
                                utils.alert(data.RspMsg);
                            }
                        });
                    }
                },
                "button":
                {
                    "label": "取消",
                    "className": "btn-sm"
                }
            }
        });
    });

}



//获取监督员工作任务统计列表
project.GetCollecterTaskList = function GetSelfProjectList(table) {

    var json = {
        StreetId: $("select[name='StreetId']").val(),
        Projcode: $("input[name='Projcode']").val(),
        Name: $("input[name='Name']").val(),
        Type: $("select[name='Type']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val()
    };

    var url = '/Collector/GetTaskStat';
    var parm = "?StreetId=" + json.StreetId + "&Projcode=" + json.Projcode + "&Name=" + json.Name + "&Type=" + json.Type + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime;

    var oSettings = table.fnSettings();
    // oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取监督员工作任务统计列表
project.initCollecterTaskTable = function () {

    var json = {
        StreetId: $("select[name='StreetId']").val(),
        Projcode: $("input[name='Projcode']").val(),
        Name: $("input[name='Name']").val(),
        Type: $("select[name='Type']").val(),
        beginTime: $("input[name='beginTime']").val(),
        endTime: $("input[name='endTime']").val()
    };

    var url = '/Collector/GetTaskStat';
    var parm = "?StreetId=" + json.StreetId + "&Projcode=" + json.Projcode + "&Name=" + json.Name + "&Type=" + json.Type + "&beginTime=" + json.beginTime + "&endTime=" + json.endTime;

    utils.httpClient(url + parm, "POST", json, function (data) {
        if (data.RspCode == 1) {
            //if (data.RspData.tasks.length > 0) {

            var source = '{{each tasks as value i}}';
            source += ' <tr role="row"> <td>{{value.CollName}}</td>  <td><a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode={{value.Projcode}}&year={{value.StartYear}}&IsEnd={{value.IsEnd}}">{{value.Projcode}}</a></td>   <td>{{value.StreetName}}</td> <td>{{value.ProbSource}}</td> <td>{{value.NodeId3}}</td>  <td>{{value.NodeId6}}</td>  <td>{{value.NodeId7}}</td> <td>{{value.PdaFlg01}}</td> <td>{{value.PdaFlg21}}</td>  <td>{{value.PdaFlg12}}</td> <td>{{value.Beizhu}}</td><td><div class="hidden-md ">   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View"> <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode={{value.Projcode}}&year={{value.StartYear}}&isend={{value.IsEnd}}"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span> </a>   <div class="inline position-relative">     </div> </div></td></tr>';
            source += '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("#collectertaskTB").find("tbody").html(html);
            $("#taskmessage").html(data.RspData.message);

            $(".projectdetail").click(function (e) {
                utils.dialog(this, "案卷详情", 600, 700);
            })
            $(".projecttrace").click(function (e) {
                utils.dialog(this, "案卷流程", 800, 400);
            })

        }
    });
}



//刷新举报栏待办案卷列表
project.GetCheckProjectList = function GetDelProjectList(table) {

    var json = {
        StreetId: $("select[name='StreetId']").val(),
        LoginName: $("input[name='LoginName']").val(),
        CollName: $("input[name='CollName']").val(),
        Mobile: $("input[name='Mobile']").val(),
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        hcFlag: $("input[name='hcFlag']").val(),
    };
    var url = '/project/GetCheckProjectList';
    var parm = "?StreetId=" + json.StreetId + "&LoginName=" + json.LoginName + "&CollName=" + json.CollName + "&Mobile=" + json.Mobile + "&startTime=" + json.startTime + "&endTime=" + json.endTime + "&hcFlag=" + json.hcFlag;

    oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//获取举报栏代办案卷列表
project.initCheckProjectTable = function (hcFlag) {

    var json = {
        StreetId: $("select[name='StreetId']").val(),
        LoginName: $("input[name='LoginName']").val(),
        CollName: $("input[name='CollName']").val(),
        Mobile: $("input[name='Mobile']").val(),
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        hcFlag: $("input[name='hcFlag']").val(),
    };
    var url = '/project/GetCheckProjectList';
    var parm = "?StreetId=" + json.StreetId + "&LoginName=" + json.LoginName + "&CollName=" + json.CollName + "&Mobile=" + json.Mobile + "&startTime=" + json.startTime + "&endTime=" + json.endTime + "&hcFlag=" + json.hcFlag;


    var oTable1 =
           $('#projectlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 10, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  { "data": "Number" },
                  { "data": "LoginName" },
                  { "data": "Name" },
                  { "data": "Mobile" },
                  { "data": "Projcode", "mRender": function (data, type, full) { return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '">' + data + '</a>'; } },
                  { "data": "SmallClass" },
                  { "data": "PFTime" },
                  { "data": "CheckTime" },
                  { "data": "IsDelay" },
                  { "data": "Square" },
                  { "data": "Address" },
                  { "data": "CheckState" },
                  { "data": "Remark" },
                   {
                       "mRender": function (data, type, full) {
                           var html = '';
                           html += '   <div class="hidden-md ">';
                           html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                           html += ' <span class="blue projecttrace" data-url="/callAcceptance/project/projecttrace?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>案卷流程</span>';
                           html += ' </a>';
                           html += '   <div class="inline position-relative">';


                           html += '     </div>';
                           html += ' </div>';
                           return html;
                       }
                   }
               ],
               "oLanguage": {
                   "sProcessing": "正在处理.....",
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)",
                   "oPaginate": {
                       "sFirst": "第一页",
                       "sPrevious": " 上一页 ",
                       "sNext": " 下一页 ",
                       "sLast": " 最后一页 "
                   }
               }, fnDrawCallback: function () {

                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })

                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })

                   var table = oTable1.DataTable();
                   if (hcFlag == 0) {
                       table.column(3).visible(false);
                       table.column(6).visible(false);
                   }

                   if (hcFlag == 1) {
                       table.column(7).visible(false);
                       table.column(8).visible(false);
                       table.column(11).visible(false);
                   }

               }
           });
    return oTable1;
}