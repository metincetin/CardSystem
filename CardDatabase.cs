using UnityEngine;

namespace CardSystem
{
	[CreateAssetMenu(menuName = "Database/Card")]
	public class CardDatabase : ScriptableObject
	{
		[SerializeField]
		private Card[] _cards;
		public Card[] Cards => _cards;
	}
}