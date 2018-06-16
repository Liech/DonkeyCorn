using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSide : MonoBehaviour {
  public bool CollideFromLeft   {get {return colFromLeft.Count > 0;}}
  public bool CollideFromTop    {get {return colFromTop.Count > 0;}}
  public bool CollideFromRight  {get {return colFromRight.Count> 0;}}
  public bool CollideFromBottom {get {return colFromBottom.Count > 0;} }

  private HashSet<GameObject> colFromLeft;
  private HashSet<GameObject> colFromTop;
  private HashSet<GameObject> colFromRight;
  private HashSet<GameObject> colFromBottom;

  private Collider2D col;

  public CollisionSide()
  {
    colFromLeft = new HashSet<GameObject>();
    colFromTop = new HashSet<GameObject>();
    colFromRight = new HashSet<GameObject>();
    colFromBottom = new HashSet<GameObject>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    Vector3 center = col.bounds.center;

    for (int i = 0; i < collision.contacts.Length; i++)
    {
      Vector3 contactPoint = collision.contacts[i].point;

      //if (contactPoint.y >= col.bounds.max.y)
      if (collision.contacts[i].normal.y > 0 && contactPoint.y >= col.bounds.max.y)
      {
        if (!colFromTop.Contains(collision.gameObject)) colFromTop.Add(collision.gameObject);
      }
      //if (contactPoint.y <= col.bounds.min.y)
      if (collision.contacts[i].normal.y < 0 && contactPoint.y <= col.bounds.min.y)
      {
        colFromBottom.Add(collision.gameObject);
      }
      //if (contactPoint.x >= col.bounds.max.x)
      if (collision.contacts[i].normal.x < 0 && contactPoint.x >= col.bounds.max.x)
      {
        colFromRight.Add(collision.gameObject);
      }
      //if (contactPoint.x <= col.bounds.min.x)
      if (collision.contacts[i].normal.x > 0 && contactPoint.x <= col.bounds.min.x)
      {
        colFromLeft.Add(collision.gameObject);
      }
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {

      if (colFromTop.Contains(collision.gameObject)) colFromTop.Remove(collision.gameObject);

      if (colFromBottom.Contains(collision.gameObject)) colFromBottom.Remove(collision.gameObject);

      if (colFromRight.Contains(collision.gameObject)) colFromRight.Remove(collision.gameObject);

      if (colFromLeft.Contains(collision.gameObject)) colFromLeft.Remove(collision.gameObject);
  }

  // Use this for initialization
  void Start () {
    col = gameObject.GetComponent<BoxCollider2D>();

  }

  // Update is called once per frame
  void Update () {
  
  }

  private void OnDrawGizmosSelected()
  {
    if (CollideFromLeft  ) Gizmos.DrawCube(transform.position + new Vector3(-3, 0, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromTop   ) Gizmos.DrawCube(transform.position + new Vector3(+3, 0, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromRight ) Gizmos.DrawCube(transform.position + new Vector3( 0, 3, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromBottom) Gizmos.DrawCube(transform.position + new Vector3( 0, -3, 0), new Vector3(0.1f, 0.1f, 5f));
  }
}
