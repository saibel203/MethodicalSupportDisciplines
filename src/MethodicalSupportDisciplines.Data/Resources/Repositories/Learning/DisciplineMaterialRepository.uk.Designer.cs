﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MethodicalSupportDisciplines.Data.Resources.Repositories.Learning {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DisciplineMaterialRepository_uk {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DisciplineMaterialRepository_uk() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MethodicalSupportDisciplines.Data.Resources.Repositories.Learning.DisciplineMater" +
                            "ialRepository.uk", typeof(DisciplineMaterialRepository_uk).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Матеріал для дисципліни успішно створено.
        /// </summary>
        internal static string CreateDisciplineMaterialSuccess {
            get {
                return ResourceManager.GetString("CreateDisciplineMaterialSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби створити новий матеріал для дисципліни, виникла невідома помилка.
        /// </summary>
        internal static string CreateDisciplineUnknownError {
            get {
                return ResourceManager.GetString("CreateDisciplineUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заняття дисципліни не знайдено.
        /// </summary>
        internal static string DisciplineMaterialNotFound {
            get {
                return ResourceManager.GetString("DisciplineMaterialNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заняття дисципліни успішно видалено.
        /// </summary>
        internal static string DisciplineMaterialSuccessRemove {
            get {
                return ResourceManager.GetString("DisciplineMaterialSuccessRemove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби видалити заняття дисциаліни, виникла невідома помилка.
        /// </summary>
        internal static string DisciplineMaterialUnknownError {
            get {
                return ResourceManager.GetString("DisciplineMaterialUnknownError", resourceCulture);
            }
        }
    }
}
