using System.Collections.Generic;

namespace Framework
{
  public class CustomBehaviourContainer : Singleton<CustomBehaviourContainer>
  {
    public List<CustomBehaviour> CustomBehaviourList { get { return customBehaviourList; } }
    private List<CustomBehaviour> customBehaviourList;

    public void AddBehaviour(CustomBehaviour objectRef)
    {
      if (customBehaviourList == null)
        customBehaviourList = new List<CustomBehaviour>(1);

      customBehaviourList.Add(objectRef);
    }
    public void RemoveBehaviour(CustomBehaviour objectRef)
    {
      if (customBehaviourList != null)
        customBehaviourList.Remove(objectRef);
    }
  }
}

