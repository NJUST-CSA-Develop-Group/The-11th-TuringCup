# PlayerHealth 脚本说明

公共属性：
+ `IsAlive`:只读，表示是否存活
+ `prefab`:受伤闪红动画的预制，需使用`hurt.prefab`文件
+ `hurtAudio`:受伤音效的`AudioClip`文件

接口：
+ `void TakeDamage(int damage)`:产生伤害，damage为伤害值(取正值)，将触发闪红动画(无论是否死亡)
+ `void Treat(int treat = 50)`:治疗，treat为治疗值的大小，仅在活着的时候有效

可能需要扩展的私有函数：
+ `void SetUIHealth()`:应使用此函数对UI中血条的值进行设置
+ `void Die()`:将在死亡时触发

## 关于死亡的时序问题：
由于伤害与治疗可在同一帧中触发，且没有时序，因此在生命为0时，仍可以治疗(需在同一帧)，**在帧结束(LateUpdate)后，将清算血量**，并计算存活状态，同时更新UI，因此，UI中血量实际上为上一帧时的血量