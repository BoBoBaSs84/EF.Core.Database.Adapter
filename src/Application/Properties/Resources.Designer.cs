﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to authenticate..
        /// </summary>
        public static string AuthenticationServiceErrors_AuthenticateFailed {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.AuthenticateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to create the user..
        /// </summary>
        public static string AuthenticationServiceErrors_CreateUserFailed {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.CreateUserFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to add the roles for the user..
        /// </summary>
        public static string AuthenticationServiceErrors_CreateUserRolesFailed {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.CreateUserRolesFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to update the user..
        /// </summary>
        public static string AuthenticationServiceErrors_UpdateUserFailed {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.UpdateUserFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the user name &apos;{0}&apos; could not be found..
        /// </summary>
        public static string AuthenticationServiceErrors_UserNotFound {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.UserNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the user name &apos;{0}&apos; could not be authorized..
        /// </summary>
        public static string AuthenticationServiceErrors_UserUnauthorized {
            get {
                return ResourceManager.GetString("AuthenticationServiceErrors.UserUnauthorized", resourceCulture);
            }
        }
    }
}
