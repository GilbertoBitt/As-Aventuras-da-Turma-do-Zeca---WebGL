using UnityEngine;

namespace DigitalRuby.RainMaker
{
    public class BaseRainScript : MonoBehaviour
    {
        [Tooltip("Camera the rain should hover over, defaults to main camera")]
        public Camera Camera;

        //[Tooltip("Light rain looping clip")]
       // public AudioClip RainSoundLight;

       // [Tooltip("Medium rain looping clip")]
       // public AudioClip RainSoundMedium;

       // [Tooltip("Heavy rain looping clip")]
       // public AudioClip RainSoundHeavy;

        [Tooltip("Intensity of rain (0-1)")]
        [Range(0.0f, 1.0f)]
        public float RainIntensity;

        [Tooltip("Rain particle system")]
        public ParticleSystem RainFallParticleSystem;

        [Tooltip("Particles system for when rain hits something")]
        public ParticleSystem RainExplosionParticleSystem;

        [Tooltip("Particle system to use for rain mist")]
        public ParticleSystem RainMistParticleSystem;

        [Tooltip("The threshold for intensity (0 - 1) at which mist starts to appear")]
        [Range(0.0f, 1.0f)]
        public float RainMistThreshold = 0.5f;

       // [Tooltip("Wind looping clip")]
       // public AudioClip WindSound;

      //  [Tooltip("Wind sound volume modifier, use this to lower your sound if it's too loud.")]
      //  public float WindSoundVolumeModifier = 0.5f;

     //   [Tooltip("Wind zone that will affect and follow the rain")]
       // public WindZone WindZone;

        [Tooltip("X = minimum wind speed. Y = maximum wind speed. Z = sound multiplier. Wind speed is divided by Z to get sound multiplier value. Set Z to lower than Y to increase wind sound volume, or higher to decrease wind sound volume.")]
        public Vector3 WindSpeedRange = new Vector3(50.0f, 500.0f, 500.0f);

        [Tooltip("How often the wind speed and direction changes (minimum and maximum change interval in seconds)")]
        public Vector2 WindChangeInterval = new Vector2(5.0f, 30.0f);

        [Tooltip("Whether wind should be enabled.")]
   //     public bool EnableWind = true;

        //protected LoopingAudioSource audioSourceRainLight;
      //  protected LoopingAudioSource audioSourceRainMedium;
      //  protected LoopingAudioSource audioSourceRainHeavy;
      //  protected LoopingAudioSource audioSourceRainCurrent;
      //  protected LoopingAudioSource audioSourceWind;
      //  protected Material rainMaterial;
     //   protected Material rainExplosionMaterial;
     //   protected Material rainMistMaterial;

        private float lastRainIntensityValue = -1.0f;
      //  private float nextWindTime;

		public GameConfig gameconfig;





       

        private void CheckForRainChange()
        {
            if (lastRainIntensityValue != RainIntensity)
            {
                lastRainIntensityValue = RainIntensity;
                if (RainIntensity <= 0.01f)
                {
                   
                    if (RainFallParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                        e.enabled = false;
                        RainFallParticleSystem.Stop();
                    }
                    if (RainMistParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainMistParticleSystem.emission;
                        e.enabled = false;
                        RainMistParticleSystem.Stop();
                    }
                }
                else
                {
                   // LoopingAudioSource newSource;
                    if (RainIntensity >= 0.67f)
                    {
                       // newSource = audioSourceRainHeavy;
                    }
                    else if (RainIntensity >= 0.33f)
                    {
                      //  newSource = audioSourceRainMedium;
                    }
                    else
                    {
                      //  newSource = audioSourceRainLight;
                    }
                    
                    if (RainFallParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                        e.enabled = RainFallParticleSystem.GetComponent<Renderer>().enabled = true;
                        if (!RainFallParticleSystem.isPlaying)
                        {
                            RainFallParticleSystem.Play();
                        }
#pragma warning disable CS0618 // Type or member is obsolete
                        ParticleSystem.MinMaxCurve rate = e.rate;
#pragma warning restore CS0618 // Type or member is obsolete
                        rate.mode = ParticleSystemCurveMode.Constant;
                        //  rate.constantMin = rate.constantMax = RainFallEmissionRate();
#pragma warning disable CS0618 // Type or member is obsolete
                        e.rate = rate;
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                    if (RainMistParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainMistParticleSystem.emission;
                        e.enabled = RainMistParticleSystem.GetComponent<Renderer>().enabled = true;
                        if (!RainMistParticleSystem.isPlaying)
                        {
                            RainMistParticleSystem.Play();
                        }
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                        float emissionRate;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
                        if (RainIntensity < RainMistThreshold)
                        {
                            emissionRate = 0.0f;
                        }
                        else
                        {
                            // must have RainMistThreshold or higher rain intensity to start seeing mist
                          //  emissionRate = MistEmissionRate();
                        }
#pragma warning disable CS0618 // Type or member is obsolete
                        ParticleSystem.MinMaxCurve rate = e.rate;
#pragma warning restore CS0618 // Type or member is obsolete
                        rate.mode = ParticleSystemCurveMode.Constant;
                        //                        rate.constantMin = rate.constantMax = emissionRate;
#pragma warning disable CS0618 // Type or member is obsolete
                        e.rate = rate;
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                }
            }
        }

        protected virtual void Start()
        {

#if DEBUG

            if (RainFallParticleSystem == null)
            {
               // Debug.LogError("Rain fall particle system must be set to a particle system");
                return;
            }

#endif

            if (Camera == null)
            {
                Camera = Camera.main;
            }


		
          
        }

        protected virtual void Update()
        {

#if DEBUG

            if (RainFallParticleSystem == null)
            {
            //    Debug.LogError("Rain fall particle system must be set to a particle system");
                return;
            }

#endif

            CheckForRainChange();
//            UpdateWind();


        }

      
    }

    /// <summary>
    /// Provides an easy wrapper to looping audio sources with nice transitions for volume when starting and stopping
    /// </summary>
 

}