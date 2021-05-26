
using System;
using System.Xml;
using TelEnvyXmlLib.Abstract;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// A login pair.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A login pair. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionNonPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class LoginPair : TeLSessionNonPairTag
    {
        bool usePassword;
        /// <summary>   The allowed attributes. </summary>
        readonly string[] _allowedAttributes = { "userName", "password", "haspassword","timeout" };  /* The allowed attributes */

        #region Documentation
        /// Gets the allowed attributes.
        ///
        /// \returns    The allowed attributes.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the allowed attributes. </summary>
        ///
        /// <value> The allowed attributes. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.AllowedAttributes"/>
        ///-------------------------------------------------------------------------------------------------

        protected override string[] AllowedAttributes { get { return _allowedAttributes; } }

        #region Documentation
        /// Gets a value indicating whether [name required].
        ///
        /// \returns    <c>true</c> if [name required]; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the name required. </summary>
        ///
        /// <value> True if name required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.NameRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool NameRequired { get { return false; } }

        #region Documentation
        /// Gets or sets the column.
        ///
        /// \returns    The column.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the name of the user. </summary>
        ///
        /// <value> The name of the user. </value>
        ///-------------------------------------------------------------------------------------------------

        public string userName { get; private set; }

        #region Documentation
        /// Gets or sets the row.
        ///
        /// \returns    The row.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the password. </summary>
        ///
        /// <value> The password. </value>
        ///-------------------------------------------------------------------------------------------------

        public string password { get; private set; }

        void setUsePassword (bool sendPassword=false)
        {
            
        }
        public bool UsePassword
        {
            get { return usePassword; }
            set
            {
                usePassword = value;
            }
        }
        
        #region Documentation
        /// Initializes a new instance of the <see cref="GrabLine" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.LoginPair class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public LoginPair(XmlNode node)
            : base(node, Enums.XmlTag.LoginPair)
        {
            userName = Helper.TeLSessionXmlParser.GetAttributeString(node, "username", false);
            password = Helper.TeLSessionXmlParser.GetAttributeString(node, "password", false);

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="LoginPair"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.LoginPair class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="tag">  The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        public LoginPair(XmlNode node, XmlTag tag)
            : base(node, tag)
        {
            userName = Helper.TeLSessionXmlParser.GetAttributeString(node, "username", false);
            password = Helper.TeLSessionXmlParser.GetAttributeString(node, "password", false);
            bool bHasPassword=false;
            bool.TryParse( Helper.TeLSessionXmlParser.GetAttributeString(node, "HasPassword", false), out bHasPassword);
            setUsePassword(bHasPassword);
            if (string.IsNullOrEmpty(password)) password = string.Empty;
            if (string.IsNullOrEmpty(userName)) userName = string.Empty;
        }

    }
}
