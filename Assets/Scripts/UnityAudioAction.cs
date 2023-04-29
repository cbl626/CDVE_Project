using UnityEngine;
using Wispy;

namespace Assets.Scripts
{
    public class UnityAudioAction : GameAction
    {
        public AudioSource Source;
        public AudioClip Clip;
        public bool IsPlay;
        public override void Init(GameEventManager manager)
        {
        }

        public override void OnTrigger()
        {
            Source.clip = Clip;
            Source.Play();
        }
    }
}
