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
    
    public partial class Language
    {
        public Language()
        {
            this.MenuCatalogLocalizations = new HashSet<MenuCatalogLocalization>();
            this.MenuItemLocalizations = new HashSet<MenuItemLocalization>();
        }
    
        public System.Guid ID { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
    
        public virtual ICollection<MenuCatalogLocalization> MenuCatalogLocalizations { get; set; }
        public virtual ICollection<MenuItemLocalization> MenuItemLocalizations { get; set; }
    }
}