using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    private float slowdownLength = 1f;

    [SerializeField]
    private float minSpeed = .1f;

    private bool slowMo = false;

    private void Update()
    {
        slowMo = Input.GetMouseButton(0);

        float timeScale = Time.timeScale;

        // Slow or speed time up with boolean, using slowdownLength (time it takes in seconds to slow or speed time up).
        timeScale += (slowMo ? -1f : 1f) * (Time.unscaledDeltaTime * (1f / slowdownLength));

        // Clamp timeScale value between minSpeed and default value
        Time.timeScale = Mathf.Clamp(timeScale, minSpeed, 1f);

        // default is 50 times per second, that is 1/50 = .02f. This times TimeScale gives correct fixedDeltaTime for slowmotion.
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }


    public void SetSlowmotion(bool _slowMo)
    {
        slowMo = _slowMo;
    }
}
