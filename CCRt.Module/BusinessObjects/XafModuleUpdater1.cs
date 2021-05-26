using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Updating;

namespace CCRt.Module.BusinessObjects
{
    public class XafModuleUpdater1 : ModuleUpdater
    {
        public XafModuleUpdater1(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            // Check whether it is a valid ObjectSpace to create objects of a certain type.
            if (ObjectSpace.Database.Contains("1"))
            {
                //if (ObjectSpace.CanInstantiate(typeof(PersistentClass1))) {
                string str = "test1";
                SyBatch theObject = ObjectSpace.FindObject<SyBatch>(CriteriaOperator.Parse("PersistentProperty1 = ?", str));
                if (theObject == null)
                {
                    theObject = ObjectSpace.CreateObject<SyBatch>();
                    theObject.BatchName = str;
                }
            }
        }
    }
}