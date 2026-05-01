# Targeting Rules

Last updated: 2026-05-01

## 1. First Phase Goal

Provide reliable LockOn and Switch Target behavior for one player and 3 to 5 enemy targets.

## 2. Candidate Target Filter

A target is a Candidate Target only if all required filters pass:

| Filter | First Phase Rule |
| --- | --- |
| Alive | Required. Dead targets are rejected. |
| Distance | Required. Reject targets beyond max lock distance. |
| Attackable | Required. Target must expose an attackable/lockable flag or interface. |
| View cone | Required. Target must be inside a camera-facing cone. |
| Visibility | Optional in first implementation. If omitted, log as not checked. |
| Same target | Allowed for maintaining lock, penalized for new lock selection. |

Initial suggested values:

- Max lock distance: 1800 Unreal units
- Initial lock view cone: 60 degrees from camera forward
- Maintain lock distance: 2200 Unreal units
- Maintain lock view cone: 80 degrees from camera forward

## 3. Target Score Draft

Lower score is better.

Suggested first formula:

```text
TargetScore =
  DistanceNormalized * 0.25
+ CameraAngleNormalized * 0.35
+ ScreenCenterOffsetNormalized * 0.30
+ CharacterFacingAngleNormalized * 0.10
+ CurrentTargetPenalty
+ VisibilityPenalty
```

Initial notes:

- Mouse/keyboard initial LockOn should strongly prefer screen center.
- Gamepad initial LockOn can use the same formula for now.
- `VisibilityPenalty` is `0` if visible, high if blocked, and `0` with a log note if visibility checks are not implemented yet.
- `CurrentTargetPenalty` should discourage reselecting the same target during Switch Target.

## 4. Mouse/Keyboard Rules

Initial LockOn:

- Use Candidate Target Filter.
- Score by camera direction and screen-center offset.
- Choose the lowest Target Score.

Switch Target:

- First phase may reject directional switch from mouse/keyboard unless explicit key bindings are added.
- Mouse camera movement should not automatically switch targets.

## 5. Gamepad Rules

Initial LockOn:

- Use the same Candidate Target Filter and Target Score as mouse/keyboard.

SwitchTargetLeft and SwitchTargetRight:

- Require an existing Locked Target.
- Project candidates to screen space.
- Filter candidates to the requested side of the current Locked Target.
- Select the candidate with the best combination of horizontal direction fit, vertical closeness, and distance.
- If no legal side target exists, keep current Locked Target and log `NoDirectionalCandidate`.

## 6. Locked Target Validity

Unlock when:

- Locked Target dies.
- Locked Target becomes non-attackable.
- Locked Target exceeds maintain distance.
- Locked Target stays outside maintain view cone beyond a grace period.
- Player explicitly toggles unlock.

Auto relock:

- Deferred for first implementation unless testing shows it is necessary.
- If not implemented, log unlock reason and leave lock state off.

## 7. Required Logs

Log these events:

- `TargetSearchStart`
- `TargetFilterReject`
- `TargetScore`
- `LockOnSuccess`
- `LockOnFail`
- `SwitchTargetSuccess`
- `SwitchTargetFail`
- `LockedTargetInvalid`
- `Unlock`

