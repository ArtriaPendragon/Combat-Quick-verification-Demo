# 源码与文档目录架构

最后更新：2026-05-10

## 1. 文档目的

本文档记录项目自有源码和 Agent 文档的目录分类。任何新增或移动文件都必须同步更新本文档，并保持中文文件级注释、函数注释和关键变量注释。

## 2. 源码目录结构

```text
Source/
  Combat_Agent_Demo.Target.cs
  Combat_Agent_DemoEditor.Target.cs
  Combat_Agent_Demo/
    Combat_Agent_Demo.Build.cs
    Core/
      Combat_Agent_Demo.h
      Combat_Agent_Demo.cpp
      Types/
        CombatAgentTypes.h
    Debug/
      CombatAgentLog.h
      CombatAgentLog.cpp
    Player/
      Characters/
      Components/
      Controllers/
    MonsterAI/
      Types/
      Data/
      Components/
      BehaviorTree/
      Controllers/
      Characters/
```

## 3. 源码分类表

| 分类 | 目录或文件 | 职责 |
| --- | --- | --- |
| 目标配置 | `Source/Combat_Agent_Demo.Target.cs`、`Source/Combat_Agent_DemoEditor.Target.cs` | 定义游戏目标和编辑器目标。 |
| 模块配置 | `Source/Combat_Agent_Demo/Combat_Agent_Demo.Build.cs` | 定义模块依赖和模块根 include 路径。 |
| 核心入口 | `Source/Combat_Agent_Demo/Core/Combat_Agent_Demo.*` | 注册项目主模块，不放玩法逻辑。 |
| 核心共享类型 | `Source/Combat_Agent_Demo/Core/Types/CombatAgentTypes.h` | 保存历史玩家输入缓冲和索敌共享类型。 |
| 调试遥测 | `Source/Combat_Agent_Demo/Debug/CombatAgentLog.*` | 声明玩家系统和怪物 AI 日志分类。 |
| 玩家角色 | `Source/Combat_Agent_Demo/Player/Characters/CombatAgentCharacter.*` | 挂载历史玩家战斗和索敌组件。 |
| 玩家组件 | `Source/Combat_Agent_Demo/Player/Components/CombatAgentCombatComponent.*`、`CombatAgentTargetingComponent.*` | 处理历史玩家输入缓冲和玩家锁定目标骨架。 |
| 玩家控制 | `Source/Combat_Agent_Demo/Player/Controllers/CombatAgentPlayerController.*` | 处理增强输入绑定、设备模式和输入转发。 |
| 怪物 AI 类型 | `Source/Combat_Agent_Demo/MonsterAI/Types/MonsterAITypes.h`、`MonsterBlackboardKeys.h` | 定义怪物状态、招式、部位、评分上下文、移动意图、调试快照和黑板键名常量。 |
| 怪物 AI 数据 | `Source/Combat_Agent_Demo/MonsterAI/Data/MonsterAIDataAsset.*` | 定义怪物主数据资产和招式查询函数。 |
| 怪物 AI 组件 | `Source/Combat_Agent_Demo/MonsterAI/Components/*.h/.cpp` | 承载怪物状态、战斗评分、移动意图和部位破坏。 |
| 怪物 AI 行为树 | `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree/BTTask_Monster*.h/.cpp`、`BTService_Monster*.h/.cpp` | 提供行为树到怪物 AI 组件的桥接任务节点和黑板刷新服务。 |
| 怪物 AI 控制器 | `Source/Combat_Agent_Demo/MonsterAI/Controllers/MonsterAIController.*` | 负责占有怪物后启动默认行为树。 |
| 怪物角色 | `Source/Combat_Agent_Demo/MonsterAI/Characters/MonsterCharacter.*` | 挂载怪物 AI 组件并分发数据资产。 |

## 4. 文档目录结构

```text
Docs/
  Agent/
    Architecture/
    MonsterAI/
    Systems/
    Memory/
    QA/
    Status/
```

## 5. 文档分类表

| 分类 | 目录 | 文件 |
| --- | --- | --- |
| 架构总览 | `Docs/Agent/Architecture` | `Agent_Master_Brief.md`、`Monster_AI_Architecture.md`、`Source_File_Architecture.md` |
| 怪物 AI 设计 | `Docs/Agent/MonsterAI` | `Monster_AI_Data_Model.md`、`Monster_AI_BehaviorTree.md`、`Monster_AI_Blackboard.md`、`Monster_AI_Debugging.md` |
| 系统规则 | `Docs/Agent/Systems` | `Input_Asset_Placeholders.md`、`Input_Buffer_Rules.md`、`Priority_Matrix.md`、`Targeting_Rules.md` |
| 上下文记忆 | `Docs/Agent/Memory` | `Monster_AI_Context_Memory.md`、`Iteration_Log.md`、`Change_Log.md` |
| 验证与风险 | `Docs/Agent/QA` | `Risk_Log.md`、`Test_Cases.md` |
| 当前状态 | `Docs/Agent/Status` | `Current_System_Status.md` |

## 6. Include 规则

项目内头文件使用模块根目录作为引用根，例如：

```cpp
#include "Debug/CombatAgentLog.h"
#include "MonsterAI/Types/MonsterAITypes.h"
#include "Player/Components/CombatAgentCombatComponent.h"
```

`Combat_Agent_Demo.Build.cs` 已通过 `PublicIncludePaths.Add(ModuleDirectory)` 明确声明模块根目录，保证上述路径可被 UHT 和 C++ 编译器识别。

## 7. 注释规则

- 每个源码文件必须有中文文件级说明。
- 每个 `UCLASS`、`USTRUCT`、`UENUM`、`UFUNCTION`、`UPROPERTY` 和关键局部变量必须有中文用途说明。
- 手写注释不使用英文。
- 变量名的中文含义应尽量在文件顶部预览或在声明处说明。

## 8. 本轮检查结果

- 已建立源码和文档目录架构。
- 已移动现有源码和 Agent 文档到对应目录。
- 已修正项目内 include 路径。
- `Combat_Agent_DemoEditor Win64 Development` 编译通过。
- 已新增怪物 AI 行为树任务目录，并通过编译验证。
- 已新增怪物 AI 控制器目录和黑板刷新服务，并通过编译验证。
- 已新增怪物黑板键名常量文件，并为行为树任务和服务配置默认键名。
