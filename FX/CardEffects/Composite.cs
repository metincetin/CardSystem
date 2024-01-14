using UnityEngine;

namespace CardSystem.FX.CardEffects
{
	[CreateAssetMenu(menuName = "Card Effects/Composite")]
	public class Composite : CardEffect
	{
		public CardEffect[] Effects;

		protected override void OnEffectCreated(ICardCaster caster, ICastTarget target)
		{
			foreach (var effect in Effects) effect.CreateEffect(caster, target);
		}
	}
}