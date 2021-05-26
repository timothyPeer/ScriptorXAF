// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TexSessionGroupCollectionNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Abstract;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// Allows: "Name" Requires: Not Applicable
    /// 
    /// The GroupCollection directive permits the iterative execution of repeated sections in the
    /// context of a TelEnvyXmlLib XML session transaction.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionGroupNode  
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Collection of groups. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionGroupNode"/>
    ///-------------------------------------------------------------------------------------------------

    public class GroupCollection : TeLSessionGroupNode
    {
        #region Documentation
        /// Initializes a new instance of the <see cref="GroupCollection" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.GroupCollection class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public GroupCollection(XmlNode node) : base(node, TelEnvyXmlLib.Enums.XmlTag.GroupCollection) { }

        #region Documentation
        /// Initializes a new instance of the <see cref="GroupCollection"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.GroupCollection class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="tag">  The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        public GroupCollection(XmlNode node, TelEnvyXmlLib.Enums.XmlTag tag)
            : base(node, tag)
        {
            
        }

    }
}
