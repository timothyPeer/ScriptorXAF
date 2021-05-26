// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TexSessionGrabLinesNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;

namespace TelEnvyXmlLib.Directives
{
 

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A grab lines. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionNonPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class GrabLines : Abstract.TeLSessionNonPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Column", "Row", "Width", "Height" };   /* The allowed attributes */



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

        protected override bool NameRequired { get { return true; } }

     
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
        /// <summary>   Gets the width. </summary>
        ///
        /// <value> The width. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Width { get; private set; }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the height. </summary>
        ///
        /// <value> The height. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Height { get; private set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.GrabLines class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public GrabLines(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.XmlTag.GrabLines)
        {
            Column = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Column", true);
            Row = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Row", true);
            Width = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Width", true);
            Height = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeInt32(node, "Height", true);
        }
    }
}
