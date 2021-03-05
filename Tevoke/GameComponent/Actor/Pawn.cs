using UnityEngine;

namespace Framework
{
  /// <summary>
  /// Pawn can be object that can be control by player or AI.
  /// Pawn must have Controller Component to handle the behaviour or Property of the object
  /// </summary>
  public class Pawn : Actor
  {
    public PawnController controller;

    public override void Awake()
    {

    }
    public override void OnEnable()
    {
      controller?.OnEnable();
    }
    public override void OnDisable()
    {
      controller?.OnDisable();
    }
    public override void Start()
    {
      controller?.Start();
    }
    public override void OnDestroy()
    {
      controller?.OnDestroy();
    }
    public override void Update()
    {
      controller?.Update();
    }
    public override void LateUpdate()
    {
      controller?.LateUpdate();
    }
    public override void FixedUpdate()
    {
      controller?.FixedUpdate();
    }
    public override void OnTriggerEnter2D(Collider2D collider)
    {
      controller?.OnTriggerEnter2D(collider);
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
      controller?.OnCollisionEnter2D(collision);
    }
  }
}
