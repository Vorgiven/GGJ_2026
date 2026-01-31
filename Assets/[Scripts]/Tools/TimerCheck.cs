using UnityEngine;

[System.Serializable]
public class TimerCheck
{
    [SerializeField] private float timeLast = 1f;
    [SerializeField] private bool autoReset = true;
    [SerializeField] private float timeElapsed = 0;

    //Constructor
    public TimerCheck() { }
    public TimerCheck(float timeLast, bool resetTimer = true)
    {
        this.timeLast = timeLast;
        this.autoReset = resetTimer;
    }
    // Check timer if condition in parameter is true
    // This should be call in update
    public bool UpdateTimer(bool condition = true)
    {
        if (condition)
        {
            if (timeElapsed >= timeLast)
            {
                // Reset timer if its true
                if (autoReset)
                    ResetTimer();
                // If timer done returns true;
                return true;
            }
            // Increase time 
            else
                timeElapsed += Time.deltaTime;
        }
        // Return false if timer has not done
        return false;
    }

    // Set time
    public void SetTime(float n) => timeLast = n;
    // Set reset timer
    public void SetAutoResetTimer(bool set) => autoReset = set;

    // Get the time last
    public float GetTimeLast() => timeLast;
    // Get the current timeElapsed
    public float GetTimeLastElapsed() => timeElapsed;

    // Get the percentage timer returns 0 to 1;
    public float GetPercentage() => timeElapsed / timeLast;
    // Reset Timer
    public void ResetTimer() => timeElapsed = 0;
}
