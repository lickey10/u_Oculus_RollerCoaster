using UnityEngine;

namespace BzKovSoft.VRCorrector
{
    public interface ICorrectorImpl
    {
        void Reset(Transform controller, Transform controllerPivot, float factor);
		void Update();
	}
}