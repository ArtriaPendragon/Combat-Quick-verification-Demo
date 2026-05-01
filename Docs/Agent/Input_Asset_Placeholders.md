# Input Asset Placeholders

Last updated: 2026-05-01

## 1. 文档目的

本文档记录增强输入资产的建议命名和路径契约。当前不会由 Agent 直接创建资产，后续由用户在编辑器中按这些路径和名称创建对应游戏资产。

重要规则：代码文件中不硬编码这些路径。`ACombatAgentPlayerController` 只暴露可配置软引用，实际资产引用应通过类默认值、蓝图子类或配置文件注入。

## 2. 占位目录

推荐在内容浏览器中创建以下目录：

```text
/Game/CombatAgent/Input
```

## 3. 输入映射上下文

| 用途 | 资产类型 | 资产名 | 完整路径 |
| --- | --- | --- | --- |
| 默认输入映射上下文 | `Input Mapping Context` | `IMC_CombatAgent_Default` | `/Game/CombatAgent/Input/IMC_CombatAgent_Default` |

## 4. 输入动作

| 输入 | 资产类型 | 资产名 | 完整路径 |
| --- | --- | --- | --- |
| 移动 | `Input Action` | `IA_Move` | `/Game/CombatAgent/Input/IA_Move` |
| 镜头 | `Input Action` | `IA_CameraLook` | `/Game/CombatAgent/Input/IA_CameraLook` |
| 轻攻击 | `Input Action` | `IA_LightAttack` | `/Game/CombatAgent/Input/IA_LightAttack` |
| 闪避 | `Input Action` | `IA_Dodge` | `/Game/CombatAgent/Input/IA_Dodge` |
| 锁定 | `Input Action` | `IA_LockOn` | `/Game/CombatAgent/Input/IA_LockOn` |
| 左切换目标 | `Input Action` | `IA_SwitchTargetLeft` | `/Game/CombatAgent/Input/IA_SwitchTargetLeft` |
| 右切换目标 | `Input Action` | `IA_SwitchTargetRight` | `/Game/CombatAgent/Input/IA_SwitchTargetRight` |

## 5. 建议软引用路径

如果项目决定采用本文档的占位命名，建议把 `ACombatAgentPlayerController` 或其蓝图子类中的软引用设置为以下路径：

```text
/Game/CombatAgent/Input/IMC_CombatAgent_Default.IMC_CombatAgent_Default
/Game/CombatAgent/Input/IA_Move.IA_Move
/Game/CombatAgent/Input/IA_CameraLook.IA_CameraLook
/Game/CombatAgent/Input/IA_LightAttack.IA_LightAttack
/Game/CombatAgent/Input/IA_Dodge.IA_Dodge
/Game/CombatAgent/Input/IA_LockOn.IA_LockOn
/Game/CombatAgent/Input/IA_SwitchTargetLeft.IA_SwitchTargetLeft
/Game/CombatAgent/Input/IA_SwitchTargetRight.IA_SwitchTargetRight
```

如果后续资产创建在这些路径下，并且这些路径被写入控制器软引用，控制器会尝试加载并绑定。若软引用为空或资产缺失，当前逻辑会输出缺失日志并跳过绑定，不会崩溃。

## 6. 推荐配置方式

优先推荐方式：

1. 创建 `ACombatAgentPlayerController` 的蓝图子类。
2. 在蓝图默认值中设置 `DefaultInputMappingContext` 和各个输入动作软引用。
3. 在 GameMode 或项目设置中使用该蓝图控制器。

也可以通过类默认值或配置文件注入这些软引用。若手写配置文件，建议以编辑器保存出的格式为准，避免软引用序列化格式不一致。

## 7. 建议输入值类型

| 输入动作 | 建议值类型 | 说明 |
| --- | --- | --- |
| `IA_Move` | Axis2D | 左右和前后移动输入。 |
| `IA_CameraLook` | Axis2D | 鼠标或右摇杆视角输入。 |
| `IA_LightAttack` | Boolean | 按下触发轻攻击。 |
| `IA_Dodge` | Boolean | 按下触发闪避。 |
| `IA_LockOn` | Boolean | 按下切换锁定或解除锁定。 |
| `IA_SwitchTargetLeft` | Boolean | 按下或由摇杆方向触发左切换。 |
| `IA_SwitchTargetRight` | Boolean | 按下或由摇杆方向触发右切换。 |

## 8. 后续验证步骤

1. 在内容浏览器创建上述资产。
2. 在 `IMC_CombatAgent_Default` 中配置键盘、鼠标和手柄映射。
3. 在控制器类默认值、蓝图子类或配置文件中设置输入资产软引用。
4. 确认项目实际使用 `ACombatAgentPlayerController` 或其蓝图子类。
5. 运行编辑器播放测试。
6. 检查日志是否出现：
   - `InputMappingAdded`
   - `InputBindingsReady`
   - `InputDeviceModeChanged`
   - `InputReceived`
