using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem.UI
{
	public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler,
		IEndDragHandler, IPointerUpHandler
	{
		[SerializeField] private TMP_Text _title;
		[SerializeField] private TMP_Text _description;
		[SerializeField] private Image _image;
		[SerializeField] private TMP_Text _manaCost;
		[SerializeField] private CardTagsDescriptionView _tagsDescriptionView;


		[SerializeField] private Image _hiderImage;

		private Vector2 _dragOffset;

		private CardInstance _card;


		private bool _isDragging;
		private bool _isHidden;

		public CardInstance CardInstance
		{
			get => _card;
			set
			{
				_card = value;
				_tagsDescriptionView.Card = value.Card;
				UpdateUI();
			}
		}

		public bool IsHidden
		{
			get => _isHidden;
			set
			{
				_isHidden = value;
				_hiderImage.enabled = value;
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			DragRequested?.Invoke(this, eventData);
			_isDragging = true;
			_tagsDescriptionView.Hide();

			_dragOffset = (Vector2)transform.position - eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var tr = transform as RectTransform;
			transform.position = eventData.position + _dragOffset;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			transform.localPosition = Vector3.zero;
			DragEnded?.Invoke(this, eventData);
			_isDragging = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!_isDragging)
			{
				_tagsDescriptionView.Show();
				PointerEntered?.Invoke(this, eventData);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!_isDragging)
				PointerExited?.Invoke(this, eventData);

			_tagsDescriptionView.Hide();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!_isDragging) PointerUp?.Invoke(this, eventData);
		}

		public event Action<CardView, PointerEventData> PointerEntered;
		public event Action<CardView, PointerEventData> PointerExited;
		public event Action<CardView, PointerEventData> PointerUp;
		public event Action<CardView, PointerEventData> DragRequested;
		public event Action<CardView, PointerEventData> DragEnded;

		private void UpdateUI()
		{
			_title.text = CardInstance.Card.CardName;
			var tagsString = string.Join(", ", CardInstance.Card.Tags.Select(x => x.name));
			_description.text = (string.IsNullOrEmpty(tagsString) ? string.Empty : $"<b>{tagsString}</b>\n") +
								CardInstance.Card.Description;
			_image.sprite = CardInstance.Card.Image;
			_manaCost.text = CardInstance.Card.Mana.ToString();
		}
	}
}