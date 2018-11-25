layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;

    //模块列表
    var tableIns = table.render({
        elem: '#actionList',
        url: '/action/list',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "actionListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: 'Id', title: 'ID', width: 60, align: "center" },
            { field: 'ActionName', title: '模块名称', align: 'center' },
            { field: 'ActionCode', title: '模块编码', align: 'center' },
            { field: 'ActionType', title: '模块类型', align: 'center' },
            { field: 'Url', title: 'url地址', align: 'center' },
            { field: 'Sort', title: '顺序', align: 'center' },
            { field: 'Area', title: '区域', align: 'center' },
            { field: 'Controller', title: '控制器', align: 'center' },
            { field: 'Action', title: '功能', align: 'center' },
            { field: 'HttpMethd', title: '安全方法', align: 'center' },
            { title: '操作', width: 170, templet: '#actionListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() != '') {
            table.reload("actionListTable", {
                url: '/action/list',
                where: {
                    actionName: $(".searchVal").val()  //搜索的关键字
                }
            })
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    //添加模块
    function addAction(edit) {
        var url = "/system/action/add";
        var title = "添加模块"
        if (edit) {
            url = "/system/action/edit?id=" + edit.Id
            title = "编辑模块"
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
    $(".addAction_btn").click(function () {
        addAction();
    })

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('actionListTable'),
            data = checkStatus.data,
            ids = "";
        if (data.length > 0) {
            for (var i in data) {
                ids += data[i].Id + ","
            }
            layer.confirm('确定删除选中的模块？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/action/Delete", { id: ids }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            })
        } else {
            layer.msg("请选择需要删除的模块");
        }
    })

    //列表操作
    table.on('tool(actionList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { //编辑
            addAction(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此模块？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/action/Delete", { id: data.Id }, function (result) {
                    tableIns.reload();
                    layer.close(index);
                    layer.msg(result.msg);
                });
            });
        }
    });


    var setting = {
        callback: {
            beforeClick: zTreeBeforeClick
        }
    };

    function zTreeBeforeClick(treeId, treeNode, clickFlag) {
        if (treeNode.id != '') {
            table.reload("actionListTable", {
                url: '/action/list',
                where: {
                    actionId: treeNode.id
                }
            })
        } else {
            layer.msg("请选中要选择的节点");
        }
    };
    //菜单初始化
    $.ajax({
        url: '/action/LoadTree?_id=0&a=' + ouroboros.getGuid(),
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $.fn.zTree.init($("#treeMenu"), setting, data);
        }
    });


})