# Input Buffer Rules

Last updated: 2026-05-01

## 1. First Implementation Shape

Use a single-slot Input Buffer for the first runnable version.

Reason:

- The first phase only needs basic LightAttack and Dodge.
- Single-slot behavior is easier to log, test, and debug.
- It avoids unclear ordering rules before animation windows are proven.

## 2. Buffered Input Data

Each buffered input should record:

- Input name: `LightAttack` or `Dodge`
- Input time: world time seconds
- Source device: `MouseKeyboard` or `Gamepad`
- Character state when received
- Expiration time
- Replace reason if overwritten

## 3. Inputs That Can Enter Buffer

| Input | Buffer Rule | Notes |
| --- | --- | --- |
| LightAttack | Can buffer | Mainly during `AttackStartup` and `AttackRecovery` if a combo may become valid. |
| Dodge | Can buffer | Can buffer during short locked states if Dodge should fire at the next legal moment. |

## 4. Inputs That Do Not Enter Buffer

| Input | Rule | Reason |
| --- | --- | --- |
| Move | Never buffer | Continuous input should be read live. |
| CameraLook | Never buffer | Continuous input should be read live. |
| LockOn | Execute or reject immediately | Lock state changes should not happen late from stale input. |
| SwitchTargetLeft | Execute or reject immediately | Late target switching feels unreliable. |
| SwitchTargetRight | Execute or reject immediately | Late target switching feels unreliable. |

## 5. Lifetime

Initial suggested values:

- `LightAttack`: 0.30 seconds
- `Dodge`: 0.20 seconds

Expired inputs are cleared before any Consume Input attempt.

## 6. Replacement Rule

The first implementation uses last-input-wins for bufferable inputs:

- A new bufferable input replaces the existing buffered input.
- The replacement is logged with old input, new input, and reason.

## 7. Consume Input Rule

Consume Input requires:

- Buffered input is not expired.
- Current state allows that action.
- Input Priority allows that action.
- Required animation or gameplay window is open.

If consumed:

- Execute the action.
- Clear the buffer.
- Log the consumed input and state.

If rejected:

- Keep the input only if it is still within lifetime and the rejection reason is temporary.
- Clear the input if the state makes it impossible or unsafe, such as death, hard hit react, or action cancel.

## 8. Forced Clear Conditions

Clear Input Buffer when:

- Character dies.
- Character enters non-bufferable HitReact.
- Player unlocks or target state invalidates an action that requires a target.
- Current action is forcibly interrupted.
- Buffered input expires.
- Possession changes or controller is removed.

## 9. Required Logs

Log these events:

- `BufferSet`
- `BufferReplace`
- `BufferConsume`
- `BufferExpire`
- `BufferReject`
- `BufferClear`

