# CALLBACK-PARA 

这是一个基于Ice的回调函数的Demo, 使用C#编写,根据Ice官网实例
[CALLBACK](https://github.com/zeroc-ice/ice-demos/tree/3.7/csharp/Ice/callback)
添加回调参数修改的.

## 1. Linux 环境搭建
***
Linux 的发行版是 Linuxmint 19.1, 需要的依赖环境:
* ZeroC 3.7
* .Net Core 2.2.

### 1.1 安装 ZeroC 3.7
ZeroC 官网Linux发行版的安装教程:
[Release Notes](https://doc.zeroc.com/ice/3.7/release-notes/using-the-linux-binary-distributions)

选择 Installing on Ubuntu, 会看到安装说明, 依次执行以下命令.

> sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv B6391CB2CFBA643D

> sudo apt-add-repository -s "deb http://zeroc.com/download/Ice/3.7/ubuntu`lsb_release -rs` stable main"

> sudo apt-get update

> sudo apt-get install zeroc-ice-all-runtime zeroc-ice-all-dev

> sudo apt-get install zeroc-ice-all-runtime zeroc-ice-all-dev

### 1.2 安装 .Net Core 2.2
作为ubuntu18.04库，根据官方的 ubuntu18.04安装方法执行以下命令

> wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb~

> sudo dpkg -i packages-microsoft-prod.deb

> sudo add-apt-repository universe

> sudo apt-get install apt-transport-https

> sudo apt-get update

> sudo apt-get install dotnet-sdk-2.2

> dotnet --list-sdks

最后一条命令打印版本信息, 这里显示的是

> 2.2.202 [/ usr / share / dotnet / sdk]

## 2. 修改配置
***
打开根目录下文件config.client
修改49行为cert目录的绝对路径

> IceSSL.DefaultDir=/home/zp/Documents/Ice/callback/certs

相同方式修改config.server

## 3. 编译调试
***
server 和 client 的编译方式相同, 这里以client为例

1. 切换到目录 Ice-csharp/callback-para/msbuild/client/netstandard2.2/client.csproj

2. 修改第六行目标框架替换为.Net Core

    ```
    <TargetFramework>netcoreapp2.2</TargetFramework>
    ```

3. 修改第七行运行时标识符替换为

    ```
    <RuntimeIdentifiers>linuxmint.17.1-x64</RuntimeIdentifiers>
    ```

    如果是Ubuntu系统则使用

    ```
    <RuntimeIdentifiers>ubuntu.16.04-x64</RuntimeIdentifiers>
    ```

4. 切换到目录 Ice-csharp/callback-para/msbuild/client/netstandard2.2, 执行编译命令

    > dotnet build -r linuxmint.17.1-x64

    如果是Ubuntu系统,则执行

    > dotnet build -r ubuntu.16.04-x64

    如果是编译Release,则执行

    > dotnet publish -c release -r linuxmint.17.1-x64

编译完成后, 会在根目录文件linuxmint.17.1-x64中生成可执行程序client.
相同方式编译server, 根目录下的两个配置文件config.server和config.client拷贝到linuxmint.17.1-x64中.

## 4. 测试运行
切换到目录linuxmint.17.1-x64 ,打开一个终端窗口执行

> ./server

再打开一个终端窗口,执行

> ./client

显示界面如下:

```
usage:
0: help
1: init
2: send callback
3: exit
==>
```

* 输入 1, 回车, 连接服务器
* 输入 2, 输入test, 回车
server 显示 received test
client 显示 callback: test

测试完成.






