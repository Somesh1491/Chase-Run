using UnityEngine;

namespace Framework
{
  /// <summary>
  /// Actor is any object that present in Scene(Simply a gameObject)
  /// </summary>
  public class Actor : AbstractActor
  {
    #region Private Serialize Fields

    #endregion

    #region MonoBehaviour Abstract
    public override void Awake()
    {
    }
    public override void OnEnable()
    {
    }
    public override void OnDisable()
    {

    }
    public override void Start()
    {

    }
    public override void OnDestroy()
    {
    }
    public override void Update()
    {
    }
    public override void LateUpdate()
    {
    }
    public override void FixedUpdate()
    {
    }
    public override void OnTriggerEnter2D(Collider2D collider)
    {
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
    }
    #endregion

    #region Public Methods
    public void SetPosition(Vector3 position)
    {
      this.transform.position = position;
    }
    #endregion
  }
}
