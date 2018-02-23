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
* Ability to select Events
	* From "EventTriggerType": PointerDown, PointerUp, PointerClick...etc
	* From a specific GameObject (ex: onClick from Button)
* Ability to add custom arguments including: "String", "int", "float", "double", "Vector2", "Vector3", "Vector4", "GameObject", "Component", "Color". More argument types will come in the future.
* Ability to select a dynamic argument
	* Example: select the "text" property of a "InputField" component



## <a id="installation"></a>Installation

You can install Unity-EventBinder using the following method:

1. From [Github Page](https://github.com/GeorgeDascalu/Unity-EventBinder-Asset). Here you can choose between the following:

    * Unity Package: /Unity Asset/ **EventBinder[date]-[build].unitypackage** - Including a sample scene
    * Source Code: /Source Code/Assets..
