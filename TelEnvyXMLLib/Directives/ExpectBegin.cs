

using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// An expect begin.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An expect begin. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class ExpectBegin : TelEnvyXmlLib.Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Grab", "Timeout", };   /* The allowed attributes */

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

        protected override bool NameRequired { get { return TeLNode.Attributes["Grab"] != null; } }

        #region Documentation
        /// Gets a value indicating whether this instance is child required.
        ///
        /// \returns    <c>true</c> if this instance is child required; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TeLSessionNode is child required. </summary>
        ///
        /// <value> True if this TeLSessionNode is child required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.IsChildRequired"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.IsChildRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool IsChildRequired { get { return true; } }

        #region Documentation
        /// Gets the type of the required child.
        ///
        /// \returns    The type of the required child.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the type of the required child. </summary>
        ///
        /// <value> The type of the required child. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.RequiredChildType"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.RequiredChildType"/>
        ///-------------------------------------------------------------------------------------------------

        protected override RequiredChildNode RequiredChildType { get { return RequiredChildNode.Text; } }

        #region Documentation
        /// Gets or sets a value indicating whether this <see cref="Expect" /> is grab.
        ///
        /// \returns    <c>true</c> if grab; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the grab. </summary>
        ///
        /// <value> True if grab, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Grab { get; private set; }

        #region Documentation
        /// Gets or sets the timeout.
        ///
        /// \returns    The timeout.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the timeout. </summary>
        ///
        /// <value> The timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Timeout { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="Expect" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.ExpectBegin class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public ExpectBegin(XmlNode node)
            : base(node, XmlTag.Expect)
        {
            Grab = Helper.TeLSessionXmlParser.GetAttributeBool(node, "Grab", false);
            Timeout = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Timeout");
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="Expect"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  texNode The TelEnvyXmlLib XML node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.ExpectBegin class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="texNode">  The tex node.</param>
        /// <param name="tag">      The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.Description("Initializes a new instance of the TexSessionPairTag class.")]
        public ExpectBegin(XmlNode texNode, XmlTag tag)
            : base(texNode, tag)
        {

        }

    }
}
