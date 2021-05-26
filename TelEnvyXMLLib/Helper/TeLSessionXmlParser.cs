// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TeLSessionXmlParser.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TelEnvyXmlLib.Abstract;
using TelEnvyXmlLib.Directives;
using TelEnvyXmlLib.Exceptions;

namespace TelEnvyXmlLib.Helper
{
 

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l session XML parser. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

	public static class TeLSessionXmlParser
	{


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
        /// <returns>   A .TeLSessionNode. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static Abstract.TeLSessionNode Parse(XmlNode node)
        {
            if (node.NodeType != XmlNodeType.Element)
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Unexpected node {0} ({1}) encountered.", node.Name, node.NodeType));

            switch (node.Name)
            {
                case "Session":
                    return new Session(node);

                case "SessSeq":
                    return new SessSeq(node);

                case "Expect":
                    return new Expect(node);
                case "WaitForData":
                    return new WaitForData(node);
                case "WaitForCursor":
                    return new WaitForCursor(node);

                case "GrabLine":
                    return new GrabLine(node);
                case "GrabLines":
                    return new GrabLines(node);
                case "GrabInt32":
                    throw new NotImplementedException();
                case "GrabDouble":
                    throw new NotImplementedException();

                case "SendEnter":
                    return new TeLSessionSendDataNotRequiredNode(node, Enums.XmlTag.SendEnter);
                case "SendTab":
                    return new TeLSessionSendDataNotRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendTab);
                case "SendSpace":
                    return new TeLSessionSendDataNotRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendSpace);

