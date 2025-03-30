using More.Basic.Management;
using System;
using UnityEngine;

/// <summary>
/// Контроллер движения.
/// </summary>
public class MovementController : MonoBehaviour
{
	#region Definitions
	/// <summary>
	/// Эффекты, влияющие на движение.
	/// </summary>
	[Flags]
	private enum MovementEffect
	{
		/// <summary>
		/// Нет.
		/// </summary>
		None = 0,

		/// <summary>
		/// Удержание.
		/// </summary>
		Hold = 1,

		/// <summary>
		/// Замедление.
		/// </summary>
		Slowdown = 2,

		/// <summary>
		/// Ускорение.
		/// </summary>
		Speedup = 4,
	}

	private class Acceleration
	{
		public static readonly float DefaultDeltaArg = Time.fixedDeltaTime * 2f;

        public Acceleration(Func<float, float> function, float maxValue, float minValue, float dArg)
        {
            Function = function;
			Argument = 0;
			DA = 0;
			MaxValue = maxValue;
			MinValue = minValue;

			for (Duration = 0; Function(Duration) >= 0 && Function(Duration) <= 1; Duration += dArg);
        }

        public Func<float, float> Function { get; private set; }
		public float Argument { get; set; }
		public float DA { get; set; }
		public float MaxValue { get; private set; }
		public float MinValue { get; private set; }
		public float Duration { get; private set; }
    }
	#endregion

	#region Fields declarations
	/// <summary>
	/// Выражение функции ускорения.
	/// </summary>
	private readonly Func<float, float> AccelerationStatement = (arg) => Mathf.Exp(arg * 2) * Mathf.Log(arg + 1);

	/// <summary>
	/// Выражение функции замедления.
	/// </summary>
	private readonly Func<float, float> DeccelerationStatement = (arg) => 1 - Mathf.Exp(arg * 2) * Mathf.Log(arg + 1);

	/// <summary>
	/// Объект игрового персонажа.
	/// </summary>
	private GameObject _characterObject;

	/// <summary>
	/// Установщик направления.
	/// </summary>
	/// <remarks> Например, камера, смотрящая вперёд. </remarks>
	private GameObject _directionSetter;

	/// <summary>
	/// Отслеживатель соприкосновения персонажа с землёй.
	/// </summary>
	private GameObject _groundChecker;

	/// <summary>
	/// Скорость поворота персонажа при включении приближения.
	/// </summary>
	private float _onAimingRotationSpeed = 30f;

	/// <summary>
	/// Базовые характеристики движения.
	/// </summary>
	private static readonly (float MoveForce, float JumpForce) _basicMoveStats = (5f, 200f);

	/// <summary>
	/// Сила движения.
	/// </summary>
	private float _moveForce;

	/// <summary>
	/// Сила прыжка.
	/// </summary>
	private float _jumpForce;

	/// <summary>
	/// Скорость вращения.
	/// </summary>
	private float _rotationSpeed = 15f;

	/// <summary>
	/// Ускоряющий движение множитель-функция.
	/// </summary>
	private float _aMultiplier = 0;

	/// <summary>
	/// Ускорение.
	/// </summary>
	private Acceleration _acceleration;

	/// <summary>
	/// Замедление.
	/// </summary>
	private Acceleration _decceleration;

	/// <summary>
	/// Коэффициент "яйцо".
	/// </summary>
	/// <remarks> Вносит корректировку скорости бега в зависимости от направления. </remarks>
	private float _egg = 0;

	/// <summary>
	/// Физический компонент.
	/// </summary>
	private Rigidbody _playerRigidbody;

	/// <summary>
	/// Персонаж на земле.
	/// </summary>
	private bool _isGrounded = false;

	/// <summary>
	/// Персонаж прицеливается.
	/// </summary>
	private bool _isAiming = false;

	/// <summary>
	/// Вектор ввода движения.
	/// </summary>
	private Vector3 _movementInput = Vector3.zero;

