var role = {};

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

role.getRoleTree = function () {
    var table;
    var setting = {
        check: {
            enable: true,
            chkboxType: { "Y": "s", "N": "s" }
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

    utils.httpClient("/account/GetRolePermissionTree", "post", { roleId: $("#roleId").val() }, function (data) {

        if (data.RspCode == 1) {

            zNodes = data.RspData.roles;

            var treeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);


        } else {
            utils.alert(data.RspMsg);
        }
    });
}

role.updateModels = function () {
    $("#saveroles").click(function () {
        if ($("#roleId").val().length == 0)
        {
            utils.alert("您没有设置用户,请选择相应角色后设置权限!");
            return false;
        }
        var roles = '';
        var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
        var nodes = treeObj.getCheckedNodes(true);
        $.each(nodes, function (index, item) {
            roles += item.id + ",";
        })
        roles = roles.substring(0, roles.length - 1);
        console.log(roles);
        utils.httpClient("/account/UpdateSystemModel", "post", { roleId: $("#roleId").val(), modelcodes: roles }, function (data) {
            if (data.RspCode == 1) {
                utils.alert("更新成功！");
            } else {
                utils.alert(data.RspMsg);
            }
        });


    })
}
