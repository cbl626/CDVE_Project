using System;
using UnityEngine;

namespace Wispy
{

	[Serializable]
	public class GameActionData
	{
		[TextArea(3, 10)]
		public string Comment;
		public float WaitTime;
		public GameAction[] Actions;
	}
	
	public class GameEventTriggerHandler : MonoBehaviour
	{
		public GameActionData[] Actions;
		public bool CanRepeat;
		public float RepeatCooldown;
		public bool HasTriggered;
		public bool IsActivated;
		private int CurrentAction;
		private GameEventManager _manager;
		private float _repeatCooldown;
		public void Init (GameEventManager manager)
		{
			_manager = manager;

			foreach (var action in Actions)
			{
				foreach (var subAction in action.Actions)
				{
					subAction.Init(manager);
				}
			}

			_repeatCooldown = RepeatCooldown;
		}
		public void ForceOnTrigger()
		{
			OnTrigger();
		}
		public bool OnTrigger ()
		{
			if (HasTriggered)
			{
				return false;
			}

			if(!IsActivated)
			{
				return false;
			}

			CurrentAction = 0;
			HasTriggered = true;

			return true;
		}

		public void Update ()
		{
			if (CurrentAction >= 0 && CurrentAction < Actions.Length && HasTriggered)
			{
				var action = Actions[CurrentAction];
				action.WaitTime -= Time.deltaTime;
				if (action.WaitTime <= 0)
				{
					foreach (var subAction in action.Actions)
					{
						subAction.OnTrigger ();
					}

					CurrentAction++;
				}

				if(CurrentAction == Actions.Length)
				{
					_manager.OnTriggerHandlerComplete(this);
				}
			}

			if(CanRepeat && CurrentAction == Actions.Length)
			{

				RepeatCooldown -= Time.deltaTime;
				if(RepeatCooldown <= 0)
				{
					CurrentAction = 0;
					HasTriggered = false;
					RepeatCooldown = _repeatCooldown;
				}
			}
		}
	}
}
