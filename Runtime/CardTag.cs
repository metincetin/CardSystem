using UnityEngine;

namespace CardSystem
{
	[CreateAssetMenu(menuName = "Tag")]
	public class CardTag : ScriptableObject
	{
		[SerializeField] [TextArea]
		private string _description;

		public string Description => _description;
	}
}