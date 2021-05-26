// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionGroupNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;

namespace TelEnvyXmlLib.Abstract
{
    using TelEnvyXmlLib.Abstract;

    #region Documentation
    /// Allows: "Name" Requires: -
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionPairTag    
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l session group node. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public abstract class TeLSessionGroupNode : TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { };  /* The allowed attributes */

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
        /// <summary>   Gets a value indicating whether this TeLSessionGroupNode is child required.
        ///             </summary>
        ///
        /// <value> True if this TeLSessionGroupNode is child required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.IsChildRequired"/>
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
        ///-------------------------------------------------------------------------------------------------

        protected override TelEnvyXmlLib.Enums.RequiredChildNode RequiredChildType { get { return TelEnvyXmlLib.Enums.RequiredChildNode.Elements; } }

        #region Documentation
        /// List of child nodes.
        ///
        /// \returns    The nodes.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the nodes. </summary>
        ///
        /// <value> The nodes. </value>
        ///-------------------------------------------------------------------------------------------------

        public List<TelEnvyXmlLib.Abstract.TeLSessionNode> Nodes { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLSessionGroupNode" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Abstract.TeLSessionGroupNode
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="tag">  The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLSessionGroupNode(XmlNode node, TelEnvyXmlLib.Enums.XmlTag tag)
            : base(node, tag)
        {
            Nodes = new List<TelEnvyXmlLib.Abstract.TeLSessionNode>();

            int i = 0;
            for (XmlNode child = Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i); child != null; child = Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i))
            {
                Nodes.Add(ParseChild(child));
            }
        }

        #region Documentation
        /// Parses the child.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  child   The child.
        ///
        /// \returns    TeLSessionNode.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Parse child. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="child">    The child.</param>
        ///
        /// <returns>   A TelEnvyXmlLib.Abstract.TeLSessionNode. </returns>
        ///-------------------------------------------------------------------------------------------------

        protected virtual TelEnvyXmlLib.Abstract.TeLSessionNode ParseChild(XmlNode child)
        {
            return TelEnvyXmlLib.Helper.TeLSessionXmlParser.Parse(child);
        }
    }
}
