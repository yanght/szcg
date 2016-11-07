var depart = {};

depart.getDepartlist = function () {
    utils.httpClient("/depart/GetDepartListView", "post", null, function (data) {
        if (data.RspCode == 1) {
            var source = '{{each departs as value i}}';
            source += '  <tr>   <td >{{value.DepartCode}}</td>  <td>{{value.DepartName}} </td> <td><div class="hidden-md "> '
            source += ' <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View"> <span class="blue departedit" data-url="/manager/Department/editdepart?departId={{value.DepartCode}}"> <i class="ace-icon fa fa-search-plus bigger-120"></i>修改</span> </a>'
            source += ' <a href="javascript:;" class="tooltip-info" data-rel="tooltip" title="View"> <span class="blue departdelete" data-url="/manager/Department/editdepart?departId={{value.DepartCode}}"> <i class="ace-icon fa fa-search-plus bigger-120"></i>删除</span> </a>'
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
                  }
              });

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

                                            utils.alert1("部门信息编辑成功!", function () {
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
                        }
                    });

                });

            })


        }
    });

}