using UnityEngine;

namespace DigitalRuby.RainMaker
{
    public class RainScript2D : BaseRainScript
    {
        private static readonly Color32 explosionColor = new Color32(255, 255, 255, 255);

        private float cameraMultiplier = 1.0f;
        private Bounds visibleBounds;
        private float yOffset;
        private float visibleWorldWidth;
        private float initialEmissionRain;
        private float initialStartSpeedRain;
        private float initialStartSizeRain;
        private float initialStartSpeedMist;
        private float initialStartSizeMist;
        private float initialStartSpeedExplosion;
        private float initialStartSizeExplosion;
        private readonly ParticleSystem.Particle[] particles = new ParticleSystem.Particle[2048];

        [Tooltip("The starting y offset for rain and mist. This will be offset as a percentage of visible height from the top of the visible world.")]
        public float RainHeightMultiplier = 0.15f;

        [Tooltip("The total width of the rain and mist as a percentage of visible width")]
        public float RainWidthMultiplier = 1.5f;

//        [Tooltip("Collision mask for the rain particles")]
       // public LayerMask CollisionMask = -1;

//        [Tooltip("Lifetime to assign to rain particles that have collided. 0 for instant death. This can allow the rain to penetrate a little bit beyond the collision point.")]
     //   [Range(0.0f, 0.5f)]
       // public float CollisionLifeTimeRain = 0.02f;

     //   [Tooltip("Multiply the velocity of any mist colliding by this amount")]
    //    [Range(0.0f, 0.99f)]
    //    public float RainMistCollisionMultiplier = 0.75f;

        private void EmitExplosion(ref Vector3 pos)
        {
            int count = UnityEngine.Random.Range(2, 5);
            while (count != 0)
            {
                float xVelocity = UnityEngine.Random.Range(-2.0f, 2.0f) * cameraMultiplier;
                float yVelocity = UnityEngine.Random.Range(1.0f, 3.0f) * cameraMultiplier;
                float lifetime = UnityEngine.Random.Range(0.1f, 0.2f);
                float size = UnityEngine.Random.Range(0.05f, 0.1f) * cameraMultiplier;
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                param.position = pos;
                param.velocity = new Vector3(xVelocity, yVelocity, 0.0f);
                param.startLifetime = lifetime;
                param.startSize = size;
                param.startColor = explosionColor;
                RainExplosionParticleSystem.Emit(param, 1);
                count--;
            }
        }

        private void TransformParticleSystem(ParticleSystem p, float initialStartSpeed, float initialStartSize)
        {
            if (p == null)
            {
                return;
            }

          // p.transform.position = new Vector3(Camera.transform.position.x, visibleBounds.max.y + yOffset, p.transform.position.z);
            p.transform.localScale = new Vector3(visibleWorldWidth * RainWidthMultiplier, 1.0f, 1.0f);
#pragma warning disable CS0618 // Type or member is obsolete
            p.startSpeed = initialStartSpeed * cameraMultiplier;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            p.startSize = initialStartSize * cameraMultiplier;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void CheckForCollisionsRainParticles()
        {
            int count = 0;
            bool changes = false;

          
            if (RainExplosionParticleSystem != null)
            {
                if (count == 0)
                {
                    count = RainFallParticleSystem.GetParticles(particles);
                }
                for (int i = 0; i < count; i++)
                {
                    if (particles[i].remainingLifetime < 0.24f)
                    {
                        Vector3 pos = particles[i].position + RainFallParticleSystem.transform.position;
                        EmitExplosion(ref pos);
                    }
                }
            }
            if (changes)
            {
                RainFallParticleSystem.SetParticles(particles, count);
            }
        }



        protected override void Start()
        {
            base.Start();

#pragma warning disable CS0618 // Type or member is obsolete
            initialEmissionRain = RainFallParticleSystem.emission.rate.constantMax;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            initialStartSpeedRain = RainFallParticleSystem.startSpeed;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            initialStartSizeRain = RainFallParticleSystem.startSize;
#pragma warning restore CS0618 // Type or member is obsolete

            if (RainMistParticleSystem != null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                initialStartSpeedMist = RainMistParticleSystem.startSpeed;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                initialStartSizeMist = RainMistParticleSystem.startSize;
#pragma warning restore CS0618 // Type or member is obsolete
            }

            if (RainExplosionParticleSystem != null)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                initialStartSpeedExplosion = RainExplosionParticleSystem.startSpeed;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                initialStartSizeExplosion = RainExplosionParticleSystem.startSize;
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        protected override void Update()
        {
            base.Update();

           // cameraMultiplier = (Camera.orthographicSize * 0.25f);
           // visibleBounds.min = Camera.main.ViewportToWorldPoint(Vector3.zero);
           // visibleBounds.max = Camera.main.ViewportToWorldPoint(Vector3.one);
            visibleWorldWidth = visibleBounds.size.x;
            yOffset = (visibleBounds.max.y - visibleBounds.min.y) * RainHeightMultiplier;

            TransformParticleSystem(RainFallParticleSystem, initialStartSpeedRain, initialStartSizeRain);
            TransformParticleSystem(RainMistParticleSystem, initialStartSpeedMist, initialStartSizeMist);
            TransformParticleSystem(RainExplosionParticleSystem, initialStartSpeedExplosion, initialStartSizeExplosion);

           // CheckForCollisionsRainParticles();
           // CheckForCollisionsMistParticles();
        }

       

   
    }
}