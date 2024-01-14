using UnityEngine;

namespace CardSystem.FX.CardEffects
{
	[CreateAssetMenu(menuName = "FX/Tint")]
	public class TintEffect : CardEffect
	{
		public Color Tint;

		protected override void OnEffectCreated(ICardCaster caster, ICastTarget target)
		{
			foreach (var t in target.Iterate<MonoBehaviour>())
			{
				var renderer = t.GetComponentInChildren<Renderer>();
				if (renderer != null)
				{
				}
			}
		}
	}
}