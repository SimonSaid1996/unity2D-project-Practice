using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyControll : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 4.0f;
    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;                //timer for changing the moving direction
    int direction = 1;          //we uses 1 and -1 to decide the enermy's moving position
    Animator animator;
    bool broken = true;
    public AudioClip collectedClip;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();            //to interact with the animator
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }

    public void Fix(){      //to remove the enermy object from the scene
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();         //stop simply stop a object creating new effects
    }

    // Update is called once per frame
    void Update(){    
        if(!broken){
            return;
        }

        timer -= Time.deltaTime;
        if(timer <0){
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2D.position;

        if(vertical){       //to decide moving directions
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);  //direction is used to indicate move direction
        }
        else{
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other){
        RubyController player = other.gameObject.GetComponent<RubyController > ();
        if(player != null){
            player.ChangeHealth(-1); 
            player.PlaySound(collectedClip);    //ruby getting hurt   
        }
    }

}
