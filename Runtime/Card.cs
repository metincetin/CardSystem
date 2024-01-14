using System;
using CardSystem.FX;
using UnityEngine;

namespace CardSystem
{
	public abstract class Card : ScriptableObject
	{
		[SerializeField]
		private string _cardName;

		public string CardName
		{
			get => _cardName;
			set => _cardName = value;
		}

		[SerializeField, TextArea] 
		private string _description;
		public string Description => _description;

		[SerializeField] 
		private Sprite _image;
		public Sprite Image => _image;

		[SerializeField] 
		private CardTag[] _tags;
		public CardTag[] Tags => _tags;

		[SerializeField] 
		private int _mana;
		public int Mana => _mana;

		[SerializeField] 
		private UseTarget _useTarget;
		public UseTarget UseTarget => _useTarget;

		[SerializeField] 
		private CardEffect[] _casterFX;
		public CardEffect[] CasterFX => _casterFX;

		[SerializeField] 
		private CardEffect[] _targetFX;
		public CardEffect[] TargetFX => _targetFX;

		protected virtual bool FXAllowed => true;


		public event Action<ICardCaster, ICastTarget> Used;

		protected virtual ICastTarget ProcessTarget(ICastTarget target)
		{
			return target;
		}


		public virtual bool CanBeUsed(ICardCaster caster, ICastTarget target)
		{
			return caster.CardWielder.Mana >= _mana;
		}

		public void Use(ICardCaster by, ICastTarget target)
		{
			var newT = ProcessTarget(target);
			if (CanBeUsed(by, newT))
			{
				by.CardWielder.Mana -= _mana;
				OnUsed(by, newT);
				Used?.Invoke(by, newT);
				RequestFX(by, newT);
			}
		}

		protected virtual void RequestFX(ICardCaster by, ICastTarget target)
		{
			CreateAllFX(by, target);
		}

		protected void CreateAllFX(ICardCaster by, ICastTarget target)
		{
			if (!FXAllowed) return;
			foreach (var fx in _casterFX) fx.CreateEffect(by, target);

			foreach (var fx in _targetFX) fx.CreateEffect(by, target);
		}

		protected abstract void OnUsed(ICardCaster by, ICastTarget target);

		public CardTag GetTagByName(string tagName)
		{
			foreach (var c in _tags)
				if (c.name == tagName)
					return c;
			return null;
		}

		public bool HasTagByName(string tagName)
		{
			return GetTagByName(tagName) != null;
		}
	}
}