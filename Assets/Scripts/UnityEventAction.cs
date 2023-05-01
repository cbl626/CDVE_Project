using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Wispy;

namespace Assets.Scripts
{
    public class UnityEventAction : GameAction
    {
        [TextArea(3, 10)]
        public string Comment;
        public UnityEvent Event;

        public override void Init(GameEventManager manager)
        {
        }

        public override void OnTrigger()
        {
            Event?.Invoke();
        }
    }
}
