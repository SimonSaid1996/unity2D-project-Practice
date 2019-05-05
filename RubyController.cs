using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //set up health for the character
    public int maxHealth = 5;  
    public float timeInvincible = 2.0f;         // to set up invincible time to avoid continuous damages   
    public float speed = 5.0f;  

    public GameObject projectilePrefab;
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    public int health { get { return currentHealth; }}  //a getter to return the health
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;

    Animator animator;      //to deal with the animation
    Vector2 lookDirection = new Vector2(1,0);//RUBY looking at right direction

    AudioSource audioSource;
    void Start()
    {
       //to make the character follow the physical movement and stop before moving into the boxes
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip){          //make sounds when shooting
        audioSource.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);       //this is the same as the line below, moving objects
        
        //rigidbody2d.MovePosition(position) ;
        //can stop before colliding with other objects
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){        //checking if moving vertically or horizontally is happening
            //changing the looking direction
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        position = position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        if(isInvincible){
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C)){        //shooting the enemy
            Launch();
        }

        if(Input.GetKeyDown(KeyCode.X)){   //talking to the NPC, first is ruby's location, want to talk to center of ruby, not the feet, direction, max distance from the frog, the layer is npc layer
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null){
                NonePlayableCharacter character = hit.collider.GetComponent<NonePlayableCharacter>();
                if(character != null){
                    character.DisplayDialog();
                }
                //Debug.Log("Raycast has hit the object" + hit.collider.gameObject);
            }
        }
    }
    
    public void ChangeHealth(int amount){
        if(amount < 0){           //only happens when getting damages
            if(isInvincible)
                return;
            animator.SetTrigger("Hit");
            //Debug.Log("hit");
            isInvincible = true;
            invincibleTimer= timeInvincible;        //update the invincible status when getting hit
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        //Debug.Log(UIHealthBar.instance);
    }


    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        //importing the projectile object
        //instantiate take the first parameter and set an instance in the second parameter, the third parameter is for rotations
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Lauch");

    }


}
