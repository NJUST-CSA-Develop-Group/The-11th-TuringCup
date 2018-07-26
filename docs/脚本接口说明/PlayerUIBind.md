# PlayerUIBind 脚本说明

> 用于将player和其对应的PlayerStatus及PlayerStatusCurrent进行关联

公共属性：
+ `playerStatus`:对应的playerSatus的UI
+ `playerStatusCurrent`:对应的playerStatusCurrent的UI
+ `index`:玩家序号，需在编辑器中设置，需和controller中保持一致

接口：
+ `void SetHealth(int health)`:更新血量显示
+ `void Die(bool dead = true)`:更新死亡状态显示
+ `void SetSkill(int index, bool type)`:更新技能状态显示，index取1,2,3
+ `void SetScore(int score)`:更新得分