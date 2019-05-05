using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    //image class is inside of the UI folder

public class UIHealthBar : MonoBehaviour
{
    //private static UIHealthBar instance = new UIHealthBar();
    //private UIHealthBar(){} 

    public static UIHealthBar instance { get; private set;}

    public Image mask;
    float originalSize;

    void Awake(){
        instance = this;
    }

    public void SetValue(float value){
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    void Start(){
        originalSize = mask.rectTransform.rect.width;
    }
}
