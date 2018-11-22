layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;

    //用户列表
    var tableIns = table.render({
        elem: '#roleList',
        url: '/role/list',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "roleListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: 'Id', title: 'ID', width: 60, align: "center" },
            { field: 'RoleName', title: '角色名', align: 'center' },
            {
                field: 'IsActlve', title: '是否激活', align: 'center', templet: function (d) {
                    var isActlve = d.IsActlve ? "checked" : "";
                    return '<input type="checkbox" name="IsActlve" value="' + d.Id + '" lay-filter="IsActlve" lay-skin="switch" lay-text="是|否" ' + isActlve + '>'
                }
            },
            { title: '操作', width: 170, templet: '#roleListBar', fixed: "right", align: "center" }
        ]]
    });

    //是否激活
    form.on('switch(IsActlve)', function (data) {
        id = this.value
        var index = layer.msg('修改中，请稍候', { icon: 16, time: false, shade: 0.8 });
        if (data.elem.checked) {
            isActlve = true;
        }
        else {
            isActlve = false;
        }
        $.post("/role/UpdateIsActlve", { id: id, isActlve: isActlve }, function (result) {
            if (result.status == "0") {
                layer.close(index);
                layer.msg(result.msg);
            }
            else {
                layer.close(index);
                layer.msg(result.msg);
            }
        });
    })

    //搜索
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() != '') {
            table.reload("roleListTable", {
                url: '/role/list',
                where: {
                    roleName: $(".searchVal").val()  //搜索的关键字
                }
            })
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    //添加用户
    function addRole(edit) {
        var url = "/system/role/add";
        var title = "添加用户"
        if (edit) {
            url = "/system/role/edit?id=" + edit.Id
            title = "编辑用户"
        }
        var index = layui.layer.open({
            title: title,
            type: 2,
            content: url,
            success: function (layero, index) {
                setTimeout(function () {
                    layui.layer.tips('点击此处返回用户列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(index);
        })
    }
    $(".addRole_btn").click(function () {
        addRole();
    })

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('roleListTable'),
            data = checkStatus.data,
            ids = "";
        if (data.length > 0) {
            for (var i in data) {
                ids += data[i].Id + ","
            }
            layer.confirm('确定删除选中的角色？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/role/Delete", { id: ids }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            })
        } else {
            layer.msg("请选择需要删除的角色");
        }
    })

    //列表操作
    table.on('tool(roleList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { //编辑
            addRole(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此角色？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/role/Delete", { id: data.Id }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            });
        }
    });

})