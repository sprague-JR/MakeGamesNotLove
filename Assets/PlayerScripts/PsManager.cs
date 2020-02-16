using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    class PsManager: MonoBehaviour
    {
        private ParticleSystem ps;
        private ParticleSystem generatedParticle;
        private Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            generatedParticle = null;

            EmitFX();
        }

        public void EmitFX()
        {
            if (generatedParticle == null)
            {
                generatedParticle = Instantiate(ps, player.transform.position, Quaternion.identity);
            }

            generatedParticle.transform.position = player.transform.position;
            generatedParticle.Play();

            // You can set a fixed duration here if your particle system is looping
            // (I assumed it was not so I used the duration of the particle system to detect the end of it)
            StartCoroutine(StopFXAfterDelay(generatedParticle.main.duration));
        }

        private IEnumerator StopFXAfterDelay(float a_Delay)
        {
            yield return new WaitForSeconds(a_Delay);
            generatedParticle.Stop();
        }

    }
}
