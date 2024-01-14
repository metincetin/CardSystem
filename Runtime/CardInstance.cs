using System;

namespace CardSystem
{
	public class CardInstance
	{
		public Card Card;

		public bool PreventDiscardPile;

		public CardInstance(Card card)
		{
			Card = card;
		}

		public event Action Used;
	}
}