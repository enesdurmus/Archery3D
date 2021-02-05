using UnityEngine;

public class SlowMotion : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    // Update is called once per frame
   /* void Update()
    {
        Time.timeScale -= (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }*/

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void ExitSlowMotion()
    {
        Time.timeScale = 1f;
    }
}
