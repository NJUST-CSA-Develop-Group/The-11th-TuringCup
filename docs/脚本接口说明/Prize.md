# PlayerInfo 脚本说明

> 此脚本用于UI部分

> 用于在ResultScene中显示队伍及排名信息

公共属性：
+ `prefab`:预制，需使用`playerStatus.prefab`或`playerStatusCurrent.prefab`
+ `tram_name`:队名
+ `avatar`:队伍头像
+ `orderTexture`:代表排名的图像

接口：
+ `void SetInfo()`:更新UI中队伍信息