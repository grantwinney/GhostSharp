using System;

namespace GhostSharp.Attributes
{
    /// <summary>
    /// The field name as it exists in the Ghost API
    /// </summary>
    public class GhostFieldAttribute : Attribute
    {
        public GhostFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <summary>
        /// The field name as it exists in the Ghost API
        /// </summary>
        public string FieldName { get; private set; }
    }
}
