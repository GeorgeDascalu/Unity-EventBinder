using EventBinder;
using UnityEngine;

public class EventBinderSampleListenerController : MonoBehaviour {

	// Use this for initialization
    private void Start ()
    {
        TriggersCollection.testAction += OnTestActionHandler;
	    TriggersCollection.testActionWithStringParam += OnTestActionWithStringParamHandler;
	    TriggersCollection.actionWithNumbersArgs += OnTestActionWithNumberArgsHandler;
	    TriggersCollection.actionWithVectorsArgs += OnTestActionWithVectorsARgsHandler;
	    TriggersCollection.actionWithGoArgs += OnTestActionWithGoArgsHandler;
	}
    
    /**HANDLERS*/

    public void OnTestActionHandler()
    {
        Debug.Log ("We got a new message ");
    }
    
    public void OnTestActionWithStringParamHandler(string value)
    {
        Debug.Log ("We got a new message with a String parameter: " + value);
    }
    
    public void OnTestActionWithNumberArgsHandler(int intValue, float floatValue, double doubleValue)
    {
        Debug.Log ("We got a new message with Number type parameters: " + intValue + " | " + doubleValue);
    }
    
    public void OnTestActionWithVectorsARgsHandler(Vector2 v2Value, Vector3 v3Value, Vector4 v4Value)
    {
        Debug.Log ("We got a new message with Vector type parameters: " + v2Value + " | " + v3Value + " | " + v4Value);
    }
    
    public void OnTestActionWithGoArgsHandler(GameObject gameObjectValue, Component componentValue)
    {
        Debug.Log ("We got a new message with a GameObject and a Component parameter: " + gameObjectValue + " | " + componentValue);
    }
}
