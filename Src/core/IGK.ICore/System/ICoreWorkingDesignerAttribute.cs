using System;

namespace IGK.ICore
{
    /// <summary>
    /// represent a designer attribute
    /// </summary>
    public  interface ICoreWorkingDesignerAttribute
    {
        /// <summary>
        /// array of type that can be used for edition including the attached type
        /// </summary>
        Type[] Edition { get; set; }
    }
}