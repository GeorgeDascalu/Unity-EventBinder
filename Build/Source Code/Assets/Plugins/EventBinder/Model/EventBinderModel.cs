namespace EventBinder
{

    public enum EventArgumentType
    {
        String,
        
        Boolean,
        
        Int,
        Float,
        Double,
        
        Vector2,
        Vector3,
        Vector4,
        
        GameObject,
        Component,
        
        Color,
        
        Enum
    }


    public enum EventArgumentKind
    {
        Static = 0,
        Dynamic = 1
    }
}