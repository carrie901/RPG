
<!-- TOC -->

- [SummerBaseCommon](#summerbasecommon)
    - [UpdateGameObject](#updategameobject)
    - [Event 可扩展的事件管理器](#event-可扩展的事件管理器)
    - [Helper 相关工具类](#helper-相关工具类)
    - [Log 日志系统](#log-日志系统)
    - [Pool 缓存池](#pool-缓存池)
    - [Res 资源加载](#res-资源加载)
    - [Sound 播放声音](#sound-播放声音)
    - [Time 时间定时器](#time-时间定时器)
    - [Tools 其他工具](#tools-其他工具)
        - [Coroutine  简单的协同管理器](#coroutine--简单的协同管理器)
        - [CoroutineTaskManager 协同管理器升级](#coroutinetaskmanager-协同管理器升级)
        - [TSingleton 单例实例化](#tsingleton-单例实例化)
        - [DynamicTexture 动态合并图集](#dynamictexture-动态合并图集)
        - [EdNode 配置文件](#ednode-配置文件)
        - [Filter 文本过滤器](#filter-文本过滤器)
        - [Other 其他](#other-其他)
        - [Task 待扩展](#task-待扩展)

<!-- /TOC -->
# SummerBaseCommon

## UpdateGameObject

## Event 可扩展的事件管理器
	通过扩展不同的Key来进行扩展事件管理器,防止事件管理器高度臃肿
	
- StringEventSystem 以string作为Key 进行管理的事件管理器
- GameEventSystem 以E_GLOBAL_EVT(全局战斗类型事件)的事件管理器



## Helper 相关工具类
- ByteHelper 读取二进制的相关的工具类
- GameObjectHelper GameObject相关的工具类**等待扩展**
- MathHelper 数学相关的工具类**等待整理扩展**
- StringHelper 文本相关的工具类**等待整理扩展**
- DateTimeHelper 日期相关的工具类**等待扩展**



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

## Pool 缓存池
	缓存工场-->缓存池-->缓存个体
	主要是为了对象和池耦合
	
	**说明待扩展**

## Res 资源加载

## Sound 播放声音


## Time 时间定时器

	Timer 时间定时器
	时间定时器，一定时间之后调用某个方法，
	调用方法:
	Timer.AddTimer(5.5f,OnTimerHandler)

- TimerInterval 检测时间间隔
- TimeModule 对UnityEngine.Time做了一层包装
- Timer 时间定时器
	

## Tools 其他工具


### Coroutine  简单的协同管理器
	简单的协同管理器StartCoroutineManager
	
### CoroutineTaskManager 协同管理器升级
	这是一个庞大的系统，是对协同管理器的升入管理器 等待后续描述
### TSingleton 单例实例化
	单利实例化

### DynamicTexture 动态合并图集

### EdNode 配置文件
	一个Xml简化版本的配置文件，带扩展为二进制

### Filter 文本过滤器
	针对文本进行过滤
### Other 其他
- BezierVfx 贝塞尔曲线
- DictionaryList 双重的Map和List
- FadeEffect 渐变过程
### Task 待扩展
	一个强大的任务流