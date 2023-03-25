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
    public class ServiceErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ServiceErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.Properties.ServiceErrors", typeof(ServiceErrors).Assembly);
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
        ///   Looks up a localized string similar to An error occurred while loading the bank accounts..
        /// </summary>
        public static string AccountService_GetAll_Failed {
            get {
                return ResourceManager.GetString("AccountService.GetAll.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No bank accounts could be found to load..
        /// </summary>
        public static string AccountService_GetAll_NotFound {
            get {
                return ResourceManager.GetString("AccountService.GetAll.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the bank account with the number: &apos;{0}&apos;..
        /// </summary>
        public static string AccountService_GetByNumber_Failed {
            get {
                return ResourceManager.GetString("AccountService.GetByNumber.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No bank account with the number: &apos;{0}&apos; could be found to load..
        /// </summary>
        public static string AccountService_GetByNumber_NotFound {
            get {
                return ResourceManager.GetString("AccountService.GetByNumber.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while creating the attendance/absence..
        /// </summary>
        public static string AttendanceService_Create_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.Create.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while creating the attendances/absences..
        /// </summary>
        public static string AttendanceService_CreateMany_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.CreateMany.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while deleting the attendance/absence..
        /// </summary>
        public static string AttendanceService_Delete_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.Delete.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The presence/absence which should be deleted could not be found..
        /// </summary>
        public static string AttendanceService_Delete_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.Delete.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while deleting the attendances/absences..
        /// </summary>
        public static string AttendanceService_DeleteMany_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.DeleteMany.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The attendances/absences which should be deleted could not be found..
        /// </summary>
        public static string AttendanceService_DeleteMany_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.DeleteMany.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the attendance/absence with the calendar date: &apos;{0}&apos;..
        /// </summary>
        public static string AttendanceService_GetByDate_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.GetByDate.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No attendance/absence with calendar date: &apos;{0}&apos; could be found..
        /// </summary>
        public static string AttendanceService_GetByDate_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.GetByDate.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the attendance/absence with the calendar day identifier: &apos;{0}&apos;..
        /// </summary>
        public static string AttendanceService_GetById_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.GetById.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No attendance/absence could be found with calendar day identifier: &apos;{0}&apos;..
        /// </summary>
        public static string AttendanceService_GetById_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.GetById.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the attendance entries based on the parameterization..
        /// </summary>
        public static string AttendanceService_GetPagedByParameters_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.GetPagedByParameters.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No attendance entries could be found based on the parameterization..
        /// </summary>
        public static string AttendanceService_GetPagedByParameters_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.GetPagedByParameters.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while updating the attendance/absence..
        /// </summary>
        public static string AttendanceService_Update_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.Update.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The presence/absence which should be updated could not be found..
        /// </summary>
        public static string AttendanceService_Update_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.Update.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while updating the attendances/absences..
        /// </summary>
        public static string AttendanceService_UpdateMany_Failed {
            get {
                return ResourceManager.GetString("AttendanceService.UpdateMany.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The attendances/absences which should be updated could not be found..
        /// </summary>
        public static string AttendanceService_UpdateMany_NotFound {
            get {
                return ResourceManager.GetString("AttendanceService.UpdateMany.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while adding the user to the user role..
        /// </summary>
        public static string AuthenticationService_AddUserToRole_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.AddUserToRole.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to authenticate..
        /// </summary>
        public static string AuthenticationService_Authenticate_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.Authenticate.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to create the user..
        /// </summary>
        public static string AuthenticationService_CreateUser_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.CreateUser.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to add the roles for the user..
        /// </summary>
        public static string AuthenticationService_CreateUserRoles_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.CreateUserRoles.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the application users..
        /// </summary>
        public static string AuthenticationService_GetAll_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.GetAll.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the user with the identifier: &apos;{0}&apos;..
        /// </summary>
        public static string AuthenticationService_GetUserById_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.GetUserById.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the user with the name: &apos;{0}&apos;..
        /// </summary>
        public static string AuthenticationService_GetUserByName_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.GetUserByName.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the name: &apos;{0}&apos; could not be found..
        /// </summary>
        public static string AuthenticationService_GetUserByName_NotFound {
            get {
                return ResourceManager.GetString("AuthenticationService.GetUserByName.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while removing the user to the user role..
        /// </summary>
        public static string AuthenticationService_RemoveUserToRole_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.RemoveUserToRole.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The role with the name: &apos;{0}&apos; could not be found..
        /// </summary>
        public static string AuthenticationService_RoleByName_NotFound {
            get {
                return ResourceManager.GetString("AuthenticationService.RoleByName.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while trying to update the user..
        /// </summary>
        public static string AuthenticationService_UpdateUser_Failed {
            get {
                return ResourceManager.GetString("AuthenticationService.UpdateUser.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the name &apos;{0}&apos; could not be authorized..
        /// </summary>
        public static string AuthenticationService_User_Unauthorized {
            get {
                return ResourceManager.GetString("AuthenticationService.User.Unauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the user identifier: &apos;{0}&apos; could not be found..
        /// </summary>
        public static string AuthenticationService_UserById_NotFound {
            get {
                return ResourceManager.GetString("AuthenticationService.UserById.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user with the name: &apos;{0}&apos; could not be found..
        /// </summary>
        public static string AuthenticationService_UserByName_NotFound {
            get {
                return ResourceManager.GetString("AuthenticationService.UserByName.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the calendar day by date..
        /// </summary>
        public static string CalendarDayService_GetByDate_Failed {
            get {
                return ResourceManager.GetString("CalendarDayService.GetByDate.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No calendar day entry with the date: &apos;{0}&apos; could be found..
        /// </summary>
        public static string CalendarDayService_GetByDate_NotFound {
            get {
                return ResourceManager.GetString("CalendarDayService.GetByDate.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the calendar day by identifier..
        /// </summary>
        public static string CalendarDayService_GetById_Failed {
            get {
                return ResourceManager.GetString("CalendarDayService.GetById.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No calendar day entry with identifier: &apos;{0}&apos; could be found..
        /// </summary>
        public static string CalendarDayService_GetById_NotFound {
            get {
                return ResourceManager.GetString("CalendarDayService.GetById.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the current calendar day..
        /// </summary>
        public static string CalendarDayService_GetCurrentDate_Failed {
            get {
                return ResourceManager.GetString("CalendarDayService.GetCurrentDate.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current calendar day could not be found..
        /// </summary>
        public static string CalendarDayService_GetCurrentDate_NotFound {
            get {
                return ResourceManager.GetString("CalendarDayService.GetCurrentDate.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the calendar entries based on the parameterization..
        /// </summary>
        public static string CalendarDayService_GetPagedByParameters_Failed {
            get {
                return ResourceManager.GetString("CalendarDayService.GetPagedByParameters.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No calendar entries could be found based on the parameterization..
        /// </summary>
        public static string CalendarDayService_GetPagedByParameters_NotFound {
            get {
                return ResourceManager.GetString("CalendarDayService.GetPagedByParameters.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the card type by identifier..
        /// </summary>
        public static string CardTypeService_GetById_Failed {
            get {
                return ResourceManager.GetString("CardTypeService.GetById.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No card type entry with identifier: &apos;{0}&apos; could be found..
        /// </summary>
        public static string CardTypeService_GetById_NotFound {
            get {
                return ResourceManager.GetString("CardTypeService.GetById.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the card type day by name..
        /// </summary>
        public static string CardTypeService_GetByName_Failed {
            get {
                return ResourceManager.GetString("CardTypeService.GetByName.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No card type entry with the name: &apos;{0}&apos; could be found..
        /// </summary>
        public static string CardTypeService_GetByName_NotFound {
            get {
                return ResourceManager.GetString("CardTypeService.GetByName.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the card type entries based on the parameterization..
        /// </summary>
        public static string CardTypeService_GetPagedByParameters_Failed {
            get {
                return ResourceManager.GetString("CardTypeService.GetPagedByParameters.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No card type entries could be found based on the parameterization..
        /// </summary>
        public static string CardTypeService_GetPagedByParameters_NotFound {
            get {
                return ResourceManager.GetString("CardTypeService.GetPagedByParameters.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the day type by identifier..
        /// </summary>
        public static string DayTypeService_GetById_Failed {
            get {
                return ResourceManager.GetString("DayTypeService.GetById.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No day type entry with identifier: &apos;{0}&apos; could be found..
        /// </summary>
        public static string DayTypeService_GetById_NotFound {
            get {
                return ResourceManager.GetString("DayTypeService.GetById.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the day type day by name..
        /// </summary>
        public static string DayTypeService_GetByName_Failed {
            get {
                return ResourceManager.GetString("DayTypeService.GetByName.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No day type entry with the name: &apos;{0}&apos; could be found..
        /// </summary>
        public static string DayTypeService_GetByName_NotFound {
            get {
                return ResourceManager.GetString("DayTypeService.GetByName.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while loading the day type entries based on the parameterization..
        /// </summary>
        public static string DayTypeService_GetPagedByParameters_Failed {
            get {
                return ResourceManager.GetString("DayTypeService.GetPagedByParameters.Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No day type entries could be found based on the parameterization..
        /// </summary>
        public static string DayTypeService_GetPagedByParameters_NotFound {
            get {
                return ResourceManager.GetString("DayTypeService.GetPagedByParameters.NotFound", resourceCulture);
            }
        }
    }
}
