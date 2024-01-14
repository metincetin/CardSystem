using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardSystem
{
	public class Deck
	{
		public List<Card> Cards { get; private set; }= new();

		public void Shuffle()
		{
			Cards = Cards.OrderBy(x => Random.value).ToList();
		}

		public int TryPick(int count, out Card[] cards)
		{
			count = Mathf.Min(count, Cards.Count);
			var ret = new Card[count];

			if (count == 0)
			{
				cards = ret;
				return 0;
			}

			cards = Cards.Take(count).ToArray();
			Cards.RemoveRange(0, count);
			return count;
		}

		public void AddCard(Card card)
		{
			Cards.Add(card);
		}

		public void AddAll(Card[] cards)
		{
			foreach(var i in cards)
			{
				AddCard(i);
			}
		}

		public void AddDatabase(CardDatabase database)
		{
			AddAll(database.Cards);
		}
	}
}