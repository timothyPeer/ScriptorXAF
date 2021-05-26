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
    [XafDisplayName("Adj.Account")]
    [Persistent("Accounts")]
    public partial class MgAccounts
    {
        public MgAccounts(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
