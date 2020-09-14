using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BzKovSoft.VRCorrector
{
	public class ImplQuaternionBased : ICorrectorImpl
	{
		Transform _controller;
		Transform _controllerPivot;
		Quaternion _ctrlrRotPrev;
		Quaternion _ctrlrRotPrev2;

		float _factor = 1.0f;


		public void Reset(Transform controller, Transform controllerPivot, float factor)
		{
			_controller = controller;
			_controllerPivot = controllerPivot;
			_factor = factor;
			_ctrlrRotPrev = _controller.localRotation;
			_ctrlrRotPrev2 = _ctrlrRotPrev;
		}

		public void Update ()
		{
			Quaternion currRot = _controller.localRotation;

			Quaternion deltaRot = currRot * Quaternion.Inverse(_ctrlrRotPrev);
			Quaternion deltaRot2 = _ctrlrRotPrev * Quaternion.Inverse(_ctrlrRotPrev2);
			float angle  = Quaternion.Angle(Quaternion.identity, deltaRot);
			float angle2 = Quaternion.Angle(Quaternion.identity, deltaRot2);
			float acceleration = angle2 / angle;

			_ctrlrRotPrev2 = _ctrlrRotPrev;
			_ctrlrRotPrev = currRot;

			if (angle == 0f)
			{
				if (angle2 != 0f)
				{
					_controllerPivot.localRotation = Quaternion.identity;
				}
				return;
			}

			if (angle2 == 0f)
				acceleration = 2f;

			float t = _factor * acceleration;
			var correcting = Quaternion.LerpUnclamped(Quaternion.identity, deltaRot, t);

			_controllerPivot.localRotation = correcting;
		}
	}
}
