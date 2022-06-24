using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

public class RunerControls
{
    public void Disable(Player player)
    {
        if (player.TryGetComponent(out MovementSystem movementSystem))
            movementSystem.enabled = false;

        if (player.TryGetComponent(out MouseInput input))
            input.enabled = false;

        if (player.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = false;

        player.GetComponentInChildren<Rotator>().enabled = false;

        player.GetComponentInChildren<InjectionBarAnimator>().enabled = true;
    }

    public void Enable(Player player)
    {
        if (player.TryGetComponent(out MovementSystem movementSystem))
            movementSystem.enabled = true;

        if (player.TryGetComponent(out MouseInput input))
            input.enabled = true;

        if (player.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;

        player.GetComponentInChildren<Rotator>().enabled = true;

        player.GetComponentInChildren<InjectionBarAnimator>().enabled = true;
    }
}
