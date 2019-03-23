using UnityEngine;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {
	void Update () {
		
        var main = GetComponent<ParticleSystem>().main;
        main.startSize = transform.lossyScale.magnitude;
    }
}