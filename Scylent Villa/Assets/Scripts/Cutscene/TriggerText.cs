using Fungus;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerText : MonoBehaviour
{
    // Declaration
    public Flowchart flowChart;
    public PlayableDirector timeLine;

    public string[] blockName;
    public string variableName;

    private void Update()
    {
        if (flowChart.GetBooleanVariable(variableName))
        {
            timeLine.Resume();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flowChart.ExecuteBlock(blockName[0]);
        timeLine.Pause();
    }
}
