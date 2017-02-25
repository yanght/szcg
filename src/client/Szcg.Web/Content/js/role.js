var role = {};

//获取所有权限列表
role.getRoleList = function (callback) {
    utils.httpClient("/account/GetRoleList", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each roles as value i}}';
            source += '     <li style="cursor:pointer;" roleId="{{value.RoleCode}}"> <a>{{value.RoleName}} </a></li>';
            source += '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("#permissionList").html(html);

            $("#permissionList li").click(function () {
                $("#roleId").val($(this).attr("roleId"));
                if (callback) {
                    callback();
                } else {
                    role.getRoleTree();
                }
            })
            role.updateModels();
        }
    });
}

//改变的权限code
var changeroleids = new Array();

//获取某个角色的权限
role.getRoleTree = function () {

    var table;
    var setting = {
        check: {
            enable: true,
            chkboxType: { "Y": "", "N": "" }
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

            },
            onCheck: function (event, treeId, treeNode) {
                var flag = false;

                $.each(changeroleids, function (index, item) {
                    if (item == treeNode.id) {
                        changeroleids.splice(index, 1);
                        flag = treeId;
                    }
                })
                if (!flag) {
                    changeroleids.push(treeNode.id);
                }
            }
        }
    };

    utils.httpClient("/account/GetRolePermissionTree", "post", { roleId: $("#roleId").val() }, function (data) {

        if (data.RspCode == 1) {

            zNodes = data.RspData.roles;

            var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);


        } else {
            utils.alert(data.RspMsg);
        }
    });
}

//更新角色的权限
role.updateModels = function () {
    $("#saveroles").click(function () {
        if ($("#roleId").val().length == 0) {
            utils.alert("您没有设置用户,请选择相应角色后设置权限!");
            return false;
        }

        utils.httpClient("/account/UpdateSystemModel", "post", { roleId: $("#roleId").val(), modelcodes: changeroleids.toString() }, function (data) {
            if (data.RspCode == 1) {
                utils.alert("更新成功！");
            } else {
                utils.alert(data.RspMsg);
            }
        });


    })
}

//就获取步骤列表
role.getStepList = function () {
    utils.httpClient("/account/GetRoleStepList", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each steps as value i}}'
           + '  <option value="{{value.StepCode}}">{{value.StepName}}</option>'
           + '{{/each}}';

            var render = template.compile(source);

            var html = render(data.RspData);

            $("#StepId").html(html);

        } else {
            utils.alert(data.RspMsg);
        }
    });
}

//获取角色
role.getRoleById = function () {
    utils.httpClient("/account/GetRoleInfo", "post", { roleId: $("#roleId").val() }, function (data) {

        if (data.RspCode == 1) {
            $("#RoleName").val(data.RspData.role.RoleName);
            $("#StepId").find("option[value='" + data.RspData.role.StepId + "']").attr("selected", true);
            $("#AreaId").find("option[value='" + data.RspData.role.AreaCode + "']").attr("selected", true);
        } else {
            utils.alert(data.RspMsg);
        }
    });
}

//保存角色
role.saveRole = function () {
    $("#saverole").click(function () {
        if ($("#RoleName").val().length == 0) {
            utils.alert("请输入角色名称！");
            return false;
        }
        if ($("#AreaId").val().length == 0) {
            utils.alert("请选择区域！");
            return false;
        }

        utils.httpClient("/account/ModifyRole", "post", { RoleCode: $("#roleId").val(), RoleName: $("#RoleName").val(), AreaCode: $("#AreaId").val(), StepId: $("#StepId").val() }, function (data) {
            if (data.RspCode == 1) {
                utils.alert("保存成功");
                role.getRoleList(function () {
                    role.getRoleById();
                });
            } else {
                utils.alert(data.RspMsg);
            }
        });
    })

}

//删除角色
role.deleteRole = function () {
    $("#deleterole").click(function () {
        if (window.confirm("确定要删除吗？")) {
            utils.httpClient("/account/DeleteRole", "post", { roleId: $("#roleId").val() }, function (data) {
                if (data.RspCode == 1) {
                    utils.alert("删除成功！");
                    $("#RoleName").val("");
                    $("#roleId").val("");
                    $('#StepId option:eq(0)').attr('selected', 'selected');
                    $('#AreaId option:eq(0)').attr('selected', 'selected');
                    role.getRoleList(function () {
                        role.getRoleById();
                    });
                } else {
                    utils.alert(data.RspMsg);
                }
            });
        }
    })
}

//添加角色
role.addRole = function () {
    $("#addrole").click(function () {
        $("#RoleName").val("");
        $("#roleId").val("");
        $('#StepId option:eq(0)').attr('selected', 'selected');
        $('#AreaId option:eq(0)').attr('selected', 'selected');
    })
}

//获取部门用户树
role.getDepartUser = function (callback) {

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
                //if (callback) {
                //    callback(treeNode);
                //}
                var usercode = treeNode.id.replace("aaaa", "");
                var username = treeNode.name;
                utils.httpClient("/account/GetRoleListByUserCode", "post", { usercode: usercode, username: username }, function (data) {
                    if (data.RspCode == 1) {
                        var purview= data.RspData.purview;
                        $("#consignerName").val(purview.consignerName);
                        $("#consignerId").val(purview.consignerId);
                        $("#accepterName").val(purview.accepterName);
                        $("#accepterId").val(purview.accepterId);

                        $("#roleNames").val(purview.roleNames);
                        $("#roleIds").val(purview.roleIds);
                        //$("#startTime").val(purview.startTime);
                        //$("#endTime").val(purview.endTime);
                    }
                })

            }
        }
    };

    utils.httpClient("/depart/GetUserTreeList", "post", null, function (data) {
        if (data.RspCode == 1) {

            zNodes = data.RspData.users;

            var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);

        } else {
            utils.alert(data.RspMsg);
        }
    })
}