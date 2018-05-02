public class Timer  {
    float time;
    float startTime;
    public Timer(float StartTime)
    {
        startTime = StartTime;
        time = startTime;
    }
    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }
    public void AddTime(float dt)
    {
        time += dt;
    }
    public void SubtractTime(float dt)
    {
        time -= dt;
    }
    public void ResetTimer()
    {
        time = startTime;
    }

}
