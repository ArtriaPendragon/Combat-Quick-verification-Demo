# 怪物 AI 上下文记忆

最后更新：2026-05-10

## 1. 长期架构决策

- 当前工程 AI 标准切换为《怪物猎人：世界》式怪物 AI。
- 怪物 AI 使用数据资产、行为树、黑板、EQS、GameplayTag 和评分系统组合实现。
- 怪物行为不得写死在 `AIController` 或 `Character Tick` 中。
- 招式参数、部位耐久、阶段条件和权重必须进入 `DataAsset` 或后续 `DataTable`。
- 组件分工固定为 AI 状态、战斗评分、移动意图、部位耐久四个核心方向。
- 旧的玩家输入缓冲与锁定目标代码暂时保留，但不再作为当前 AI Agent 的主标准。
- 源码目录按 `Core`、`Debug`、`Player`、`MonsterAI` 分类。
- 文档目录按 `Architecture`、`MonsterAI`、`Systems`、`Memory`、`QA`、`Status` 分类。
- 项目内 include 使用模块根路径，例如 `MonsterAI/Components/MonsterCombatComponent.h`。
- 行为树桥接层放在 `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree`。
- 行为树通过 `BTTask_MonsterSelectAttackMove`、`BTTask_MonsterBuildMoveIntent`、`BTTask_MonsterUpdateDebugSnapshot` 调用怪物 AI 组件。
- 怪物 AI 控制器放在 `Source/Combat_Agent_Demo/MonsterAI/Controllers`，负责运行默认行为树。
- `BTService_MonsterUpdateCombatBlackboard` 负责刷新评分所需黑板字段。
- 怪物 AI 黑板键名常量放在 `Source/Combat_Agent_Demo/MonsterAI/Types/MonsterBlackboardKeys.h`。

## 2. 本轮输入

- 用户提供的新标准文档：`D:\笔记\MD笔记\Demo开发\怪猎AI Agent.md`。
- 当前工程：`F:\UnrealProjects\Combat_Agent_Demo`。
- 约束：禁止直接控制 Git；所有文档和注释使用中文。

## 3. 本轮输出

- 已新增怪物 AI 架构、数据模型、行为树、黑板、调试和上下文记忆文档。
- 已新增 `MonsterAITypes.h`、`MonsterAIDataAsset.*`、`MonsterAIComponent.*`、`MonsterCombatComponent.*`、`MonsterMoveComponent.*`、`MonsterPartComponent.*`、`MonsterCharacter.*`。
- 已修改构建依赖，加入 `AIModule`、`GameplayTags` 和 `NavigationSystem`。
- 已新增 `LogMonsterAI`、`LogMonsterCombat`、`LogMonsterPart` 日志分类。
- 已更新状态、变更、风险、测试和源码架构记录。

## 4. 临时调试信息

- 第一次构建被 Live Coding 活跃检查阻止。
- 使用 `-NoHotReloadFromIDE` 后进入完整编译。
- 发现 `Engine/PrimaryDataAsset.h` 在 UE 5.4 中不存在，已改回 `Engine/DataAsset.h`。
- 最终 `Combat_Agent_DemoEditor Win64 Development` 编译通过。
- 当前尚未运行怪物 AI 的编辑器播放测试。

## 5. 废弃或降级方案

- 不把怪物行为写入玩家控制器。
- 不在怪物角色 Tick 中堆叠行为分支。
- 不使用大量 `if-else` 为每个招式写专门条件。
- 不创建 `.uasset` 资产；本轮只提供 C++ 和文档骨架。

## 6. 下一轮建议

- 创建 `UMonsterAIDataAsset` 蓝图资产实例。
- 创建黑板、行为树和基础 EQS 查询资产。
- 在怪物蓝图中挂载新组件或继承 `AMonsterCharacter`。
- 运行编辑器测试，记录评分和失败回退日志。

## 7. 目录重构记录

- 本轮已移动现有源码到标准分层目录。
- 本轮已移动 Agent 文档到分类目录。
- 已通过 `Combat_Agent_DemoEditor Win64 Development` 编译验证。
- 若 IDE 未及时刷新目录，需要重新加载项目或刷新工程文件。

## 8. 行为树桥接记录

- 本轮新增三个 C++ 行为树任务节点。
- 招式选择任务读取黑板目标、距离、角度和玩家数量，写回招式编号、招式标签、失败原因和评分摘要。
- 移动意图任务可根据最高分招式失败原因自动生成接近、转向、后撤或等待意图。
- 调试快照任务把黑板摘要、EQS 摘要和评分结果写入 AI 组件。
- 已通过 `Combat_Agent_DemoEditor Win64 Development` 编译验证。

## 9. 行为树运行入口记录

- 本轮新增 `AMonsterAIController`。
- 本轮新增 `UBTService_MonsterUpdateCombatBlackboard`。
- `AMonsterCharacter` 默认使用 `AMonsterAIController` 并自动占有 AI。
- 黑板刷新服务会写入目标距离、目标角度、参与玩家数量、状态枚举和关键状态布尔值。
- 已通过 `Combat_Agent_DemoEditor Win64 Development` 编译验证。

## 10. 黑板键契约记录

- 本轮新增 `MonsterBlackboardKeys.h`。
- 行为树任务和黑板刷新服务已设置默认键名。
- `BB_CurrentMoveTag` 当前以字符串形式写入，便于黑板和日志调试。
- 已通过 `Combat_Agent_DemoEditor Win64 Development` 编译验证。
