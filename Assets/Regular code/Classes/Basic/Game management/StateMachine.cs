using System;
using System.Collections;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Машина состояний.
	/// </summary>
	public abstract class StateMachine
	{
		#region Fields declarations
		/// <summary>
		/// Объект MonoBehaviour, на котором работает машина.
		/// </summary>
		protected readonly MonoBehaviour _mono;
		#endregion

		#region Properties declarations
		/// <summary>
		/// Работает ли машина (обрабатывает какие-либо состояния).
		/// </summary>
		public bool IsRunning { get; protected set; } = false;
		#endregion

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="mono"> Объект MonoBehaviour для запуска на нём машины. </param>
		public StateMachine(MonoBehaviour mono)
		{
			_mono = mono;
		}

		protected abstract IEnumerator InternalRun(params Enum[] states);
	}
}
