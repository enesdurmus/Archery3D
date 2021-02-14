using UnityEngine;

public class SlowMotion : MonoBehaviour
{

    private float slowDownFactor = 0.4f;

    /* void Update()
     {
         if (isTimeSlowed)
         {
             Time.timeScale -= (1f / slowdownLength) * Time.unscaledDeltaTime;
             Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
         }

     }*/
    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void DoSlowMotion(float slowDownFactor)
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ExitSlowMotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }
}
