# 当前系统状态

最后更新：2026-05-10

## 1. 怪物 AI 架构

- 当前完成度：已建立怪物 AI 的文档标准和 C++ 组件骨架。
- 已有内容：架构文档、数据结构文档、行为树结构文档、黑板字段表、调试说明、上下文记忆、标准化源码和文档目录、C++ 行为树任务桥接层、怪物 AIController、黑板刷新 Service 和黑板键名常量。
- 未完成内容：实际行为树资产、黑板资产、EQS 查询资产和怪物数据资产实例尚未创建。
- 已验证内容：`Combat_Agent_DemoEditor Win64 Development` 编译通过。

## 2. 数据建模

- 当前完成度：已新增 `UMonsterAIDataAsset`。
- 已有内容：怪物显示名称、初始状态、初始标签、生命比例、感知半径、仇恨切换阈值、招式表和部位表。
- 未完成内容：尚未在编辑器中创建具体怪物数据资产。

## 3. 招式评分

- 当前完成度：已新增 `UMonsterCombatComponent`。
- 已有内容：按距离、角度、冷却、玩家数量、仇恨、状态标签和阶段标签评估招式；最高分招式不可执行时记录失败原因；行为树可通过 `UBTTask_MonsterSelectAttackMove` 调用评分。
- 未完成内容：尚未在编辑器行为树资产中实际接线，尚未接入动画蒙太奇和真实攻击执行。

## 4. 状态、目标与仇恨

- 当前完成度：已新增 `UMonsterAIComponent`。
- 已有内容：当前状态、当前目标、仇恨表、生命阶段更新、评分上下文构建、调试快照，以及 `UBTService_MonsterUpdateCombatBlackboard` 黑板刷新。
- 未完成内容：尚未接入 UE 感知系统、多人玩家发现和黑板自动同步。

## 5. 移动意图

- 当前完成度：已新增 `UMonsterMoveComponent`。
- 已有内容：接近、转向、后撤、等待和换区意图生成；行为树可通过 `UBTTask_MonsterBuildMoveIntent` 按失败原因回退。
- 未完成内容：尚未接入行为树 `MoveTo`、导航路径和 EQS 输出点。

## 6. 部位破坏

- 当前完成度：已新增 `UMonsterPartComponent`。
- 已有内容：部位运行时耐久、破坏判定、硬直累计和一次性硬直标记。
- 未完成内容：尚未接入真实伤害系统、物理部位碰撞和动画反应。

## 7. 调试

- 当前完成度：已新增 `LogMonsterAI`、`LogMonsterCombat`、`LogMonsterPart` 日志分类，并提供 `FMonsterAIDebugSnapshot` 和 `UBTTask_MonsterUpdateDebugSnapshot`。
- 未完成内容：尚未做编辑器运行时测试，尚未把调试快照显示到 UI 或屏幕。

## 8. 历史玩家输入系统

- 当前状态：玩家输入缓冲、增强输入和玩家锁定目标骨架仍保留。
- 当前目录：`Source/Combat_Agent_Demo/Player`。
- 当前定位：历史基础能力，不作为当前怪物 AI 标准的核心交付物。

## 9. 当前目录状态

- 源码已按 `Core`、`Debug`、`Player`、`MonsterAI` 分类。
- 文档已按 `Architecture`、`MonsterAI`、`Systems`、`Memory`、`QA`、`Status` 分类。
- 项目内 include 已统一使用模块根路径。

## 10. 行为树运行入口

- 当前完成度：已新增 `AMonsterAIController`。
- 已有内容：控制器可在占有怪物后运行 `DefaultBehaviorTree`；`AMonsterCharacter` 默认使用该控制器并自动占有 AI。
- 未完成内容：尚未在编辑器中创建控制器蓝图并配置真实行为树资产。

## 11. 当前最高优先级待办

1. 在编辑器中创建 `UMonsterAIDataAsset` 实例并填写至少 3 个招式和 2 个部位。
2. 创建黑板资产，并按 `MonsterBlackboardKeys.h` 和黑板文档加入同名键。
3. 创建行为树资产，在根或战斗分支挂载 `MonsterUpdateCombatBlackboard` 服务。
4. 在控制器蓝图中设置 `DefaultBehaviorTree`。
5. 运行编辑器测试，记录评分、失败回退、部位破坏和阶段切换日志。
