using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : Item {
    //All these variables are taken from the player's
    public int damage;
    public int speed;
    public int knock_back;
    private PlayerScript Parent;
    private Animator parentAnimator;
    private List<Collider2D> hitList; //A list that contains the colliders that the weapon have hit during that swing
	// Use this for initialization
	void Start () {
        hitList = new List<Collider2D>();
        Parent = transform.parent.GetComponent<PlayerScript>();
        parentAnimator = Parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base.Player_Swinging")) { //If not swinging
            hitList.Clear(); //Reset the hitlist to prepare for the next swing
        }
	}

    public void ChildOnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Enemy" && !hitList.Find(x => x == col)) { //Find enemy and make sure it haven't already been hit this swing
            Debug.Log("hit");
            PushBackward(col.transform, knock_back);
            hitList.Add(col);
            //Enemy scr = col.GetComponent<Enemy>();
            //scr.moveLocal(-push_back, 0);
        }
    }

    public void ChildOnTriggerExit2D(Collider2D col) { }

    void PushBackward(Transform target, float distance) { 
        //Push back an object backward on local axis
        target.position = target.position - (target.up * distance);
    }
}
