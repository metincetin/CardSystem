using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem.UI
{
    public class CardWrapper : MonoBehaviour
    {
		private CardView _cardView;
        private bool _hovered;

        private void Start()
        {
			_cardView = GetComponentInChildren<CardView>();
            (transform.GetChild(0) as RectTransform).DOLocalMoveY(0, .4f).From(100);

            _cardView.PointerEntered += (CardView _, PointerEventData _) => { _hovered = true; };
            _cardView.PointerExited += (CardView _, PointerEventData _) => { _hovered = false; };

        }

        private void Update()
        {
            var t = (transform.GetChild(0));

            var maxAngle = Mathf.Lerp(5, 15, transform.parent.childCount / 10f);
            var maxOffset = Mathf.Lerp(50, 100, transform.parent.childCount / 10f);

            var i = (transform.GetSiblingIndex() / ((float)transform.parent.childCount - 1));
            var deg = Mathf.Lerp(maxAngle, -maxAngle, i);

            var iHalf = (i - 0.5f);
            var originOffset = (1 - Mathf.Cos(iHalf * Mathf.PI)) * -maxOffset;

            if (!_hovered)
            {
                //t.eulerAngles = new Vector3(0, 0, deg);
                //t.transform.localPosition = new Vector3(0, originOffset, 0);
            }
        }
    }
}
