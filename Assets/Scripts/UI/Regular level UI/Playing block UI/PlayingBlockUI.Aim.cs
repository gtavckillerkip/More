using More.Basic.Management;
using UnityEngine;

namespace More.UI
{
	public partial class PlayingBlockUI
	{
		#region Fields declarations
		private const string AIM = "Aim";

		/// <summary>
		/// Прицел.
		/// </summary>
		private GameObject _aim;
		#endregion

		/// <summary>
		/// Настройка прицела.
		/// </summary>
		private void SetupAim()
		{
			_aim = _content.transform.Find(AIM).gameObject;

			_aim.SetActive(true);

			//var attackController = PlayerManager.Instance.Controller.GetComponent<AttackController>();

			//attackController.Aimed += () => { };
			//attackController.Unaimed += () => { };
			//attackController.EnemySpotted += () => { };
			//attackController.EnemyUnspotted += () => { };
		}
	}
}
