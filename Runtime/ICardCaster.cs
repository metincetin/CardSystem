namespace CardSystem
{
	public interface ICardCaster : ICardWielderReference
	{
		void OnCasted(Card card, ICastTarget target);
	}
}