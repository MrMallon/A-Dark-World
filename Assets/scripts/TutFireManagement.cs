using UnityEngine;
using System.Collections;

public class TutFireManagement : MonoBehaviour {

    private int sparkFire = 0;

    public void startFire()
    {
        var fireOBJ = GameObject.Find("Tut FireParticle").GetComponent<ParticleSystem>();
        var sparkOBJ = GameObject.Find("Tut Sparks Particle").GetComponent<ParticleSystem>();
        var fireLight = GameObject.Find("Tut fire light").GetComponent<Light>();
        setSparkAttempt();
        sparkOBJ.Emit(1000);

        if (sparkFire >= 3)
        {
            sparkOBJ.Emit(1000);
            fireOBJ.startSize = 1.8f;
            fireLight.range = 80;
        }
        else
            sparkOBJ.Emit(1000);
    }

    public void setSparkAttempt()
    {
        sparkFire += 1;
    }

}
