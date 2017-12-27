<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AshxTest.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Hellow World
    </div>
    </form>

    <script type="text/javascript">
        $(function () {
            alert(1);
            //构造一个json对象，很类似于C#中的匿名类型
            var order = {
                Name: "chenxizhang",
                Country: "China",
                ProductList: [
                    { YhqProductId: 10248, ZyProductId: 1001, ProductCount: 1 },
                    { YhqProductId: 10249, ZyProductId: 1002, ProductCount: 2 },
                    { YhqProductId: 10250, ZyProductId: 1003, ProductCount: 3 },
                ]
            };

            //发送POST请求，数据也是json格式。但数值部分需要转换为字符串
            $.post("OrderConfirm.ashx", { data: JSON.stringify(order) }, function (result) {
                alert(2);
                var r = JSON.parse(result);
                alert(r.Message);
                alert(3);
            });
        });
    </script>

</body>
</html>
