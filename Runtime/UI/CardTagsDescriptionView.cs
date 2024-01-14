using System.Linq;
using TMPro;
using UnityEngine;

namespace CardSystem.UI
{
	public class CardTagsDescriptionView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		private Card _card;
		public Card Card
		{
			get => _card;
			set
			{
				_card = value;
				UpdateUI();
			}
		}

		private void UpdateUI()
		{
			var str = Card.Tags.Select(x => $"<b>{x.name}: </b> {x.Description}");
			_text.text = string.Join("\n", str);
		}

		public void Show()
		{
			if (_card.Tags.Length == 0) return;
			GetComponent<Canvas>().enabled = true;
		}

		public void Hide()
		{
			GetComponent<Canvas>().enabled = false;
		}
	}
}