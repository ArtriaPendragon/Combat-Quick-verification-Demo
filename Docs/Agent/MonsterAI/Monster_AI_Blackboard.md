# 怪物 AI 黑板字段表

最后更新：2026-05-10

## 1. 字段命名规则

黑板字段使用 `BB_` 前缀记录在文档中，实际 UE 资产中可以去掉前缀，但语义必须保持一致。

当前 C++ 已在 `Source/Combat_Agent_Demo/MonsterAI/Types/MonsterBlackboardKeys.h` 中定义这些默认键名。行为树任务和服务构造时会自动填入推荐键名；编辑器资产中仍需要创建同名黑板键。

## 2. 目标与感知

| 字段 | 类型 | 用途 |
| --- | --- | --- |
| `BB_CurrentTarget` | Object | 当前仇恨最高或被锁定的玩家目标。 |
| `BB_TargetDistance` | Float | 怪物到目标的距离。 |
| `BB_TargetAngle` | Float | 目标相对怪物前向的角度。 |
| `BB_ActivePlayerCount` | Int | 当前参与战斗的玩家数量。 |
| `BB_CurrentHatred` | Float | 当前目标的仇恨值。 |

## 3. 状态与阶段

| 字段 | 类型 | 用途 |
| --- | --- | --- |
| `BB_MonsterState` | Enum | 当前行为状态。 |
| `BB_StateTags` | GameplayTagContainer | 当前状态标签。 |
| `BB_PhaseTags` | GameplayTagContainer | 当前阶段标签。 |
| `BB_IsEnraged` | Bool | 是否处于愤怒状态。 |
| `BB_IsStaggered` | Bool | 是否处于硬直状态。 |
| `BB_IsKnockdown` | Bool | 是否倒地。 |
| `BB_IsNearDeath` | Bool | 是否进入濒死逻辑。 |
| `BB_ShouldChangeArea` | Bool | 是否需要换区。 |
| `BB_IsDead` | Bool | 是否死亡。 |

## 4. 招式与评分

| 字段 | 类型 | 用途 |
| --- | --- | --- |
| `BB_SelectedMoveId` | Name | 当前评分选中的招式编号。 |
| `BB_CurrentMoveTag` | String | 当前招式标签文本。 |
| `BB_LastMoveFailureReason` | String | 最近一次招式失败原因。 |
| `BB_LastScoreSummary` | String | 最近一次评分摘要。 |
| `BB_MoveCooldownRemaining` | Float | 当前招式剩余冷却。 |

## 5. EQS 与移动

| 字段 | 类型 | 用途 |
| --- | --- | --- |
| `BB_PatrolLocation` | Vector | 巡逻目标点。 |
| `BB_ApproachLocation` | Vector | 接近目标点。 |
| `BB_RetreatLocation` | Vector | 后撤或侧移点。 |
| `BB_ChangeAreaLocation` | Vector | 换区目标点。 |
| `BB_DesiredMoveLocation` | Vector | 移动意图任务输出的期望移动位置。 |
| `BB_DesiredMoveYaw` | Float | 移动意图任务输出的期望朝向。 |
| `BB_MoveIntentSummary` | String | 移动意图类型和原因摘要。 |
| `BB_LastEQSSummary` | String | 最近一次 EQS 结果摘要。 |

## 6. 调试字段

| 字段 | 类型 | 用途 |
| --- | --- | --- |
| `BB_DebugCurrentState` | String | 当前状态文本。 |
| `BB_DebugCurrentTarget` | String | 当前目标名称。 |
| `BB_DebugCurrentMove` | String | 当前招式名称。 |
| `BB_DebugFailureReason` | String | 当前失败原因。 |
| `BB_BlackboardSummary` | String | 当前关键黑板值摘要。 |

## 7. C++ 默认键使用情况

| C++ 类 | 默认读取键 | 默认写入键 |
| --- | --- | --- |
| `UBTService_MonsterUpdateCombatBlackboard` | `BB_CurrentTarget` | `BB_TargetDistance`、`BB_TargetAngle`、`BB_ActivePlayerCount`、`BB_MonsterState`、`BB_IsStaggered`、`BB_IsKnockdown`、`BB_IsNearDeath`、`BB_IsDead`、`BB_DebugCurrentTarget`、`BB_DebugCurrentState` |
| `UBTTask_MonsterSelectAttackMove` | `BB_CurrentTarget`、`BB_TargetDistance`、`BB_TargetAngle`、`BB_ActivePlayerCount` | `BB_SelectedMoveId`、`BB_CurrentMoveTag`、`BB_LastMoveFailureReason`、`BB_LastScoreSummary` |
| `UBTTask_MonsterBuildMoveIntent` | `BB_CurrentTarget`、`BB_ChangeAreaLocation` | `BB_DesiredMoveLocation`、`BB_DesiredMoveYaw`、`BB_MoveIntentSummary` |
| `UBTTask_MonsterUpdateDebugSnapshot` | `BB_TargetDistance`、`BB_TargetAngle`、`BB_ActivePlayerCount`、`BB_SelectedMoveId`、`BB_LastMoveFailureReason`、`BB_LastEQSSummary` | `BB_BlackboardSummary` |
