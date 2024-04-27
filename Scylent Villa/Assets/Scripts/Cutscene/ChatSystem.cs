using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChatSystem : MonoBehaviour
{
    public Flowchart cut1;
    public PlayableDirector pD; 

    private void Update()
    {
        if (cut1.GetBooleanVariable("isFinished"))
        {
            pD.Resume();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stranger"))
        {
            cut1.ExecuteBlock("Cut 1");
            pD.Pause();
        }
    }
}
