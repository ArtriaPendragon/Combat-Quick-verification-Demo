# Test Cases

Last updated: 2026-05-01

## TC-01 Input Buffer Basic Consume

- Preconditions: Character is in `AttackStartup`.
- Action: Press `LightAttack` during startup.
- Expected: `LightAttack` enters Input Buffer and is consumed during `ComboWindow`.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Requires Combat Component and animation window signal.

## TC-02 Input Priority: Dodge vs LightAttack

- Preconditions: Character is in `Idle`.
- Action: Press `Dodge` and `LightAttack` nearly simultaneously.
- Expected: The priority matrix decides the selected action and logs the decision.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Exact near-simultaneous arbitration must be implemented in Combat Component or input aggregation.

## TC-03 Mouse LockOn Target Selection

- Preconditions: Multiple enemies stand in front of the camera.
- Action: Press `LockOn` using mouse/keyboard.
- Expected: System selects the valid target closest to screen center/camera direction.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Requires Targeting Component and target actors.

## TC-04 Gamepad Switch Target Left/Right

- Preconditions: Player has a Locked Target. Valid candidates exist on both screen-space sides.
- Action: Press or flick `SwitchTargetLeft` or `SwitchTargetRight`.
- Expected: System switches to the nearest valid target on the requested side.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Requires Current Input Device Mode and target screen projection.

## TC-05 Lost Lock Handling

- Preconditions: Player has a Locked Target.
- Action: Current Locked Target dies or exceeds maintain distance.
- Expected: System unlocks and logs the reason. Auto relock is deferred unless later enabled.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Requires locked target validity ticking or event-driven invalidation.

## TC-06 Device Mode Detection

- Preconditions: Player can use both mouse/keyboard and gamepad.
- Action: Move mouse, press keyboard key, then use gamepad stick/button.
- Expected: Current Input Device Mode changes to `MouseKeyboard` or `Gamepad` and logs the transition.
- Actual: Not run. Runtime implementation does not exist yet.
- Result: Not run.
- Notes: Required before device-specific targeting behavior is reliable.

## TC-07 C++ Scaffold Compile

- Preconditions: UE 5.4 build tool is available.
- Action: Build the `Combat_Agent_DemoEditor` target.
- Expected: New shared types, log categories, Combat Component, Targeting Component, Character, and Player Controller compile without UHT or C++ errors.
- Actual: Built with `F:\EpicGames\UE_5.4\Engine\Build\BatchFiles\Build.bat Combat_Agent_DemoEditor Win64 Development -Project=F:\UnrealProjects\Combat_Agent_Demo\Combat_Agent_Demo.uproject -WaitMutex -NoHotReload`.
- Result: Passed.
- Notes: UHT generated reflection code and the project module linked successfully in Iteration 04.

## TC-08 Source Comment Compliance

- Preconditions: Project-owned source files have been modified.
- Action: Review changed files under `Source`.
- Expected: Every code file has Chinese file-level comments; every function and important variable has Chinese comments; no manually maintained English comments remain.
- Actual: Existing source files were updated in Iteration 03. Manual review and keyword scan were performed.
- Result: Partial.
- Notes: Comment compliance remains a manual/source-scan check. Compile verification is now available through the discovered UE 5.4 build path.

## TC-09 Enhanced Input Receipt Compile

- Preconditions: Iteration 05 Player Controller input binding code exists.
- Action: Build the `Combat_Agent_DemoEditor` target.
- Expected: Enhanced Input mapping context fields, input action references, callbacks, and `InputKey` device mode detection compile without UHT or C++ errors.
- Actual: Built with `F:\EpicGames\UE_5.4\Engine\Build\BatchFiles\Build.bat Combat_Agent_DemoEditor Win64 Development -Project=F:\UnrealProjects\Combat_Agent_Demo\Combat_Agent_Demo.uproject -WaitMutex -NoHotReload`.
- Result: Passed.
- Notes: Runtime input behavior still requires assets to be assigned and tested in editor play mode.

## TC-10 Input Asset Placeholder Compile

- Preconditions: Player Controller uses soft references with default placeholder paths.
- Action: Build the `Combat_Agent_DemoEditor` target.
- Expected: Soft reference defaults and synchronous loading logic compile before the actual input assets exist.
- Actual: Built with `F:\EpicGames\UE_5.4\Engine\Build\BatchFiles\Build.bat Combat_Agent_DemoEditor Win64 Development -Project=F:\UnrealProjects\Combat_Agent_Demo\Combat_Agent_Demo.uproject -WaitMutex -NoHotReload`.
- Result: Passed.
- Notes: Runtime validation remains blocked until assets are created under `/Game/CombatAgent/Input`.

## TC-11 Input Asset Hardcoded Path Removal

- Preconditions: Player Controller previously assigned default input soft references from hardcoded asset paths.
- Action: Remove hardcoded path assignment and build the `Combat_Agent_DemoEditor` target.
- Expected: Player Controller compiles while input assets remain configurable through defaults, Blueprint, or config; no hardcoded `/Game/CombatAgent/Input` path remains in source.
- Actual: Build succeeded and source scan found no previous hardcoded input asset paths in `CombatAgentPlayerController.h` or `CombatAgentPlayerController.cpp`.
- Result: Passed.
- Notes: Runtime input binding now requires explicit asset reference assignment.
