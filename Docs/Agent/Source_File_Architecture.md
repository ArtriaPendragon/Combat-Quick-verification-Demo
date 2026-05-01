# Source File Architecture

Last updated: 2026-05-01

## 1. 文档目的

本文档用于定义项目自有代码文件的分类方式。每个新增代码文件都必须归入明确分类，并在代码中使用中文注释说明文件功能、实现思路、函数职责和变量用途。

## 2. 当前源码分类

| 分类 | 文件 | 职责 |
| --- | --- | --- |
| 构建配置 | `Source/Combat_Agent_Demo.Target.cs`, `Source/Combat_Agent_DemoEditor.Target.cs`, `Source/Combat_Agent_Demo/Combat_Agent_Demo.Build.cs` | 定义游戏目标、编辑器目标和主模块依赖。 |
| 模块入口 | `Source/Combat_Agent_Demo/Combat_Agent_Demo.h`, `Source/Combat_Agent_Demo/Combat_Agent_Demo.cpp` | 注册 UE 主模块，并提供最小模块级包含。 |
| 共享类型 | `Source/Combat_Agent_Demo/CombatAgentTypes.h` | 定义输入名称、设备模式、行为状态、输入决策和单槽输入缓冲结构。 |
| 调试遥测 | `Source/Combat_Agent_Demo/CombatAgentLog.h`, `Source/Combat_Agent_Demo/CombatAgentLog.cpp` | 声明和定义输入、缓冲、优先级、索敌日志分类。 |
| 角色 | `Source/Combat_Agent_Demo/CombatAgentCharacter.h`, `Source/Combat_Agent_Demo/CombatAgentCharacter.cpp` | 挂载战斗组件和索敌组件，作为玩家角色承载点。 |
| 玩家控制 | `Source/Combat_Agent_Demo/CombatAgentPlayerController.h`, `Source/Combat_Agent_Demo/CombatAgentPlayerController.cpp` | 维护当前输入设备模式，从配置、类默认值或蓝图读取增强输入软引用，添加输入映射上下文，绑定输入动作，并把输入请求转发给受控对象组件。 |
| 战斗 | `Source/Combat_Agent_Demo/CombatAgentCombatComponent.h`, `Source/Combat_Agent_Demo/CombatAgentCombatComponent.cpp` | 处理输入优先级决策和首阶段单槽输入缓冲骨架。 |
| 索敌 | `Source/Combat_Agent_Demo/CombatAgentTargetingComponent.h`, `Source/Combat_Agent_Demo/CombatAgentTargetingComponent.cpp` | 处理锁定、解锁和切换目标请求骨架。 |

## 3. 后续目录规划

当前不立刻移动文件，避免在编译未验证前增加路径风险。等 C++ 骨架编译通过后，项目增长时按以下方向拆分：

```text
Source/Combat_Agent_Demo/
  Core/
    CombatAgentTypes.*
  Debug/
    CombatAgentLog.*
  Player/
    CombatAgentPlayerController.*
  Character/
    CombatAgentCharacter.*
  Combat/
    CombatAgentCombatComponent.*
  Targeting/
    CombatAgentTargetingComponent.*
  Animation/
    CombatAgentAnimInstance.*
    CombatAgentAnimNotify_*.*
  Interfaces/
    CombatAgentTargetableInterface.*
```

## 4. 文件级注释规则

每个代码文件开头必须使用中文注释说明：

- 文件功能。
- 实现思路。
- 该文件属于运行时逻辑、构建配置、共享数据还是调试支持。

## 5. 函数与变量注释规则

每个项目自有函数和变量都必须有中文注释：

- 公共函数：说明由谁调用、完成什么行为、返回什么结果。
- 私有辅助函数：说明内部判断、状态转换或数据处理逻辑。
- 成员变量：说明所有权、状态含义和对玩法的影响。
- 局部变量：当变量影响流程、日志、状态判断或玩法行为时必须说明。
- 枚举值：说明该枚举值的玩法意义和预期使用场景。

## 6. 每轮检查清单

任何代码迭代结束前都必须检查：

- 新增或修改的源码文件是否有中文文件级说明。
- 新增或修改的函数是否有中文职责注释。
- 新增或修改的成员变量和关键局部变量是否有中文用途注释。
- 项目自有源码文件中是否仍存在手写英文注释。
- 新增目录或文件分类时，是否同步更新本文档。

## 7. 相关资产命名文档

增强输入资产不属于源码文件，但会影响玩家控制层是否能运行。相关建议命名、路径契约和配置方式记录在 `Input_Asset_Placeholders.md`。源码中不得硬编码这些资产路径。
