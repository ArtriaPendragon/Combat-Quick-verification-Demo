# 迭代记录

## 历史迭代 01 至 07 摘要

- 日期：2026-05-01
- 目标：建立玩家输入缓冲、玩家锁定目标、增强输入绑定、日志分类和中文注释规则。
- 结果：历史玩家系统 C++ 骨架通过编译，但输入资产、运行时播放测试和目标搜索仍未完成。
- 当前定位：保留为工程已有基础，不作为当前怪物 AI Agent 的主标准。

## Iteration 08

### 1. 日期

2026-05-10

### 2. 目标

按新文档重构项目方向，建立《怪物猎人：世界》式怪物 AI 的文档标准、数据结构和 C++ 组件骨架。

### 3. 起始上下文

- 用户提供新文档：`D:\笔记\MD笔记\Demo开发\怪猎AI Agent.md`。
- 当前工程已有玩家输入和索敌骨架，但没有怪物 AI 架构。
- 新约束要求：禁止主动控制 Git；所有文档和注释使用中文；怪物行为不得写死在 `AIController` 或 `Character Tick` 中。

### 4. 范围

本轮处理：

- 补齐怪物 AI 架构、数据建模、行为树、黑板、调试和记忆文档。
- 新增数据驱动的怪物 AI C++ 组件骨架。
- 编译验证新增源码。

本轮不处理：

- 不创建 `.uasset` 数据资产、行为树、黑板或 EQS 资产。
- 不接入真实动画蒙太奇。
- 不实现完整攻击命中、伤害和多人感知。
- 不关闭用户正在运行的编辑器。

### 5. 修改文件

- 新增文档：
  - `Monster_AI_Architecture.md`
  - `Monster_AI_Data_Model.md`
  - `Monster_AI_BehaviorTree.md`
  - `Monster_AI_Blackboard.md`
  - `Monster_AI_Debugging.md`
  - `Monster_AI_Context_Memory.md`
- 新增源码：
  - `MonsterAITypes.h`
  - `MonsterAIDataAsset.h`
  - `MonsterAIDataAsset.cpp`
  - `MonsterAIComponent.h`
  - `MonsterAIComponent.cpp`
  - `MonsterCombatComponent.h`
  - `MonsterCombatComponent.cpp`
  - `MonsterMoveComponent.h`
  - `MonsterMoveComponent.cpp`
  - `MonsterPartComponent.h`
  - `MonsterPartComponent.cpp`
  - `MonsterCharacter.h`
  - `MonsterCharacter.cpp`
- 修改源码：
  - `Combat_Agent_Demo.Build.cs`
  - `CombatAgentLog.h`
  - `CombatAgentLog.cpp`
- 修改记录文档：
  - `Agent_Master_Brief.md`
  - `Current_System_Status.md`
  - `Source_File_Architecture.md`
  - `Change_Log.md`
  - `Risk_Log.md`
  - `Test_Cases.md`
  - `Iteration_Log.md`

### 6. 验证

- 第一次构建被 Live Coding 活跃检查阻止；UHT 已生成反射代码，但未进入完整编译。
- 第二次构建加入 `-NoHotReloadFromIDE` 后进入完整编译，发现 `Engine/PrimaryDataAsset.h` 在 UE 5.4 中不存在。
- 修正为 `Engine/DataAsset.h` 后重新构建。
- 最终结果：`Combat_Agent_DemoEditor Win64 Development` 编译通过。

### 7. 新问题

- UnrealBuildTool 内部会打印工作集判断信息；本轮没有主动执行 Git 控制命令。
- 编辑器仍在运行，当前只完成外部编译验证，没有做编辑器播放测试。
- 行为树、黑板、EQS 和数据资产实例仍需在编辑器中创建。

### 8. 下一轮建议

1. 创建怪物数据资产实例并配置基础招式和部位。
2. 创建黑板字段并对齐 `Monster_AI_Blackboard.md`。
3. 创建行为树，调用 `UMonsterCombatComponent::SelectAttackMove`。
4. 创建基础 EQS 查询，用于接近点、后撤点和巡逻点。
5. 运行编辑器测试并把评分结果写回上下文记忆。

## Iteration 09

### 1. 日期

2026-05-10

### 2. 目标

建立标准游戏开发目录架构，并把当前源码与文档按职责分类移动到对应文件夹。

### 3. 起始上下文

- 怪物 AI 代码和历史玩家代码仍平铺在 `Source/Combat_Agent_Demo`。
- Agent 文档仍平铺在 `Docs/Agent`。
- 用户要求创建对应目录和文件夹，并把现有文件分类。

### 4. 目录方案

- 源码：`Core`、`Debug`、`Player`、`MonsterAI`。
- 怪物 AI 子目录：`Types`、`Data`、`Components`、`Characters`。
- 玩家子目录：`Characters`、`Components`、`Controllers`。
- 文档：`Architecture`、`MonsterAI`、`Systems`、`Memory`、`QA`、`Status`。

### 5. 修改内容

