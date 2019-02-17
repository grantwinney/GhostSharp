using System;

namespace GhostSharp.Attributes
{
    public class GhostFieldAttribute : Attribute
    {
        public GhostFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; private set; }
    }
}
