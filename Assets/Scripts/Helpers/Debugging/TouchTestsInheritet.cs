using UnityEngine;

namespace com.halbach.helper.debugging {
    public class TouchTestsInheritet : TouchTests
    {
        // Update is called once per frame
        void Update()
        {
            touchCount = Input.touchCount;
        }

    }
}
