using More.Basic.Management;
using UnityEngine;

/// <summary>
/// Поведение камеры при прицеливании.
/// </summary>
public class CameraAiming : MonoBehaviour
{
	#region Fields declarations
	/// <summary>
	/// Компонент камеры.
	/// </summary>
	private Camera _cameraComponent;

	/// <summary>
	/// Скрипт движения камеры.
	/// </summary>
	private CameraCommonMovement _cameraCommonMovement;

	/// <summary>
	/// Field of view камеры по умолчанию и с приближением при прицеливании.
	/// </summary>
	private (float, float) _cameraFieldsOfView;

	/// <summary>
	/// Скорость движения камеры без приближения и с.
	/// </summary>
	private (float, float) _cameraRotationSpeeds;

	/// <summary>
	/// Скорость приближения камеры при прицеливании.
	/// </summary>
	private readonly float _scopingSpeed = 15f;

	/// <summary>
	/// Осуществляется ли прицеливание.
	/// </summary>
	private bool _isScoping;

	/// <summary>
	/// Множитель вращения при прицеливании.
	/// </summary>
	private float _scopeSensivityScale;
	#endregion

	private void Start()
	{
		var mouseSettings = CameraManager.Instance.RetrieveAxialSettings();

		_scopeSensivityScale = mouseSettings.ScopeSensivityScale;

		_cameraComponent = CameraManager.Instance.CameraObject.GetComponent<Camera>();
		_cameraCommonMovement = CameraManager.Instance.Controller.GetComponent<CameraCommonMovement>();

		_cameraFieldsOfView = (_cameraComponent.fieldOfView, _cameraComponent.fieldOfView / 2);

		_cameraRotationSpeeds = (_cameraCommonMovement.RotationSpeed, _cameraCommonMovement.RotationSpeed * _scopeSensivityScale);

		_isScoping = false;

		PlayerManager.Instance.Controller.GetComponent<AttackController>().Aimed += () => _isScoping = true;
		PlayerManager.Instance.Controller.GetComponent<AttackController>().Unaimed += () => _isScoping = false;

		CameraManager.Instance.AxialSettingsChanged += AxialSettingsChangedHandler;
	}

	private void Update()
	{
		if (_isScoping)
		{
			if (_cameraComponent.fieldOfView != _cameraFieldsOfView.Item2)
			{
				Aim();
			}
		}
		else
		{
			if (_cameraComponent.fieldOfView != _cameraFieldsOfView.Item1)
			{
				UnAim();
			}
		}
	}

	/// <summary>
	/// Прицеливание.
	/// </summary>
	private void Aim()
	{
		_cameraComponent.fieldOfView = Mathf.Lerp(_cameraComponent.fieldOfView, _cameraFieldsOfView.Item2, _scopingSpeed * Time.deltaTime);
		_cameraCommonMovement.RotationSpeed = Mathf.Lerp(_cameraCommonMovement.RotationSpeed, _cameraRotationSpeeds.Item2, _scopingSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Отмена прицеливания.
	/// </summary>
	private void UnAim()
	{
		_cameraComponent.fieldOfView = Mathf.Lerp(_cameraComponent.fieldOfView, _cameraFieldsOfView.Item1, _scopingSpeed * Time.deltaTime);
		_cameraCommonMovement.RotationSpeed = Mathf.Lerp(_cameraCommonMovement.RotationSpeed, _cameraRotationSpeeds.Item1, _scopingSpeed * Time.deltaTime);
	}

	#region Events handlers
	private void AxialSettingsChangedHandler(CameraManager.AxialSettingsChangedEventArgs args)
	{
		if (args.ScopeSensivityScale.HasValue)
		{
			_scopeSensivityScale = args.ScopeSensivityScale.Value;
			_cameraRotationSpeeds = (_cameraCommonMovement.RotationSpeed, _cameraCommonMovement.RotationSpeed * _scopeSensivityScale);
		}
	}
	#endregion
}
