using UnityEngine;
using System.Collections;

public class EnemyRobotBehaviorScript : Enemy
{
    /*Enemy robots have very basic and predictable AI, cuz they're robots after all*/
    //public float speed;   //How fast it's going. Positive speed means it will follow the target, negative means it will run away from the target
    //public Transform target;    //The object it wants to follow (e.g. the player)
    //public ParticleSystem shotAtExplosion; //Explosion when shot at
    //public ParticleSystem hitPlayerExplosion; //Explosion when hit player

    void Awake()
    {
        target = GameObject.Find("Player").transform; //Set the player as the target for move towards
    }

    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        lookAt(transform.position, target.position);
        //moveToward(speed, target);
        moveLocal(speed, 0);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Player") //If collided w/ player
        //{ //If collied w/ a bullet
        //    //GameObject player = col.gameObject; //Renaming for clarity
        //    Destroy(gameObject); //Destroy self
        //}
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.tag == "Bullet")
        //{ //If collied w/ a bullet
        //    GameObject bullet = col.gameObject; //Renaming for clarity
        //    Destroy(bullet); //Destroy the bullet
        //    Destroy(gameObject); //Destroy self

        //    Instantiate(shotAtExplosion, transform.position, Quaternion.identity);
        //}
    }
    void moveToward(float speed, Transform target)
    {
        /*This function moves the object through rigidbody, so put it in the fixed update loop*/
        rigidbody2D.velocity = (target.position - transform.position) //Difference between the two positions.  
                                * (speed * (1 / Vector2.Distance(target.position, transform.position))); //Scale the speed down depending on how far the two objects are. (Else the object would be come slower the closer the 2 objects are)
    }

    //void moveLocal(float verSpeed, float horSpeed)
    //{
    //    /*Move on the local axis. In other words, up would mean toward wherever the object is facing*/
    //    rigidbody2D.velocity = (transform.up * verSpeed) + (transform.right * horSpeed);
    //}

    //void lookAt(Vector2 playerPos, Vector2 target)
    //{
    //    /*Makes the player rotate toward the given point*/

    //    //Vector2.angle here is used to get the angle between the (0,1) vector(a vertical line) and the vector between the player and the mouse
    //    if (transform.position.x < target.x)
    //    { //If the mouse is on the right side of the player

    //        //Make the angle negative (e.g. if the mouse position relative to the player is (1,1), vector2.angle((0,1),(1,1)) would return 45, which is facing the left side.
    //        //If we make that number negative, it would face the right side.
    //        transform.rotation = Quaternion.Euler(0, 0, -Vector2.Angle(new Vector2(0, 1), target - playerPos));
    //    }
    //    if (transform.position.x > target.x)
    //    { //If the mouse is on the left side of the player
    //        transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(new Vector2(0, 1), target - playerPos));
    //    }
    //}
}
