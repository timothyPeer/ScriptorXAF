namespace CCRt.Win {
    partial class CCRtWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new CCRt.Module.CCRtModule();
            this.module4 = new CCRt.Module.Win.CCRtWindowsFormsModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationWindowsFormsModule = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.cloneObjectModule1 = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            this.reportsModuleV21 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsWindowsFormsModuleV21 = new DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2();
            this.securityStrategyComplex2 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=TIM-HPZ640\\SQLEXPRESS;Initial Catalog=xafCRU;Integrated Security=True" +
    "";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // cloneObjectModule1
            // 
            this.cloneObjectModule1.ClonerType = null;
            // 
            // reportsModuleV21
            // 
            this.reportsModuleV21.EnableInplaceReports = true;
            this.reportsModuleV21.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            // 
            // securityStrategyComplex2
            // 
            this.securityStrategyComplex2.AllowAnonymousAccess = false;
            this.securityStrategyComplex2.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex2.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache;
            this.securityStrategyComplex2.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            this.securityStrategyComplex2.UseOptimizedPermissionRequestProcessor = false;
            this.securityStrategyComplex2.UserType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // CCRtWindowsFormsApplication
            // 
            this.ApplicationName = "CCRt";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Connection = this.sqlConnection1;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.cloneObjectModule1);
            this.Modules.Add(this.reportsModuleV21);
            this.Modules.Add(this.reportsWindowsFormsModuleV21);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.validationWindowsFormsModule);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Security = this.securityStrategyComplex2;
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.CCRtWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.CCRtWindowsFormsApplication_CustomizeLanguagesList);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private CCRt.Module.CCRtModule module3;
        private CCRt.Module.Win.CCRtWindowsFormsModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWindowsFormsModule;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule1;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV21;
        private DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2 reportsWindowsFormsModuleV21;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex2;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
    }
}
