﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MethodicalSupportDisciplines.Shared.Resources.ViewModels.Forms.Auth {
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
    internal class RegisterViewModel_en {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegisterViewModel_en() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MethodicalSupportDisciplines.Shared.Resources.ViewModels.Forms.Auth.RegisterViewM" +
                            "odel.en", typeof(RegisterViewModel_en).Assembly);
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
        ///   Looks up a localized string similar to ConfirmPassword field is required.
        /// </summary>
        internal static string ConfirmPasswordRequired {
            get {
                return ResourceManager.GetString("ConfirmPasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email field is required.
        /// </summary>
        internal static string EmailRequired {
            get {
                return ResourceManager.GetString("EmailRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The minimum password length in 6 characters.
        /// </summary>
        internal static string PasswordMinLength {
            get {
                return ResourceManager.GetString("PasswordMinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password must contain at least one number, one uppercase letter, and one lowercase letter.
        /// </summary>
        internal static string PasswordRegularExpression {
            get {
                return ResourceManager.GetString("PasswordRegularExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password field is required.
        /// </summary>
        internal static string PasswordRequired {
            get {
                return ResourceManager.GetString("PasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Passwords do not match.
        /// </summary>
        internal static string PasswordsNotMatch {
            get {
                return ResourceManager.GetString("PasswordsNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The length of the Username field must be between 6 and 20 characters.
        /// </summary>
        internal static string UsernameLength {
            get {
                return ResourceManager.GetString("UsernameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username field is required.
        /// </summary>
        internal static string UsernameRequired {
            get {
                return ResourceManager.GetString("UsernameRequired", resourceCulture);
            }
        }
    }
}