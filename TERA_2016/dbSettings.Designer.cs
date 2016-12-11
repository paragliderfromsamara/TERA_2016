﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TERA_2016 {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class dbSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static dbSettings defaultInstance = ((dbSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new dbSettings())));
        
        public static dbSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("db_tera")]
        public string dbName {
            get {
                return ((string)(this["dbName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT roles.id, roles.name FROM roles\r\n  ORDER BY roles.id ASC")]
        public string selectRoles {
            get {
                return ((string)(this["selectRoles"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"  select users.id, users.last_name, users.name, users.third_name, users.employee_number, users.password, roles.name as role_name from users LEFT JOIN roles ON (users.role_id = roles.id) WHERE users.is_active = 1 AND users.role_id > 1 
  ORDER BY users.last_name ASC")]
        public string selectActiveUsersJoinedRoles {
            get {
                return ((string)(this["selectActiveUsersJoinedRoles"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("INSERT INTO users (users.last_name, users.name, users.third_name, users.password," +
            " users.employee_number, users.role_id, users.is_active) VALUES(\"{0}\",\"{1}\",\"{2}\"" +
            ",\"{3}\",{4},{5}, 1)")]
        public string insertUser {
            get {
                return ((string)(this["insertUser"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("UPDATE users SET users.is_active = 0 \r\n\tWHERE users.id IN({0})")]
        public string hideUsers {
            get {
                return ((string)(this["hideUsers"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("UPDATE users SET users.name = \"{1}\", users.last_name = \"{2}\", users.third_name = " +
            "\"{3}\", users.employee_number = {4}, users.role_id = {5}, users.password = \"{6}\" " +
            "WHERE users.id IN({0})")]
        public string updateUserWithPassword {
            get {
                return ((string)(this["updateUserWithPassword"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("UPDATE users SET users.name = \"{1}\", users.last_name = \"{2}\", users.third_name = " +
            "\"{3}\", users.employee_number = {4}, users.role_id = {5} WHERE users.id IN({0})")]
        public string updateUserWithoutPassword {
            get {
                return ((string)(this["updateUserWithoutPassword"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT * FROM isolation_materials")]
        public string selectMaterials {
            get {
                return ((string)(this["selectMaterials"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT * FROM camera_types")]
        public string selectCameraTypes {
            get {
                return ((string)(this["selectCameraTypes"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT * FROM bringing_types")]
        public string selectBringingTypes {
            get {
                return ((string)(this["selectBringingTypes"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT * FROM devices WHERE serial_number = \"{0}\"")]
        public string selectDeviceBySerial {
            get {
                return ((string)(this["selectDeviceBySerial"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("INSERT INTO devices (serial_number) VALUES (\"{0}\")")]
        public string insertDevice {
            get {
                return ((string)(this["insertDevice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"UPDATE devices SET devices.zero_range_coeff = {1}, devices.first_range_coeff = {2}, devices.second_range_coeff = {3}, devices.third_range_coeff = {4}, devices.third_range_additional_coeff = {5}, devices.one_hundred_volts_coeff = {6}, devices.five_hundred_volts_coeff = {7}, devices.thousand_volts_coeff = {8}, devices.coeffs_check_sum = {9} WHERE devices.serial_number IN(""{0}"")")]
        public string updateRangeAndVoltageCoeffs {
            get {
                return ((string)(this["updateRangeAndVoltageCoeffs"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SELECT * FROM isolation_material_tcoeffs WHERE isolation_material_id IN ({0}) ORD" +
            "ER BY isolation_material_id ASC")]
        public string selectIsolationMaterialCoeffs {
            get {
                return ((string)(this["selectIsolationMaterialCoeffs"]));
            }
        }
    }
}
