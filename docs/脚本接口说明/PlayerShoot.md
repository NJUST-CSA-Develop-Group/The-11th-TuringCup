# PlayerShoot 脚本说明

公共属性：
+ `Damage`:单发子弹伤害大小，取正值，默认为25
+ `prefab`:射击动画的预制，需使用`shoot.prefab`文件
+ `shootAudio`:射击音效的`AudioClip`文件
+ `shootLineMaterial`:射击线的材质，需使用`ray.mat`文件
+ `particleMaterial`:射击粒子系统的材质，需使用`shootFire.mat`文件
+ `fixDistance`:用于修正的距离，修正射击检测时，命中自己的情况，为player中心到player外边缘的距离

接口：
+ `void Shoot()`:射击一次，已经对0.5sCD做处理