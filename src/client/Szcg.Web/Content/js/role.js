var role = {};

//获取所有权限列表
role.getRoleList = function () {
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
                role.getRoleTree();
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
