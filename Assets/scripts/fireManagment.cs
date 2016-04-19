using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fireManagment : MonoBehaviour
{
    private Vector3 newVector3;

    private double firetimeRemaining = 00;
    private double daylightRemaining = 00;
    private double decreaseFire = 30.00;
    private double brightness = 0.20;
    private double darkness = 0.20;
    private int sparkFire = 0;

    void Update()
    {
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("fire light").GetComponent<Light>();
        var fireLightDark = GameObject.Find("fire light Dark").GetComponent<Light>();
        var campdarkness = GameObject.Find("Darken Screen").GetComponent<Image>();
        firetimeRemaining += Time.deltaTime;
        daylightRemaining += Time.deltaTime;
        Color Alpha = campdarkness.color;

        if (fireOBJ.startSize > 0)
        {
            fireLightDark.range = 0;
            if (daylightRemaining > brightness)
            {
                if (Alpha.a > 0)
                {
                    Alpha.a -= 0.01f;
                    campdarkness.color = Alpha;
                    daylightRemaining = 0;
                }
            }

            if (firetimeRemaining > decreaseFire)
            {
                Debug.Log(firetimeRemaining);
                fireOBJ.startSize = fireOBJ.startSize - 0.1f;
                fireLight.range = fireLight.range - 5.5f;
                firetimeRemaining = 0;
            }

            if (firetimeRemaining >= 120)
            {           
                decreaseFire = 30;
                firetimeRemaining = 0;
            }
        }
        else if (fireOBJ.startSize <= 0)
        {
            Debug.Log(Alpha.a);
            if (firetimeRemaining > darkness)
            {   
                if (Alpha.a < 0.7f)
                {
                    Debug.Log("DARKNESS!!");
                    Alpha.a += 0.01f;
                    campdarkness.color = Alpha;
                    decreaseFire = 30;
                    firetimeRemaining = 0;
                    daylightRemaining = 0;
                    if (fireLightDark.range < 100)
                    {
                        fireLightDark.range += 5;
                    }
                }             
            }
        }
    }

    public void increaseFire()
    {
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
        var sparkOBJ = GameObject.Find("Sparks Particle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("fire light").GetComponent<Light>();
        var inventoryComponent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        var items = inventoryComponent.inventory;

        if (items[1].itemQuantity > 0)
        {
            if (fireOBJ.startSize < 1.8)
            {
                sparkOBJ.Emit(1000);
                fireOBJ.startSize = fireOBJ.startSize + 0.2f;
                fireLight.range = fireLight.range + 20f;
                items[1].itemQuantity -= 1;
            }
            if (fireOBJ.startSize >= 1.8) { fireOBJ.startSize = 1.8f; }
            if (fireLight.range >= 100f) { fireLight.range = 100f; }
        }
    }

    public void resetFire()
    {
        var fireOBJ = GameObject.Find("FireParticle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("fire light").GetComponent<Light>();
        fireOBJ.startSize = 0.8f;
        fireLight.range = 60;
    }
}
