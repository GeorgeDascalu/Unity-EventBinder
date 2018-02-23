using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EventBinder
{

    public enum EventTriggerArgumentType
    {
        String, 
        
        Int,
        Float,
        Double,
        
        Vector2,
        Vector3,
        Vector4,
        
        GameObject,
        Component,
        
        Color
    }


    public enum EventTriggerArgumentKind
    {
        Static = 0,
        Dynamic = 1
    }
}