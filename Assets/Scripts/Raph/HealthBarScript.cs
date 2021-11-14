using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    public Image damageShade;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        if(slider.value == 1)
        {
            StartCoroutine(DamageShade());
        }
    }

    IEnumerator DamageShade()
    {
        
        while(true){
            float t = Time.time;
            byte alpha = (byte)( 50 * (0.5f + 0.5f * Mathf.Sin(10*t)) );
            damageShade.color = new Color32(150,0,0,alpha); //Une fonction qui varie entre 0 et 1
            yield return new WaitForSeconds(0.1f);
        }
    }
}
