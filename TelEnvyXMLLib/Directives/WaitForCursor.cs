// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TexSessionWaitForCursorNode.cs" company="eNVy Systems, Inc.">
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
    /// XML Directive: WaitForCursor Allows: "Name", "Column", "Row", "Timeout" Requires: Column, Row
    /// Performs a Wait "stall" of the transaction until the cursor is set to the coordinate as
    /// defined by Row and Column.
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
    ///             <term>&lt;WaitForCursor&gt; </term>
    ///             <description>Element - string pattern - processed as a Regular Expression.</description>
    ///         </item>
    ///         <item>
    ///             <term>&lt;WaitForCursor name="somename" column = "0" row="0" timeout="1000"&gt; </term>
    ///             <description> The combination of these properties the data starting at coordinates
    ///             column="x" row="y" and width="z".  All values are one referenced for coordinate locations.
    ///             <see cref="TelEnvyXmlLib.TeLSession"> event</see>.
    ///             </description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A wait for cursor. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class WaitForCursor : Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Column", "Row", "Timeout" };   /* The allowed attributes */

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the allowed attributes. </summary>
        ///
        /// <value> The allowed attributes. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.AllowedAttributes"/>
        ///-------------------------------------------------------------------------------------------------

        protected override string[] AllowedAttributes { get { return _allowedAttributes; } }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the name required. </summary>
        ///
        /// <value> True if name required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.NameRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool NameRequired { get { return false; } }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the column. </summary>
        ///
        /// <value> The column. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Column { get; private set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the row. </summary>
        ///
        /// <value> The row. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Row { get; private set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the timeout. </summary>
        ///
        /// <value> The timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? Timeout { get; private set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.WaitForCursor class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public WaitForCursor(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.XmlTag.WaitForCursor)
        {
            Column = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Column", true);
            Row = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Row", true);
            Timeout = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "Timeout");
        }
    }
}
