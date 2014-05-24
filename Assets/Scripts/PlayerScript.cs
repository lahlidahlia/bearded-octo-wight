using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public LayerMask lightLayerMask;
    Vector2 playerPos;

    //Player's attributes
    //public int damage; //How much damage the player deals
    public float speed; //How fast the player moves
    public float aspd; //How fast the player attacks

    //Lighting
    public int rayCastAmnt; //Amount of rays being cast to check for visibility
    public float shadowLength;
    public float shadowOffset;
    public float visionRange;

    private SpriteRenderer spriteRenderer;
    

    private enum Direction { W, E, N, S };
    private Direction direction; //direction that the player is facing

    private Animator animator;
    private bool attack = false;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

   
    void Update() {
        //Casting rays and creating dynamic shadows
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y); //Shortening mouse position and changing it into a vector2 instead of a v3
        playerPos = new Vector2(transform.position.x, transform.position.y); //Changing player position into a vector2 instead of v3
        

        Vector2 prevPoint1 = new Vector2(-9999, -9999); //Previous points are used to keep track of previous ray hits in order to draw quads
        Vector2 prevPoint2 = new Vector2(-9999, -9999); //Initialized as -9999 because you can't initialize it as null
        Vector2 startPoint1 = new Vector2(-9999, -9999); //The first ray hit position. Used to connect the last ray hit and the first ray hit together
        Vector2 startPoint2 = new Vector2(-9999, -9999);
        for (int i = 0; i < rayCastAmnt; i++) //Iterate through all the rays
        {
            float angle = (float)i / (float)rayCastAmnt * 360; //Figure out what angle the ray is in degree.
            float cos = Mathf.Cos(angle * Mathf.Deg2Rad); //Figure out the cosine of the angle.  Also turn the angle into radians because mathf.sin/cos only take radians
            float sin = Mathf.Sin(angle * Mathf.Deg2Rad); //Figure out the sine of the angle.
            Vector2 direction = new Vector2(cos, sin); //Figure out the direction of the raycast (in vector form, e.g. 30deg would be (cos(30),sin(30))).
            RaycastHit2D rayhit = Physics2D.Raycast(transform.position, direction, visionRange, lightLayerMask); //Cast the ray and store the hit info
            Vector2 hitPoint;
            if (rayhit.transform == null)
            { //check if the ray hits nothing
                hitPoint = new Vector2(cos * visionRange, sin * visionRange) + playerPos;
            }
            else
            { //If the ray hits something
                hitPoint = rayhit.point;
            }

            //Renderer ren = obj.transform.GetComponent<Renderer>();
            //ren.enabled = true;
            Vector2 curPoint1 = hitPoint + new Vector2(cos * shadowOffset, sin * shadowOffset); //Ray hit point + shadow offset
            Vector2 curPoint2 = hitPoint + new Vector2(cos * shadowLength, sin * shadowLength); //Ray hit point + shadow length

            if (prevPoint1 == new Vector2(-9999, -9999) && prevPoint2 == new Vector2(-9999, -9999))
            { //If "uninitialized"
                prevPoint1 = curPoint1;
                prevPoint2 = curPoint2;
                startPoint1 = curPoint1; //Set first ray hit point, so we can connect the first and last together later
                startPoint2 = curPoint2;
                continue;
            }

            //Draw the shadows
            DrawScript.drawList.Add(prevPoint1);
            DrawScript.drawList.Add(curPoint1);
            DrawScript.drawList.Add(curPoint2);
            DrawScript.drawList.Add(prevPoint2);

            prevPoint1 = curPoint1;
            prevPoint2 = curPoint2;

            if (i == rayCastAmnt - 1)
            { //If the last ray is casted, draw a shadow connecting the first and last shadow together
                DrawScript.drawList.Add(startPoint1);
                DrawScript.drawList.Add(curPoint1);
                DrawScript.drawList.Add(curPoint2);
                DrawScript.drawList.Add(startPoint2);
            }

        }


        //Animator:
        animator.speed = 1/aspd;
        //If no animations are currently playing
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Base.Player_Swinging"))
        {
            //Player look at mouse
            lookAt(playerPos, mousePos);

            //Player attack
            if (Input.GetButton("Attack"))
            {
                animator.SetTrigger("Attack");
            }
        }
        
        
    }
	

    ///////////////////////////////////////

	// Update is called once every 0.2 seconds (not every frame)
	void FixedUpdate () {
        //Get keyboard input for the movement and calculate the player's velocity
        float horMove = Input.GetAxisRaw("Horizontal") * speed; 
        float verMove = Input.GetAxisRaw("Vertical") * speed;
        rigidbody2D.velocity = new Vector2(horMove, verMove); //Set the velocity

        if (horMove > 0)
        {
            direction = Direction.E;
        }
        if (horMove < 0)
        {
            direction = Direction.W;
        }
        if (verMove > 0)
        {
            direction = Direction.N;
        }
        if (verMove < 0)
        {
            direction = Direction.S;
        }
    }

    void lookAt(Vector2 playerPos, Vector2 target)
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

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Hit!");
    }
}    