	/// <summary>
	/// Вектор направления движения.
	/// </summary>
	private Vector3 _movementDirection = Vector3.zero;

	/// <summary>
	/// Эффекты, влияющие на движение.
	/// </summary>
	private MovementEffect _movementEffects = MovementEffect.None;
	#endregion

	private void Start()
	{
		_characterObject = PlayerManager.Instance.PlayerObject;
		_directionSetter = PlayerManager.Instance.DirectionSetter;
		_groundChecker = PlayerManager.Instance.GroundChecker;

		#region Acceleration/decceleration
		_acceleration = new Acceleration(AccelerationStatement, 1, 0, Time.fixedDeltaTime);
		_acceleration.Argument = 0;
		_acceleration.DA = Acceleration.DefaultDeltaArg;
		_decceleration = new Acceleration(DeccelerationStatement, 1, 0, Time.fixedDeltaTime);
		_decceleration.Argument = _decceleration.Duration;
		_decceleration.DA = Acceleration.DefaultDeltaArg;
		#endregion

		_playerRigidbody = _characterObject.GetComponent<Rigidbody>();

		CalculateMovementStats();

		GetComponent<AttackController>().Aimed += () =>
		{
			_isAiming = true;
			_movementEffects |= MovementEffect.Slowdown;
			CalculateMovementStats();
		};
		GetComponent<AttackController>().Unaimed += () =>
		{
			_isAiming = false;
			_movementEffects &= ~MovementEffect.Slowdown;
			CalculateMovementStats();
		};

		#region Movement controls events
		InputSystem.Instance.MoveForwardPressed += () => { _movementInput.z++; EggCorrection(); };
		InputSystem.Instance.MoveForwardReleased += () => { _movementInput.z--; EggCorrection(); };
		InputSystem.Instance.MoveBackwardPressed += () => { _movementInput.z--; EggCorrection(); };
		InputSystem.Instance.MoveBackwardReleased += () => { _movementInput.z++; EggCorrection(); };
		InputSystem.Instance.MoveLeftPressed += () => { _movementInput.x--; EggCorrection(); };
		InputSystem.Instance.MoveLeftReleased += () => { _movementInput.x++; EggCorrection(); };
		InputSystem.Instance.MoveRightPressed += () => { _movementInput.x++; EggCorrection(); };
		InputSystem.Instance.MoveRightReleased += () => { _movementInput.x--; EggCorrection(); };
		InputSystem.Instance.JumpPressed += Jump;
		InputSystem.Instance.SlowdownPressed += () =>
		{
			_movementEffects |= MovementEffect.Slowdown;
			CalculateMovementStats();
		};
		InputSystem.Instance.SlowdownReleased += () =>
		{
			_movementEffects &= ~MovementEffect.Slowdown;
			CalculateMovementStats();
		};
		#endregion
	}

	private void FixedUpdate()
	{
		_isGrounded = Physics.OverlapSphere(_groundChecker.transform.position, 0.1f).Length > 1;

		#region Whether grounded or not, move
		if (_movementInput != Vector3.zero)
		{
			MoveControlled();
		}
		else
		{
			MoveUncontrolled();
		}
		#endregion

		#region If aiming then rotate
		if (_isAiming && _isGrounded)
		{
			Rotate(Quaternion.Euler(0, _directionSetter.transform.eulerAngles.y, 0), _onAimingRotationSpeed);
		}
		#endregion
	}

