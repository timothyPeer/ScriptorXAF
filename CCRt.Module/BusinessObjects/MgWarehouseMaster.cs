using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace CCRt.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Organization")]
    [NavigationItem("Lists")]
    [XafDisplayName("Warehouse")]

    [Persistent("Warehouse")]
    public partial class MgWarehouseMaster
    {
        public MgWarehouseMaster(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
