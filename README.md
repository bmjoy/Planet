# 合成大星星
学了一个月unity3D，花了几天模仿合成大西瓜，做了一个合成大星星。一共实现了以下功能：

1. 用户输入：玩家触摸屏幕后，星球会在相应的位置下落，并冻结玩家的点击。
2. 输入锁定：星球落地后解冻玩家输入，并播放落地音效。
3. 碰撞合成检测：每个星球都会检测周围的碰撞体，如果遇到了相同星星就会向更高级更大的星星合成，合成时播放合成音效，并进行适当的记分。
4. 游戏结束判定：当屏幕内积攒的星球数量溢出时，游戏结束。
5. 用户UI：玩家在失败界面可以查看排行榜和重新开始游戏。