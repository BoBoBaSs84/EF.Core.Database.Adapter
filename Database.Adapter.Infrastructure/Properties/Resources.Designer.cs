﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database.Adapter.Infrastructure.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Database.Adapter.Infrastructure.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Could not load connection {0}..
        /// </summary>
        internal static string Exception_Configuration_GetConnectionString {
            get {
                return ResourceManager.GetString("Exception.Configuration.GetConnectionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The database context could not be loaded..
        /// </summary>
        internal static string Exception_Configuration_GetContextFailed {
            get {
                return ResourceManager.GetString("Exception.Configuration.GetContextFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The database server could not be loaded..
        /// </summary>
        internal static string Exception_Configuration_GetGetSqlServerFailed {
            get {
                return ResourceManager.GetString("Exception.Configuration.GetGetSqlServerFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The configuration has already been loaded..
        /// </summary>
        internal static string Exception_Configuration_Load_AlreadyLoaded {
            get {
                return ResourceManager.GetString("Exception.Configuration.Load.AlreadyLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading of the configuration failed..
        /// </summary>
        internal static string Exception_Configuration_Load_Failed {
            get {
                return ResourceManager.GetString("Exception.Configuration.Load.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading of the configuration file failed. &apos;{0}&apos;.
        /// </summary>
        internal static string Exception_Configuration_Load_FileFailed {
            get {
                return ResourceManager.GetString("Exception.Configuration.Load.FileFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reading the configuration file information failed..
        /// </summary>
        internal static string Exception_Configuration_Load_FileReadFailed {
            get {
                return ResourceManager.GetString("Exception.Configuration.Load.FileReadFailed", resourceCulture);
            }
        }
    }
}
