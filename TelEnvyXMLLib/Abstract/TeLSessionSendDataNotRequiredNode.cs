// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-25-2018
//
// Last Modified By : Timothy Peer, eNVy Systems Inc.
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionSendDataNotRequiredNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Xml;

namespace TelEnvyXmlLib.Abstract
{
    #region Documentation
    /// Allows: "Name", "Format" Requires: -
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionPairTag    
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l session send data not required node. This class cannot be inherited.
    ///             </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public sealed class TeLSessionSendDataNotRequiredNode : Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Format" }; /* The allowed attributes */

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

        protected override bool IsChildRequired { get { return false; } }

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

        protected override TelEnvyXmlLib.Enums.RequiredChildNode RequiredChildType { get { return TelEnvyXmlLib.Enums.RequiredChildNode.Text; } }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLSessionSendDataNotRequiredNode" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Abstract.TeLSessionSendDataNotRequiredNode class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="tag">  The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLSessionSendDataNotRequiredNode(XmlNode node, TelEnvyXmlLib.Enums.XmlTag tag) : base(node, tag) { TransformData(node); }
    }
}
