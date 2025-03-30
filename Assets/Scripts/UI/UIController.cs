using System;
using UnityEngine;

namespace More.UI
{
	/// <summary>
	/// Базовый класс контроллеров интерфейса.
	/// </summary>
	[Obsolete]
	public abstract class UIController : MonoBehaviour
	{
		#region Fields declarations
		protected const string PERFORMING_UI_CANVAS = "Performing UI Canvas";
		#endregion

		#region Properties declarations
		/// <summary>
		/// Холст интерфейса.
		/// </summary>
		public Canvas PerformingUICanvas { get; protected set; }
		#endregion

		/// <summary>
		/// Настройка.
		/// </summary>
		public abstract void Setup();
	}
}
