using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string DISCARD_BUTTON = "Discard Button";

		/// <summary>
		/// Словарь дополнительных кнопок.
		/// </summary>
		private Dictionary<string, GameObject> _additionalButtonNamesToGameObjects;
		#endregion

		/// <summary>
		/// Настройка дополнительных кнопок.
		/// </summary>
		private void SetupAdditionalButtons()
		{
			_additionalButtonNamesToGameObjects = new()
			{
				[DISCARD_BUTTON] = _miscellaneous.transform.Find(DISCARD_BUTTON).gameObject
			};

			_additionalButtonNamesToGameObjects[DISCARD_BUTTON].GetComponent<Button>().onClick.AddListener(DiscardButtonClickedHandler);
		}

		#region Events handlers
		private void DiscardButtonClickedHandler()
		{
			SettingsSystem.Instance.DiscardChanges();
		}
		#endregion
	}
}
