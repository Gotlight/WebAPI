//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuItemLocalization
    {
        public System.Guid ID { get; set; }
        public System.Guid LanguageID { get; set; }
        public System.Guid MenuItemID { get; set; }
        public string MenuItemLocalizationName { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
