using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace CardSystem
{
	public class Hand
	{
		public List<CardInstance> Cards { get; set; }= new();
		public int Length => Cards.Count;

        public event Action<int, CardInstance> CardAdded;
		public event Action<int, CardInstance> CardRemoved;

		public void AddCard(Card card)
		{
			var inst = new CardInstance(card);
			Cards.Add(inst);
			CardAdded?.Invoke(Cards.Count - 1, inst);
		}

		public void RemoveCard(int i)
		{
			if (i < Cards.Count)
			{
				var c = Cards[i];
				Cards.RemoveAt(i);
				CardRemoved?.Invoke(i, c);
			}
		}
	}
}