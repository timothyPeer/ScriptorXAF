// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-30-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionIfNode.cs" company="eNVy Systems, Inc.">
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
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An if. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class If : Abstract.TeLSessionPairTag
    {
        /// <summary>   The allowed attributes. </summary>
        string[] _allowedAttributes = { };  /* The allowed attributes */

       

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
        /// <summary>   Gets a value indicating whether this TeLSessionNode is child required. </summary>
        ///
        /// <value> True if this TeLSessionNode is child required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.IsChildRequired"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.IsChildRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool IsChildRequired { get { return true; } }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the type of the required child. </summary>
        ///
        /// <value> The type of the required child. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.RequiredChildType"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.RequiredChildType"/>
        ///-------------------------------------------------------------------------------------------------

        protected override TelEnvyXmlLib.Enums.RequiredChildNode RequiredChildType { get { return TelEnvyXmlLib.Enums.RequiredChildNode.Elements; } }

        /// <summary>   The condition. </summary>
        private TelEnvyXmlLib.Conditions.ConditionBase _condition;  /* The condition */
        /// <summary>   The else. </summary>
        private TeLSessionGroupNode _else;  /* The else */

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the condition. </summary>
        ///
        /// <value> The condition. </value>
        ///-------------------------------------------------------------------------------------------------

        public TelEnvyXmlLib.Conditions.ConditionBase Condition { get { return _condition; } }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the condition nodes. </summary>
        ///
        /// <value> The condition nodes. </value>
        ///-------------------------------------------------------------------------------------------------

        public List<TelEnvyXmlLib.Abstract.TeLSessionNode> ConditionNodes { get { return _condition.ConditionNodes; } }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the else nodes. </summary>
        ///
        /// <value> The else nodes. </value>
        ///-------------------------------------------------------------------------------------------------

        public List<TelEnvyXmlLib.Abstract.TeLSessionNode> ElseNodes { get { return _else == null ? null : _else.Nodes; } }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.If class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public If(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.XmlTag.If)
        {
            int i = 0;
            XmlNode first = TelEnvyXmlLib.Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i);
            XmlNode second = TelEnvyXmlLib.Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i);
            XmlNode third = TelEnvyXmlLib.Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i);

            if (first == null || first.Name != "IfCondition")
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException("<If> node requires <IfCondition> as a first child node.");
            if (second != null && second.Name != "IfElse")
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException("<If> node allows only <IfElse> as a second child node.");
            if (third != null)
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException("<If> node allows only one <IfCondition> and one <IfElse> child nodes.");

            _condition = TelEnvyXmlLib.Conditions.ConditionBase.Parse(first);
            if (second != null)
                _else = new GroupCollection(second);
        }
    }
}
