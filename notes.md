## 环境说明
数据库：sql server 2019

IDE: Visual Studio 2019

## 步骤

1. **新建项目**

* 选择asp.net web应用程序（.net framework）  模板
* framework版本选择4.7.2
* 应用程序选择 MVC模式

2. **数据库连接**

* 在Web.config文件中配置数据库连接，添加以下代码

	  <connectionStrings>
		<!--<add name="defaultconnection" providername="system.data.sqlclient" connectionstring="data source=(localdb)\v11.0;initial catalog=aspnet-itcast.cms.webapp-20141109181452;integrated security=sspi;attachdbfilename=|datadirectory|\aspnet-itcast.cms.webapp-20141109181452.mdf" />-->
		<add name="defaultconnection" providerName="system.data.sqlclient" connectionString="Data Source=.;Initial Catalog=ItcastDb;Integrated Security=True" />
		<add name="connStr" connectionString="server=DESKTOP-JFJ2LNM;uid=admin;pwd=123456;database=sysUser" />
		<add name="sysUserEntities" connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-JFJ2LNM;initial catalog=sysUser;persist security info=True;user id=admin;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
		<add name="dbEntities" connectionString="metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-JFJ2LNM;initial catalog=sysUser;persist security info=True;user id=admin;multipleactiveresultsets=True;application name=EntityFramework&quot;" providerName="System.Data.EntityClient" />
		<add name="systemUserEntities" connectionString="metadata=res://*/csdl|res://*/ssdl|res://*/msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-JFJ2LNM;initial catalog=sysUser;user id=admin;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
		<add name="studentEntities" connectionString="metadata=res://*/Models.Student.csdl|res://*/Models.Student.ssdl|res://*/Models.Student.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-JFJ2LNM;initial catalog=student;user id=admin;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
	  </connectionStrings>

* 在model文件下添加 新建 实体数据模型 选择 来着ef设计 添加数据库连接，并选择数据库

3. 业务代码编写

* 前段代码写在views中
* 后端代码写在controller中(前后端通过方法名进行关联)
* 数据模型写在model中

  