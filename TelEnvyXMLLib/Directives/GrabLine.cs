// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="Grabline.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Abstract;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// XML Directive: GrabLine Allows: "Name", "Column", "Row", "Width" Requires: Column, Row, Width
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.TeLSession                    
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
    ///             <term>&lt;GrabLine&gt;  </term>
    ///             <description>Element - string pattern - processed as a Regular Expression.</description>
    ///         </item>
    ///         <item>
    ///             <term>&lt;GrabLine column = "0" row="0" width="12"&gt;  </term>
    ///             <description> The combination of these properties the data starting at coordinates
    ///             column="x" row="y" and width="z".  All values are one referenced for coordinate locations.
    ///             <see cref="TelEnvyXmlLib.TeLSession"> event</see>.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>&lt;GrabDouble column = "0" row="0" width="12"&gt;    </term>
    ///             <description>The directive will attempt to convert the string to System.Double. A
    ///             <see cref="TelEnvyXmlLib.Exceptions.TeLInvalidCastException">TeXInvalidCastException</see>
    ///             will be thrown if the conversion cannot be performed.
    ///             </description>
    ///         </item>
    ///     <item>
    ///             <term>&lt;GrabInt32 column = "0" row="0" width="12"&gt; </term>
    ///             <description>The directive will attempt to convert the string to System.Int32. A
    ///             <see cref="TelEnvyXmlLib.Exceptions.TeLInvalidCastException">TeXInvalidCastException</see>
    ///             will be thrown if the conversion cannot be performed.
    ///             </description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A grab line. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionNonPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class GrabLine : TeLSessionNonPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        readonly string[] _allowedAttributes = { "Column", "Row", "Width" };    /* The allowed attributes */

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

        protected override bool NameRequired { get { return true; } }

        #region Documentation
        /// Gets or sets the column.
        ///
        /// \returns    The column.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the column. </summary>
        ///
        /// <value> The column. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Column { get; private set; }

        #region Documentation
        /// Gets or sets the row.
        ///
        /// \returns    The row.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the row. </summary>
        ///
        /// <value> The row. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Row { get; private set; }

        #region Documentation
        /// Gets or sets the width.
        ///
        /// \returns    The width.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the width. </summary>
        ///
        /// <value> The width. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Width { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="GrabLine" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.GrabLine class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public GrabLine(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.XmlTag.GrabLine)
        {
            Column = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Column", true);
            Row = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Row", true);
            Width = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Width", true);
        }
    }
}
