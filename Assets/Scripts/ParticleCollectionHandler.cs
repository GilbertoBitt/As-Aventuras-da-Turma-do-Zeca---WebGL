using UnityEngine;

public class ParticleCollectionHandler : OverridableMonoBehaviour {

    public ParticleSystem[] Particles = new ParticleSystem[0];
    
    public void PauseParticles() {
        int tempCount = Particles.Length;
        for (int i = 0; i < tempCount; i++) {
            if (!Particles[i].isPaused) {
                Particles[i].Pause();
            }
        }
    }

    public void PlayParticles() {
        int tempCount = Particles.Length;
        for (int i = 0; i < tempCount; i++) {
            if(!Particles[i].isPlaying) {
                Particles[i].Play();
            }
        }
    }

    public void StopParticles() {
        int tempCount = Particles.Length;
        for (int i = 0; i < tempCount; i++) {
            if (!Particles[i].isStopped) {
                Particles[i].Stop();
            }
        }
    }
}
