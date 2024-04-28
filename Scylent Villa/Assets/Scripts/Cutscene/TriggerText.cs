using Fungus;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerText : MonoBehaviour
{
    // Declaration
    public Flowchart flowChart;
    public PlayableDirector timeLine;

    public string objectName;
    public string blockName;
    public string variableName;

    private void Update()
    {
        if (objectName == "TextTrigger1")
        {
            if (flowChart.GetBooleanVariable(variableName))
            {
                timeLine.Resume();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectName == "TextTrigger1")
        {
            flowChart.ExecuteBlock(blockName);
            StartCoroutine(WaitTime());
        }

        else if (objectName == "TextTrigger2")
        {
            flowChart.ExecuteBlock(blockName);
        }

        else if (objectName == "TextTrigger3")
        {
            flowChart.ExecuteBlock(blockName);
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        timeLine.Pause();
    }
}
