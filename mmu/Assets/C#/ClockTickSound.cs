using UnityEngine;

public class ClockTickSound : MonoBehaviour
{
    public AudioSource tick;

    int lastSecond = -1;

    void Update()
    {
        int current = System.DateTime.Now.Second;

        if (current != lastSecond)
        {
            tick.Play();  // ¨C¬í tick
            lastSecond = current;
        }
    }
}
