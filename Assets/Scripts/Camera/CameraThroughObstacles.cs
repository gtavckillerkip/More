using More.Basic.Management;
using UnityEngine;

/// <summary>
/// Запрет прохождения камеры сквозь препятствия.
/// </summary>
public class CameraThroughObstacles : MonoBehaviour
{
	#region Fields declarations
	/// <summary>
	/// Менеджер игрока.
	/// </summary>
	private PlayerManager _playerManager;

	/// <summary>
	/// Менеджер камеры.
	/// </summary>
	private CameraManager _cameraManager;

	/// <summary>
	/// Скрипт движения камеры.
	/// </summary>
	private CameraCommonMovement _cameraCommonMovement;

	/// <summary>
	/// Буфер.
	/// </summary>
	private Collider[] _buffer = new Collider[10];
	#endregion

	private void Start()
	{
		_playerManager = PlayerManager.Instance;
		_cameraManager = CameraManager.Instance;

		_cameraCommonMovement = _cameraManager.Controller.GetComponent<CameraCommonMovement>();
	}

	private void LateUpdate()
	{
		int outer = Physics.OverlapCapsuleNonAlloc(
			_playerManager.PlayerObject.transform.position + new Vector3(0, _cameraCommonMovement.DefaultPositionOffset.y, 0),
			_cameraManager.CameraObject.transform.position,
			0.21f,
			_buffer
			);

		int inner = Physics.OverlapCapsuleNonAlloc(
			_playerManager.PlayerObject.transform.position + new Vector3(0, _cameraCommonMovement.DefaultPositionOffset.y, 0),
			_cameraManager.CameraObject.transform.position,
			0.2f,
			_buffer
			);


		if (inner != 1)
		{
			_cameraCommonMovement.CurrentPositionOffset = Vector3.Lerp(
				_cameraCommonMovement.CurrentPositionOffset,
				new Vector3(_cameraCommonMovement.DefaultPositionOffset.x / 2, _cameraCommonMovement.DefaultPositionOffset.y, 0),
				Time.deltaTime * 10
				);
		}
		else if (outer == inner)
		{
			_cameraCommonMovement.CurrentPositionOffset = Vector3.Lerp(
				_cameraCommonMovement.CurrentPositionOffset,
				_cameraCommonMovement.DefaultPositionOffset,
				Time.deltaTime * 5
				);
		}
	}
}
