using UnityEngine.Events;

namespace Framework
{
  public class UIButtonController : UIElementController
  {
    private readonly UIButton uiButton;
    private UnityAction action;
    private bool prevIsClickableValue;
    public UIButtonController(UIElement uiElement) : base(uiElement)
    {
      uiButton = uiElement as UIButton;
      action = new UnityAction(uiButton.OnClick);

    }

    public override void Start()
    {
      base.Start();
      prevIsClickableValue = uiButton.IsClickable;
      OnValueChange();
    }

    public override void Update()
    {
      base.Update();

      if (prevIsClickableValue != uiButton.IsClickable)
      {
        OnValueChange();
        prevIsClickableValue = uiButton.IsClickable;
      }
    }

    private void OnValueChange()
    {
      if (uiButton.IsClickable)
      {
        uiButton.Button.onClick.AddListener(action);
        uiButton.Button.interactable = true;
      }

      else
      {
        uiButton.Button.onClick.RemoveListener(action);
        uiButton.Button.interactable = false;
      }
    }
  }
}
