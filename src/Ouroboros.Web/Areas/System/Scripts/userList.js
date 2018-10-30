layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;

    //用户列表
    var tableIns = table.render({
        elem: '#userList',
        url: '/user/list',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "userListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: 'Id', title: 'ID', width: 60, align: "center" },
            { field: 'UserName', title: '用户名', align: 'center' },
            { field: 'ShowName', title: '显示名', align: 'center' },
            { title: '操作', width: 170, templet: '#userListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() != '') {
            table.reload("userListTable", {
                url: '/user/list',
                where: {
                    userName: $(".searchVal").val()  //搜索的关键字
                }
            })
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    //添加用户
    function addUser(edit) {
        var url = "/system/user/add";
        var title ="添加用户"
        if (edit) {
            url = "/system/user/edit?id=" + edit.Id
            title="编辑用户"
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
    $(".addUser_btn").click(function () {
        addUser();
    })

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('userListTable'),
            data = checkStatus.data,
            ids = "";
        if (data.length > 0) {
            for (var i in data) {
                ids += data[i].Id + ","
            }
            layer.confirm('确定删除选中的用户？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/user/Delete", { id: ids }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            })
        } else {
            layer.msg("请选择需要删除的用户");
        }
    })

    //列表操作
    table.on('tool(userList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { //编辑
            addUser(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此用户？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/user/Delete", { id: data.Id }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            });
        }
    });

})