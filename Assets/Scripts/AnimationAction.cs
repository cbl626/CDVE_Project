using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wispy;

namespace Assets.Scripts
{
    public class AnimationAction : GameAction
    {
        public Animator Animator;
        public string TriggerName;

        public override void Init(GameEventManager manager)
        {
           
        }

        public override void OnTrigger()
        {
            Animator.SetTrigger(TriggerName);
        }
    }
}