	/// <summary>
	/// Контролируемое движение.
	/// </summary>
	private void MoveControlled()
	{
		if (_isGrounded)
		{
			Rotate(Quaternion.Euler(0, _directionSetter.transform.rotation.eulerAngles.y, 0), _rotationSpeed);

			if (Mathf.Cos(Vector3.Angle(_movementInput, _movementDirection) / 180 * Mathf.PI) >= 0)
			{
				_decceleration.DA = Acceleration.DefaultDeltaArg;

				if (_aMultiplier < 1)
				{
					Accel();
				}

				_movementDirection = _movementInput;
			}
			else
			{
				_decceleration.DA = Acceleration.DefaultDeltaArg * 2;

				if (_aMultiplier > 0)
				{
					Deccel();
				}
			}

			var moveVector = _characterObject.transform.rotation * _movementDirection.normalized * _moveForce * _aMultiplier * _egg;
			_playerRigidbody.velocity = new Vector3(moveVector.x, _playerRigidbody.velocity.y, moveVector.z);

			if (moveVector == Vector3.zero)
			{
				_movementDirection = Vector3.zero;
			}
        }
	}

	/// <summary>
	/// Неконтролируемое движение.
	/// </summary>
	private void MoveUncontrolled()
	{
		if (_isGrounded)
		{
			if (_aMultiplier > 0)
			{
				Deccel();
			}

			var moveVector = _playerRigidbody.velocity.normalized * _moveForce * _aMultiplier;
			_playerRigidbody.velocity = new Vector3(moveVector.x, _playerRigidbody.velocity.y, moveVector.z);
		}
	}

	/// <summary>
	/// Прыжок игрового персонажа.
	/// </summary>
	private void Jump()
	{
		if (_isGrounded)
		{
			_playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
		}
	}

	/// <summary>
	/// Поворот игрового персонажа.
	/// </summary>
	/// <param name="rotateTo"> Кватернион поворота. </param>
	/// <param name="rotationSpeed"> Скорость поворота. </param>
	public void Rotate(Quaternion rotateTo, float rotationSpeed)
	{
		_characterObject.transform.rotation = Quaternion.Lerp(_characterObject.transform.rotation, rotateTo, rotationSpeed * Time.fixedDeltaTime);
	}

	/// <summary>
	/// Ускорение.
	/// </summary>
	private void Accel()
	{
		_aMultiplier = Mathf.Clamp(_acceleration.Function(_acceleration.Argument), 0, 1);

		_acceleration.Argument += _acceleration.DA;

		while (_decceleration.Function(_decceleration.Argument - _acceleration.DA) < _aMultiplier)
		{
			_decceleration.Argument -= _acceleration.DA;
		}
	}

	/// <summary>
	/// Замедление.
	/// </summary>
	private void Deccel()
	{
		_aMultiplier = Mathf.Clamp(_decceleration.Function(_decceleration.Argument), 0, 1);

		_decceleration.Argument += _decceleration.DA;

		while (_acceleration.Function(_acceleration.Argument) > _aMultiplier)
		{
			_acceleration.Argument -= _decceleration.DA;
		}
	}

	/// <summary>
	/// Расчитать характеристики движения.
	/// </summary>
	private void CalculateMovementStats()
	{
		_moveForce = _basicMoveStats.MoveForce;
		_jumpForce = _basicMoveStats.JumpForce;

		if (_movementEffects == MovementEffect.None)
		{
			return;
		}

		if (_movementEffects.HasFlag(MovementEffect.Hold))
		{
			_moveForce = 0;
			_jumpForce = 0;
		}

		if (_movementEffects.HasFlag(MovementEffect.Slowdown))
		{
			_moveForce *= 0.5f;
			_jumpForce *= 0.75f;
		}

		if (_movementEffects.HasFlag(MovementEffect.Speedup))
		{
			_moveForce *= 2;
		}
	}

	/// <summary>
	/// Корректировка "яйца".
	/// </summary>
	private void EggCorrection()
	{
		if (_movementEffects.HasFlag(MovementEffect.Slowdown))
		{
			_egg = 0.8f;
			return;
		}

		if (_movementInput.z == 1 && _movementInput.x == 0)
		{
			_egg = 1f;
			return;
		}

		if (_movementInput.z == 1 && _movementInput.x != 0)
		{
			_egg = 0.9f;
			return;
		}

		_egg = 0.8f;
	}
}
