using Fungus;
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
            timeLine.Pause();
        }

        else if (objectName == "TextTrigger2")
        {
            flowChart.ExecuteBlock(blockName);
        }
    }
}
