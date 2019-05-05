using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDamagable : MonoBehaviour{
    public AudioClip hitClip;
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Object that entered the trigger :"+ other);
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null){
            controller.ChangeHealth(-1);
            controller.PlaySound(hitClip);
        }
    }
}
