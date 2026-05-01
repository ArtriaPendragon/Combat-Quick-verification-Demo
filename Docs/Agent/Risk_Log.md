# Risk Log

Last updated: 2026-05-01

## RISK-001

- Title: Rules may drift from implementation.
- Description: The first iteration creates design rules before runtime code. Later implementation may diverge if records are not updated.
- Trigger condition: Any C++ or Blueprint implementation changes behavior without updating `Docs/Agent`.
- Affected modules: All gameplay interaction modules.
- Severity: Medium.
- Temporary mitigation: Require each implementation iteration to update `Iteration_Log.md`, `Change_Log.md`, and `Current_System_Status.md`.
- Follow-up suggestion: Add log examples after the first runtime implementation exists.
- Current status: Open.

## RISK-002

- Title: No gameplay class structure exists yet.
- Description: The project previously had only a module skeleton. Iteration 02 added C++ scaffold classes, and Iteration 04 compiled them successfully. They are still not assigned in project settings.
- Trigger condition: Attempting to validate Input Buffer, Input Priority, or Targeting behavior before classes are used by the game mode/blueprints.
- Affected modules: Player Controller, Character, Combat Component, Targeting Component.
- Severity: Medium.
- Temporary mitigation: Treat behavior as unavailable until classes are assigned and play-tested.
- Follow-up suggestion: Assign or subclass these classes in the project after input bindings are planned.
- Current status: Mitigated.

## RISK-003

- Title: Visibility filtering may be deferred too long.
- Description: Targeting without obstruction checks can select targets behind walls.
- Trigger condition: Target search uses only distance and angle checks.
- Affected modules: Targeting Component, camera behavior, player lock-on feel.
- Severity: Medium.
- Temporary mitigation: Mark visibility as optional in first implementation but log `VisibilityNotChecked`.
- Follow-up suggestion: Add line trace visibility validation after basic lock-on works.
- Current status: Open.

## RISK-004

- Title: Unreal build tool unavailable in current shell.
- Description: Initial search did not find `Build.bat` in PATH or the common `C:\Program Files\Epic Games\UE_5.4` path. Iteration 04 found the engine at `F:\EpicGames\UE_5.4`.
- Trigger condition: Any future build command assumes the old common path instead of the discovered engine path.
- Affected modules: All C++ modules.
- Severity: Low.
- Temporary mitigation: Use `F:\EpicGames\UE_5.4\Engine\Build\BatchFiles\Build.bat` for command-line builds.
- Follow-up suggestion: Reuse the discovered path for future compile verification.
- Current status: Closed.

## RISK-005

- Title: Comment rules may become inconsistent during rapid implementation.
- Description: The project now requires Chinese comments for every project-owned code file, function, and variable. Fast feature work may accidentally skip these comments.
- Trigger condition: Adding or modifying source files without updating file-level, function-level, or variable-level Chinese comments.
- Affected modules: All project-owned source files.
- Severity: Medium.
- Temporary mitigation: Check modified source files for Chinese comments before ending each iteration.
- Follow-up suggestion: Add a lightweight review checklist or script later if the codebase grows.
- Current status: Open.

## RISK-006

- Title: Enhanced Input assets are not assigned.
- Description: Player Controller now reads configurable soft references, but the project does not yet contain or assign the corresponding assets.
- Trigger condition: Running the game with `ACombatAgentPlayerController` active before assigning input assets through defaults, Blueprint, or config.
- Affected modules: Player Controller, input layer, Combat Component, Targeting Component.
- Severity: Medium.
- Temporary mitigation: Log missing mapping context and missing action bindings without crashing.
- Follow-up suggestion: Create assets using `Input_Asset_Placeholders.md`, then run editor play testing.
- Current status: Open.

## RISK-007

- Title: Input asset references may be left unconfigured.
- Description: Removing hardcoded fallback paths makes the input setup more flexible, but runtime input binding depends on explicit asset assignment.
- Trigger condition: No Blueprint subclass, class default, or config value sets the input soft references.
- Affected modules: Player Controller, input layer, runtime input logs.
- Severity: Medium.
- Temporary mitigation: Missing references are logged and binding is skipped safely.
- Follow-up suggestion: Create a configured Player Controller Blueprint or add a verified config entry after assets exist.
- Current status: Open.
