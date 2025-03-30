using More.Basic.Management;
using UnityEngine;

/// <summary>
/// Основное движение камеры.
/// </summary>
public class CameraCommonMovement : MonoBehaviour
{
	#region Fields declarations
	/// <summary>
	/// Смещение камеры относительно игрока по умолчанию.
	/// </summary>
	public readonly Vector3 DefaultPositionOffset = new(0.5f, 1.6f, -2.5f);

	/// <summary>
	/// Камера.
	/// </summary>
	private GameObject _camera;

	/// <summary>
	/// Объект игрового персонажа.
	/// </summary>
	private GameObject _playerCharacter;

	/// <summary>
	/// Смещение камеры относительно игрока.
	/// </summary>
	public Vector3 CurrentPositionOffset;

	/// <summary>
	/// Поворот вокруг осей X и Y.
	/// </summary>
	private Vector2 _rotation;

	/// <summary>
	/// Скорость вращения.
	/// </summary>
	public float RotationSpeed;
	#endregion

	private void Awake()
	{
		CurrentPositionOffset = new(DefaultPositionOffset.x, DefaultPositionOffset.y, DefaultPositionOffset.z);
	}

	private void Start()
	{
		InputSystem.Instance.MouseRotated += MouseRotatedHandler;

		_camera = CameraManager.Instance.CameraObject;
		_playerCharacter = PlayerManager.Instance.PlayerObject;

		_camera.transform.rotation = _playerCharacter.transform.rotation;
		_camera.transform.position = _playerCharacter.transform.position +
			_camera.transform.rotation * new Vector3(CurrentPositionOffset.x, 0, CurrentPositionOffset.z) +
			new Vector3(0, CurrentPositionOffset.y, 0);

		_rotation = new Vector2(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y);

		RotationSpeed = CameraManager.Instance.RetrieveAxialSettings().Sensivity;

		CameraManager.Instance.AxialSettingsChanged += AxialSettingsChangedHandler;
	}

	private void LateUpdate()
	{
		_camera.transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);

		var t1 = _playerCharacter.transform.position +
			_camera.transform.rotation * new Vector3(CurrentPositionOffset.x, 0, CurrentPositionOffset.z) +
			new Vector3(0, CurrentPositionOffset.y, 0);

		var delta = _camera.transform.position - t1;

		var projection = Vector3.Project(delta, _camera.transform.rotation * new Vector3(CurrentPositionOffset.x, 0, CurrentPositionOffset.z));

		projection = Vector3.Lerp(projection, Vector3.zero, Time.deltaTime * 10);

		projection = Vector3.Project(projection, _camera.transform.rotation * new Vector3(0, 0, CurrentPositionOffset.z));

		_camera.transform.position = t1 + projection;
	}

	#region Events handlers
	private void MouseRotatedHandler(Vector2 _mouseRotation)
	{
		if (enabled)
		{
			_rotation.x -= _mouseRotation.y * RotationSpeed;
			_rotation.x = Mathf.Clamp(_rotation.x, -85f, 80f);

			_rotation.y += _mouseRotation.x * RotationSpeed;
		}
	}

	private void AxialSettingsChangedHandler(CameraManager.AxialSettingsChangedEventArgs args)
	{
		if (args.Sensivity.HasValue)
		{
			RotationSpeed = args.Sensivity.Value;
		}
	}
	#endregion
}
