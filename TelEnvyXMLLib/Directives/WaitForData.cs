// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionWaitForDataNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// Allows: "Name", "Column", "Row", "Width", "Height", "Timeout" Requires: InnerText, and
    /// either: - no region attributes to be specified - only Row attribute to be specified - all
    /// region attributes to be specified
    /// 
    /// Performs a Wait "stall" of the transaction until the cursor is set to the coordinate as
    /// defined by Row.
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
    ///             <description>Description</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Name</term>
    ///             <description></description>
    ///         </item>
    ///         <item>
    ///             <term>Column</term>
    ///             <description>The starting column in the selection.</description>
    ///         </item>
    ///         <item>
    ///             <term>Row</term>
    ///             <description>The starting Row in the selection.</description>
    ///         </item>
    ///         <item>
    ///             <term>Width</term>
    ///             <description>The width of the selection in columns.</description>
    ///         </item>
    ///         <item>
    ///             <term>Height</term>
    ///             <description>The number of rows to be captured.  Height is zero based starting from the
    ///             current row.</description>
    ///         </item>
    ///         <item>
    ///             <term>Timeout</term>
    ///             <description>The Timeout in Milliseconds to wait to receive data
    /// </description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A wait for data. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class WaitForData : Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Column", "Row", "Width", "Height", "Timeout" };    /* The allowed attributes */

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

        protected override TelEnvyXmlLib.Enums.RequiredChildNode RequiredChildType { get { return TelEnvyXmlLib.Enums.RequiredChildNode.Text; } }

        #region Documentation
        /// Gets or sets the column.
        ///
        /// \returns    The column.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the column. </summary>
        ///
        /// <value> The column. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Column { get; private set; }

        #region Documentation
        /// Gets or sets the row.
        ///
        /// \returns    The row.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the row. </summary>
        ///
        /// <value> The row. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Row { get; private set; }

        #region Documentation
        /// Gets or sets the width.
        ///
        /// \returns    The width.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the width. </summary>
        ///
        /// <value> The width. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Width { get; private set; }

        #region Documentation
        /// Gets or sets the height.
        ///
        /// \returns    The height.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the height. </summary>
        ///
        /// <value> The height. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Height { get; private set; }

        #region Documentation
        /// Gets or sets the timeout.
        ///
        /// \returns    The timeout.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the timeout. </summary>
        ///
        /// <value> The timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Timeout { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="WaitForData" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \exception  TeLInvalidDataFormatException   Thrown when a Te L Invalid Data Format error
        ///                                             condition occurs.
        ///
        /// \param  node    The node.
        ///
        /// ### exception   TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException  .
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.WaitForData class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public WaitForData(XmlNode node)
            : base(node, 0)
        {
            Column = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Column");
            Row = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Row");
            Width = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Width");
            Height = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Height");
            Timeout = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Timeout");

            // allow following cases only:
            if (Column == null && Row == null && Width == null && Height == null)
                TeLTag = TelEnvyXmlLib.Enums.XmlTag.WaitForDataWholeScreen;
            else if (Column == null && Row != null && Width == null && Height == null)
                TeLTag = TelEnvyXmlLib.Enums.XmlTag.WaitForDataOneRow;
            else if (Column != null && Row != null && Width != null && Height != null)
                TeLTag = TelEnvyXmlLib.Enums.XmlTag.WaitForDataRegion;
            else
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Invalid use of <{0}> node. Specify no attribute, or Row only, or all attributes.", node.Name));
        }
    }
}
