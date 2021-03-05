using System.Collections.Generic;

namespace Framework
{
  public class CustomMonoBehaviourContainer : Singleton<CustomMonoBehaviourContainer>
  {
    public List<CustomMonoBehaviour> CustomMonoBehaviourList { get { return customMonoBehaviourList; } }
    private List<CustomMonoBehaviour> customMonoBehaviourList;

    public void AddMonoBehaviour(CustomMonoBehaviour objectRef)
    {
      if (customMonoBehaviourList == null)
        customMonoBehaviourList = new List<CustomMonoBehaviour>(1);

      customMonoBehaviourList.Add(objectRef);
    }
    public void RemoveMonoBehaviour(CustomMonoBehaviour objectRef)
    {
      if (customMonoBehaviourList != null)
        customMonoBehaviourList.Remove(objectRef);
    }
  }
}

