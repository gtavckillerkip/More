using System;
using System.Reflection;
using UnityEngine;

public class ButtonsListener : MonoBehaviour
{
	public GameObject ButtonObject;

	public Action<GameObject, KeyCode> TextUpdater;

	public string FieldName;

	private KeyCode? _newValue = null;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			_newValue = KeyCode.A;
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			_newValue = KeyCode.B;
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			_newValue = KeyCode.C;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			_newValue = KeyCode.D;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			_newValue = KeyCode.E;
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			_newValue = KeyCode.F;
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			_newValue = KeyCode.G;
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			_newValue = KeyCode.H;
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			_newValue = KeyCode.I;
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			_newValue = KeyCode.J;
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			_newValue = KeyCode.K;
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			_newValue = KeyCode.L;
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			_newValue = KeyCode.M;
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			_newValue = KeyCode.N;
		}

		if (Input.GetKeyDown(KeyCode.O))
		{
			_newValue = KeyCode.O;
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			_newValue = KeyCode.P;
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			_newValue = KeyCode.Q;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			_newValue = KeyCode.R;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			_newValue = KeyCode.S;
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			_newValue = KeyCode.T;
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			_newValue = KeyCode.U;
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			_newValue = KeyCode.V;
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			_newValue = KeyCode.W;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			_newValue = KeyCode.X;
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			_newValue = KeyCode.Y;
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			_newValue = KeyCode.Z;
		}

		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			_newValue = KeyCode.Alpha0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			_newValue = KeyCode.Alpha1;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			_newValue = KeyCode.Alpha2;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			_newValue = KeyCode.Alpha3;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			_newValue = KeyCode.Alpha4;
		}

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			_newValue = KeyCode.Alpha5;
		}

		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			_newValue = KeyCode.Alpha6;
		}

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			_newValue = KeyCode.Alpha7;
		}

		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			_newValue = KeyCode.Alpha8;
		}

		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			_newValue = KeyCode.Alpha9;
		}

		if (Input.GetKeyDown(KeyCode.Minus))
		{
			_newValue = KeyCode.Minus;
		}

		if (Input.GetKeyDown(KeyCode.Equals))
		{
			_newValue = KeyCode.Equals;
		}

		if (Input.GetKeyDown(KeyCode.Tilde))
		{
			_newValue = KeyCode.Tilde;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			_newValue = KeyCode.LeftShift;
		}

		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			_newValue = KeyCode.RightShift;
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			_newValue = KeyCode.LeftControl;
		}

		if (Input.GetKeyDown(KeyCode.RightControl))
		{
			_newValue = KeyCode.RightControl;
		}

		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			_newValue = KeyCode.LeftAlt;
		}

		if (Input.GetKeyDown(KeyCode.RightAlt))
		{
			_newValue = KeyCode.RightAlt;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_newValue = KeyCode.Space;
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			_newValue = KeyCode.Tab;
		}

		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			_newValue = KeyCode.LeftBracket;
		}

		if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			_newValue = KeyCode.RightBracket;
		}

		if (Input.GetKeyDown(KeyCode.Semicolon))
		{
			_newValue = KeyCode.Semicolon;
		}

		if (Input.GetKeyDown(KeyCode.Quote))
		{
			_newValue = KeyCode.Quote;
		}

		if (Input.GetKeyDown(KeyCode.Comma))
		{
			_newValue = KeyCode.Comma;
		}

		if (Input.GetKeyDown(KeyCode.Period))
		{
			_newValue = KeyCode.Period;
		}

		if (Input.GetKeyDown(KeyCode.Slash))
		{
			_newValue = KeyCode.Slash;
		}

		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			_newValue = KeyCode.Backslash;
		}

		if (Input.GetKeyDown(KeyCode.CapsLock))
		{
			_newValue = KeyCode.CapsLock;
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			_newValue = KeyCode.Backspace;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			_newValue = KeyCode.UpArrow;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			_newValue = KeyCode.DownArrow;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_newValue = KeyCode.LeftArrow;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			_newValue = KeyCode.RightArrow;
		}

		if (Input.GetKeyDown(KeyCode.Insert))
		{
			_newValue = KeyCode.Insert;
		}

		if (Input.GetKeyDown(KeyCode.Delete))
		{
			_newValue = KeyCode.Delete;
		}

		if (Input.GetKeyDown(KeyCode.Home))
		{
			_newValue = KeyCode.Home;
		}

		if (Input.GetKeyDown(KeyCode.End))
		{
			_newValue = KeyCode.End;
		}

		if (Input.GetKeyDown(KeyCode.PageUp))
		{
			_newValue = KeyCode.PageUp;
		}

		if (Input.GetKeyDown(KeyCode.PageDown))
		{
			_newValue = KeyCode.PageDown;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_newValue = KeyCode.Mouse0;
		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			_newValue = KeyCode.Mouse1;
		}

		if (Input.GetKeyDown(KeyCode.Mouse2))
		{
			_newValue = KeyCode.Mouse2;
		}

		if (Input.GetKeyDown(KeyCode.Mouse3))
		{
			_newValue = KeyCode.Mouse3;
		}

		if (Input.GetKeyDown(KeyCode.Mouse4))
		{
			_newValue = KeyCode.Mouse4;
		}

		if (Input.GetKeyDown(KeyCode.Mouse5))
		{
			_newValue = KeyCode.Mouse5;
		}

		if (Input.GetKeyDown(KeyCode.Mouse6))
		{
			_newValue = KeyCode.Mouse6;
		}

		if (_newValue != null)
		{
			SettingsSystem.Instance.ChangeProperty(
				SettingsSystem.Instance.CurrentSettings.GetType().GetField(FieldName),
				_newValue.Value
				);

			TextUpdater?.Invoke(ButtonObject, _newValue.Value);

			_newValue = null;
			gameObject.SetActive(false);
		}
	}
}
