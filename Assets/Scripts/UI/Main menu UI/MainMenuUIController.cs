using More.Basic.Management;
using System;
using UnityEngine;

namespace More.UI
{
	/// <summary>
	/// ���������� ���������� �������� ����.
	/// </summary>
	[Obsolete]
	public class MainMenuUIController : UIController
	{
		#region Properties declarations
		/// <summary>
		/// ��������� ��������� �����.
		/// </summary>
		public MainBlockUI MainBlockUI { get; private set; }

		/// <summary>
		/// ��������� ����� ��������.
		/// </summary>
		public SettingsBlockUI SettingsBlockUI { get; private set; }
		#endregion

		public override void Setup()
		{
			PerformingUICanvas = UIManager.Instance.MainMenuUI.transform.Find("UI Objects Container").Find(PERFORMING_UI_CANVAS).GetComponent<Canvas>();

			MainBlockUI = PerformingUICanvas.gameObject.transform.Find("Main Block").gameObject.GetComponent<MainBlockUI>();
			MainBlockUI.Setup();

			SettingsBlockUI = PerformingUICanvas.gameObject.transform.Find("Settings Block").gameObject.GetComponent<SettingsBlockUI>();
			SettingsBlockUI.Setup();

			MainBlockUI.SettingsButtonClicked += () =>
			{
				MainBlockUI.Hide();
				SettingsBlockUI.Show();
			};

			SettingsBlockUI.BackButtonClicked += () =>
			{
				SettingsBlockUI.Hide();
				MainBlockUI.Show();
			};
		}
	}
}
