using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace CCRt.Module.BusinessObjects.Database
{

    public partial class SyCmdClass
    {
        public SyCmdClass(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
