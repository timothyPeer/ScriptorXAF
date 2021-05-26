// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-25-2018
//
// Last Modified By : tim
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="DataBeforeCursorPosition.cs" company="eNVy Systems, Inc.">
//     Copyright � eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Xml;


namespace TelEnvyXmlLib.Directives
{
    #region Conditions

    #region Documentation
    /// Class DataBeforeCursorPosition.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Conditions.FinalCondition <autogeneratedoc />
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
    ///             <term>Type</term>
    ///             <description></description>
    ///         </item>
    ///         <item>
    ///             <term>Pattern</term>
    ///             <description>The string pattern to search for.</description>
    ///         </item>
    ///     </list>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A data before cursor position. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Conditions.FinalCondition"/>
    ///-------------------------------------------------------------------------------------------------

    public class DataBeforeCursorPosition : TelEnvyXmlLib.Conditions.FinalCondition
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Type", "Pattern" };    /* The allowed attributes */

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
        /// Gets or sets the pattern.
        ///
        /// \returns    The pattern.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the pattern. </summary>
        ///
        /// <value> The pattern. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Pattern { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="DataBeforeCursorPosition" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Directives.DataBeforeCursorPosition class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public DataBeforeCursorPosition(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.IfConditionType.DataBeforeCursorPosition)
        {
            Pattern = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeString(node, "Pattern", true);
        }
    }

    #endregion
}
