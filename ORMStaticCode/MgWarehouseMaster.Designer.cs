﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace CCRt.Module.BusinessObjects.Database
{

    public partial class MgWarehouseMaster : XPCustomObject
    {
        Guid fOid;
        [Key(true)]
        public Guid Oid
        {
            get { return fOid; }
            set { SetPropertyValue<Guid>(nameof(Oid), ref fOid, value); }
        }
        string fWarehouseCode;
        public string WarehouseCode
        {
            get { return fWarehouseCode; }
            set { SetPropertyValue<string>(nameof(WarehouseCode), ref fWarehouseCode, value); }
        }
        [Association(@"SyCmdClassReferencesMgWarehouseMaster")]
        public XPCollection<SyCmdClass> SyCmdClasses { get { return GetCollection<SyCmdClass>(nameof(SyCmdClasses)); } }
    }

}