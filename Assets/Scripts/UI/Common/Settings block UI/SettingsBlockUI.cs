using System;
using UnityEngine;

namespace More.UI
{
	/// <summary>
	/// Интерфейс блока настроек.
	/// </summary>
	public partial class SettingsBlockUI : BlockUI
	{
		#region Fields declarations
		private (float x, float y) _contentAnchoredPosition;

		private (float x, float y) _contentInitialPosition;

		private GameObject _focusedContentChild;

		public event Action<(float newX, float newY)> ContentAnchoredPositionChanged;
		#endregion

		#region Properties declarations
		public (float x, float y) ContentAnchoredPosition
		{
			get => _contentAnchoredPosition;
			set
			{
				_contentAnchoredPosition = value;
				ContentAnchoredPositionChanged?.Invoke(value);
			}
		}
		#endregion

		public override void Setup()
		{
			base.Setup();

			SetupChildObjects();

			SetupBlockContent();
			SetupAdditionalButtons();

			Hide();

			_focusedContentChild = _mainButtons;

			_content.AddComponent<MobileSettingsUI>();
		}

		protected override void SetupBlockTitling() { }

		protected override void SetupBlockContent()
		{
			_contentInitialPosition = _contentAnchoredPosition = (_content.GetComponent<RectTransform>().anchoredPosition.x, _content.GetComponent<RectTransform>().anchoredPosition.y);

			SetupMainButtons();
			SetupVideoSettings();
			SetupAudioSettings();
			SetupControlsSettingsButtons();
			SetupGameplaySettings();
			SetupMouseSettings();
			SetupKeysSettings();
		}

		protected override void SetupBlockMiscellaneous()
		{
			_miscellaneous = _block.transform.Find(BLOCK_MISCELLANEOUS).gameObject;

			SetupAdditionalButtons();
		}
	}
}