- 移动所有项目源码到新目录。
- 移动所有 Agent 文档到新目录。
- 修正所有项目内 `#include` 路径。
- 在构建规则中加入 `PublicIncludePaths.Add(ModuleDirectory)`。
- 更新源码与文档目录架构说明、当前状态、变更记录、测试记录和上下文记忆。

### 6. 验证

- 首次构建发现模块根路径没有进入 include 搜索路径。
- 添加模块根 include 路径后重新构建。
- 最终 `Combat_Agent_DemoEditor Win64 Development` 编译通过。

### 7. 下一轮建议

- 在编辑器中刷新工程或重新生成项目文件，让 IDE 识别新的目录结构。
- 后续新增怪物 AI 文件优先放入 `Source/Combat_Agent_Demo/MonsterAI` 对应子目录。
- 后续新增文档优先放入 `Docs/Agent` 的对应分类目录。

## Iteration 10

### 1. 日期

2026-05-10

### 2. 目标

补齐怪物 AI 的行为树 C++ 桥接层，让编辑器行为树可以直接调用现有怪物 AI 组件。

### 3. 评估

- 当前已有数据资产、AI组件、战斗评分组件、移动意图组件和调试快照。
- 缺口是行为树无法直接把黑板数据传给组件。
- 最小有效目标是新增任务节点，而不是直接创建 `.uasset` 行为树资产。

### 4. 修改内容

- 新增 `BTTask_MonsterSelectAttackMove`。
- 新增 `BTTask_MonsterBuildMoveIntent`。
- 新增 `BTTask_MonsterUpdateDebugSnapshot`。
- 更新行为树文档、源码目录文档、当前状态、变更记录、测试记录和上下文记忆。

### 5. 验证

- 构建 `Combat_Agent_DemoEditor Win64 Development`。
- UHT 生成行为树任务反射代码。
- C++ 编译和模块链接通过。

### 6. 未完成内容

- 尚未创建编辑器行为树资产。
- 尚未在黑板中实际配置任务键。
- 尚未接入动画蒙太奇执行招式。

### 7. 下一轮建议

- 在编辑器中创建黑板资产并按文档字段命名。
- 创建行为树资产，依次接入三个 C++ 任务节点。
- 创建怪物数据资产实例，配置基础招式后运行播放测试。

## Iteration 11

### 1. 日期

2026-05-10

### 2. 目标

继续行为树接入工作，补齐怪物行为树运行入口和战斗黑板刷新服务。

### 3. 评估

- 上一轮已有行为树任务节点，但缺少自动运行行为树的控制器。
- 招式评分任务依赖距离、角度和玩家数量，需要一个服务统一刷新黑板。
- 本轮优先补 C++ 运行入口和 Service，而不是直接创建 `.uasset` 资产。

### 4. 修改内容

- 新增 `MonsterAI/Controllers/MonsterAIController.*`。
- 新增 `BTService_MonsterUpdateCombatBlackboard.*`。
- 修改 `MonsterCharacter.cpp`，默认使用怪物 AIController 并自动占有 AI。
- 更新行为树文档、黑板字段表、源码架构、当前状态、变更记录、测试记录、风险记录和上下文记忆。

### 5. 验证

- 构建 `Combat_Agent_DemoEditor Win64 Development`。
- 新增控制器和 Service 通过 UHT、C++ 编译和链接。

### 6. 未完成内容

- 尚未创建控制器蓝图。
- 尚未配置真实 `DefaultBehaviorTree`。
- 尚未运行编辑器播放测试。

### 7. 下一轮建议

- 创建黑板和行为树资产。
- 创建 `AMonsterAIController` 蓝图子类并配置默认行为树。
- 创建 `AMonsterCharacter` 蓝图子类并配置怪物数据资产。

## Iteration 12

### 1. 日期

2026-05-10

### 2. 目标

固化怪物 AI 黑板键名契约，并让行为树任务和服务默认使用文档中的黑板字段。

### 3. 评估

- 行为树任务已经能工作，但默认黑板键为空会增加编辑器接线错误。
- 当前最小可执行改进是新增键名常量，并在构造函数中设置 `SelectedKeyName`。

### 4. 修改内容

- 新增 `MonsterBlackboardKeys.h`。
- 为 `MonsterSelectAttackMove` 设置默认目标、距离、角度、玩家数量、选中招式、招式标签、失败原因和评分摘要键。
- 为 `MonsterBuildMoveIntent` 设置默认目标、换区位置、期望移动位置、期望朝向和移动意图摘要键。
- 为 `MonsterUpdateDebugSnapshot` 设置默认评分和调试摘要键。
- 为 `MonsterUpdateCombatBlackboard` 设置默认目标、距离、角度、玩家数量、状态和调试键。

### 5. 验证

- 构建 `Combat_Agent_DemoEditor Win64 Development`。
- 编译和链接通过。

### 6. 下一轮建议

- 在编辑器中创建同名黑板键。
- 创建行为树资产，确认节点默认键名自动匹配黑板。
- 运行播放测试观察日志。
