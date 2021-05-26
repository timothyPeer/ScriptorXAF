// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="Expect.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// XML Directive: &lt;Expect&gt;  
    /// Allows: "Name", "Grab", "Timeout" Requires: InnerText, Name only if Grab is specified
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionPairTag    
    ///
    /// ### remarks
    /// Supported Attributes:
    /// 
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Name</term>
    ///             <description>Type</description>
    ///         </listheader>
    ///         <item>
    ///             <term>&lt;Expect&gt;    </term>
    ///             <description>Element - string pattern - processed as a Regular Expression.</description>
    ///         </item>
    ///         <item>
    ///             <term>&lt;Expect name = "someName" grab="true" timeout = "1000"&gt; </term>
    ///             <description>An attribute collection must be a named- name= "" ".  The Grab attribute,
    ///             signals the TelEnvyXmlLib processor will send captured text to the application's
    ///             GrabChanged <see cref="TelEnvyXmlLib.TeLSession"> event</see>.  The &lt;Expect&gt;
    ///             directive will timeout after timeout="" milliseconds.
    ///             </description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An expect. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class Expect : TelEnvyXmlLib.Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        readonly string[] _allowedAttributes = { "Grab", "Timeout", "StartTag" };   /* The allowed attributes */

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
        /// Gets or sets the start tag
        ///
        /// \returns    The start tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the start tag. </summary>
        ///
        /// <value> The start tag. </value>
        ///-------------------------------------------------------------------------------------------------

        public string StartTag { get; set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="Expect" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.Expect class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public Expect(XmlNode node)
            : base(node, XmlTag.Expect)
        {
            Grab = Helper.TeLSessionXmlParser.GetAttributeBool(node, "Grab", false);
            Timeout = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Timeout");
            StartTag = Helper.TeLSessionXmlParser.GetAttributeString(node, "StartTag",false);
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.Expect class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="texNode">  The tex node.</param>
        /// <param name="tag">      The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.Description("Initializes a new instance of the TexSessionPairTag class.")]
        public Expect(XmlNode texNode, XmlTag tag)
            : base(texNode, tag)
        {

        }

    }
}
