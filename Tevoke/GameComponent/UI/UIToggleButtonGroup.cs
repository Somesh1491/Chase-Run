using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
  public class UIToggleButtonGroup : UIElement
  {
    public ToggleGroupType ToggleGroupType { get { return toggleGroupType; } }
    public List<UIToggleButton> UIToggleButtons { get { return uiToggleButtons; } }

    [SerializeField] private ToggleGroupType toggleGroupType;
    [SerializeField] private List<UIToggleButton> uiToggleButtons;

    public override void Awake()
    {
      base.Awake();
      controller = new UIToggleButtonGroupController(this);
    }
  }

  public enum ToggleGroupType
  {
    AtMostOneSelected,
    ExactOneSelected,
    AtLeastOneSelected
  }
}
