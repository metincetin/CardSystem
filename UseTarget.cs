using UnityEngine;

namespace CardSystem
{
    public abstract class UseTarget : ScriptableObject
    {
        public abstract bool CanBeUsedOn(ICardCaster from, ICastTarget to);
    }
}