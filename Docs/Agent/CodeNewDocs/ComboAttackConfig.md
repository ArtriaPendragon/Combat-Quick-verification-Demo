# Combat Combo Structs 说明

## 建议拆成两个结构体

推荐拆成：

1. `FCombatAttackNode`
2. `FCombatAttackTransition`

原因是：

- `AttackNode` 表示“动作本身是什么”
- `Transition` 表示“动作之间怎么连接”

如果只做一个结构体，会把动画资产、Section、输入条件、Tag条件、优先级、目标动作全部混在一起。前期能跑，但后期多武器、多派生、多取消技时会非常难维护。

## 核心运行流程

```text
玩家输入
→ 得到 InputType
→ 用 CurrentActionID + InputType 查找 Transition
→ 检查 RequiredTags / BlockedTags / Priority
→ 得到 ToActionID
→ 用 ToActionID 查找 AttackNode
→ 得到 Montage + SectionName
→ 播放或跳转到对应 Section
```

## 示例

`Light_01` 是一个动作节点：

```text
ActionID = Light_01
Montage = MTG_Sword_Attack
SectionName = Light_01
```

派生规则：

```text
FromActionID = Light_01
InputType = LightAttack
ToActionID = Light_02
```

另一条派生规则：

```text
FromActionID = Light_01
InputType = HeavyAttack
ToActionID = Light01_To_Heavy
```

这样系统就知道：

```text
Light_01 + 轻攻击 → Light_02
Light_01 + 重攻击 → Light01_To_Heavy
```
