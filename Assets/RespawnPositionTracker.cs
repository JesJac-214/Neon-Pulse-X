using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPositionTracker : MonoBehaviour
{
    public int courseProgress = 0;
    
    public void IncrementProgress()
    {
        courseProgress++;
    }

    public void DecrementProgress()
    {
        courseProgress--;
    }
}
