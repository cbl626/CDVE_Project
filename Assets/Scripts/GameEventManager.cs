using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wispy
{
	public class GameEventManager : MonoBehaviour
	{
		[Serializable]
		private class GameEventMeta
        {
            [TextArea(3, 10)]
            public string Comment;
            public GameEventTriggerHandler[] RequiredActivations;
			public GameEventTriggerHandler Handler;
		}

		[SerializeField]
		private GameEventMeta[] TriggerHandlers;
		public GameObject WispyGameObject;
		private Dictionary<GameEventTriggerHandler, bool> activatedHandler = new Dictionary<GameEventTriggerHandler, bool>();

		public void Start ()
		{
			foreach (var handler in TriggerHandlers)
			{
				handler.Handler.Init(this);
			}

			TriggerHandlers[0].Handler.IsActivated = true;
		}

		public void OnTriggerHandlerComplete (GameEventTriggerHandler handler)
		{ 
			activatedHandler.Add(handler,true);

			foreach (var meta in TriggerHandlers)
			{
				foreach (var h in meta.RequiredActivations)
				{
					if(activatedHandler.TryGetValue(h, out var activated))
					{
						if(activated)
						{
							meta.Handler.IsActivated = true;
						}
					}
				}
			}
		}

	}
}
