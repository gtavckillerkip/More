namespace More.UI
{
	/// <summary>
	/// Интерфейс игрового блока.
	/// </summary>
	public partial class PlayingBlockUI : BlockUI
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
			SetupAim();
			SetupPlayerStats();
			SetupTasks();
		}
	}
}
