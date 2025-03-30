namespace More.UI
{
	/// <summary>
	/// Интерфейс основного блока.
	/// </summary>
	public partial class MainBlockUI : BlockUI
	{
		public override void Setup()
		{
			base.Setup();

			SetupChildObjects();

			SetupBlockContent();

			Show();
		}

		protected override void SetupBlockContent()
		{
			SetupMainButtons();
		}
	}
}
