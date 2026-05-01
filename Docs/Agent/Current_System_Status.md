# Current System Status

Last updated: 2026-05-01

## 1. Input Buffer

- Current completion: Minimal C++ scaffold exists in `UCombatAgentCombatComponent`.
- Supported content: Single-slot buffer storage for `LightAttack` and `Dodge` decisions during `AttackStartup` and `AttackRecovery`.
- Unsupported content: Runtime expiration tick, actual Consume Input window, animation Notify integration, and action execution.
- Known issues: Runtime expiration, Consume Input, and action execution are not implemented yet. C++ scaffold compiled successfully in Iteration 04.

## 2. Input Priority

- Current completion: Minimal C++ decision scaffold exists in `UCombatAgentCombatComponent::DecideInput`.
- Supported states: `Idle`, `AttackStartup`, `ComboWindow`, `AttackRecovery`, `Dodge`, `LockedOn`, and `HitReact` enum values exist.
- Unsupported states: Weapon-specific states, air states, stagger depth, execution states, and boss-specific states.
- Known issues: Decision rules are scaffold-level and are not yet validated against animation or gameplay execution.

## 3. Mouse Targeting

- Current completion: Minimal C++ scaffold exists in `UCombatAgentTargetingComponent`.
- Current rules: `RequestLockOn` toggles unlock if a Locked Target exists; actual target search intentionally logs `TargetSearchNotImplemented`.
- Known issues: No target interface, enemy class, candidate search, camera projection helper, scoring, or visibility check exists yet.

## 4. Gamepad Targeting And Switch Target

- Current completion: Minimal C++ scaffold exists.
- Current rules: `SwitchTargetLeft` and `SwitchTargetRight` requests are routed through Player Controller to Targeting Component; without a Locked Target they log `NoLockedTarget`.
- Known issues: No right-stick input binding, candidate search, or screen-space side selection exists yet.

## 5. Debug Tools

- Current completion: Basic C++ log categories exist.
- Existing logs: `LogCombatAgentInput`, `LogCombatAgentBuffer`, `LogCombatAgentPriority`, and `LogCombatAgentTargeting`. Player Controller now logs input mapping, binding readiness, input receipt, and device mode changes.
- Missing logs: Target Score details, candidate filtering, Consume Input result, runtime buffer expiration, and actual validation logs from in-game tests.

## 6. Current Highest Priority TODO

- Create Enhanced Input assets using the recommended names in `Input_Asset_Placeholders.md`.
- Assign those assets through `ACombatAgentPlayerController` class defaults, a Blueprint subclass, or config.
- Assign `ACombatAgentPlayerController` or its configured Blueprint subclass as the active Player Controller.
- Run an in-editor play test to verify input logs and Current Input Device Mode transitions.
- Implement candidate target search and a minimal target interface.

## 8. Enhanced Input Receipt

- Current completion: Minimal C++ binding scaffold exists in `ACombatAgentPlayerController`.
- Supported content: Configurable soft references, optional mapping context addition, input action callbacks, input routing to Combat Component and Targeting Component, and device mode detection through `InputKey`.
- Unsupported content: Input assets are not created, movement/camera behavior is not executed yet, and no runtime editor play test has been run.
- Known issues: No hardcoded fallback paths exist. Input assets must be assigned through defaults, Blueprint, or config before runtime input can bind.

## 7. Code Architecture And Comment Compliance

- Current completion: Initial source architecture document exists in `Source_File_Architecture.md`.
- Current rules: Every project-owned code file must have Chinese file-level comments describing function and implementation approach. Every functional function and variable must have Chinese comments.
- Supported content: Existing `Source` files have been updated with Chinese comments in Iteration 03.
- Known issues: Compile verification passed in Iteration 04, but no in-editor runtime play test has been run yet.
