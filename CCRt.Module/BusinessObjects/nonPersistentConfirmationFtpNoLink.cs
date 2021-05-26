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
using System.Runtime.CompilerServices;

namespace CCRt.Module.BusinessObjects
{
    [DomainComponent, NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class nonPersistentConfirmationFtpNoLink : BaseObject, IXafEntityObject, INotifyPropertyChanged
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public nonPersistentConfirmationFtpNoLink(Session session)
            : base(session)
        {
            Oid = Guid.NewGuid();
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            message = "Select the Ftp Sources Tab below then select the Link icon then select an FTP Source then Retry the operation.";
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

        public nonPersistentConfirmationFtpNoLink(string message, IObjectSpace objectSpace)
        {
            Oid = Guid.NewGuid();
            this.message = message;
            this.objectSpace = objectSpace;
        }
        public nonPersistentConfirmationFtpNoLink(string message)
        {
            Oid = Guid.NewGuid();
            this.message = message;
        }
        string message;
        private IObjectSpace objectSpace;
        [DevExpress.ExpressApp.Data.Key]
        [Browsable(false)]  // Hide the entity identifier from UI.  
        public Guid Oid { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnCreated()
        {

        }

        void IXafEntityObject.OnSaving()
        {

        }

        void IXafEntityObject.OnLoaded()
        {

        }

        [XafDisplayName("")]
        [Size(SizeAttribute.Unlimited)]
        public string Message
        {
            get { return message; }
            
        }

    //    public IObjectSpace ObjectSpace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    }
}