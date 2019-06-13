# IndentityServerCenter
授权认证中心

1.`dotnet new -i identityserver4.templates`安装模板

2.`dotnet new --debug:reinit` 使用

3.`dotnet new is4ef` 使用模板 ，默认sqlserver数据库
> 用于docker中sqlserver要求3g以前才能允许运行，我换成了mysql，土豪随意。
> mysql中和sqlserver有很多区别，具体查看授权中心代码

4. ` identityServer.AddDeveloperSigningCredential(true,"tempkey.rsa");`
> 每次重启项目的时候，都会重新生成一个新的证书，这时候就会导致一个问题，重启之前生成的access_token，在重启之后，就不适用了，因为证书改变了，对应的加密方式也改变了

建议使用aspnetcore2.2   3.0存在扩展方法不存在的bug。

## 客户端模式
### 客户端id
### 客户端id加上账号密码
### OPenid
### Hybrid 授权码模式 


http://shawnwonglee.cn:5001/    客户端模式和密码模式  在这边
http://shawnwonglee.cn:5003/     api资源服务器
http://shawnwonglee.cn:5004/Home/SimpleModel   简化模式 opid
http://shawnwonglee.cn:5005/Home/HybridModel   授权码模式


账号 1223138295@qq.com
密码 Pass@word123
