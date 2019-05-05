using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;
    public AudioClip hitClip;

    void Awake()            //called immediately when the object is created, rigidbody2d initialized before launch
    {
        rigidbody2D = GetComponent<Rigidbody2D>();          //initialize object with a body 
    }

    public void Launch(Vector2 direction, float force){
        rigidbody2D.AddForce(direction * force);
    }

    void OnCollisionEnter2D( Collision2D other){            //for collision detecting
        //Debug.Log("Projectile Collision with "+ other.gameObject);
        EnermyControll e = other.collider.GetComponent<EnermyControll>();
        if(e != null){
            e.Fix();
            e.PlaySound(hitClip);
        }
        Destroy(gameObject);    //fix the enermy if collide
    }

    // Update is called once per frame
    void Update(){
        if(transform.position.magnitude > 1000.0f){     //if the object move more than 100o frames, destroy them
            Destroy(gameObject);
        }
    }
}
