using System;
using UnityEngine;

namespace CardSystem
{
	[Serializable]
	public class CardWielder
	{
		private int _mana;
		public int Mana
		{
			get => _mana;
			set => _mana = Mathf.Clamp(value, 0, MaxMana);
		}

		public int MaxMana { get; set; } = 10;
		public Deck Deck { get; set; } = new();
		public DiscardPile DiscardPile { get; set; } = new();
		public Hand Hand { get; set; } = new();


		public CardWielder()
		{
			_mana = MaxMana;
		}


		public event Action<float> Damaged;
		public event Action<Card, CardWielder> CardUsed;
		public event Action<Card, CardWielder> AppliedCard;

		public void DiscardAll()
		{
			for (int i = Hand.Cards.Count - 1; i >= 0; i--)
			{
				Discard(i);
			}
		}

		public void Discard(int index)
		{
			var card = GetCardOfHand(index);

			Hand.RemoveCard(index);
			DiscardPile.Cards.Add(card.Card);

			HandleTags(card);

		}

		public void Use(int cardIndex, ICardCaster caster, ICastTarget target)
		{
			var c = GetCardOfHand(cardIndex);
			var card = c.Card;
			if (!card.CanBeUsed(caster, target)) return;
			if (c != null)
			{
				Debug.Log($"Card used on {target}");
				Hand.RemoveCard(cardIndex);
				card.Use(caster, target);
				HandleTags(c);
			}
		}

		private void HandleTags(CardInstance cardInstance)
		{
			var card = cardInstance.Card;
			if (!card.HasTagByName("Consumable"))
				if (!cardInstance.PreventDiscardPile)
					DiscardPile.Cards.Add(card);
		}

		private void MoveAllFromDiscardPileToDeck()
		{
			foreach (var pileCard in DiscardPile.Cards) Deck.AddCard(pileCard);
			DiscardPile.Cards.Clear();
		}

		public void DrawFromDeck(int number = 1)
		{
			var picked = Deck.TryPick(number, out var cards);
			foreach (var c in cards) Hand.AddCard(c);

			if (picked != number) MoveAllFromDiscardPileToDeck();
		}

		private CardInstance GetCardOfHand(int index)
		{
			if (index >= Hand.Cards.Count) return null;
			return Hand.Cards[index];
		}
	}
}