using More.Settings_system;
using UnityEngine;

/// <summary>
/// Класс-контейнер настроек.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable objects/Servicing/SettingsSO")]
public class SettingsSO : ScriptableObject
{
	#region Properties declarations
	/// <summary>
	/// Название.
	/// </summary>
	public string Title;

	#region Video
	/// <summary>
	/// Разрешение экрана.
	/// </summary>
	public ResolutionIdentifier ResolutionIdentifier;

	/// <summary>
	/// Поле зрения.
	/// </summary>
	public float FOV;

	/// <summary>
	/// Масштаб интерфейса.
	/// </summary>
	public float InterfaceScale;
	#endregion

	#region Audio
	/// <summary>
	/// Громкость звуков.
	/// </summary>
	public int SoundVolume;

	/// <summary>
	/// Громкость музыки.
	/// </summary>
	public int MusicVolume;
	#endregion

	#region Controls
	/// <summary>
	/// Чувствительность мыши.
	/// </summary>
	public float MouseSensivity;

	/// <summary>
	/// Коэффициент чувствительности мыши при прицеливании.
	/// </summary>
	public float MouseScopeSensivityScale;

	/// <summary>
	/// Инверсия мыши.
	/// </summary>
	public bool MouseInversion;

	/// <summary>
	/// Движение вперёд.
	/// </summary>
	public KeyCode MoveForward;

	/// <summary>
	/// Движение назад.
	/// </summary>
	public KeyCode MoveBackward;

	/// <summary>
	/// Движение влево.
	/// </summary>
	public KeyCode MoveLeft;

	/// <summary>
	/// Движение вправо.
	/// </summary>
	public KeyCode MoveRight;

	/// <summary>
	/// Прыжок.
	/// </summary>
	public KeyCode Jump;

	/// <summary>
	/// Приседание.
	/// </summary>
	public KeyCode Duck;

	/// <summary>
	/// Замедление движения.
	/// </summary>
	public KeyCode Slowdown;

	/// <summary>
	/// Способность 1.
	/// </summary>
	public KeyCode Ability1;

	/// <summary>
	/// Способность 2.
	/// </summary>
	public KeyCode Ability2;

	/// <summary>
	/// Способность 3.
	/// </summary>
	public KeyCode Ability3;

	/// <summary>
	/// Переключение полноэкранного режима.
	/// </summary>
	public KeyCode FullScreenSwitch;

	/// <summary>
	/// Первичная атака.
	/// </summary>
	public KeyCode PrimaryAttack;

	/// <summary>
	/// Дополнительная атака.
	/// </summary>
	public KeyCode SecondaryAttack;

	/// <summary>
	/// Использовать.
	/// </summary>
	public KeyCode Use;
	#endregion

	#region Gameplay
	/// <summary>
	/// Сложность.
	/// </summary>
	public Difficulty Difficulty;
	#endregion

	#region Extra

	#endregion
	#endregion
}
