using Framework;

public class UIToggleButtonController : UIButtonController
{
  private readonly UIToggleButton uiToggleButton;
  private bool prevSwitchValue;
  public UIToggleButtonController(UIButton uiButton) : base(uiButton)
  {
    uiToggleButton = uiButton as UIToggleButton;
  }

  public override void Start()
  {
    base.Start();
    prevSwitchValue = uiToggleButton.IsOn;
    OnValueChange();
  }
  public override void Update()
  {
    base.Update();
    if (prevSwitchValue != uiToggleButton.IsOn)
    {
      OnValueChange();
      prevSwitchValue = uiToggleButton.IsOn;
    }
  }

  private void OnValueChange()
  {
    if (uiToggleButton.IsOn)
      uiToggleButton.SwitchOn();

    else
      uiToggleButton.SwitchOff();
  }
}
