// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-25-2018
//
// Last Modified By : tim
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="ConditionBase.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;


namespace TelEnvyXmlLib.Conditions
{
    #region Documentation
    /// Class ConditionBase.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A condition base. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public abstract class ConditionBase
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { "Type" };   /* The allowed attributes */

        #region Documentation
        /// Gets the allowed attributes.
        ///
        /// \returns    The allowed attributes.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the allowed attributes. </summary>
        ///
        /// <value> The allowed attributes. </value>
        ///-------------------------------------------------------------------------------------------------

        protected virtual string[] AllowedAttributes { get { return _allowedAttributes; } }

        #region Documentation
        /// Gets or sets the type of the condition.
        ///
        /// \returns    The type of the condition.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the type of the condition. </summary>
        ///
        /// <value> The type of the condition. </value>
        ///-------------------------------------------------------------------------------------------------

        public TelEnvyXmlLib.Enums.IfConditionType ConditionType { get; private set; }

        #region Documentation
        /// Gets the condition nodes.
        ///
        /// \returns    The condition nodes.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the condition nodes. </summary>
        ///
        /// <value> The condition nodes. </value>
        ///-------------------------------------------------------------------------------------------------

        public abstract List<TelEnvyXmlLib.Abstract.TeLSessionNode> ConditionNodes { get; }

        #region Documentation
        /// Initializes a new instance of the <see cref="ConditionBase" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node            The node.
        /// \param  conditionType   Type of the condition.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Conditions.ConditionBase class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="conditionType">    Type of the condition.</param>
        ///-------------------------------------------------------------------------------------------------

        public ConditionBase(XmlNode node, TelEnvyXmlLib.Enums.IfConditionType conditionType)
        {
            ConditionType = conditionType;
            TelEnvyXmlLib.Helper.TeLSessionXmlParser.CheckAttributes(node, AllowedAttributes);
        }

        #region Documentation
        /// Parses the specified node.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \exception  TeLInvalidDataFormatException   Thrown when a Te L Invalid Data Format error
        ///                                             condition occurs.
        ///
        /// \param  node    The node.
        ///
        /// \returns    ConditionBase.
        ///
        /// ### exception   TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException  Unrecognized &lt;
        ///                                                                         IfCondition&gt;
        ///                                                                         'Type' attribute
        ///                                                                         value encountered {0}.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Parses the given node. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///
        /// <returns>   A ConditionBase. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal static ConditionBase Parse(XmlNode node)
        {
            string type = TelEnvyXmlLib.Helper.TeLSessionXmlParser.GetAttributeString(node, "Type", true);

            TelEnvyXmlLib.Enums.IfConditionType conditionType;
            if (!Enum.TryParse<TelEnvyXmlLib.Enums.IfConditionType>(type, out conditionType))
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException("Unrecognized <IfCondition> 'Type' attribute value encountered {0}.", type);

            switch (conditionType)
            {
                case TelEnvyXmlLib.Enums.IfConditionType.Not:
                    return new NotCondition(node);
                case TelEnvyXmlLib.Enums.IfConditionType.And:
                    return new AndCondition(node);
                case TelEnvyXmlLib.Enums.IfConditionType.Or:
                    return new OrCondition(node);
                case TelEnvyXmlLib.Enums.IfConditionType.EmptyIfCondition:
                    return new EmptyCondition(node);
                case TelEnvyXmlLib.Enums.IfConditionType.DataWithinRegion:
                    return new TelEnvyXmlLib.Conditions.DataWithinRegionCondition(node);
                case TelEnvyXmlLib.Enums.IfConditionType.DataBeforeCursorPosition:
                    return new TelEnvyXmlLib.Directives.DataBeforeCursorPosition(node);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
