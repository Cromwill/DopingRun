public class SumoControls
{
    public void Enable(Player player, JoystickCanvas joystickCanvas)
    {
        joystickCanvas.gameObject.SetActive(true);

        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = true;
    }

    public void Disable(Player player, JoystickCanvas joystickCanvas)
    {
        joystickCanvas.gameObject.SetActive(false);

        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = false;
    }
}
