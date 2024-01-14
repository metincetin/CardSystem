using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem.UI
{
	public class HandDisplay : MonoBehaviour
	{
		[SerializeField] private bool _isHidden;

		[SerializeField] private CardView _cardViewPrefab;

		[SerializeField] private CardWrapper _cardWrapperPrefab;

		protected List<CardView> _activeCardViews = new();

		private CardView _dragging;

		private Hand _hand;
		public CardWielder Target { get; set; }

		public Hand Hand
		{
			get => _hand;
			set
			{
				if (_hand != null) UnlistenEvents();

				_hand = value;
				ListenEvents();
				if (_activeCardViews.Count != Hand.Cards.Count)
				{
					InitializeCards();
				}
			}
		}

		public bool IsHidden
		{
			get => _isHidden;
			set
			{
				_isHidden = value;
				HandleCardVisibility();
			}
		}


		public bool AllowExecution { get; set; }

		private void Start()
		{
			HandleCardVisibility();
		}

		private void InitializeCards()
		{
			for (var i = 0; i < Hand.Cards.Count; i++)
			{
				var card = Hand.Cards[i];
				OnCardAdded(i, card);
			}
		}

		private void ListenEvents()
		{
			Hand.CardAdded += OnCardAdded;
			Hand.CardRemoved += OnCardRemoved;
		}

		private void UnlistenEvents()
		{
			Hand.CardAdded -= OnCardAdded;
			Hand.CardRemoved -= OnCardRemoved;
		}

		private void HandleCardVisibility()
		{
			foreach (var c in _activeCardViews) c.IsHidden = IsHidden;
		}

		private void OnCardRemoved(int index, CardInstance cardData)
		{
			var activeCard = _activeCardViews[index];
			Destroy(activeCard.transform.parent.gameObject);
			_activeCardViews.RemoveAt(index);

			UpdateLayout();
		}

		private void OnCardAdded(int index, CardInstance cardData)
		{
			var wrapper = Instantiate(_cardWrapperPrefab, transform);
			var cardView = Instantiate(_cardViewPrefab, wrapper.transform);
			cardView.CardInstance = cardData;
			cardView.transform.SetSiblingIndex(index);
			_activeCardViews.Insert(index, cardView);

			HandleCardVisibility();

			UpdateLayout();

			cardView.PointerEntered += OnCardPointerEntered;
			cardView.PointerExited += OnCardPointerExited;
			cardView.DragRequested += OnCardDragRequested;
			cardView.PointerUp += OnCardPointerUp;
			cardView.DragEnded += OnCardDragEnded;
		}

		private void OnCardPointerUp(CardView view, PointerEventData eventData)
		{
			if (!AllowExecution) return;
			HandlePointerUp(view, eventData);
		}


		private void OnCardDragEnded(CardView view, PointerEventData eventData)
		{
			if (!AllowExecution) return;
			HandleDrop(view, eventData);
		}

		protected virtual void HandleDrop(CardView view, PointerEventData eventData)
		{
		}

		protected virtual void HandlePointerUp(CardView view, PointerEventData eventData)
		{
		}

		private void OnCardDragRequested(CardView view, PointerEventData eventData)
		{
			if (!AllowExecution) return;
			_dragging = view;
		}

		private void OnCardPointerExited(CardView view, PointerEventData eventData)
		{
			view.transform.DOKill();
			view.transform.DOLocalMove(Vector3.zero, .2f);
		}

		private void OnCardPointerEntered(CardView view, PointerEventData eventData)
		{
			view.transform.DOLocalMove(new Vector3(0, 300, 0), .2f);
		}

		private void UpdateLayout()
		{
			IEnumerator UpdateLayoutInternal()
			{
				yield return new WaitForEndOfFrame();
				LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
			}

			StartCoroutine(UpdateLayoutInternal());
		}
	}
}