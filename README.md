# FLKP (Franklin's Life Keeper Program)

这是一个基于WinUI 3的Windows应用程序,用于跟踪和记录每日自我提升的习惯养成情况。

## 功能特点

- 以表格形式展示每周的自我提升项目
- 支持12个关键习惯的跟踪:
  - 节制欲望
  - 自我控制
  - 沉默寡言
  - 有条不紊
  - 信心坚定
  - 节约开支
  - 勤奋努力
  - 表里一致
  - 待人公正
  - 保持清洁
  - 谨言慎行
  - 谦虚有礼
- 使用SQLite本地数据库存储记录
- 自动高亮显示当天的记录项
- 仅允许修改当天的记录

## 开发技术

- 使用WinUI 3构建用户界面
- 使用C#作为开发语言
- 使用SQLite作为数据存储
- 使用Microsoft.Data.Sqlite进行数据库操作

## 系统要求

- Windows 10 version 1809 或更高版本
- [Windows App SDK](https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads) 1.6或更高版本

## 构建与运行

1. 克隆此仓库
2. 使用Visual Studio 2022打开FLKP.sln
3. 选择目标平台(x64/x86/ARM64)
4. 编译并运行项目

## 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE.txt) 文件
