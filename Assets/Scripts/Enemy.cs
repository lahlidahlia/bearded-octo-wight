using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    /*Handles enemies' attributes like health and damage. Also handles behaviors like deaths*/
    public float speed;   //How fast it's going. Positive speed means it will follow the target, negative means it will run away from the target
    public Transform target;    //The object it wants to follow (e.g. the player)
    //public ParticleSystem shotAtExplosion; //Explosion when shot at
    //public ParticleSystem hitPlayerExplosion; //Explosion when hit player
    //void Awake()
    //{
    //    target = GameObject.Find("Player").transform; //Set the player as the target for move towards
    //}

    public virtual void lookAt(Vector2 playerPos, Vector2 target)
    {
        /*Makes the player rotate toward the given point*/

        //Vector2.angle here is used to get the angle between the (0,1) vector(a vertical line) and the vector between the player and the mouse
        if (transform.position.x < target.x)
        { //If the mouse is on the right side of the player

            //Make the angle negative (e.g. if the mouse position relative to the player is (1,1), vector2.angle((0,1),(1,1)) would return 45, which is facing the left side.
            //If we make that number negative, it would face the right side.
            transform.rotation = Quaternion.Euler(0, 0, -Vector2.Angle(new Vector2(0, 1), target - playerPos));
        }
        if (transform.position.x > target.x)
        { //If the mouse is on the left side of the player
            transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(new Vector2(0, 1), target - playerPos));
        }
    }

    public virtual void moveLocal(float verSpeed, float horSpeed)
    {
        /*Move on the local axis. In other words, up would mean toward wherever the object is facing*/
        rigidbody2D.velocity = (transform.up * verSpeed) + (transform.right * horSpeed);
    }
}
