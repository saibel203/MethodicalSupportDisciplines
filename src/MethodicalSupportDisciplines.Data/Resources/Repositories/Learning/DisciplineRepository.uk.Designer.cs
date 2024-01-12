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
    internal class DisciplineRepository_uk {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DisciplineRepository_uk() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MethodicalSupportDisciplines.Data.Resources.Repositories.Learning.DisciplineRepos" +
                            "itory.uk", typeof(DisciplineRepository_uk).Assembly);
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
        ///   Looks up a localized string similar to Дисципліну успішно створено.
        /// </summary>
        internal static string CreateSuccess {
            get {
                return ResourceManager.GetString("CreateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби створити нову дисципліну, виникла невідома помилка.
        /// </summary>
        internal static string CreateUnknownError {
            get {
                return ResourceManager.GetString("CreateUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дисципліну успішно отимано.
        /// </summary>
        internal static string DisciplineGetSuccess {
            get {
                return ResourceManager.GetString("DisciplineGetSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби отримани дисципліну з вказаним ID, виникла невідома помилка.
        /// </summary>
        internal static string DisciplineGetUnknownError {
            get {
                return ResourceManager.GetString("DisciplineGetUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дисципліну з заданим ID не знайдено.
        /// </summary>
        internal static string DisciplineNotFound {
            get {
                return ResourceManager.GetString("DisciplineNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Список дисциплін успішно отримано.
        /// </summary>
        internal static string DisciplinesForAdminSuccess {
            get {
                return ResourceManager.GetString("DisciplinesForAdminSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to При спробі отримати інформацію про дисципліни з бази даних, виникла невідома помилка.
        /// </summary>
        internal static string DisciplinesForAdminUnknownError {
            get {
                return ResourceManager.GetString("DisciplinesForAdminUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Список дисциплін успішно отримано.
        /// </summary>
        internal static string DisciplinesSuccess {
            get {
                return ResourceManager.GetString("DisciplinesSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to При спробі отримати інформацію про дисципліни з бази даних, виникла невідома помилка.
        /// </summary>
        internal static string DisciplinesUnknownError {
            get {
                return ResourceManager.GetString("DisciplinesUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неможливо перейти. Ви не є властинок дисципліни з вказаним ID.
        /// </summary>
        internal static string NotDisciplineOwner {
            get {
                return ResourceManager.GetString("NotDisciplineOwner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дисципліну успішно видалено.
        /// </summary>
        internal static string RemoveDisciplineSuccess {
            get {
                return ResourceManager.GetString("RemoveDisciplineSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби отримати та видалити дисципліну з бази даних, виникла невідома помилка.
        /// </summary>
        internal static string RemoveDisciplineUnknownError {
            get {
                return ResourceManager.GetString("RemoveDisciplineUnknownError", resourceCulture);
            }
        }
    }
}