using UnityEngine;

namespace Framework
{
  /// <summary>
  /// Controller of a associated Pawn
  /// </summary>
  public class PawnController : AbstractController
  {
    public Pawn pawn;
    public PawnController(Pawn pawn)
    {
      this.pawn = pawn;
    }

    #region AbstractController Methods
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
  }
}
