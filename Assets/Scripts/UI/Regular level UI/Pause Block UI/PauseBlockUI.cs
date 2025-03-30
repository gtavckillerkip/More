namespace More.UI
{
	/// <summary>
	/// Интерфейс блока паузы.
	/// </summary>
	public partial class PauseBlockUI : BlockUI
	{
		public override void Setup()
		{
			base.Setup();

			SetupChildObjects();

			SetupBlockContent();

			Hide();
		}

		protected override void SetupBlockContent()
		{
			SetupMainButtons();
		}
	}
}
