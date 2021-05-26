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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DisplayNameAttribute = System.ComponentModel.DisplayNameAttribute;

namespace CCRt.Module.BusinessObjects
{

    [NonPersistent()]
    public abstract class SyCustomBase : XPCustomObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SyCustomBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            if (SecuritySystem.CurrentUser != null)
            {
                this.createdBy = SecuritySystem.CurrentUserName;
            }
            this.createdOn = DateTime.Now;

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {

            base.OnChanged(propertyName, oldValue, newValue);
           this.updatedOn = DateTime.Now;
            if (SecuritySystem.CurrentUser != null)
            {
                this.updatedBy = SecuritySystem.CurrentUserName;
            }
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
        }

        protected override void OnLoading()
        {
            base.OnLoading();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            this.updatedBy = SecuritySystem.CurrentUserName;
            this.updatedOn = DateTime.Now;
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


 
string updatedBy;
        DateTime createdOn;
        DateTime updatedOn;
        string createdBy;
        [DisplayName("CreatedBy")]
        public string CreatedBy
        {
           get => createdBy;
           protected set => SetPropertyValue(nameof(CreatedBy), ref createdBy, value);
        }
        [DisplayName("CreatedDate")]
        public DateTime CreatedOn
        {
            get => createdOn;
           protected set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DisplayName("UpdatedBy")]
        public         string UpdatedBy
        {
            get => updatedBy;
           protected  set => SetPropertyValue(nameof(UpdatedBy), ref updatedBy, value);
        }
        [DisplayName("UpdateDate")]
        public DateTime UpdatedOn

        {
            get => updatedOn;
            protected set => SetPropertyValue(nameof(UpdatedOn), ref updatedOn, value);
        }

       
        public static OidInitializationMode OidInitializationMode { get; set; }
        public override string ToString()
        {
            return base.ToString(); 
        }
    }
}