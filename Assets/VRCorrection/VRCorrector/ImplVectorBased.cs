using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BzKovSoft.VRCorrector
{
	public class ImplVectorBased : ICorrectorImpl
	{
		Transform _controller;
		Transform _controllerPivot;
		Quaternion _ctrlrRotPrev;
		Quaternion _ctrlrRotPrev2;

		float _factor = 1.0f;


		Vector2 _prevRotPos;
		Vector2 _prevRotPos2;

		public void Reset(Transform controller, Transform controllerPivot, float factor)
		{
			_controller = controller;
			_controllerPivot = controllerPivot;
			_factor = factor;

			_prevRotPos = Vector2.zero;
			_prevRotPos2 = Vector2.zero;
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

			Vector2 currRotPos = Vector2.zero;

			if (angle != 0f)
			{
				float acceleration = angle2 / angle;
				
				Vector3 currRotPos3 = deltaRot * Vector3.forward;

				currRotPos3 = GetLinePlaneIntersection(Vector3.forward, Vector3.back, Vector3.zero, currRotPos3);

				currRotPos = currRotPos3;

				Vector3 v3;
				if (angle2 == 0f)
				{
					v3 = currRotPos * 2f;
				}
				else
				{
					Vector2 p1 = _prevRotPos2;
					Vector2 p2 = _prevRotPos + p1;
					Vector2 p3 = currRotPos + p2;
					Vector2 v1 = p2 - p1;
					Vector2 v2 = p3 - p2;

					var vectorRot = Quaternion.FromToRotation(v1, v2);
					v3 = (vectorRot * v2) * _factor * acceleration;
				}

				Debug.Assert(v3.z == 0f);
				v3.z = 1;

				Quaternion correcting = Quaternion.FromToRotation(Vector3.forward, v3);

				_controllerPivot.localRotation = correcting;

				_prevRotPos2 = _prevRotPos;
				_prevRotPos = currRotPos;

				_ctrlrRotPrev2 = _ctrlrRotPrev;
				_ctrlrRotPrev = currRot;
			}
			else if (angle2 != 0f)
			{
				_controllerPivot.localRotation = Quaternion.identity;
				_prevRotPos2 = currRotPos;
				_prevRotPos = currRotPos;

				_ctrlrRotPrev2 = currRot;
				_ctrlrRotPrev = currRot;
			}
		}

		public static Vector3 GetLinePlaneIntersection(Vector3 p0, Vector3 n, Vector3 l0, Vector3 l)
		{
			Vector3 currRotPos3;
			float ld = Vector3.Dot(n, l);
			float p0l0n = Vector3.Dot((p0 - l0), n);
			if (ld == 0f)
			{
				//if (p0l0n == 0f)
				currRotPos3 = p0;
			}
			else
			{
				float d = p0l0n / ld;
				currRotPos3 = d * l + l0;
			}

			return currRotPos3;
		}
	}
}
