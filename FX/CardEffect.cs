using UnityEngine;

namespace CardSystem.FX
{
	public abstract class CardEffect : ScriptableObject
	{
		public void CreateEffect(ICardCaster caster, ICastTarget target)
		{
			OnEffectCreated(caster, target);
		}

		protected abstract void OnEffectCreated(ICardCaster caster, ICastTarget target);
	}
}