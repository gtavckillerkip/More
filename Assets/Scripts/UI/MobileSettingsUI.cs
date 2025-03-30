using UnityEngine;

namespace More.UI
{
	/// <summary>
	/// Скрипт подвижного интерфейса настроек.
	/// </summary>
	public class MobileSettingsUI : MonoBehaviour
	{
		private (float x, float y) _contentCurrentAnchoredPosition;

		private (float x, float y) _contentNewAnchoredPosition;

		private readonly float _time = 0.5f;

		private float _timePassed;

		private (float x, float y) _difference;

		private void OnEnable()
		{
			_contentNewAnchoredPosition = _contentCurrentAnchoredPosition = gameObject.GetComponentInParent<SettingsBlockUI>().ContentAnchoredPosition;

			gameObject.GetComponentInParent<SettingsBlockUI>().ContentAnchoredPositionChanged += ContentPositionChangedHandler;
		}

		private void Update()
		{
			if (_contentCurrentAnchoredPosition != _contentNewAnchoredPosition)
			{
				if (_timePassed < _time)
				{
					_timePassed += Time.deltaTime;

					var multiplier = Mathf.Sin(_timePassed / _time * Mathf.PI / 2);

					(float aPosX, float aPosY) = (_contentCurrentAnchoredPosition.x + multiplier * _difference.x, _contentCurrentAnchoredPosition.y + multiplier * _difference.y);

					gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(aPosX, aPosY);
				}
				else
				{
					_contentCurrentAnchoredPosition = _contentNewAnchoredPosition;
				}
			}
		}

		private void ContentPositionChangedHandler((float newX, float newY) newPosition)
		{
			_contentNewAnchoredPosition = newPosition;

			_difference = (newPosition.newX - _contentCurrentAnchoredPosition.x, newPosition.newY - _contentCurrentAnchoredPosition.y);

			_timePassed = 0;
		}
	}
}
