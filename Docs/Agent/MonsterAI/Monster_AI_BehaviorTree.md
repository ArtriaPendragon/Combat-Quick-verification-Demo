# 怪物 AI 行为树结构

最后更新：2026-05-10

## 1. 行为树职责

行为树负责大流程，不负责写死具体招式条件。具体距离、角度、冷却、阶段和硬直判断由 `UMonsterCombatComponent` 根据数据资产评分完成。

## 2. 建议根结构

```text
Root
  Selector
    Sequence 死亡处理
      条件：BB_IsDead
      任务：播放死亡或停机
    Sequence 倒地/硬直处理
      条件：BB_IsKnockdown 或 BB_IsStaggered
      任务：等待恢复
    Sequence 换区处理
      条件：BB_ShouldChangeArea
      任务：EQS_选择换区点
      任务：移动到换区点
    Sequence 濒死处理
      条件：BB_IsNearDeath
      任务：选择逃离或巢穴点
    Sequence 战斗处理
      条件：BB_CurrentTarget 有效
      Selector
        Sequence 可攻击
          任务：组装评分上下文
          任务：请求选择招式
          条件：BB_SelectedMoveId 有效
          任务：执行招式
        Sequence 需要转向
          条件：BB_TargetAngle 超出招式角度
          任务：转向目标
        Sequence 需要接近
          条件：BB_TargetDistance 超出攻击距离
          任务：移动到接近点
        Sequence 后撤或等待
          条件：招式失败原因为过近、冷却或硬直
          任务：执行回退移动意图
    Sequence 搜索目标
      任务：感知或查询玩家
      任务：更新仇恨表
    Sequence 巡逻
      任务：EQS_选择巡逻点
      任务：移动到巡逻点
```

## 3. EQS 使用点

| 查询 | 用途 | 输出 |
| --- | --- | --- |
| 巡逻点查询 | 非战斗状态选择巡逻点 | `BB_PatrolLocation` |
| 接近点查询 | 战斗状态选择可攻击位置 | `BB_ApproachLocation` |
| 后撤点查询 | 目标过近或招式失败时拉开距离 | `BB_RetreatLocation` |
| 换区点查询 | 阶段变化或濒死时选择目标区域 | `BB_ChangeAreaLocation` |

## 4. 失败回退规则

| 失败原因 | 行为树回退 |
| --- | --- |
| 距离过远 | 接近目标或选择接近点。 |
| 距离过近 | 后撤、侧移或等待短时间。 |
| 角度不匹配 | 转向目标。 |
| 冷却未结束 | 重新评分或等待。 |
| 状态标签阻止 | 进入硬直、倒地或恢复分支。 |
| 阶段标签不满足 | 重新评分普通阶段招式。 |
| 没有数据资产 | 停止攻击分支并记录配置错误。 |

## 5. 行为树任务边界

- 行为树任务可以读取组件、调用评分、写黑板。
- 行为树任务不写具体招式判断公式。
- 行为树任务不直接修改部位耐久。
- 行为树任务必须把失败原因写入黑板或调试快照。

## 6. 当前 C++ 行为树任务

| 任务类 | 文件 | 用途 |
| --- | --- | --- |
| `UBTTask_MonsterSelectAttackMove` | `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree/BTTask_MonsterSelectAttackMove.*` | 从黑板读取目标、距离、角度和玩家数量，调用 `UMonsterCombatComponent::SelectAttackMove`，写回招式编号、招式标签、失败原因和评分摘要。 |
| `UBTTask_MonsterBuildMoveIntent` | `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree/BTTask_MonsterBuildMoveIntent.*` | 根据最近最高分招式失败原因，或手动指定模式，生成接近、转向、后撤、等待或换区移动意图。 |
| `UBTTask_MonsterUpdateDebugSnapshot` | `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree/BTTask_MonsterUpdateDebugSnapshot.*` | 把黑板摘要、EQS 摘要、评分结果和失败原因写入 `UMonsterAIComponent` 的调试快照。 |
| `UBTService_MonsterUpdateCombatBlackboard` | `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree/BTService_MonsterUpdateCombatBlackboard.*` | 周期性更新当前目标、距离、角度、玩家数量和状态布尔值。 |

## 7. 建议黑板接线

| 黑板字段 | 任务使用方式 |
| --- | --- |
| `BB_CurrentTarget` | 作为 `TargetActorKey` 输入。 |
| `BB_TargetDistance` | 作为评分和调试输入。 |
| `BB_TargetAngle` | 作为评分和调试输入。 |
| `BB_ActivePlayerCount` | 作为多人招式评分输入。 |
| `BB_SelectedMoveId` | 由招式选择任务写入。 |
| `BB_CurrentMoveTag` | 由招式选择任务写入字符串形式标签。 |
| `BB_LastMoveFailureReason` | 由招式选择任务写入，移动回退任务间接使用组件中的结构化失败原因。 |
| `BB_LastScoreSummary` | 由招式选择任务写入。 |
| `BB_ApproachLocation`、`BB_RetreatLocation`、`BB_ChangeAreaLocation` | 移动意图任务可写入或读取。 |
| `BB_DebugCurrentState`、`BB_LastEQSSummary` | 调试快照任务可组合为调试输出。 |

这些键名已经在 `MonsterBlackboardKeys.h` 中作为 C++ 常量定义，行为树任务和服务会默认使用这些名称。编辑器中创建黑板资产时，应优先创建同名键，减少逐个手动改节点配置的工作。

## 8. 推荐行为树片段

```text
Sequence 战斗评分
  Task: MonsterSelectAttackMove
  Decorator: BB_SelectedMoveId 已设置
  Task: 执行招式动画

Sequence 招式失败回退
  Task: MonsterBuildMoveIntent(AutoFromFailure)
  Selector
    MoveTo: BB_ApproachLocation 或 DesiredLocation
    Task: 转向或等待
  Task: MonsterUpdateDebugSnapshot
```

## 9. 当前 C++ 控制器入口

`AMonsterAIController` 位于 `Source/Combat_Agent_Demo/MonsterAI/Controllers/MonsterAIController.*`。

职责：

- 在占有怪物后自动运行配置的默认行为树。
- 解除占有时停止行为树逻辑。
- 不写招式选择、移动回退或部位破坏规则。

`AMonsterCharacter` 当前默认使用 `AMonsterAIController`，并设置为场景放置或运行时生成时自动占有 AI。

## 10. 推荐资产配置步骤

1. 创建 `AMonsterAIController` 的蓝图子类。
2. 在控制器蓝图中设置 `DefaultBehaviorTree`。
3. 创建 `AMonsterCharacter` 的蓝图子类或直接使用 C++ 类。
4. 给怪物角色配置 `UMonsterAIDataAsset`。
5. 在行为树根部或战斗分支挂载 `MonsterUpdateCombatBlackboard` 服务。
6. 在战斗分支中按顺序使用 `MonsterSelectAttackMove`、移动回退任务和调试快照任务。
