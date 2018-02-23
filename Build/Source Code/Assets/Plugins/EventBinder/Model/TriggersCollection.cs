using System;
using UnityEngine;

namespace EventBinder
{
    public static class TriggersCollection
    {
        //GENERAL
        
        //SAMPLE
        public static event Action testAction = delegate { };
        
        public static event Action<string> testActionWithStringParam = delegate (string testValue) {};

        public static event Action<int, float, double> actionWithNumbersArgs = delegate(int intValue, float floatArg, double doubleArg) { };
        public static event Action<Vector2, Vector3, Vector4> actionWithVectorsArgs = delegate(Vector2 v2Value, Vector3 v3Value, Vector4 v4Value) { };
        public static event Action<GameObject, Component> actionWithGoArgs = delegate(GameObject gameObjectValue, Component componentValue) { };
    }
}