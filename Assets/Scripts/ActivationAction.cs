
using System.Collections.Generic;
using UnityEngine;
using Wispy;

namespace Assets.Scripts
{
    public class ActivationAction : GameAction
    {
        public List<GameObject> _objects;
        public bool Activate = true;
        public override void Init(GameEventManager manager)
        {
        }

        public override void OnTrigger()
        {
            foreach(GameObject obj in _objects)
            {
                obj.SetActive(Activate);
            }
        }
    }
}
