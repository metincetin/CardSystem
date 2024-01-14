using UnityEngine;

namespace CardSystem
{
	public class CardWielderComponent : MonoBehaviour, ICardWielderReference
	{
		public CardWielder CardWielder { get; set; } = new();
	}
}