# Agent Master Brief

## 1. Project Goal

Build a UE5 third-person action game interaction baseline focused on:

- Input Buffer
- Input Priority
- Target Lock-On
- Mouse/keyboard target selection
- Gamepad target selection and left/right Switch Target

The first phase is a single-player, small-scope prototype. The project should prioritize clear rules, reliable logs, and traceable iteration records before broad gameplay expansion.

## 2. First Phase Scope

Included:

- One player character
- 3 to 5 enemy targets
- Basic LightAttack
- Basic Dodge
- LockOn and unlock
- Mouse/keyboard target selection
- Gamepad target selection
- Gamepad SwitchTargetLeft and SwitchTargetRight
- Basic Input Buffer
- Basic Input Priority

Excluded for now:

- Multiple weapon systems
- Skill trees
- Complex airborne target switching
- Execution system
- Full boss phase logic
- Networking
- Complex multi-level terrain targeting
- Full UI presentation

## 3. Current Source Baseline

As of Iteration 01, the source project is still a UE C++ module skeleton:

- `EnhancedInput` is already listed in `Combat_Agent_Demo.Build.cs`.
- `DefaultInput.ini` uses `EnhancedPlayerInput` and `EnhancedInputComponent`.
- No project-specific Character, Player Controller, Combat Component, or Targeting Component C++ classes were found in `Source`.
- No existing `Docs/Agent` records were found before this iteration.

## 4. Module Responsibilities

### Player Controller

Responsible for:

- Receiving Enhanced Input callbacks.
- Tracking Current Input Device Mode: `MouseKeyboard` or `Gamepad`.
- Forwarding `LockOn`, `SwitchTargetLeft`, and `SwitchTargetRight` requests.
- Forwarding combat input requests such as `LightAttack` and `Dodge`.

Not responsible for:

- Detailed combat state validation.
- Input Buffer storage or Consume Input logic.
- Target Score calculation details.

### Character

Responsible for:

- Owning movement, facing, and lock-on presentation state.
- Forwarding gameplay requests to Combat Component and Targeting Component.
- Applying movement/camera behavior based on lock-on state.

### Combat Component

Responsible for:

- Input Buffer storage and Consume Input.
- Input Priority decisions.
- Executable State checks for combat actions.
- Animation window integration.
- Combat-related debug logs.

### Targeting Component

Responsible for:

- Candidate Targets search.
- Target validity checks.
- Target Score calculation.
- LockOn, unlock, and Switch Target behavior.
- Locked Target validity maintenance.

### AnimInstance / Montage / Notify

Responsible for:

- Emitting attack phase changes such as `AttackStartup`, `ComboWindow`, and `AttackRecovery`.
- Opening and closing Consume Input windows.
- Reporting animation-driven state changes to Combat Component.

### Debug / Telemetry

Responsible for:

- Input event logs.
- Buffer state logs.
- Input Priority decision logs.
- Targeting filter, score, lock, unlock, and switch logs.
- Failure reason logs.

## 5. Unified Terms

| Chinese Term | English Term |
| --- | --- |
| 输入缓冲 | Input Buffer |
| 输入消费 | Consume Input |
| 锁定目标 | Locked Target |
| 候选目标 | Candidate Targets |
| 目标评分 | Target Score |
| 切换目标 | Switch Target |
| 当前设备模式 | Current Input Device Mode |
| 可执行状态 | Executable State |

## 6. Agent Workflow Rules

Every implementation iteration must:

1. Read `Current_System_Status.md`.
2. Read the latest entry in `Iteration_Log.md`.
3. Read recent entries in `Change_Log.md`.
4. Define one main goal for the iteration.
5. State the scope that will not be handled.
6. List affected modules and old behavior that must be protected.
7. Plan debug logs before implementation.
8. Update `Iteration_Log.md`, `Change_Log.md`, and `Current_System_Status.md` before finishing.

## 7. 代码文件架构与中文注释规则

从 Iteration 03 开始，任何创建或修改代码的 Agent 迭代都必须执行以下强制规则：

1. 必须维护清晰的项目文件架构。每个代码文件都必须归入明确分类，例如核心模块、共享类型、玩家控制、角色、战斗、索敌、动画对接、调试遥测或构建配置。
2. 每个代码文件都必须包含中文文件级说明，写清楚该文件的实现功能和实现思路。
3. 每个功能函数和变量都必须写中文注释说明用途。范围包括 `UFUNCTION`、辅助函数、关键局部变量、`UPROPERTY` 成员变量、枚举值和构建规则字段。
4. 项目代码文件中的所有注释必须使用中文。除非是外部工具生成且不由本项目维护的内容，否则不允许保留英文注释。
5. 文件组织发生变化时，必须在同一轮更新 `Source_File_Architecture.md`。
6. 新增或修改注释时，必须在 `Change_Log.md` 记录受影响文件。

这些规则适用于 `Source` 目录下的所有项目自有代码文件，也适用于后续新增的项目代码文件。
