public class SumoControls
{
    public void Enable(Player player)
    {
        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = true;
    }

    public void EnableJouystick(JoystickCanvas joystickCanvas)
    {
        joystickCanvas.gameObject.SetActive(true);
    }

    public void Disable(Player player)
    {
        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = false;
    }
}
