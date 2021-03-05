namespace Framework
{
  public class CustomMonoBehaviour : CustomBehaviour
  {
    private bool isEnable = false;
    public CustomMonoBehaviour()
    {
      if (!isEnable)
      {
        CustomMonoInterfaceContainer.Instance.AddInterface(this);
        CustomBehaviourContainer.Instance.AddBehaviour(this);
        CustomMonoBehaviourContainer.Instance.AddMonoBehaviour(this);
        isEnable = true;
      }
    }
    public virtual void OnEnable()
    {
      if (!isEnable)
      {
        CustomMonoInterfaceContainer.Instance.AddInterface(this);
        CustomBehaviourContainer.Instance.AddBehaviour(this);
        isEnable = true;
      }
    }
    public virtual void OnDisable()
    {
      if (isEnable)
      {
        CustomMonoInterfaceContainer.Instance.RemoveInterface(this);
        CustomBehaviourContainer.Instance.RemoveBehaviour(this);
        isEnable = false;
      }
    }
    public virtual void OnDestroy()
    {
      CustomMonoBehaviourContainer.Instance.RemoveMonoBehaviour(this);
      isEnable = false;
    }
  }
}
