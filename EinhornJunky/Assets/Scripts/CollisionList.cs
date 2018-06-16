using UnityEngine;
using System.Collections;
using System.Collections.Generic;    // Don't forget to add this if using a List.

public class CollisionList : MonoBehaviour
{

  // Declare and initialize a new List of GameObjects called currentCollisions.
  public List<GameObject> currentCollisions = new List<GameObject>();

  void OnCollisionEnter2D(Collision2D col)
  {

    // Add the GameObject collided with to the list.
    currentCollisions.Add(col.gameObject);

    //// Print the entire list to the console.
    //foreach (GameObject gObject in currentCollisions)
    //{
    //  print(gObject.name);
    //}
  }

  void OnCollisionExit2D(Collision2D col)
  {

    // Remove the GameObject collided with from the list.
    currentCollisions.Remove(col.gameObject);

    //// Print the entire list to the console.
    //foreach (GameObject gObject in currentCollisions)
    //{
    //  print(gObject.name);
    //}
  }
}