                case "SendData":
                    return new TeLSessionSendDataRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendData);
                case "SendPF1":
                    return new TeLSessionSendDataRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendPF1);
                case "SendPF2":
                    return new TeLSessionSendDataRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendPF2);
                case "SendPF3":
                    return new TeLSessionSendDataRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendPF3);
                case "SendPF4":
                    return new TeLSessionSendDataRequiredNode(node, TelEnvyXmlLib.Enums.XmlTag.SendPF4);

                case "SendF1":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF1);
                case "SendF2":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF2);
                case "SendF3":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF3);
                case "SendF4":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF4);
                case "SendF5":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF5);
                case "SendF6":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF6);
                case "SendF7":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF7);
                case "SendF8":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF8);
                case "SendF9":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF9);
                case "SendF10":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF10);
                case "SendF11":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF11);
                case "SendF12":
                    return new TeLSessionSendDataNotAllowedNode(node, TelEnvyXmlLib.Enums.XmlTag.SendF12);
                case "SendLogin":
                    var vNode = new LoginPair(node, Enums.XmlTag.SendLogin) { };
                    return vNode;
                case "GroupCollection":
                    return new GroupCollection(node);

                case "If":
                    return new If(node);

                case "While":
                    return new While(node);

                default:
                    throw new Exceptions.TeLInvalidDataFormatException(string.Format("Unrecognized node {0} with data[{1}] encountered.", node.Name, node.InnerText));
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a child elements count. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///
        /// <returns>   The child elements count. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static int GetChildElementsCount(XmlNode node)
		{
			if (!node.HasChildNodes)
				return 0;
			int total = 0;
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				XmlNode child = node.ChildNodes[i];
				if (child.NodeType == XmlNodeType.Element)
					total++;
				else if (child.NodeType != XmlNodeType.Comment)
                    throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Unexpected node {0} ({1}) encountered.", child.Name, child.NodeType));
			}
			return total;
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Reads next child element. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node">                 The node.</param>
        /// <param name="currentChildIndex">    [in,out] The current child index.</param>
        ///
        /// <returns>   The next child element. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static XmlNode ReadNextChildElement(XmlNode node, ref int currentChildIndex)
		{
			if (!node.HasChildNodes)
				return null;
			for (; currentChildIndex < node.ChildNodes.Count; currentChildIndex++)
			{
				XmlNode child = node.ChildNodes[currentChildIndex];
				if (child.NodeType == XmlNodeType.Element)
				{
					currentChildIndex++;
					return child;
				}
				if (child.NodeType != XmlNodeType.Comment)
					throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Unexpected node {0} ({1}) encountered.", child.Name, child.NodeType));
			}
			return null;
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Reads child text. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///
        /// <returns>   The child text. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static string ReadChildText(XmlNode node)
		{
			if (!node.HasChildNodes)
				return null;
			if (node.ChildNodes.Count == 1)
			{
				XmlNode child = node.ChildNodes[0];
				if (child.NodeType == XmlNodeType.Text)
					return child.Value;
				else
                    throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Unexpected child node {0} ({1}) encountered.", child.Name, child.NodeType));
			}
            throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Insufficient number of child nodes encountered at node {0}.", node.Name));
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check no inner text. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

		public static void CheckNoInnerText(XmlNode node)
		{
			if (!string.IsNullOrEmpty(node.InnerText))
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Node {0} requires no InnerText to be specified.", node.Name));
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check attributes. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node">                 The node.</param>
        /// <param name="allowedAttributes"> A variable-length parameters list containing allowed
        ///                                  attributes.</param>
        ///-------------------------------------------------------------------------------------------------

		public static void CheckAttributes(XmlNode node, params string[] allowedAttributes)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				if (attribute.Name == "Name")
					continue;
				if (allowedAttributes == null || allowedAttributes.Length == 0 || Array.IndexOf(allowedAttributes, attribute.Name.ToLower()) < 0)
                    throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("Unexpected attribute {1} encountered at node {0}.", node.Name, attribute.Name));
			}
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute string. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        /// <param name="required">         True if required.</param>
        ///
        /// <returns>   The attribute string. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static string GetAttributeString(XmlNode node, string attributeName, bool required)
		{
			XmlAttribute attribute = node.Attributes[attributeName];
			if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
			{
                if (node.Attributes.Count == 0)
                {
				if (required)
                    throw new Exceptions.TeLInvalidDataFormatException(string.Format("Node {0} requires attribute {1} to be specified.", node.Name, attributeName));
				else
					return null;
                }
			}
            if (attribute != null)
            {
            if (attribute.Name.Contains(attributeName))
                return attribute.Value;
            }


            return "not defined - with attribute count gt 0";
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute int 32. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        /// <param name="required">         True if required.</param>
        ///
        /// <returns>   The attribute int 32. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static int GetAttributeInt32(XmlNode node, string attributeName, bool required)
		{
			string value = GetAttributeString(node, attributeName, required);
            int iValue = 0;
            int.TryParse(value, out iValue);

            if (value == null)
                return 0;
            else
                //return int.Parse(value);
                return iValue;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute n int 32. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        ///
        /// <returns>   The attribute n int 32. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static int? GetAttributeNInt32(XmlNode node, string attributeName)
		{
            
			string value = GetAttributeString(node, attributeName, false);
            int iValue = 0;
            int.TryParse(value,out iValue);

            if (value == null)
				return 0;
			else
				//return int.Parse(value);
				return iValue;
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute bool. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        /// <param name="required">         True if required.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static bool GetAttributeBool(XmlNode node, string attributeName, bool required)
		{
			string value = GetAttributeString(node, attributeName, required);
            bool bValue = false;
            bool.TryParse(value, out bValue);

            if (value == null)
				return false;
			else
				return bValue;
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute n bool. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        ///
        /// <returns>   The attribute n bool. </returns>
        ///-------------------------------------------------------------------------------------------------

		public static bool? GetAttributeNBool(XmlNode node, string attributeName)
		{
  
            string value = GetAttributeString(node, attributeName, false);
            bool bValue = false;
            bool.TryParse(value, out bValue);

            if (value == null)
                return false;
            else
                return bValue;
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets attribute bool. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">             The node.</param>
        /// <param name="attributeName">    Name of the attribute.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool GetAttributeBool(XmlNode node, string attributeName)
        {
            string value = GetAttributeString(node, attributeName, false);
            bool bValue = false;
            bool.TryParse(value, out bValue);

            if (value == null)
                return false;
            else
                return bValue;
        }

        //public static string GetInnerText(XmlNode node, bool required)
        //{
        //    if (!string.IsNullOrEmpty(node.InnerText))
        //        return node.InnerText;

        //    if (required)
        //        throw new TeXInvalidDataFormatException(string.Format("Node {0} requires InnerText to be specified.", node.Name));
        //    else
        //        return null;
        //}
    }
}
