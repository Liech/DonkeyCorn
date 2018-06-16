using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSide : MonoBehaviour {
  public bool CollideFromLeft { get { return colFromLeft.Count > 0; } }
  public bool CollideFromTop { get { return colFromTop.Count > 0; } }
  public bool CollideFromRight { get { return colFromRight.Count > 0; } }
  public bool CollideFromBottom { get { return colFromBottom.Count > 0; } }

  private HashSet<GameObject> colFromLeft;
  private HashSet<GameObject> colFromTop;
  private HashSet<GameObject> colFromRight;
  private HashSet<GameObject> colFromBottom;

  private GameObject leftObject;
  private GameObject rightObject;
  private GameObject topObject;
  private GameObject bottomObject;


  private BoxCollider2D col;

  public CollisionSide()
  {
    colFromLeft = new HashSet<GameObject>();
    colFromTop = new HashSet<GameObject>();
    colFromRight = new HashSet<GameObject>();
    colFromBottom = new HashSet<GameObject>();
  }

  void OnCollisionEnterLeft(CollisionDelegate c, Collision2D col)
  {
    colFromLeft.Add(col.gameObject);
  }
  void OnCollisionExitLeft(CollisionDelegate c, Collision2D col)
  {
    if (col.gameObject != null) colFromLeft.Remove(col.gameObject);
  }
  void OnCollisionEnterRight(CollisionDelegate c, Collision2D col)
  {
    colFromRight.Add(col.gameObject);
  }
  void OnCollisionExitRight(CollisionDelegate c, Collision2D col)
  {
    if (col.gameObject != null) colFromRight.Remove(col.gameObject);
  }
  void OnCollisionEnterTop(CollisionDelegate c, Collision2D col)
  {
    colFromTop.Add(col.gameObject);
  }
  void OnCollisionExitTop(CollisionDelegate c, Collision2D col)
  {
    if (col.gameObject != null) colFromTop.Remove(col.gameObject);
  }
  void OnCollisionEnterBot(CollisionDelegate c, Collision2D col)
  {
    colFromBottom.Add(col.gameObject);
  }
  void OnCollisionExitBot(CollisionDelegate c, Collision2D col)
  {
    if (col.gameObject != null) colFromBottom.Remove(col.gameObject);
  }


  // Use this for initialization
  void Start()
  {
    col = gameObject.GetComponent<BoxCollider2D>();
    leftObject = new GameObject();
    leftObject.name = "Left";
    leftObject.AddComponent<BoxCollider2D>();
    BoxCollider2D lCol = leftObject.GetComponent<BoxCollider2D>();
    lCol.offset = col.offset + new Vector2(-col.size.x / 2 + 0.1f, 0);
    lCol.size = new Vector2(0.1f, col.size.y * 0.9f);
    lCol.transform.parent = gameObject.transform;
    lCol.transform.localPosition = new Vector3(0, 0, 0);
    leftObject.AddComponent<CollisionDelegate>();
    leftObject.GetComponent<CollisionDelegate>().OnCollisionEnter += OnCollisionEnterLeft;
    leftObject.GetComponent<CollisionDelegate>().OnCollisionExit += OnCollisionExitLeft;


    rightObject = new GameObject();
    rightObject.name = "Right";
    rightObject.AddComponent<BoxCollider2D>();
    BoxCollider2D rCol = rightObject.GetComponent<BoxCollider2D>();
    rCol.offset = col.offset + new Vector2(col.size.x / 2 - 0.1f, 0);
    rCol.size = new Vector2(0.1f, col.size.y * 0.9f);
    rCol.transform.parent = gameObject.transform;
    rCol.transform.localPosition = new Vector3(0, 0, 0);
    rightObject.AddComponent<CollisionDelegate>();
    rightObject.GetComponent<CollisionDelegate>().OnCollisionEnter += OnCollisionEnterRight;
    rightObject.GetComponent<CollisionDelegate>().OnCollisionExit += OnCollisionExitRight;


    topObject = new GameObject();
    topObject.name = "Top";
    topObject.AddComponent<BoxCollider2D>();
    BoxCollider2D tCol = topObject.GetComponent<BoxCollider2D>();
    tCol.offset = col.offset + new Vector2(0, col.size.y / 2 + 0.1f);
    tCol.size = new Vector2(col.size.x * 0.9f, 0.1f);
    tCol.transform.parent = gameObject.transform;
    tCol.transform.localPosition = new Vector3(0, 0, 0);
    topObject.AddComponent<CollisionDelegate>();
    topObject.GetComponent<CollisionDelegate>().OnCollisionEnter += OnCollisionEnterTop;
    topObject.GetComponent<CollisionDelegate>().OnCollisionExit += OnCollisionExitTop;

    bottomObject = new GameObject();
    bottomObject.name = "Bot";
    bottomObject.AddComponent<BoxCollider2D>();
    BoxCollider2D bCol = bottomObject.GetComponent<BoxCollider2D>();
    bCol.offset = col.offset + new Vector2(0, -col.size.y / 2 + 0.1f);
    bCol.size = new Vector2(col.size.x * 0.9f, 0.1f);
    bCol.transform.parent = gameObject.transform;
    bCol.transform.localPosition = new Vector3(0, 0, 0);
    rightObject.AddComponent<CollisionDelegate>();
    rightObject.GetComponent<CollisionDelegate>().OnCollisionEnter += OnCollisionEnterRight;
    rightObject.GetComponent<CollisionDelegate>().OnCollisionExit += OnCollisionExitRight;

    col.enabled = false;
  }

  private void OnDrawGizmosSelected()
  {
    if (CollideFromLeft  ) Gizmos.DrawCube(transform.position + new Vector3(-3, 0, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromRight) Gizmos.DrawCube(transform.position + new Vector3(+3, 0, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromTop ) Gizmos.DrawCube(transform.position + new Vector3( 0, -3, 0), new Vector3(0.1f, 0.1f, 5f));
    if (CollideFromBottom) Gizmos.DrawCube(transform.position + new Vector3( 0, 3, 0), new Vector3(0.1f, 0.1f, 5f));
  }
}
