# PlayerInfo 脚本说明

> 此脚本用于UI部分

> 用于在GameScene中显示所有player的信息的一栏，以及指定player的信息

公共属性：
+ `prefab`:预制，需使用`playerStatus.prefab`或`playerStatusCurrent.prefab`
+ `deadMaterial`:死亡后灰度显示材质，需使用`deadMaterial`材质(使用了`greyShader.shader`渲染脚本)
+ `tram_name`:队名
+ `avatar`:队伍头像
+ `skill1Texture`:技能1图标
+ `skill2Texture`:技能2图标
+ `skill3Texture`:技能3图标

接口：
+ `void SetInfo()`:更新UI中队伍信息
+ `void SetHealth(int health)`:更新血量显示
+ `void Die(bool dead = true)`:更新死亡状态显示
+ `void SetSkill(int index, bool type)`:更新技能状态显示，index取1,2,3
+ `void SetScore(int score)`:更新得分