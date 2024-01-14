using System;
using UnityEngine;

namespace CardSystem.FX.CardEffects
{
	[CreateAssetMenu(menuName = "Card Effects/Task Composite")]
	public class TaskComposite : CardEffect
	{
		public Task[] Tasks;

		protected override void OnEffectCreated(ICardCaster caster, ICastTarget target)
		{
			Execute(caster, target);
		}

		private async void Execute(ICardCaster caster, ICastTarget on)
		{
			foreach (var task in Tasks)
			{
				await System.Threading.Tasks.Task.Delay(task.Delay);
				task.Effect.CreateEffect(caster, on);
				await System.Threading.Tasks.Task.Delay(task.Wait);
			}
		}

		[Serializable]
		public class Task
		{
			public int Delay;
			public int Wait;
			public CardEffect Effect;
		}
	}
}