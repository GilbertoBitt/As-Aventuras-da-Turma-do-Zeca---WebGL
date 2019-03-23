using System.Collections.Generic;
using UnityEngine;
using MEC;

public class SQParticle : MonoBehaviour {

    public ParticleSystem ps;
    public MarkSongQuiz msmgr;

    // Use this for initialization
    void Start() {
        this.ps = this.GetComponent<ParticleSystem>();
    }

    public void ExecuteParticle()
	{
        ps.Play();
        Timing.RunCoroutine(CheckIfAlive());
	}

    IEnumerator<float> CheckIfAlive() {
        while (true && ps != null) {
            yield return Timing.WaitForSeconds(0.5f);
            if (!ps.IsAlive(true)) {
                ps.Stop();
                msmgr.startParticles.Enqueue(this);
                break;
            }
        }
    }
}
