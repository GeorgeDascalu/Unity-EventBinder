# Unity-EventBinder-Asset
EventBinder asset for Unity

## Event binder Mini-Framework for Unity3D

## <a id="introduction"></a>Introduction

The Unity-EventBinder plugin is a de-coupling event framework built to specifically to target Unity 3D. It can be used to separate the event triggers (ex: Button "onClick") from the event listeners.

This project is open source. You can find the officical repository [here](https://github.com/GeorgeDascalu/Unity-EventBinder-Asset)

For general troubleshooting / support, please post to [stack overflow](https://stackoverflow.com/questions/ask) using the tag 'UnityEventBinder'.

Or, if you have found a bug, you are also welcome to create an issue on the [github page](https://github.com/GeorgeDascalu/Unity-EventBinder-Asset), or a pull request if you have a fix / extension.


## <a id="features"></a>Features

* De-coupling
	* Separates the event "triggers" from the event "listeners"
	* Uses specific Actions to connect the triggers to the listeners
* Multiple listeners for an UI event
* Ability to add static event handlers for events - something that you cannot do with default Unity events
* Ability to select Events
	* From "EventTriggerType": PointerDown, PointerUp, PointerClick...etc
	* From a specific GameObject (ex: **onClick** from **Button**, or **onValueChanged** from **InputField**)
* Ability to add custom arguments including: "String", "int", "float", "double", "Vector2", "Vector3", "Vector4", "GameObject", "Component", "Color". More argument types will come in the future.
* Ability to select a dynamic argument
	* Example: select the "text" property of a "InputField" component


## <a id="installation"></a>Installation

You can install Unity-EventBinder using the following method:

1. From [Github Page](https://github.com/GeorgeDascalu/Unity-EventBinder-Asset). Here you can choose between the following:

    * Unity Package: /Unity Asset/ **EventBinder[date]-[build].unitypackage** - Including a sample scene
    * Source Code: /Source Code/Assets..


## <a id="usage"></a>Usage

There are 3 main components for the EventBinder plugin

1. The **EventsCollection** class
	* This is a collection with all the events that the plugin will search, display and use throughout the framework.
	* You can remove the events provided and add new ones as you would add an event normally
	* The events are **Action** type, they should be **static** & **public**.
	* It is best (yet optional) to name the arguments in your declaration (eg: **delegate (string testValue)**").
	```csharp
	public static event Action<string> EventWithStringArgument = delegate (string stringArgument) {};
	```
	

2. The **EventBinderBehaviour** class
	* Attach this to any GameObject in your scene 
	* Set the **Target Object** from which the user event should be listened from. Will default to the GameObject attached.
	* Select the **Event Type** that will be listened
		* From TargetObject: Will display a list of components and a list of Events for that component
		Currently it only supports events extending **UnityEvent**, will add support for **UnityEvent<T0,T1,T2..>** in future releases.
		* EventTrigger Type: Will listen to any event supported by the **EventTrigger** component (PointerDown, PointerEnter, PointerClick...etc) 
	* Select the **Event Delegate**
		* This will be an event from the **EventsCollection** class
		* If the event has arguments you will need to populate the arguments with values
			* **Static** - A static, hard-coded value written in the Inspector
			* **Dynamic** - The value will be retrieved at runtime from the specified component. For example: the **text** property of a **InputField**  or the rotation of an object, or the position of an object..etc. 
			Combine this with the ability of adding limitless listeners to the events and you can more easily modularize your project.


![EventBinderBehaviour](https://i.imgur.com/uzQnLFj.png)

3. The listener class - In the sample **EventBinderSampleListenerController**
	* Add a listener to any event from the class as written below:
	```csharp
	public class EventBinderSampleListenerController : MonoBehaviour
	{
	    /**LIFECYCLE*/
	    private void Start ()
	    {
	  	AddListeners();
		AddListenersStatic();
	    }
	    
	    
	    /**EVENT REGISTRATION*/
	    private void AddListeners()
	    {
	   	 EventsCollection.eventWithStringArgs  += OnEventWithStringArgsHandler;
	    }
	    
	    private static void AddListenersStatic()
	    {
	  	  EventsCollection.eventWithStringArgs  += OnStaticEventWithStringArgsHandler;
	    }
	    
	    
	    
	    /**EVENT HANDLERS*/
	    public void OnEventWithStringArgsHandler(string value)
	    {
	    	Debug.Log ("We got a new message with a String parameter: " + value);
	    }	
	    
	    
	    public void OnStaticEventWithStringArgsHandler(string value)
	    {
	    	Debug.Log ("[STATIC] We got a new message with a String parameter: " + value);
	    }	
	}
	```



## <a id="license"></a>License

    The MIT License (MIT)

    Copyright (c) 2018 George Dascalu

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
