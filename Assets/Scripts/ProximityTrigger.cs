using UnityEngine;

namespace Wispy
{
	public class ProximityTrigger : MonoBehaviour
	{
		public GameEventTriggerHandler Handler;
		public bool HasTriggered;
		public void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag("Player") && !HasTriggered)
			{
				var hasTriggered = Handler.OnTrigger ();
				HasTriggered = hasTriggered;
			}
		}

		public void OnTriggerStay(Collider other)
		{
            if (other.CompareTag("Player") && !HasTriggered)
            {
                var hasTriggered = Handler.OnTrigger();
                HasTriggered = hasTriggered;
            }
        }
	}
}
