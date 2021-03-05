namespace Framework
{
  public class UIToggleButtonGroupController : UIElementController
  {
    private UIToggleButtonGroup uiToggleButtonGroup;
    private UIToggleButton currentSelectedButton;
    public UIToggleButtonGroupController(UIElement uiElement) : base(uiElement)
    {
      uiToggleButtonGroup = uiElement as UIToggleButtonGroup;
    }

    public override void Start()
    {
      base.Start();
    }

    public override void Update()
    {
      base.Update();
    }

    public void OnValueChange(UIToggleButton uiToggleButton)
    {
      switch (uiToggleButtonGroup.ToggleGroupType)
      {
        case ToggleGroupType.AtMostOneSelected:

          if (currentSelectedButton != null)
          {
            if (currentSelectedButton != uiToggleButton)
            {
              currentSelectedButton.IsOn = false;
              currentSelectedButton = uiToggleButton;
            }
          }

          if (uiToggleButton.IsOn)
            currentSelectedButton = uiToggleButton;
          else
            currentSelectedButton = null;

          break;
      }
    }
  }
}
