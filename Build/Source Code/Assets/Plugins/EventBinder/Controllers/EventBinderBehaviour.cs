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
    public class EventBinderBehaviour : MonoBehaviour
    {
        /**PROPERTIES*/
        public GameObject targetObject;

        
        /**EVENTS & ACTIONS*/
        [HideInInspector] public int eventTypeIndex = 0;
        
        [HideInInspector] public int eventIndex  = 0; 
        [HideInInspector] public int delegateIndex = 0; 
        [HideInInspector] public EventTriggerType eventTriggerType;
        [HideInInspector] public Delegate targetDelegate; // The delegate that will be called after an UI event is proccessed

        [HideInInspector] public int eventComponentIndex;
        
        [HideInInspector] public int eventPropertyIndex;

        [HideInInspector] public UnityEventBase selectedUnityEventBase;
        [HideInInspector] public EventTriggerType selectedEventTriggerType;
        [HideInInspector] public EventTrigger.Entry eventEntry = null;

        /**ARGUMENTS*/
        [HideInInspector] public GameObject argsGameObjectTarget;
        [HideInInspector] public Component[] argsComponents;
        [HideInInspector] public int[] argsComponentIndexes;
        [HideInInspector] public int[] argsTargetsPropertiesIndexes;
        [HideInInspector] public string[] argsTargetsProperties;
        
        [HideInInspector] public EventArgumentType[] argumentTypes;
        
        [HideInInspector] public int[] argsChoiceIndexList;
        
        // STRING TYPE ARGUMENTS
        // All the arguments that were parsed to String
        [HideInInspector] public string[] stringArgs;
        
        //GAME OBJECT TYPE ARGUMENTS
        // GameObject arguments that couldn't be serialized to String
        [HideInInspector] public GameObject[] gameObjectArgs;
        [HideInInspector] public Component[] componentArgs;
        
        //OTHER TYPE ARGUMENTS
        [HideInInspector] public Color[] colorArgs;
        

        // Quick accessor to get The EventTrigger Component
        public List<EventTrigger.Entry> triggers
        {
            get { return GetComponent<EventTrigger>().triggers; }
        }
        
        
        /**METHODS*/

        private void Start()
        {
            RefreshTargetDelegate();
        }

#if UNITY_EDITOR
        
        // Sets the Taret delegate using the "actionIndex" property
        private void RefreshTargetDelegate()
        {
            FieldInfo[] fieldsCollection = typeof(EventsCollection).GetFields (BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            targetDelegate = fieldsCollection[delegateIndex].GetValue (null) as Delegate;
        }
        
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

        //Removes any event attached to the EventTrigger with connection to this object
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
                
                // IF AN EVENT WAS FOUND OR THE ENTRY LIST IS EMPTY -> REMOVE THE ENTRY 
                if (loopEntry.eventID == eventEntry.eventID && loopEntry.callback.GetPersistentEventCount() == 0)
                    indexToRemove = i;

                if (indexToRemove == -1) continue;
                
                triggers.RemoveAt (i);
                i--;
            } 
        }
        
#endif

        /*Dispatch the action with the selected arguments*/
        private void DispatchAction()
        {
            ParameterInfo[] parametersList = targetDelegate.Method.GetParameters();
            object[] argumentsObjectsList = new object[argumentTypes.Length];
            for (var index = 0; index < argumentTypes.Length; index++)
            {
                switch (argsChoiceIndexList[index])
                {
                    case (int)EventArgumentKind.Static:
                        switch (argumentTypes[index])
                        {
                            case EventArgumentType.String: argumentsObjectsList[index] = stringArgs[index]; break;
                    
                            case EventArgumentType.Boolean: argumentsObjectsList[index] = Convert.ToBoolean(stringArgs[index]); break;
                            
                            case EventArgumentType.Int:    argumentsObjectsList[index] = stringArgs[index].ParseToInt();    break;
                            case EventArgumentType.Float:  argumentsObjectsList[index] = stringArgs[index].ParseToFloat();  break;
                            case EventArgumentType.Double: argumentsObjectsList[index] = stringArgs[index].ParseToDouble(); break;
                    
                            case EventArgumentType.Vector2: argumentsObjectsList[index] = stringArgs[index].DeserializeToVector2(); break;
                            case EventArgumentType.Vector3: argumentsObjectsList[index] = stringArgs[index].DeserializeToVector3(); break;
                            case EventArgumentType.Vector4: argumentsObjectsList[index] = stringArgs[index].DeserializeToVector4(); break;
                    
                            case EventArgumentType.GameObject: argumentsObjectsList[index] = gameObjectArgs[index]; break;
                            case EventArgumentType.Component:  argumentsObjectsList[index] = componentArgs[index];  break;
                    
                            case EventArgumentType.Color: argumentsObjectsList[index] = colorArgs[index]; break;
                            
                            case EventArgumentType.Enum: argumentsObjectsList[index] = Enum.Parse(parametersList[index].ParameterType, stringArgs[index]); break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case (int) EventArgumentKind.Dynamic:
                        if (argsGameObjectTarget != null && argsComponents[index] != null && !string.IsNullOrEmpty (argsTargetsProperties[index]))
                            argumentsObjectsList[index] = argsComponents[index].GetType().GetProperty (argsTargetsProperties[index]).GetValue (argsComponents[index], null);

                        break;
                }
            }
            
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