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
    [ImageName("BO_Transition")]
    [NavigationItem("Lists")]
    [XafDisplayName("Parts")]
    [Persistent("PartList")]
    public partial class MgPartMaster
    {
        public MgPartMaster(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        
    }

}
