using EventBinder;
using UnityEngine;

public class EventBinderSampleListenerController : MonoBehaviour {

	// Use this for initialization
    private void Start ()
    {
        AddListeners();
        AddListenersStatic();
	}

    /**EVENT REGISTRATION*/
    private void AddListeners()
    {
        EventsCollection.eventEmpty           += OnEventEmptyHandler;
        EventsCollection.eventWithStringArgs  += OnEventWithStringArgsHandler;
        EventsCollection.eventWithBoolArgs    += OnEventWithBoolArgsHandler;
        EventsCollection.eventWithNumbersArgs += OnEventWithNumberArgsHandler;
        EventsCollection.eventWithVectorsArgs += OnEventWithVectorsARgsHandler;
        EventsCollection.eventWithGoArgs      += OnEventWithGoArgsHandler;
    }

    private static void AddListenersStatic()
    {
        EventsCollection.eventEmpty           += OnStaticEventEmptyHandler;
        EventsCollection.eventWithStringArgs  += OnStaticEventWithStringArgsHandler;
        EventsCollection.eventWithBoolArgs    += OnStaticEventWithBoolArgsHandler;
        EventsCollection.eventWithNumbersArgs += OnStaticEventWithNumberArgsHandler;
        EventsCollection.eventWithVectorsArgs += OnStaticEventWithVectorsARgsHandler;
        EventsCollection.eventWithGoArgs      += OnStaticEventWithGoArgsHandler;
    }
    
    /**EVENT HANDLERS*/
    public void OnEventEmptyHandler()
    {
        Debug.Log ("We got a new message ");
    }
    
    public void OnEventWithStringArgsHandler(string value)
    {
        Debug.Log ("We got a new message with a String parameter: " + value);
    }
    
    public void OnEventWithBoolArgsHandler(bool boolValue1, bool boolValue2)
    {
        Debug.Log ("We got a new message with Boolean type parameters: " + boolValue1 + " | " + boolValue2);
    }

    public void OnEventWithNumberArgsHandler(int intValue, float floatValue, double doubleValue)
    {
        Debug.Log ("We got a new message with Number type parameters: " + intValue + " | " + floatValue + " | " + doubleValue);
    }
    
    public void OnEventWithVectorsARgsHandler(Vector2 v2Value, Vector3 v3Value, Vector4 v4Value)
    {
        Debug.Log ("We got a new message with Vector type parameters: " + v2Value + " | " + v3Value + " | " + v4Value);
    }
    
    public void OnEventWithGoArgsHandler(GameObject gameObjectValue, Component componentValue)
    {
        Debug.Log ("We got a new message with a GameObject and a Component parameter: " + gameObjectValue + " | " + componentValue);
    }
    
    /**EVENT HANDLERS STATIC*/
    public static void OnStaticEventEmptyHandler()
    {
        Debug.Log ("[STATIC] We got a new message ");
    }
    
    public static void OnStaticEventWithStringArgsHandler(string value)
    {
        Debug.Log ("[STATIC] We got a new message with a String parameter: " + value);
    }
    
    public static void OnStaticEventWithBoolArgsHandler(bool boolValue1, bool boolValue2)
    {
        Debug.Log ("[STATIC] We got a new message with Boolean type parameters: " + boolValue1 + " | " + boolValue2);
    }
    
    public static void OnStaticEventWithNumberArgsHandler(int intValue, float floatValue, double doubleValue)
    {
        Debug.Log ("[STATIC] We got a new message with Number type parameters: " + intValue + " | " + floatValue + " | " + doubleValue);
    }
    
    public static void OnStaticEventWithVectorsARgsHandler(Vector2 v2Value, Vector3 v3Value, Vector4 v4Value)
    {
        Debug.Log ("[STATIC] We got a new message with Vector type parameters: " + v2Value + " | " + v3Value + " | " + v4Value);
    }
    
    public static void OnStaticEventWithGoArgsHandler(GameObject gameObjectValue, Component componentValue)
    {
        Debug.Log ("[STATIC] We got a new message with a GameObject and a Component parameter: " + gameObjectValue + " | " + componentValue);
    }
}
