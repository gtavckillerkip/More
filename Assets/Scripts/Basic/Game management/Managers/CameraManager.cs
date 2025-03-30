using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Менеждер камеры.
	/// </summary>
	public class CameraManager : SingletonMB<CameraManager>
	{
		#region Definitions
		public class AxialSettingsChangedEventArgs : EventArgs
		{
			public float? Sensivity { get; set; }
			public float? ScopeSensivityScale { get; set; }
		}
		#endregion

		#region Properties declarations
		/// <summary>
		/// Объект камеры.
		/// </summary>
		[field: SerializeField] public GameObject CameraObject { get; private set; } // has already been gotten through the inspector

		/// <summary>
		/// Контроллер.
		/// </summary>
		public GameObject Controller { get; private set; }

		/// <summary>
		/// Машина состояний камеры.
		/// </summary>
		public CameraStateMachine CameraStateMachine { get; private set; }
		#endregion

		#region Events declarations
		/// <summary>
		/// Изменены настройки мыши.
		/// </summary>
		public event Action<AxialSettingsChangedEventArgs> AxialSettingsChanged;
		#endregion

		private void Start()
		{
			Controller = CameraObject.transform.Find("Controller").gameObject;

			CameraStateMachine = new(this);
			CameraStateMachine.RunOnGameStart();

			SettingsSystem.Instance.CurrentSettingsChanged += CurrentSettingsChangedHandler;
		}

		#region Methods
		/// <summary>
		/// Получить аксиальные настройки.
		/// </summary>
		/// <returns> Набор настроек мыши: чувствительность, множитель чувствительности при приближении, инверсия. </returns>
		public (float Sensivity, float ScopeSensivityScale, bool Inversion) RetrieveAxialSettings() =>
			(SettingsSystem.Instance.CurrentSettings.MouseSensivity,
			SettingsSystem.Instance.CurrentSettings.MouseScopeSensivityScale,
			SettingsSystem.Instance.CurrentSettings.MouseInversion);
		#endregion

		#region Events handlers
		private void CurrentSettingsChangedHandler(Dictionary<FieldInfo, object> changedFields)
		{
			var args = new AxialSettingsChangedEventArgs() { Sensivity = null, ScopeSensivityScale = null };

			try
			{
				args.Sensivity = (float)changedFields[SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MouseSensivity))];
			}
			catch { }

			try
			{
				args.ScopeSensivityScale = (float)changedFields[SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MouseScopeSensivityScale))];
			}
			catch { }

			if (args.Sensivity != null || args.ScopeSensivityScale != null)
			{
				AxialSettingsChanged?.Invoke(args);
			}
		}
		#endregion
	}
}
