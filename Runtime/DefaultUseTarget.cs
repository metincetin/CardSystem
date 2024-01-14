using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(menuName = "UseTargets/Default")]
    public class DefaultUseTarget : UseTarget
    {

        public override bool CanBeUsedOn(ICardCaster from, ICastTarget to)
        {
            return true;
        }
    }
}