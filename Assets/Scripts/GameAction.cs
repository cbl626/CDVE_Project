using System;
using UnityEngine;

namespace Wispy
{
	[Serializable]
	public abstract class GameAction : MonoBehaviour
	{
		public abstract void Init (GameEventManager manager);
		public abstract void OnTrigger ();
	}
}
