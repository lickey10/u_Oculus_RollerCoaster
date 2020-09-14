using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BzKovSoft.VRCorrector
{
	public class VRCorrector : MonoBehaviour
	{
		[SerializeField]
		float _factor = 7.0f;
		[SerializeField]
		int _count = 0;
		[SerializeField]
		CorrectorMode _correctorMode;
		[SerializeField]
		string[] _controllerNames;
		[SerializeField]
		Transform[] _controllers;
		[SerializeField]
		Transform[] _controllerPivots;
		ICorrectorImpl[] _correctorsRot;

		void OnEnable ()
		{
			ApplyChanges();
		}

		void OnDisable ()
		{
			_correctorsRot = null;
		}

		public void ApplyChanges()
		{
			ValidateData();

			_correctorsRot = new ICorrectorImpl[_count];

			for (int i = 0; i < _count; i++)
			{
				ICorrectorImpl corr = null;
				switch (_correctorMode)
				{
					case CorrectorMode.Quaternion:
						corr = new ImplQuaternionBased();
						break;
					case CorrectorMode.Vector:
						corr = new ImplVectorBased();
						break;
				}
				var controller = _controllers[i];
				var controllerPivot = _controllerPivots[i];
				corr.Reset(controller, controllerPivot, _factor);

				_correctorsRot[i] = corr;
			}
		}

		public bool IsCorrect()
		{
			for (int i = 0; i < _count; i++)
			{
				var controller = _controllers[i];
				var controllerPivot = _controllerPivots[i];

				// if some values is null
				if (controller == null | controllerPivot == null)
					return false;
				
				// controller must be a child of pivot
				if (!controller.IsChildOf(controllerPivot))
					return false;

				// controller and pivot must be different objects
				if (controller == controllerPivot)
					return false;

				// one controller cannot be child of another controller
				for (int j = 0; j < _count; j++)
				{
					if (j != i && _controllerPivots[j].IsChildOf(controllerPivot))
					{
						return false;
					}
				}
			}

			if (_controllerNames.Distinct().Count() != _controllerNames.Length)
				return false;

			return true;
		}

		private void ValidateData()
		{
			if (!IsCorrect())
				throw new InvalidOperationException("Incorrect state");
			
			if (!Application.isPlaying)
				throw new InvalidOperationException("changes can be applied only in playing mode");

			if (_count != _controllerNames.Length)
				throw new InvalidOperationException("_count != _controllerNames.Length");

			if (_count != _controllers.Length)
				throw new InvalidOperationException("_count != _controllers.Length");

			if (_count != _controllerPivots.Length)
				throw new InvalidOperationException("_count != _controllerPivots.Length");
		}

		void LateUpdate ()
		{
			for (int i = 0; i < _correctorsRot.Length; i++)
			{
				var corr = _correctorsRot[i];
				corr.Update();
			}
		}
	}

	public enum CorrectorMode
	{
		Quaternion,
		Vector,
	}
}
