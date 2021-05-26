// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionPairTag.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;

namespace TelEnvyXmlLib.Abstract
{
    #region Documentation
    /// Class TexSessionPairTag, A key-value pair with TelEnvyXmlLib Directory and the associated
    /// KeyTag ordinal ID.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionNode   
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l session pair tag. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionNode"/>
    ///-------------------------------------------------------------------------------------------------

    public abstract class TeLSessionPairTag : TelEnvyXmlLib.Abstract.TeLSessionNode
    {
        #region Documentation
        /// Gets a value indicating whether this instance is pair tag.
        ///
        /// \returns    <c>true</c> if this instance is pair tag; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TeLSessionNode is pair tag. </summary>
        ///
        /// <value> True if this TeLSessionNode is pair tag, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.IsPairTag"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool IsPairTag { get { return true; } }

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
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.RequiredChildType"/>
        ///-------------------------------------------------------------------------------------------------

        protected override TelEnvyXmlLib.Enums.RequiredChildNode RequiredChildType { get { return 0; } }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLSessionPairTag" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  texNode The TelEnvyXmlLib XML node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Abstract.TeLSessionPairTag class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="texNode">  The tex node.</param>
        /// <param name="tag">      The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.Description("Initializes a new instance of the TexSessionPairTag class.")]
        public TeLSessionPairTag(XmlNode texNode, TelEnvyXmlLib.Enums.XmlTag tag) : base(texNode, tag) {

         
        }
    }
}
