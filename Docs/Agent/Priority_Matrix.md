# Input Priority Matrix

Last updated: 2026-05-01

## 1. Legend

- `Execute`: run immediately.
- `Buffer`: save to Input Buffer if bufferable.
- `Reject`: ignore and log reason.
- `Live`: read continuously without priority arbitration.
- `Switch`: execute target switch logic immediately.
- `Toggle`: execute lock/unlock logic immediately.

## 2. Matrix Draft

| State | LightAttack | Dodge | LockOn | SwitchTargetLeft | SwitchTargetRight | Move | CameraLook |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Idle | Execute | Execute | Toggle | Reject if unlocked, Switch if locked | Reject if unlocked, Switch if locked | Live | Live |
| AttackStartup | Buffer | Buffer | Toggle if allowed | Switch if locked | Switch if locked | Live with movement limits | Live |
| ComboWindow | Execute combo | Execute if dodge-cancel allowed, otherwise Buffer | Toggle if allowed | Switch if locked | Switch if locked | Live with movement limits | Live |
| AttackRecovery | Buffer | Execute if recovery-cancel allowed, otherwise Buffer | Toggle if allowed | Switch if locked | Switch if locked | Live with movement limits | Live |
| Dodge | Reject | Reject | Toggle if allowed | Switch if locked | Switch if locked | Live with movement limits | Live |
| LockedOn | Execute | Execute | Toggle unlock | Switch | Switch | Live | Camera orbit or lock-on look rule |
| HitReact | Reject | Reject | Reject | Reject | Reject | Reject or limited | Reject or limited |

## 3. Preemption Rules

Initial rules:

- `Dodge` may preempt `Idle`.
- `Dodge` may preempt `AttackRecovery` only if recovery cancel is explicitly enabled.
- `LightAttack` does not preempt `Dodge`.
- `SwitchTargetLeft` and `SwitchTargetRight` do not preempt combat actions; they update Locked Target if lock state is valid.
- `LockOn` does not enter Input Buffer.

## 4. Rejection Rules

Reject and log:

- Switch Target when there is no Locked Target.
- LockOn when no valid Candidate Targets exist and unlock is not requested.
- Any combat input during hard `HitReact`.
- Duplicate `Dodge` during `Dodge`.
- Expired buffered input before Consume Input.

## 5. Required Logs

Each priority decision should log:

- Current state
- Received input
- Source device
- Candidate action
- Decision: Execute, Buffer, Reject, Live, Switch, or Toggle
- Reason

