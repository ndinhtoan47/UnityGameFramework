namespace GameFramework.CustomAttribute
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PropertyType : Attribute
    {
        public readonly Type RequirementType;

        public PropertyType(Type requirementType)
        {
            RequirementType = requirementType;
        }        
    }
}