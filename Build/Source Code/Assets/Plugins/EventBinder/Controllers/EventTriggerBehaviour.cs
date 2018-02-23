using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if UNITY_EDITOR 
using UnityEditor.Events; 
#endif

namespace EventBinder
{
    [RequireComponent(typeof(EventTrigger))]
    public class EventTriggerBehaviour : MonoBehaviour
    {
        /**PROPERTIES*/
        public GameObject targetObject;

        
        /**EVENTS & ACTIONS*/
        [HideInInspector] public int eventTypeIndex = 0;
        
        [HideInInspector] public int eventIndex  = 0; 
        [HideInInspector] public int actionIndex = 0; 
        [HideInInspector] public EventTriggerType eventTriggerType;
        [HideInInspector] public UnityEventBase unityEventBase;
        [HideInInspector] public Delegate targetDelegate;

        [HideInInspector] public int eventComponentIndex;
        [HideInInspector] public Component eventComponent;
        
        [HideInInspector] public int eventPropertyIndex;
        [HideInInspector] public string eventProperty;

        [HideInInspector] public UnityEventBase selectedUnityEventBase;
        [HideInInspector] public EventTriggerType selectedEventTriggerType;
        [HideInInspector] public EventTrigger.Entry eventEntry = null;


        /**ARGUMENTS*/
        [HideInInspector] public GameObject argsGameObjectTarget;
        [HideInInspector] public Component[] argsComponents;
        [HideInInspector] public int[] argsComponentIndexes;
        [HideInInspector] public int[] argsTargetsPropertiesIndexes;
        [HideInInspector] public string[] argsTargetsProperties;
        
        [HideInInspector] public EventTriggerArgumentType[] argumentTypes;
        
        [HideInInspector] public int[] argsChoiceIndexList;
        
        //STRING TYPE ARGUMENTS
        [HideInInspector] public string[] stringArgs;
        
        //NUMBER TYPE ARGUMENTS
        [HideInInspector] public int[] intArgs;
        [HideInInspector] public float[] floatArgs; 
        [HideInInspector] public double[] doubleArgs;
        
        //VECTOR TYPE ARGUMENTS
        [HideInInspector] public Vector2[] vector2Args;
        [HideInInspector] public Vector3[] vector3Args;
        [HideInInspector] public Vector4[] vector4Args;
        
        //GAME OBJECT TYPE ARGUMENTS
        [HideInInspector] public GameObject[] gameObjectArgs;
        [HideInInspector] public Component[] componentArgs;
        
        //OTHER TYPE ARGUMENTS
        [HideInInspector] public Color[] colorArgs;
        

        public List<EventTrigger.Entry> triggers
        {
            get { return GetComponent<EventTrigger>().triggers; }
        }
        
        
        
        /**METHODS*/

        private void Start()
        {
            RefreshTargetDelegate();
            
            Debug.Log ("Target Delegate Name: " + targetDelegate.GetType());
        }


        private void RefreshTargetDelegate()
        {
            FieldInfo[] fieldsCollection = typeof(TriggersCollection).GetFields (BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            targetDelegate = fieldsCollection[actionIndex].GetValue (null) as Delegate;
        }
        
#if UNITY_EDITOR
        public void RefreshUnityEventBase()
        {
            if(selectedUnityEventBase is UnityEvent)
            {
                UnityEventTools.RemovePersistentListener ((UnityEvent) selectedUnityEventBase, EventTriggerHandler);
                UnityEventTools.AddPersistentListener ((UnityEvent) selectedUnityEventBase, EventTriggerHandler);
            }
        }
        
        public void RefreshEventTriggerList ()
        {
            if (eventEntry != null && selectedEventTriggerType == eventTriggerType) return;
             
            ClearEventTrigger();
            
            eventEntry = triggers.FirstOrDefault (entry => entry.eventID == eventTriggerType);

            if (eventEntry == null)
            {
                eventEntry = new EventTrigger.Entry {eventID = eventTriggerType};
                triggers.Add (eventEntry);
            }
            
            selectedEventTriggerType = eventTriggerType;
            UnityEventTools.AddPersistentListener (eventEntry.callback, EventTriggerHandler);
        }

        public void ClearEventTrigger()
        { 
            if (eventEntry == null) return;
            
            //REMOVING OLD LISTENERS
            UnityEventTools.RemovePersistentListener (eventEntry.callback, EventTriggerHandler);
            
            //CLEARING EMPTY EVENT ENTRIES
            for (int i = 0; i < triggers.Count; i++)
            { 
                int indexToRemove = -1;
                EventTrigger.Entry loopEntry = triggers[i];
                
                for (int j = 0; j < loopEntry.callback.GetPersistentEventCount(); j++)
                {
                    Component targetComponent = loopEntry.callback.GetPersistentTarget (j) as Component;
                    
                    if (targetComponent == null || targetComponent.GetInstanceID() == GetInstanceID())
                        indexToRemove = i;
                }    
                
                if (loopEntry.eventID == eventEntry.eventID && loopEntry.callback.GetPersistentEventCount() == 0)
                    indexToRemove = i;

                if (indexToRemove == -1) continue;
                
                triggers.RemoveAt (i);
                i--;
            } 
        }
        
#endif


        private void DispatchAction()
        {
            Debug.Log ("Dispatch Action");
            
            object[] argumentsObjectsList = new object[argumentTypes.Length];
            for (var index = 0; index < argumentTypes.Length; index++)
            {
                switch (argsChoiceIndexList[index])
                {
                    case (int)EventTriggerArgumentKind.Static:
                        switch (argumentTypes[index])
                        {
                            case EventTriggerArgumentType.String: argumentsObjectsList[index] = stringArgs[index];break;
                    
                            case EventTriggerArgumentType.Int:    argumentsObjectsList[index] = intArgs[index];    break;
                            case EventTriggerArgumentType.Float:  argumentsObjectsList[index] = floatArgs[index];  break;
                            case EventTriggerArgumentType.Double: argumentsObjectsList[index] = doubleArgs[index]; break;
                    
                            case EventTriggerArgumentType.Vector2: argumentsObjectsList[index] = vector2Args[index]; break;
                            case EventTriggerArgumentType.Vector3: argumentsObjectsList[index] = vector3Args[index]; break;
                            case EventTriggerArgumentType.Vector4: argumentsObjectsList[index] = vector4Args[index]; break;
                    
                            case EventTriggerArgumentType.GameObject: argumentsObjectsList[index] = gameObjectArgs[index]; break;
                            case EventTriggerArgumentType.Component:  argumentsObjectsList[index] = componentArgs[index];  break;
                    
                            case EventTriggerArgumentType.Color: argumentsObjectsList[index] = colorArgs[index]; break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case (int) EventTriggerArgumentKind.Dynamic:
                        if(argsGameObjectTarget != null)
                            argumentsObjectsList[index] = argsComponents[index].GetType().GetProperty (argsTargetsProperties[index]).GetValue(argsComponents[index], null);
                        break;
                }
            }
            
            Debug.Log ("Target delegate: " + targetDelegate);
            Debug.Log ("Arguments Objects List: " + argumentsObjectsList.Length);

            targetDelegate.DynamicInvoke(argumentsObjectsList);
        }
        
        
        /**EVENT HANDLER*/
        private void EventTriggerHandler()
        {
            DispatchAction();
        }
        
        private void EventTriggerHandler(BaseEventData baseEventData)
        {
            DispatchAction();
        }
        
        
    }
}