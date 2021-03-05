using System;

namespace Framework
{
  public interface IState
  {
    void Execute(Action onCompleteHandler);
    void Execute(Action onCompleteHandler, params object[] gameData);
    void Terminate();
  }
}
