// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-25-2018
//
// Last Modified By : tim
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="DataWithinRegionCondition.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Xml;

namespace TelEnvyXmlLib.Conditions
{
    #region Documentation
    /// Class DataWithinRegionCondition.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Conditions.FinalCondition 
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
    ///             <description>The name of the region.</description>
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
    ///         <item>
    ///             <term>Pattern</term>
    ///             <description>The pattern to search for. </description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A data within region condition. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Conditions.FinalCondition"/>
    ///-------------------------------------------------------------------------------------------------

    public class DataWithinRegionCondition : FinalCondition
    {
        /// <summary>   The allowed attributes. </summary>

        readonly string[] _allowedAttributes = { "Type", "Column", "Row", "Width", "Height", "Pattern" };   /* The allowed attributes */

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
        /// <seealso cref="P:TelEnvyXmlLib.Conditions.ConditionBase.AllowedAttributes"/>
        ///-------------------------------------------------------------------------------------------------

        protected override string[] AllowedAttributes { get { return _allowedAttributes; } }

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

        public int Column { get; private set; }

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

        public int Row { get; private set; }

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

        public int Width { get; private set; }

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

        public int Height { get; private set; }

        #region Documentation
        /// Gets or sets the pattern.
        ///
        /// \returns    The pattern.
        ///
        /// ### remarks Attribute is required.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the pattern. </summary>
        ///
        /// <value> The pattern. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Pattern { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="DataWithinRegionCondition" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Conditions.DataWithinRegionCondition class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public DataWithinRegionCondition(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.IfConditionType.DataWithinRegion)
        {
            Column = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Column", true);
            Row = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Row", true);
            Width = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Width", true);
            Height = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Height", true);
            Pattern = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeString(node, "Pattern", true);
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="DataWithinRegionCondition"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        /// \param  type    The type.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Conditions.DataWithinRegionCondition class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="type"> The type.</param>
        ///-------------------------------------------------------------------------------------------------

        public DataWithinRegionCondition(XmlNode node, TelEnvyXmlLib.Enums.IfConditionType type)
            : base(node, type)
        {

        }

    }
}
