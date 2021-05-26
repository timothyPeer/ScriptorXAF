using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Configuration;
using DevExpress.ExpressApp.DC.Xpo;
using CCRt.Module.BusinessObjects;
using DevExpress.ExpressApp.CloneObject;


namespace CCRt.Module {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.

 
    public sealed partial class CCRtModule : ModuleBase {

        private static readonly object lockObj = new object();
        private static XpoTypeInfoSource typeInfoSource1 = null;

        public CCRtModule() {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;

            RequiredModuleTypes.Add(typeof(CloneObjectModule));

        }

 
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {

            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.

           // application.Connection.ConnectionString =  CCRt.Module.Properties.Settings1.Default.connectionString;

            application.CreateCustomObjectSpaceProvider += application_CreateCustomObjectSpaceProvider;

             
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }

        void application_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e)
        {
            XafApplication application = (XafApplication)sender;
            if (typeInfoSource1 == null)
            {
                lock (lockObj)
                {
                    if (typeInfoSource1 == null)
                    {
                        typeInfoSource1 = new XpoTypeInfoSource((TypesInfo)application.TypesInfo,
                            typeof(SyBatch),
                            typeof(SyCmdClass), 
                            typeof(SyCmdDetClass),  
                            typeof(ModuleInfo1), 
                            typeof(PermissionPolicyUser), 
                            typeof(SimpleUser),
                            typeof(PermissionPolicyRole),
                            typeof(ModelDifference),
                            typeof(EventLogClass),
                            typeof(NoteClass),
                            typeof(FtpSourceClass),
                            typeof(nonPersistentConfirmationFtpNoLink),
                            typeof(MgWarehouseMaster),
                            typeof(MgPartMaster),
                            typeof(MgAccounts),
                            typeof(CommandProperties),
                            typeof(DevExpress.Persistent.BaseImpl.ReportDataV2),
                            typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
                        
                        );
                    }
                }
            }
         //   XPObjectSpaceProvider objectSpaceProvider1 = new XPObjectSpaceProvider(
          //          new ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString),
         //           application.TypesInfo,
         //           typeInfoSource1, true
         //       );
            XPObjectSpaceProvider objectSpaceProvider1 = new XPObjectSpaceProvider(
                new ConnectionStringDataStoreProvider(application.ConnectionString));
            e.ObjectSpaceProviders.Add(objectSpaceProvider1);
        }
    }
}
