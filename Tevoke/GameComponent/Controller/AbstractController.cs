using UnityEngine;

namespace Framework
{
  public abstract class AbstractController
  {
    public abstract void OnEnable();
    public abstract void OnDisable();
    public abstract void Start();
    public abstract void OnDestroy();
    public abstract void Update();
    public abstract void LateUpdate();
    public abstract void FixedUpdate();
    public abstract void OnTriggerEnter2D(Collider2D collider);
    public abstract void OnCollisionEnter2D(Collision2D collision);
  }
}
