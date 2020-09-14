using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

namespace BzKovSoft.VRCorrector.Editor
{
	public class VectorImplTests
	{
		[Test]
		public void ColliderTest()
		{
			Vector2 a = new Vector2(22f, 10f);
			Vector2 a1 = new Vector2(100f, 0f);

			float b = Vector2.Dot(a.normalized, a1.normalized);

			float r = a.magnitude * b;
			

			Assert.AreEqual(10f, r);
		}

		[Test]
		public void LinePlaneTest()
		{
			var r = ImplVectorBased.GetLinePlaneIntersection(
				Vector3.forward,
				Vector3.back,
				Vector3.zero,
				new Vector3(1f, 10, 10));
			

			Assert.AreEqual(new Vector3(0.1f, 1f, 1f), r);
		}
	}
}