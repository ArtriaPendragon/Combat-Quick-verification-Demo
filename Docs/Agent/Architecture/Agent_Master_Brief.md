# Agent 总纲

最后更新：2026-05-10

## 1. 当前项目目标

当前工程的 Agent 标准已切换为《怪物猎人：世界》式怪物战斗 AI。后续实现以怪物行为决策、招式选择、仇恨切换、距离判断、硬直反应、部位破坏、多人目标选择、阶段变化和战斗节奏控制为核心。

旧的玩家输入缓冲、玩家锁定目标和增强输入代码保留为工程已有基础，但不再作为本轮 AI Agent 的主目标。

## 2. 强制约束

1. 不主动执行 Git 控制操作。
2. 所有项目文档使用中文。
3. 所有项目自有源码注释使用中文。
4. 怪物行为不得写死在 `AIController` 或 `Character Tick` 中。
5. 策划可调参数必须进入 `DataAsset` 或后续 `DataTable`。
6. 招式选择必须使用评分系统，不使用大量招式专属 `if-else` 堆叠。

## 3. Harness Engine 工作流

每次迭代按以下顺序推进：

1. 设计分析：补齐怪物行为清单、状态关系和核心循环。
2. 数据建模：定义怪物状态、招式、阶段、部位和硬直数据。
3. 行为实现：用组件、行为树、黑板、EQS、GameplayTag 和评分系统组合实现。
4. 调试验证：输出状态、目标、招式、评分、黑板、EQS 和失败原因。
5. 记忆沉淀：更新迭代记录、变更记录、当前状态、风险和上下文记忆。

## 4. 当前运行时模块职责

| 模块 | 职责 |
| --- | --- |
| `UMonsterAIDataAsset` | 保存怪物基础状态、阶段条件、招式表和部位表。 |
| `UMonsterAIComponent` | 保存当前状态、阶段标签、当前目标、仇恨表和调试快照。 |
| `UMonsterCombatComponent` | 评分招式、检查可执行条件、记录冷却和失败原因。 |
| `UMonsterMoveComponent` | 生成接近、转向、后撤、等待和换区移动意图。 |
| `UMonsterPartComponent` | 管理部位耐久、破坏状态和硬直累计。 |
| `AMonsterCharacter` | 只挂载组件和分发数据资产，不承载行为分支。 |

## 5. 必读文档

- `Docs/Agent/Architecture/Monster_AI_Architecture.md`：完整架构和 Harness 节点。
- `Docs/Agent/Architecture/Source_File_Architecture.md`：源码与文档目录分类。
- `Docs/Agent/MonsterAI/Monster_AI_Data_Model.md`：数据资产、招式、部位和评分结构。
- `Docs/Agent/MonsterAI/Monster_AI_BehaviorTree.md`：行为树结构和失败回退规则。
- `Docs/Agent/MonsterAI/Monster_AI_Blackboard.md`：黑板字段表。
- `Docs/Agent/MonsterAI/Monster_AI_Debugging.md`：日志、调试快照和验证规则。
- `Docs/Agent/Memory/Monster_AI_Context_Memory.md`：长期架构决策和临时调试信息。

## 6. 每轮交付要求

每次交付必须说明：

- 已完成内容。
- 未完成内容。
- 风险点。
- 下一步执行建议。
