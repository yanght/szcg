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

            $("select[name='area']").html(html);

            $("select[name='area']").change(function () {
                var area = $(this).val();
                if (area == "") {
                    $("select[name='square']").html("<option>全部</option>");
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

            $("select[name='street']").html(html);

            $("select[name='street']").change(function () {
                var area = $("select[name='street']:selected").val();
                var street = $(this).val();
                project.getcommounityList(area, street);
            })

        }
        else {
            utils.alert(e.RspMsg);
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

            $("select[name='square']").html(html);

        }
        else {
            utils.alert(e.RspMsg);
        }
    });
}

project.GetDelProjectList = function GetDelProjectList() {

    var json = {
        area: $("select[name='area']").val(),
        street: $("select[name='street']"),
        square: $("select[name='square']"),
        startTime: $("input[name='startTime']").val(),
        endTime: $("input[name='endTime']").val(),
        projcode: $("input[name='projcode']").val(),
        nodeid: 2,

    };

    utils.httpClient("/project/GetDelProjectList", "post", {}, function () {

    });

}