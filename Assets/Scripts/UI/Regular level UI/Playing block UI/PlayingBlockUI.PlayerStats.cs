using More.Basic.Management;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class PlayingBlockUI
	{
		#region Fields declarations
		private const string PLAYER_STATS = "Player Stats";

		private const string HEALTH_BAR = "Health Bar";
		private const string ABILITIES = "Abilities";

		/// <summary>
		/// Показатели игрока.
		/// </summary>
		private GameObject _playerStats;

		/// <summary>
		/// Шкала здоровья.
		/// </summary>
		private Image _healthBar;

		/// <summary>
		/// Рамка способности 1.
		/// </summary>
		private GameObject _ability1border;

		/// <summary>
		/// Рамка способности 2.
		/// </summary>
		private GameObject _ability2border;

		/// <summary>
		/// Рамка способности 3.
		/// </summary>
		private GameObject _ability3border;
		#endregion

		/// <summary>
		/// Настройка показателей игрока.
		/// </summary>
		private void SetupPlayerStats()
		{
			_playerStats = _content.transform.Find(PLAYER_STATS).gameObject;

			_playerStats.SetActive(true);

			var playerManager = PlayerManager.Instance;
			var uiManager = UIManager.Instance;

			//playerManager.Controller.GetComponent<AbilitiesController>().AbilityStateChanged += AbilityStateChangedHandler;

			#region Health
			_healthBar = _playerStats.transform.Find(HEALTH_BAR).Find("Bar").GetComponent<Image>();

			//_healthBar.fillAmount = playerManager.PlayerObject.GetComponent<Character>().Health / playerManager.PlayerObject.GetComponent<Character>().MaxHealth;
			#endregion

			#region Abilities
			_ability1border = _playerStats.transform.Find(ABILITIES).Find("Ability1").Find("Border").gameObject;
			_ability2border = _playerStats.transform.Find(ABILITIES).Find("Ability2").Find("Border").gameObject;
			_ability3border = _playerStats.transform.Find(ABILITIES).Find("Ability3").Find("Border").gameObject;

			AbilityBorderChange(_ability1border, AbilityPointer.Ability1);
			AbilityBorderChange(_ability2border, AbilityPointer.Ability2);
			AbilityBorderChange(_ability3border, AbilityPointer.Ability3);
			#endregion
		}

		private void AbilityBorderChange(GameObject border, AbilityPointer ability)
		{
			//border.SetActive(PlayerManager.Instance.Controller.GetComponent<AbilitiesController>().AbilitiesStates[ability] == AbilitiesController.AbilityState.Activated);
		}

		#region Events handlers
		private void AbilityStateChangedHandler(AbilitiesController.AbilityStateChangedEventArgs e)
		{
			switch (e.Ability)
			{
				case AbilityPointer.Ability1:
					AbilityBorderChange(_ability1border, e.Ability);
					break;

				case AbilityPointer.Ability2:
					AbilityBorderChange(_ability2border, e.Ability);
					break;

				case AbilityPointer.Ability3:
					AbilityBorderChange(_ability3border, e.Ability);
					break;
			}
		}
		#endregion
	}
}
