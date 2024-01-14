using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CardSystem.Cards
{
    [CreateAssetMenu(menuName = "Cards/Composite")]
    public class CompositeCard : Card
    {
        public Card[] Cards;
        public bool DelayBetweenCards;
        public float Delay;
        protected override void OnUsed(ICardCaster by, ICastTarget target)
        {
            if (DelayBetweenCards)
            {
                UseDelayed(by, target);
            }
            else
            {
                foreach (var c in Cards)
                {
                    c.Use(by, target);
                }
            }
        }

        private async void UseDelayed(ICardCaster by, ICastTarget target)
        {
            foreach(var c in Cards)
            {
                c.Use(by, target);
                await Task.Delay((int)(1000 * Delay));
            }
        }
    }
}