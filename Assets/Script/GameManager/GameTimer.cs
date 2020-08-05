using UnityEngine;

public class GameTimer : MonoBehaviour
{
    float timer = 0;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start () { }

    // Update is called once per frame
    void Update ()
    {
        if (isRunning)
            UpdateTime();
    }

    void UpdateTime ()
    {
        timer += Time.deltaTime;
    }

    public float GetTime ()
    {
        return timer;
    }

    public void SetTime (float value)
    {
        timer = value;
    }

    public void StopTimer ()
    {
        isRunning = false;
    }

    public void StartTimer ()
    {
        isRunning = true;
    }

}
