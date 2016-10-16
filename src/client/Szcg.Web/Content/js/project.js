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
        ButtonCode: buttoncode,
    };

    //查询当前页面的操作按钮
    var optionNames = new Array();
    project.GetFlowNodePower(function (data) {
        $.each(data, function (index, item) {
            if (item.ModelCode == modelcode) {
                $.each(item.ChildPowers, function (i, k) {
                    optionNames.push(k);
                })
            }
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
                      "data": "ProjName", "mRender": function (data, type, full) {
                          return '<a class="projectdetail" href="javascript:;" data-url="/callAcceptance/project/preview?projectcode=' + full.Projcode + '&year=' + full.StartYear + '&isend=' + full.IsEnd + '&nodeid=' + full.NodeId + '">' + data + '</a>';
                      }
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
                   'sSearch': '数据筛选:',
                   "sLengthMenu": "每页显示 _MENU_ 项记录",
                   "sZeroRecords": "没有符合条件的数据...",
                   "sInfo": "当前数据为从第 _START_ 到第 _END_ 项数据；总共有 _TOTAL_ 项记录",
                   "sInfoEmpty": "显示 0 至 0 共 0 项",
                   "sInfoFiltered": "(_MAX_)"
               }, fnDrawCallback: function () {
                   $(".projectdetail").click(function (e) {
                       utils.dialog(this, "案卷详情", 600, 700);
                   })
                   $(".projecttrace").click(function (e) {
                       utils.dialog(this, "案卷流程", 800, 400);
                   })
               }
           });
    return oTable1;
}

//获取当前页面操作按钮
project.GetFlowNodePower = function GetFlowNodePower(callback) {
    utils.httpClient("/account/GetFlowNodePower", "post", null, function (data) {
        if (data.RspCode == 1) {
            callback(data.RspData.nodepower);
        } else {
            utils.alert(data.RspMsg);
        }

    });
}


//案卷上报
project.projectReport = function projectReport(dialog) {

    $('#projectreport').submit(function () {
        utils.httpClient(this.action, this.method, $(this).serialize(), function (data) {
            if (data.RspCode == 1) {
                dialog.dialog("destroy").remove();
                utils.alert("案卷上报成功!", function () {
                    var table = $('#projectlistTB').DataTable();
                    table.ajax.reload();
                });
            } else {
                utils.alert(data.RspMsg);
            }
        });
        return false;
    });

    $("#cancle").click(function () {
        dialog.dialog("destroy").remove();
    })
}

project.projectPz = function projectPz(dialog) {
    $('#projectPz').click(function () {
        utils.httpClient("/project/ProjectApproved", "post", $("#projectreport").serialize(), function (data) {
            if (data.RspCode == 1) {
                dialog.dialog("destroy").remove();
                utils.alert("案卷批转成功!", function () {
                    var table = $('#projectlistTB').DataTable();
                    table.ajax.reload();
                });
            } else {
                utils.alert(data.RspMsg);
            }
        });
        return false;
    });

}


//获取案卷明细
project.getProjectDetail = function getProjectDetail(projectcode, year, isend, nodeid) {

    var json = {
        projcode: projectcode,
        year: year,
        isend: isend,
        nodeid: nodeid
    };

    utils.httpClient("project/GetProjectDetail", "post", json, function (data) {
        if (data.RspCode == 1) {
            var html = template('projectdetailtpl', data.RspData.project);
            document.getElementById('projectdetail').innerHTML = html;
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
        case "img_ajbl":

            var url = "/CallAcceptance/project/projectreport?projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;
            $(this).attr("data-url", url);
            utils.dialog(this, "案卷上报", 600, 720, function (dialog) {
                project.projectReport(dialog);
                project.projectPz(dialog);
            });

            break;
        case "img_PZ":

            var url = "/CallAcceptance/project/ProjectApprovedView?projectcode=" + projcode + "&buttoncode=" + buttoncode + "&year=" + year + "&nodeid=" + nodeid + "&isend=" + isend;
            $(this).attr("data-url", url);
            utils.dialog(this, "案卷批转", 600, 720, function (dialog) {
                project.projectReport(dialog);
                project.projectPz(dialog);
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

            $("#TargetDepartName").click(function () {

                utils.dialog(this, "选择部门", 400, 550, function (dialog) {
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
                            onClick: onClick
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

                    function onClick(e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                        $("#departselected").click(function () {
                            $("#TargetDepartName").val(treeNode.name);
                            $("#TargetDepartCode").val(treeNode.id);
                            dialog.dialog("destroy").remove();
                        });
                    }

                });
            });

            $("#projectclass").click(function () {

                utils.dialog(this, "选择大小类", 400, 550, function (dialog) {
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
                            onClick: onClick
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

                    function onClick(e, treeId, treeNode) {

                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                        $("#departselected").click(function () {

                            $("#projectclass").closest(".form-group").find("input").val("");

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
                            dialog.dialog("destroy").remove();
                        });
                    }

                });
            })

            $("#areaName").click(function () {

                utils.dialog(this, "选择区域街道", 400, 550, function (dialog) {
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
                            onClick: onClick
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

                    function onClick(e, treeId, treeNode) {
                        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                        zTree.expandNode(treeNode);
                        $("#departselected").click(function () {

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
                            dialog.dialog("destroy").remove();
                        });
                    }

                });
            })

        }
        else {
            utils.alert(data.RspMsg);
        }
    });
}

