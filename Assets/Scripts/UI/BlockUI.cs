using System;
using UnityEngine;

namespace More.UI
{
	public class BlockUI : MonoBehaviour
	{
		#region Fields declarations
		protected const string BLOCK_TITLING = "Block Titling";
		protected const string BLOCK_CONTENT = "Block Content";
		protected const string BLOCK_MISCELLANEOUS = "Block Miscellaneous";

		/// <summary>
		/// Объект блока.
		/// </summary>
		protected GameObject _block;

		/// <summary>
		/// Объект названия.
		/// </summary>
		protected GameObject _titling;

		/// <summary>
		/// Объект с основным содержимым.
		/// </summary>
		protected GameObject _content;

		/// <summary>
		/// Разное.
		/// </summary>
		protected GameObject _miscellaneous;

		public event Action BlockShown;

		public event Action BlockHidden;
		#endregion

		protected void SetupChildObjects()
		{
			if (_block != null)
			{
				if (_block.transform.Find(BLOCK_TITLING) is Transform t)
				{
					_titling = t.gameObject;
				}

				if (_block.transform.Find(BLOCK_CONTENT) is Transform c)
				{
					_content = c.gameObject;
				}

				if (_block.transform.Find(BLOCK_MISCELLANEOUS) is Transform m)
				{
					_miscellaneous = m.gameObject;
				}
			}
		}

		/// <summary>
		/// Показать.
		/// </summary>
		public void Show()
		{
			if (_block != null)
			{
				_block.SetActive(true);
				BlockShown?.Invoke();
			}
		}

		/// <summary>
		/// Скрыть.
		/// </summary>
		public void Hide()
		{
			if (_block != null)
			{
				_block.SetActive(false);
				BlockHidden?.Invoke();
			}
		}

		/// <summary>
		/// Настройка блока.
		/// </summary>
		public virtual void Setup() => _block = gameObject;

		/// <summary>
		/// Настройка заголовка блока.
		/// </summary>
		protected virtual void SetupBlockTitling() { }

		/// <summary>
		/// Настройка содержимого блока.
		/// </summary>
		protected virtual void SetupBlockContent() { }

		/// <summary>
		/// Настройка прочего.
		/// </summary>
		protected virtual void SetupBlockMiscellaneous() { }
	}
}
