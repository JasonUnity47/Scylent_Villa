using Fungus;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerText : MonoBehaviour
{
    // Declaration
    // Specific Object Reference
    public Flowchart flowChart;
    public PlayableDirector timeLine;

    // Variable
    public string objectName;
    public string blockName;
    public string variableName;

    private void Update()
    {
        // If this gameobject's name = TextTrigger1.
        if (objectName == "TextTrigger1")
        {
            // Get the value of a boolean variable and check whether it is true.
            if (flowChart.GetBooleanVariable(variableName))
            {
                // Unfreeze the time.
                timeLine.Resume();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this gameobject's name = TextTrigger1.
        if (objectName == "TextTrigger1")
        {
            // Execute specific block.
            flowChart.ExecuteBlock(blockName);

            // Set a timer to freeze the time.
            StartCoroutine(WaitTime());
        }

        // Else if this gameobject's name = TextTrigger2.
        else if (objectName == "TextTrigger2")
        {
            // Execute specific block.
            flowChart.ExecuteBlock(blockName);
        }

        // Else if this gameobject's name = TextTrigger3.
        else if (objectName == "TextTrigger3")
        {
            // Execute specific block.
            flowChart.ExecuteBlock(blockName);
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        
        // Freeze time.
        timeLine.Pause();
    }
}
