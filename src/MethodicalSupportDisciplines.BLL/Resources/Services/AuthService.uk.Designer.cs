﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MethodicalSupportDisciplines.BLL.Resources.Services {
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
    internal class AuthService_uk {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AuthService_uk() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MethodicalSupportDisciplines.BLL.Resources.Services.AuthService.uk", typeof(AuthService_uk).Assembly);
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
        ///   Looks up a localized string similar to Перед входом перейдіть до своєї електронної пошти та підтвердьте свою особу.
        /// </summary>
        internal static string ConfirmEmailError {
            get {
                return ResourceManager.GetString("ConfirmEmailError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби підтвердити вашу електронну адресу сталася помилка.
        /// </summary>
        internal static string ConfirmEmailUnknownError {
            get {
                return ResourceManager.GetString("ConfirmEmailUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби підтвердити електронну адресу сталася невідома помилка.
        /// </summary>
        internal static string ConfirmError {
            get {
                return ResourceManager.GetString("ConfirmError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Електронну пошту успішно підтвердженно.
        /// </summary>
        internal static string ConfirmSuccess {
            get {
                return ResourceManager.GetString("ConfirmSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не вдалося отримати роль користувача &quot;guest&quot;..
        /// </summary>
        internal static string ErrorGetRole {
            get {
                return ResourceManager.GetString("ErrorGetRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби створити нового користувача сталася помилка.
        /// </summary>
        internal static string ErrorTryCreateAccount {
            get {
                return ResourceManager.GetString("ErrorTryCreateAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувач успішно авторизувався.
        /// </summary>
        internal static string LoginSuccess {
            get {
                return ResourceManager.GetString("LoginSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби входу користувача сталася невідома помилка.
        /// </summary>
        internal static string LoginUnknownError {
            get {
                return ResourceManager.GetString("LoginUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Метод отримав неправильне значення, можливо, null.
        /// </summary>
        internal static string MethodGetIncorrectData {
            get {
                return ResourceManager.GetString("MethodGetIncorrectData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувач із вказаним ідентифікатором не знайдений.
        /// </summary>
        internal static string NoUserWithId {
            get {
                return ResourceManager.GetString("NoUserWithId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пароль користувача з указаною електронною поштою неправильний.
        /// </summary>
        internal static string PasswordForEmailIncorrect {
            get {
                return ResourceManager.GetString("PasswordForEmailIncorrect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Паролі NewPassword, ConfirmNewPassword не співпадають.
        /// </summary>
        internal static string PasswordsMatchError {
            get {
                return ResourceManager.GetString("PasswordsMatchError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувача створено успішно.
        /// </summary>
        internal static string RegisterSuccess {
            get {
                return ResourceManager.GetString("RegisterSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Запит на зміну пароля успішно надіслано на вказану адресу електронної пошти.
        /// </summary>
        internal static string RemindSuccess {
            get {
                return ResourceManager.GetString("RemindSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби надіслати електронний лист для зміни пароля сталася невідома помилка.
        /// </summary>
        internal static string RemindUnknownError {
            get {
                return ResourceManager.GetString("RemindUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби змінити пароль, виникла помилка.
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
        ///   Looks up a localized string similar to Під час спроби змінити пароль, виникла невідома помилка.
        /// </summary>
        internal static string ResetPasswordUnknownError {
            get {
                return ResourceManager.GetString("ResetPasswordUnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час спроби зареєструвати нового користувача сталася невідома помилка.
        /// </summary>
        internal static string UnknownRegisterError {
            get {
                return ResourceManager.GetString("UnknownRegisterError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувача із вказаною електронною поштою не знайдено.
        /// </summary>
        internal static string UserWithEmailNotFound {
            get {
                return ResourceManager.GetString("UserWithEmailNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Користувач не має електронної пошти.
        /// </summary>
        internal static string UserWithoutEmail {
            get {
                return ResourceManager.GetString("UserWithoutEmail", resourceCulture);
            }
        }
    }
}