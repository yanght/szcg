var depart = {};

depart.getDepartlist = function () {

    utils.httpClient("/depart/GetDepartListView", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each departs as value i}}';
            source += '  <tr>   <td >{{value.DepartCode}}</td>  <td>{{value.DepartName}} </td> <td><div class="hidden-md"> '
            source += ' <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View"> <span class="blue departedit" data-url="/manager/Department/editdepart?departId={{value.DepartCode}}"> <i class="ace-icon fa fa-search-plus bigger-120"></i>修改</span> </a>'
            source += ' <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View"> <span class="blue departdelete" departId="{{value.DepartCode}}"> <i class="ace-icon fa fa-search-plus bigger-120"></i>删除</span> </a>'
            source += ' </div></td> </tr>';
            source += '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);
            $("#departlistTB").find("tbody").html(html);

            $('#departlistTB')
              .DataTable({
                  "iDisplayLength": 25, //每页显示10条记录
                  "sZeroRecords": "对不起，查询不到相关数据！",
                  "sEmptyTable": "表中无数据存在！",
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

                      $(".departedit").click(function () {

                          var url = $(this).attr("data-url");

                          $.get(url, function (data) {
                              bootbox.dialog({
                                  message: data,
                                  title: "编辑部门信息",
                                  buttons:
                                  {
                                      "success":
                                      {
                                          "label": "保存",
                                          "className": "btn-sm btn-primary",
                                          "callback": function () {
                                              utils.httpClient("/depart/InsertDepart", "post", $("#depart").serialize(), function (data) {
                                                  if (data.RspCode == 1) {

                                                      utils.alert1("部门编辑成功!", function () {
                                                          var table = $('#projectlistTB').DataTable();
                                                          table.ajax.reload();
                                                      });

                                                  } else {
                                                      utils.alert1(data.RspMsg);
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

                              utils.httpClient("/depart/GetDepartInfo", "post", { departId: $("#DepartCode").val() }, function (data) {
                                  if (data.RspCode == 1) {
                                      var html = template('departdetailtpl', data.RspData);
                                      document.getElementById('departdetail').innerHTML = html;
                                      project.getareaList(function () {
                                          $("#AreaId").find("option[value='" + data.RspData.depart.Area + "']").attr("selected", true);
                                      });

                                      project.initPropDepartTree(function () {
                                          var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                                          var nodes = zTree.getSelectedNodes();
                                          if (nodes != null) {
                                              var treeNode = nodes[0];
                                              $("#TargetDepartName").val(treeNode.name);
                                              $("#ParentCode").val(treeNode.tag);
                                          }

                                      });
                                  }
                              });

                          });

                      })

                      $(".departdelete").click(function () {
                          if (window.confirm("确定要删除吗？")) {
                              var departId = $(this).attr("departId");
                              utils.httpClient("/depart/DeleteDepart", "post", { departId: departId }, function (data) {
                                  if (data.RspCode == 1) {
                                      utils.alert("删除成功！");
                                      var table = $('#departlistTB').DataTable();
                                      table.ajax.reload();
                                  } else {
                                      utils.alert(data.RspMsg);
                                  }
                              });
                          }
                      })

                      $("#adddepart").click(function () {

                          var url = $(this).attr("data-url");

                          $.get(url, function (data) {
                              bootbox.dialog({
                                  message: data,
                                  title: "添加部门信息",
                                  buttons:
                                  {
                                      "success":
                                      {
                                          "label": "保存",
                                          "className": "btn-sm btn-primary",
                                          "callback": function () {
                                              utils.httpClient("/depart/InsertDepart", "post", $("#depart").serialize(), function (data) {
                                                  if (data.RspCode == 1) {

                                                      utils.alert1("部门编辑成功!", function () {
                                                          var table = $('#projectlistTB').DataTable();
                                                          table.ajax.reload();
                                                      });

                                                  } else {
                                                      utils.alert1(data.RspMsg);
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

                              utils.httpClient("/depart/GetDepartInfo", "post", { departId: 0 }, function (data) {
                                  if (data.RspCode == 1) {
                                      var html = template('departdetailtpl', data.RspData);
                                      document.getElementById('departdetail').innerHTML = html;
                                      project.getareaList(function () {
                                          $("#AreaId").find("option[value='" + data.RspData.depart.Area + "']").attr("selected", true);
                                      });

                                      project.initPropDepartTree(function () {
                                          var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                                          var nodes = zTree.getSelectedNodes();
                                          if (nodes != null) {
                                              var treeNode = nodes[0];
                                              $("#TargetDepartName").val(treeNode.name);
                                              $("#ParentCode").val(treeNode.tag);
                                          }

                                      });

                                  }

                              });

                          });


                      })
                  }
              });
        }
    });

}

depart.getDepartlistForUser = function () {

    utils.httpClient("/depart/GetDepartListView", "post", null, function (data) {

        if (data.RspCode == 1) {

            var source = '{{each departs as value i}}';
            source += '     <li style="cursor:pointer;" departId="{{value.DepartCode}}"> <a>{{value.DepartName}} </a></li>';
            source += '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("#departlist").html(html);

            var table = depart.initUserListTable();

            $("#query").click(function () {
                depart.GetUserList(table);
            })

            $("#departlist li").click(function () {

                var departId = $(this).attr("departid");
                $("#departId").val(departId);

                $(this).css("background-color", "#EEE").siblings().css("background-color", "white");

                depart.GetUserList(table);

            })

        }
    });
}

//刷新用户列表
depart.GetUserList = function GetUserList(table) {

    var json = {
        departId: $("#departId").val(),
        userName: $("input[name='userName']").val(),
        loginName: $("input[name='loginName']").val(),
        departName: $("input[name='departName']").val()
    };

    var url = '/depart/GetUserByDept';
    var parm = "?userName=" + json.userName + "&loginName=" + json.loginName + "&departName=" + json.departName + "&departId=" + json.departId;

    oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//初始化用户列表
depart.initUserListTable = function () {

    var json = {
        departId: $("#departId").val(),
        userName: $("input[name='userName']").val(),
        loginName: $("input[name='loginName']").val(),
        departName: $("input[name='departName']").val()
    };

    var url = '/depart/GetUserByDept';
    var parm = "?userName=" + json.userName + "&loginName=" + json.loginName + "&departName=" + json.departName + "&departId=" + json.departId;

    var oTable1 =
           $('#userlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 20, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  { "data": "departName" },
                  { "data": "userName" },
                  { "data": "loginName" },
                  { "data": "sex" },
                  { "data": "tel" },
                  { "data": "mobile" },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue userview" data-url="/manager/user/InsertUser?usercode=' + full.userCode + '"><i class="ace-icon fa fa-location-arrow  bigger-120"></i>查看</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue usermodify" data-url="/manager/user/InsertUser?usercode=' + full.userCode + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>修改</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue userdelete" usercode="' + full.userCode + '"><i class="ace-icon fa fa-trash-o bigger-120"></i>删除</span>';
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

                   $(".usermodify").click(function () {

                       var url = $(this).attr("data-url");

                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "确定",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           var json = {
                                               userCode: $("#userCode").val(),
                                               loginName: $("#loginName1").val(),
                                               userName: $("#userName1").val(),
                                               password: $("#password").val(),
                                               sex: $("#sex").val(),
                                               birthday: $("#birthday").val(),
                                               departName: $("#departName").val(),
                                               departcode: $("#departcode").val(),
                                               mobile: $("#mobile").val(),
                                               mobile1: $("#mobile1").val(),
                                               hometel: $("#hometel").val(),
                                               tel: $("#tel").val(),
                                               fax: $("#fax").val(),
                                               email: $("#email").val(),
                                               videolevel: $("#videolevel").val(),
                                               priWeb: $("#priWeb").val(),
                                               pubWeb: $("#pubWeb").val(),
                                               callCenterUserID: $("#callCenterUserID").val(),
                                               email: $("#email").val(),
                                               Sort: $("#Sort").val(),
                                               Hcpower: $("#StreetId").val(),
                                               rolenames: $("#rolenames").val(),
                                               roleids: $("#roleids").val(),
                                               address: $("#address").val(),
                                               memo: $("#memo").val()
                                           };

                                           utils.httpClient("/depart/insertuser", "POST", json, function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert("编辑成功！");
                                                   var table = $('#userlistTB').DataTable();
                                                   table.ajax.reload();
                                               } else {
                                                   utils.alert(data.RspMsg);
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

                   $(".userdelete").click(function () {
                       if (confirm("确定要删除吗？")) {
                           var usercode = $(this).attr("usercode");
                           utils.httpClient("/depart/DeleteUser", "POST", { usercode: usercode }, function (data) {
                               if (data.RspCode == 1) {
                                   utils.alert("删除成功！");
                                   var table = $('#userlistTB').DataTable();
                                   table.ajax.reload();
                               } else {
                                   utils.alert(data.RspMsg);
                               }
                           })
                       }
                   })
               }
           });

    return oTable1;
}


//刷新用户列表
depart.GetCollecterList = function GetUserList(table) {

    var json = {
        Name: $("input[name='Name']").val(),
        LoginName: $("input[name='LoginName']").val(),
        Type: $("input[name='Type']").val(),
        Id: $("input[name='Id']").val(),
        GridCode: $("input[name='GridCode']").val()
    };

    var url = '/Collector/GetCollecters';
    var parm = "?Name=" + json.Name + "&LoginName=" + json.LoginName + "&GridCode=" + json.GridCode + "&Type=" + json.Type + "&Id=" + json.Id;


    oSettings = table.fnSettings();
    oSettings.ajax.url = url + parm;
    table.fnDraw();

}

//初始化用户列表
depart.initCollecterListTable = function () {

    var json = {
        Name: $("input[name='Name']").val(),
        LoginName: $("input[name='LoginName']").val(),
        Type: $("input[name='Type']").val(),
        Id: $("input[name='Id']").val(),
        GridCode: $("input[name='GridCode']").val()
    };

    var url = '/Collector/GetCollecters';
    var parm = "?Name=" + json.Name + "&LoginName=" + json.LoginName + "&GridCode=" + json.GridCode + "&Type=" + json.Type + "&Id=" + json.Id;

    var oTable1 =
           $('#userlistTB')
           .dataTable({
               "bServerSide": true,
               'bPaginate': true, //是否分页
               "iDisplayLength": 20, //每页显示10条记录
               "ajax": {
                   "url": url + parm
               },
               'bFilter': false, //是否使用内置的过滤功能
               "bSort": false,
               "bProcessing": true,
               "columns": [
                  { "data": "CollName" },
                  { "data": "LoginName" },
                  { "data": "GridCode" },
                  { "data": "Mobile" },
                  { "data": "IMEI" },
                  { "data": "HomeTel" },
                  { "data": "HomeAddress" },
                  {
                      "mRender": function (data, type, full) {
                          var html = '';
                          html += '   <div class="hidden-md ">';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue collecterview" data-url="/manager/user/InsertUser?collcode=' + full.CollCode + '"><i class="ace-icon fa fa-location-arrow  bigger-120"></i>查看</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue collectermodify" data-url="/manager/user/InsertCollecter?collcode=' + full.CollCode + '"><i class="ace-icon fa fa-search-plus bigger-120"></i>修改</span>';
                          html += ' </a>';
                          html += '   <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View">';
                          html += ' <span class="blue collecterdelete" collcode="' + full.CollCode + '" ><i class="ace-icon fa fa-trash-o bigger-120"></i>删除</span>';
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

                   $(".addcollecter,.collectermodify").click(function () {
                       var url = $(this).attr("data-url");
                       $.get(url, function (data) {
                           bootbox.dialog({
                               message: data,
                               title: "编辑监督员",
                               buttons:
                               {
                                   "success":
                                   {
                                       "label": "保存",
                                       "className": "btn-sm btn-primary",
                                       "callback": function () {
                                           utils.httpClient("/collector/AddCollecter", "post", $("#collecter").serialize(), function (data) {
                                               if (data.RspCode == 1) {
                                                   utils.alert1("编辑成功!", function () {
                                                       var table = $('#userlistTB').DataTable();
                                                       table.ajax.reload();
                                                   });
                                               } else {
                                                   utils.alert1(data.RspMsg);
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
                   })

                   $(".collecterdelete").click(function () {
                       if (confirm("确定要删除吗？")) {
                           var collcode = $(this).attr("collcode");
                           utils.httpClient("/collector/DeleteCollector", "POST", { collcode: collcode }, function (data) {
                               if (data.RspCode == 1) {
                                   utils.alert("删除成功！");
                                   var table = $('#userlistTB').DataTable();
                                   table.ajax.reload();
                               } else {
                                   utils.alert(data.RspMsg);
                               }
                           })
                       }
                   })
               }
           });

    return oTable1;
}

depart.getUserDetail = function (usercode) {

    utils.httpClient("/depart/GetUserById", "post", { userCode: usercode }, function (data) {

        if (data.RspCode == 1) {

            var html = template('userdetailtpl', data.RspData);

            document.getElementById('userdetail').innerHTML = html;

            project.getstreetList(data.RspData.user.user_areacode, function () {
                $("#StreetId").find("option[value='" + data.RspData.user.Hcpower + "']").attr("selected", true);
            });

            project.initPropDepartTree(function (e, treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#TargetDepartName").val(treeNode.name);
                    $("#departcode").val(treeNode.tag);
                }
            });

            $("#rolenames").click(function () {
                depart.initRoleTree(function () {
                    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                    var nodes = zTree.getCheckedNodes(true);
                    if (nodes != null) {
                        var names = ""; var codes = "";
                        $.each(nodes, function (index, item) {
                            if (item.id.indexOf("aaaa") >= 0) {
                                names += item.name + ",";
                                codes += item.id.replace("aaaa", "") + ",";
                            }
                        })
                        $("#rolenames").val(names.substring(0, names.length - 1));
                        $("#roleids").val(codes.substring(0, codes.length - 1));
                    }
                });
            })


            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'zh-CN'
            })

        } else {
            utils.alert(data.RspMsg);
        }
    });
}

depart.getCollecterDetail = function (collcode) {

    utils.httpClient("/collector/GetCollecterInfoByCode", "post", { collcode: collcode }, function (data) {

        if (data.RspCode == 1) {

            var html = template('collecterdetailtpl', data.RspData);

            document.getElementById('collecterdetail').innerHTML = html;


            project.initPropDepartTree(function (e, treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                var nodes = zTree.getSelectedNodes();
                if (nodes != null) {
                    var treeNode = nodes[0];
                    $("#TargetDepartName").val(treeNode.name);
                    $("#departcode").val(treeNode.tag);
                }
            });

            $("#rolenames").click(function () {
                depart.initRoleTree(function () {
                    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                    var nodes = zTree.getCheckedNodes(true);
                    if (nodes != null) {
                        var names = ""; var codes = "";
                        $.each(nodes, function (index, item) {
                            if (item.id.indexOf("aaaa") >= 0) {
                                names += item.name + ",";
                                codes += item.id.replace("aaaa", "") + ",";
                            }
                        })
                        $("#rolenames").val(names.substring(0, names.length - 1));
                        $("#roleids").val(codes.substring(0, codes.length - 1));
                    }
                });
            })

        } else {
            utils.alert(data.RspMsg);
        }
    });
}

depart.initRoleTree = function (callback) {
    $.get("/manager/user/RoleTree", function (data) {
        var dialog = bootbox.dialog({
            message: data,
            title: "选择角色",
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
        utils.httpClient("/account/GetRoleTree", "post", null, function (data) {
            if (data.RspCode == 1) {

                zNodes = data.RspData.roles;

                var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

            } else {
                utils.alert(data.RspMsg);
            }
        });
    });
}

depart.initAreaTree = function () {
    var table;
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

                var nodes = zTree.getSelectedNodes();
                if (nodes != null && nodes.length > 0) {

                    $("#Id").val(nodes[0].tag);

                    if (nodes[0].level == 0) {
                        $("#Type").val("area");
                    } else {
                        $("#Type").val("street");
                    }

                }

                depart.GetCollecterList(table);

            }
        }
    };

    utils.httpClient("/project/GetAreaTree", "post", null, function (data) {

        if (data.RspCode == 1) {

            zNodes = data.RspData.areaTree;

            var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

            var node = treeObj.getNodes();
            var nodes = treeObj.transformToArray(node);

            $.each(nodes, function (index, item) {
                if (item.level > 1) {
                    treeObj.removeNode(item);
                }
            })

            table = depart.initCollecterListTable();

            $("#query").click(function () {
                depart.GetCollecterList(table);
            })


        } else {
            utils.alert(data.RspMsg);
        }
    });
}