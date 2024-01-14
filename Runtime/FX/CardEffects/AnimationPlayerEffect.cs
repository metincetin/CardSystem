using UnityEngine;

namespace CardSystem.FX.CardEffects
{
	[CreateAssetMenu(menuName = "FX/Animation Player")]
	public class AnimationPlayerEffect : CardEffect
	{
		public string TriggerName;

		protected override void OnEffectCreated(ICardCaster caster, ICastTarget target)
		{
			if (caster is MonoBehaviour mb)
			{
				var animator = mb.GetComponentInChildren<Animator>();
				if (animator) animator.SetTrigger(TriggerName);
			}
		}
	}
}