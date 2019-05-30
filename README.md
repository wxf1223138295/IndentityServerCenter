# IndentityServerCenter
授权认证中心

1.`dotnet new -i identityserver4.templates`安装模板
2.`dotnet new --debug:reinit` 使用
3.`dotnet new is4ef` 使用模板 ，默认sqlserver数据库
>用于docker中sqlserver要求3g以前才能允许运行，我换成了mysql，土豪随意。
>mysql中和sqlserver有很多区别，具体查看授权中心代码

建议使用aspnetcore2.2   3.0存在扩展方法不存在的bug。

##客户端模式
###客户端id
###客户端id加上账号密码
