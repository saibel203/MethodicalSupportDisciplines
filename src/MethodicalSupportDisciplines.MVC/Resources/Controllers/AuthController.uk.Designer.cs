﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MethodicalSupportDisciplines.MVC.Resources.Controllers {
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
    internal class AuthController_uk {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AuthController_uk() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MethodicalSupportDisciplines.MVC.Resources.Controllers.AuthController.uk", typeof(AuthController_uk).Assembly);
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
        ///   Looks up a localized string similar to Виникла помилка при спробі підтвердити Email.
        /// </summary>
        internal static string ConfirmEmailError {
            get {
                return ResourceManager.GetString("ConfirmEmailError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email успішно підтверджено.
        /// </summary>
        internal static string ConfirmEmailSuccess {
            get {
                return ResourceManager.GetString("ConfirmEmailSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка при спробі авторизуватися.
        /// </summary>
        internal static string LoginError {
            get {
                return ResourceManager.GetString("LoginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ви успішно авторизувалися.
        /// </summary>
        internal static string LoginSuccess {
            get {
                return ResourceManager.GetString("LoginSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ви успішно вийшли з акаунту.
        /// </summary>
        internal static string LogoutSuccess {
            get {
                return ResourceManager.GetString("LogoutSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка при спробі зареєструватися.
        /// </summary>
        internal static string RegisterError {
            get {
                return ResourceManager.GetString("RegisterError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувача було успішно створено.
        /// </summary>
        internal static string RegisterSuccess {
            get {
                return ResourceManager.GetString("RegisterSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Виникла помилка при спробі відправити запит на відновлення паролю.
        /// </summary>
        internal static string RemindPasswordError {
            get {
                return ResourceManager.GetString("RemindPasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Запит на відновлення паролю надіслано. Перевірте ваш Email.
        /// </summary>
        internal static string RemindPasswordSuccess {
            get {
                return ResourceManager.GetString("RemindPasswordSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка при спробі відновити пароль.
        /// </summary>
        internal static string ResetPasswordError {
            get {
                return ResourceManager.GetString("ResetPasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пароль успішно змінено.
        /// </summary>
        internal static string ResetPasswordSuccess {
            get {
                return ResourceManager.GetString("ResetPasswordSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувача не знайдено або виникла помилка передачі даних.
        /// </summary>
        internal static string UserNotFoundError {
            get {
                return ResourceManager.GetString("UserNotFoundError", resourceCulture);
            }
        }
    }
}
