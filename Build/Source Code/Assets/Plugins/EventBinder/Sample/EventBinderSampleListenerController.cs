using EventBinder;
using UnityEngine;

public class EventBinderSampleListenerController : MonoBehaviour {

	// Use this for initialization
    private void Start ()
	{
	    TriggersCollection.testActionWithStringParam += OnTestActionWithStringParamHandler;
	    TriggersCollection.actionWithNumbersArgs += OnTestActionWithNumberArgsHandler;
	    TriggersCollection.actionWithVectorsArgs += OnTestActionWithVectorsARgsHandler;
	    TriggersCollection.actionWithGoArgs += OnTestActionWithGoArgsHandler;
	}

    public void OnTestActionWithStringParamHandler(string value)
    {
        Debug.Log ("We listened a new message with a String parameter: " + value);
    }
    
    public void OnTestActionWithNumberArgsHandler(int intValue, float floatValue, double doubleValue)
    {
        Debug.Log ("We listened a new message with an Int parameter: " + intValue + " | " + doubleValue);
    }
    
    public void OnTestActionWithVectorsARgsHandler(Vector2 v2Value, Vector3 v3Value, Vector4 v4Value)
    {
        Debug.Log ("We listened a new message with a GameObject parameter: " + v2Value + " | " + v3Value + " | " + v4Value);
    }
    
    public void OnTestActionWithGoArgsHandler(GameObject gameObjectValue, Component componentValue)
    {
        Debug.Log ("We listened a new message with a Color parameter: " + gameObjectValue + " | " + componentValue);
    }
}
