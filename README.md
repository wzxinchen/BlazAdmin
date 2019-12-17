# BlazAdmin

## 介绍
基于Blazui （[https://github.com/wzxinchen/Blazui](https://github.com/wzxinchen/Blazui)） 的后台管理模板，无JS，无TS，非 Silverlight，非 WebForm，开箱即用。 

## 计划特性

1.  一个标签，进行一些设置，得到一个后台管理界面 [x] 
2.  标签页式设计，天然模块化设计 [x]
3.  集成Identity认证、用户登录 [x]

## 使用说明

### 准备条件
 > * .net core 3.1
 > * VS2019
 
### 新建一个 Blazor 服务端渲染应用
  ![image.png-49.6kB][5]
### 安装 BlazAdmin.ServerRender Nuget 包
 ![image.png-160.2kB][6]
### 删除 NavMenu.razor 文件
 ![image.png-73.6kB][7]
### _Imports.razor 文件增加以下内容

```csharp
@using BlazAdmin
@using Blazui.Component.Container
@using Blazui.Component.Button
@using Blazui.Component.Dom
@using Blazui.Component.Dynamic
@using Blazui.Component.NavMenu
@using Blazui.Component.Input
@using Blazui.Component.Radio
@using Blazui.Component.Select
@using Blazui.Component.CheckBox
@using Blazui.Component.Switch
@using Blazui.Component.Table
@using Blazui.Component.Popup
@using Blazui.Component.Pagination
@using Blazui.Component.Form
@using Blazui.Component
```
### 为了启用登录，App.razor 文件的内容需要替换为如下

```csharp
<Router AppAssembly="@typeof(Program).Assembly">
    <Found Context="routeData">
         <AuthorizeRouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
    </Found>
    <NotFound>
        <LayoutView Layout="@typeof(MainLayout)">
            <p>Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```
### 登录需要用到数据库，所以添加 DemoDbContext 类
![image.png-22.6kB][8]

```csharp
public class DemoDbContext : IdentityDbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }
}
```
缺少什么命名空间就直接 using，不再赘述

### Startup 文件 ConfigureService 方法替换为如下内容
示例为了方便所以用到了内存数据库，需要安装 nuget 包 Microsoft.EntityFrameworkCore.InMemory
需要 using 相关的命名空间
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<DemoDbContext>(options =>
    {
        options.UseInMemoryDatabase("demo");
    });
    services.AddBlazAdmin<DemoDbContext>();
    services.AddSingleton<WeatherForecastService>();
}
```
### Startup 文件 Configure 方法增加如下内容

#### 增加登录相关配置

```csharp
app.UseAuthorization();
app.UseAuthentication();
```
注意需要加到 app.UseRouting() 方法之下
![image.png-32.2kB][9]

#### 增加 WebApi 相关配置，这主要为登录服务
![image.png-27.6kB][10]

### _Host.cshtml 页面内容替换如下

```csharp
@page "/"
@namespace BlazorApp4.Pages //此处 BlazorApp4 需要改成你实际的命名空间，一般就是项目名
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BlazAdmin Demo</title>
    <base href="~/" />
    <link href="/_content/BlazAdmin/css/admin.css" rel="stylesheet" />
    <link rel="stylesheet" href="/_content/Blazui.Component/css/index.css" />
    <link rel="stylesheet" href="/_content/Blazui.Component/css/fix.css" />
</head>
<body>
    <app>
        @(await Html.RenderComponentAsync<App>(RenderMode.ServerPrerendered))
    </app>
    <script src="/_content/Blazui.Component/js/dom.js"></script>
    <script src="_framework/blazor.server.js"></script>
</body>
</html>
```

### 接下来就是测试菜单和页面，将 MainLayout.razor 文件的内容替换为如下

```csharp
@inherits LayoutComponentBase
<BAdmin Menus="Menus" NavigationTitle="BlazAdmin Demo"></BAdmin>
@code{
    protected List<MenuModel> Menus { get; set; } = new List<MenuModel>();
    protected override void OnInitialized()
    {
        Menus.Add(new MenuModel()
        {
            Label = "示例页面",
            Icon = "el-icon-s-promotion",
            Children = new List<MenuModel>() {
              new MenuModel(){
               Label="示例子页面1",
            Icon = "el-icon-s-promotion",
               Route="/page1"
              },
                 new MenuModel(){
               Label="示例子页面2",
            Icon = "el-icon-s-promotion",
               Route="/page2"
              }
             }
        });
    }
}
```
### 在 Pages 页面下新建两个 Razor 组件，注意是 Razor 组件，将路由分别设置为 /page1 和 /page2

![image.png-123.3kB][11]

### 运行查看效果
![image.png-44.2kB][12]

## 关注与讨论

加入QQ群：74522853

[5]: http://static.zybuluo.com/wzxinchen/gdblemd4hqpdzcq30mrmfiln/image.png
  [6]: http://static.zybuluo.com/wzxinchen/0hx1fjfwb83wvtsm711kxn3f/image.png
  [7]: http://static.zybuluo.com/wzxinchen/wofx18gqb3mogtn7m16kelgz/image.png
  [8]: http://static.zybuluo.com/wzxinchen/un5ci7s8ed9qqa3al2sk3egs/image.png
  [9]: http://static.zybuluo.com/wzxinchen/g69x0f81e009zychu6nsyhol/image.png
  [10]: http://static.zybuluo.com/wzxinchen/ht551s84uhx6cae92vwv0rpx/image.png
  [11]: http://static.zybuluo.com/wzxinchen/c5qzc8jj9e0ds06z7zct1yd9/image.png
  [12]: http://static.zybuluo.com/wzxinchen/r2svafomyl8t2syxv45w7jj2/image.png
