# SummerBaseCommon

## UpdateGameObject

## Log 日志系统
	LogManager
- 提供日志的级别输出
- 通过LOG 宏可以屏蔽掉String.Format的性能消耗
- 提供了多种日志管道,在不同情况下日志的输出
	- StringBuilderLog 在退出游戏的时候把日志输入到一个文本中
	- UnityLog 在Unity Console中输出日志
	- RuntimeLog 通过OnGUI输出日志，可以在游戏中看到
	- RemoteLog
	- FileLog 有一点类似StringBuilderLog 只是StringBuilderLog消耗更小一点
- 提供了不同模块的日志输出(目前没有太好的办法)
	- SkillLog
	- PanelLog
	- ....
- ErrorLog 网络错误日志 监听Application.logMessageReceived

## Event 可扩展的事件管理器
	通过扩展不同的Key来进行扩展事件管理器,防止事件管理器高度臃肿
	
- StringEventSystem 以string作为Key 进行管理的事件管理器
- GameEventSystem 以E_GLOBAL_EVT(全局战斗类型事件)的事件管理器

## Pool 缓存池
	缓存工场-->缓存池-->缓存个体
	主要是为了对象和池耦合
	
	**说明待扩展**
	
## Res 资源加载

## Sound 播放声音
	
## Helper
- ByteHelper 读取二进制的相关的工具类
- GameObjectHelper GameObject相关的工具类**等待扩展**
- MathHelper 数学相关的工具类**等待整理扩展**
- StringHelper 文本相关的工具类**等待整理扩展**
- DateTimeHelper 日期相关的工具类**等待扩展**

### 



## Tools


### Coroutine 
	简单的协同管理器StartCoroutineManager
	
### CoroutineTaskManager
	这是一个庞大的系统，是对协同管理器的升入管理器 等待后续描述
### TSingleton
	单利实例化


