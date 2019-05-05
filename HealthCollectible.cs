using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour{
    public ParticleSystem lightEffect;
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other){
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null){            //can't touch the health collectables unless injured
            if(controller.health < controller.maxHealth){
                controller.ChangeHealth(1);   //destroy the health providing object
		        Destroy(gameObject);
                lightEffect.Stop();
                controller.PlaySound(collectedClip);
            }    
        }
    }

    


}
