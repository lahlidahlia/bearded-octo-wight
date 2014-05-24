using UnityEngine;
using System.Collections;

public class ChildCollision2D : MonoBehaviour {
    /*When the object receives an oncollision message, it will call its parent's respective collision functions with "Child" appended 
     infront of the funtion name. Ex: ChildOnTriggerEnter2D etc...*/
    private Weapon parentScript;
    void Start() {
        parentScript = transform.parent.GetComponent<Weapon>();
    }
    void OnTriggerEnter2D(Collider2D col) {
        parentScript.ChildOnTriggerEnter2D(col);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        parentScript.ChildOnTriggerExit2D(col);
    }
    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    parentScript.ChildOnTriggerEnter2D();
    //}


    //public static bool HasMethod(this object objectToCheck, string methodName)
    //{//Checks if the object has a certain method
    //    var type = objectToCheck.GetType();
    //    return type.GetMethod(methodName) != null;
    //} 
}
