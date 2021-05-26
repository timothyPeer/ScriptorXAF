// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-25-2018
//
// Last Modified By : Timothy Peer, eNVy Systems Inc.
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TexSessionWhileNode.cs" company="eNVy Systems, Inc.">
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
    /// XML Directive: &lt;While&gt;
    /// Allows: "Name", IfCondition and IfElse child nodes Requires: IfCondition child node as a
    /// first child node
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa TelEnvyXmlLib.Abstract.TeLSessionPairTag    
    ///
    /// ### remarks Supported Attributes:
    ///             
    ///                 <list type="table">
    ///                     <listheader>
    ///                         <term>Name</term>
    ///                         <description>Type</description>
    ///                     </listheader>
    ///                     <item>
    ///                         <term>&lt;While&gt; </term>
    ///                         <description>Start a conditional If-ElseIf construct.</description>
    ///                     </item>
    ///                     <item>
    ///                         <term>&lt;While &gt; &lt;If&gt; </term>
    ///                         <description> The &lt;If&gt; conditional directive is used in combination with
    ///                         &lt;IfCondition&gt;,&lt;IfElse&gt; and &lt;While&gt;.
    ///                         <see cref="TelEnvyXmlLib.TeLSession"> event</see>.
    ///                         </description>
    ///                     </item>
    ///                 </list>
    /// \remarks    .
    /// ### example Implementation examples using conditional tags:
    ///             <code>
    ///             &lt;IfCondition Type="DataAtCursorPosition" Data="Pricing Table"&gt;&lt;!-- this section begins the true false condition which automatically calculates the product sales price or if the product sales price is overridden by the XML. For this example. I removed the nodes related to automatic calculation of product sales price.--&gt;&lt;/IfCondition&gt;
    ///             </code>
    /// \example    The session may receive display prompt "Recalculate" -- if the prompt is
    ///             displayed, handle the prompt.
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A while. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionPairTag"/>
    ///-------------------------------------------------------------------------------------------------

    public class While : Abstract.TeLSessionPairTag
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
        private Conditions.ConditionBase _condition;    /* The condition */


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the condition. </summary>
        ///
        /// <value> The condition. </value>
        ///-------------------------------------------------------------------------------------------------

        public Conditions.ConditionBase Condition { get { return _condition; } }

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

        public List<Abstract.TeLSessionNode> ConditionNodes { get { return _condition.ConditionNodes; } }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.While class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public While(XmlNode node)
            : base(node, TelEnvyXmlLib.Enums.XmlTag.While)
        {
            int i = 0;
            XmlNode first = Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i);
            XmlNode second = Helper.TeLSessionXmlParser.ReadNextChildElement(node, ref i);

            if (first == null || first.Name != "IfCondition")
                throw new Exceptions.TeLInvalidDataFormatException("<While> node requires <IfCondition> as a first child node.");
            if (second != null)
                throw new Exceptions.TeLInvalidDataFormatException("<While> node allows only one child node (the <IfCondition> node).");

            _condition = Conditions.ConditionBase.Parse(first);
        }
    }
}
