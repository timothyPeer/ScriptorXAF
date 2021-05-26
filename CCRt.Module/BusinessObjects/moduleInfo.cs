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
    [MemberDesignTimeVisibility(false)]
    public class ModuleInfo1 : XPBaseObject, IModuleInfo
    {
        public ModuleInfo1(Session session) : base(session) { }
        [Key(true)]
        public int ID { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string AssemblyFileName { get; set; }
        public bool IsMain { get; set; }
        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }
    }
}