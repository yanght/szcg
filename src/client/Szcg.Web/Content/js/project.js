﻿var project = {};

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

//刷举报栏待办案卷列表
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
                              html += '  <li onclick=project.operateProject("' + item.ButtonId + '","' + full.Projcode + '","' + item.ButtonCode + '","' + full.NodeId + '","' + full.StartYear + '","' + full.IsEnd + '")>';
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
                   "sProcessing":"正在处理.....",
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

//跳转至案卷登记页
project.operateProject = function operateProject(buttonid, projcode, buttoncode, nodeid, year, isend) {
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
                                project.projectReport();
                            }
                        },
                        "approve":
                          {
                              "label": "批转",
                              "className": "btn-sm btn-primary",
                              "callback": function () {
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

        case "img_zybmht":
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

