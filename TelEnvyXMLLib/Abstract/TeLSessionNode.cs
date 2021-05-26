// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Tim Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer, eNVy Systems Inc.
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TeLSessionNode.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Abstract
{
    //TODO Document TeLSessionNode Class

    #region Documentation
    /// The TeLSessionNode represents an XML node of the TelEnvyXmlLib XML document.
    /// TelEnvySessionNodes are validated during preprocessing. They must be well formed and follow
    /// standard tags used by TelEnvyXmlLib.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l session node. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public abstract class TeLSessionNode
	{

        #region Protected Fields and Properties
        //public abstract XmlNode XmlNode { get; set; }

        #region Documentation
        /// Gets or sets the TelEnvyXmlLib Directive XML Node.
        ///
        /// \returns    An XmlNode.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l node. </summary>
        ///
        /// <value> The te l node. </value>
        ///-------------------------------------------------------------------------------------------------

        public XmlNode TeLNode { get; set; }
        /// <summary>   The tag. </summary>

        private XmlTag __tag;   /* The tag */
        /// <summary>   True if name required. </summary>
        private bool nameRequired;  /* True if name required */

        #region Documentation
        /// Gets a value indicating whether this instance is pair tag.
        ///
        /// \returns    <c>true</c> if this instance is pair tag; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TeLSessionNode is pair tag. </summary>
        ///
        /// <value> True if this TeLSessionNode is pair tag, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        protected abstract bool IsPairTag { get; }

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

        protected abstract string[] AllowedAttributes { get; }

        #region Documentation
        /// Gets a value indicating whether [name required].
        ///
        /// \returns    <c>true</c> if [name required]; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the name required. </summary>
        ///
        /// <value> True if name required, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        protected abstract bool NameRequired { get; }

        #region Documentation
        /// Gets a value indicating whether this instance is child required.
        ///
        /// \returns    <c>true</c> if this instance is child required; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TeLSessionNode is child required. </summary>
        ///
        /// <value> True if this TeLSessionNode is child required, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        protected abstract bool IsChildRequired { get; }

        #region Documentation
        /// Gets the type of the required child.
        ///
        /// \returns    The type of the required child.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the type of the required child. </summary>
        ///
        /// <value> The type of the required child. </value>
        ///-------------------------------------------------------------------------------------------------

        protected abstract RequiredChildNode RequiredChildType { get; }
        #endregion

        #region Documentation
        /// Gets or sets the tag.
        ///
        /// \exception  InvalidOperationException   Cannot set Tag in current state.
        ///
        /// \returns    The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the te l tag. </summary>
        ///
        /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid.</exception>
        ///
        /// <value> The te l tag. </value>
        ///-------------------------------------------------------------------------------------------------

        public XmlTag TeLTag
		{
            get { return __tag; }
			protected set
			{
                if (__tag == 0)
                    __tag = value;
				else
					throw new InvalidOperationException("Cannot set Tag in current state.");
			}
		}

        #region Documentation
        /// Gets or sets the name.
        ///
        /// \returns    The name.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Name { get; set; }

        #region Documentation
        /// Gets or sets the data.
        ///
        /// \returns    The data.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the data. </summary>
        ///
        /// <value> The data. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Data { get; private set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLSessionNode"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \exception  TeLInvalidDataFormatException   Non-pair node &lt;{0}&gt; contains child nodes.
        ///                                             or Node &lt;{0}&gt; requires one child node. or
        ///                                             Node &lt;{0}&gt; requires at least one child
        ///                                             node. or Node &lt;{0}&gt; requires one Text child
        ///                                             node.
        /// \exception  InvalidOperationException       RequiredChild out of range.
        ///
        /// \param  node    The node.
        /// \param  tag     The tag.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Abstract.TeLSessionNode class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        /// <exception cref="InvalidOperationException">     Thrown when the requested operation is
        ///                                                  invalid.</exception>
        ///
        /// <param name="node"> The node.</param>
        /// <param name="tag">  The tag.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLSessionNode(XmlNode node, XmlTag tag)
		{
			TeLNode = node;
			TeLTag = tag;

            List<string> arr = new List<string>();
            foreach (string s in this.AllowedAttributes)
            {
                arr.Add(s.ToLower());
            }
            string[] __allowedAttributes = arr.ToArray();
            if (!IsPairTag && node.HasChildNodes)
				throw new Exceptions.TeLInvalidDataFormatException("Non-pair node <{0}> contains child nodes.", node.Name);
            Helper.TeLSessionXmlParser.CheckAttributes(node,__allowedAttributes);
           // Helper.TeLSessionXmlParser.CheckAttributes(node, AllowedAttributes);
			if (NameRequired)
				Name = Helper.TeLSessionXmlParser.GetAttributeString(node, "Name", true);
			if (RequiredChildType == Enums.RequiredChildNode.Text)
				Data = Helper.TeLSessionXmlParser.ReadChildText(node);
			if (IsChildRequired)
			{
				switch (RequiredChildType)
				{
					case Enums.RequiredChildNode.OneElement:
						if (Helper.TeLSessionXmlParser.GetChildElementsCount(node) != 1)
							throw new Exceptions.TeLInvalidDataFormatException("Node <{0}> requires one child node.", node.Name);
						break;
					case Enums.RequiredChildNode.Elements:
						if (Helper.TeLSessionXmlParser.GetChildElementsCount(node) == 0)
							throw new Exceptions.TeLInvalidDataFormatException("Node <{0}> requires at least one child node.", node.Name);
						break;
					case Enums.RequiredChildNode.Text:
						if (string.IsNullOrEmpty(Data))
							throw new Exceptions.TeLInvalidDataFormatException("Node <{0}> requires one Text child node.", node.Name);
						break;
					default:
						throw new InvalidOperationException("RequiredChild out of range.");
				}
			}
		}

        #region Documentation
        /// Transforms the data.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \exception  InvalidOperationException       Text node required.
        /// \exception  TeLInvalidDataFormatException   Thrown when a Te L Invalid Data Format error
        ///                                             condition occurs.
        /// \exception  TeLMessageException             Thrown when a Te L Message error condition occurs.
        ///
        /// \param  node    The node.
        ///
        /// ### exception   TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException  Invalid DateTime
        ///                                                                         Format {0}. or
        ///                                                                         Invalid DateTime Data
        ///                                                                         {0}. or Invalid
        ///                                                                         Numeric Format {0}.
        ///                                                                         or Invalid Numeric
        ///                                                                         Data {0} data [{1}].
        ///                                                                         or Invalid Bool
        ///                                                                         Format {0}. or
        ///                                                                         Invalid Bool Data
        ///                                                                         {0}. or Text: Invalid
        ///                                                                         Text Attributes Value-
        ///                                                                         Pair('=') {0}. or
        ///                                                                         Text: Invalid Text
        ///                                                                         Length {0}. or Text
        ///                                                                         (text) Format doesn't
        ///                                                                         define value for
        ///                                                                         specified data {0}.
        ///                                                                         or TrimStart: Invalid
        ///                                                                         Text Attributes Value-
        ///                                                                         Pair or Text: Invalid
        ///                                                                         Text Length {0}. or
        ///                                                                         Text (TrimStart)
        ///                                                                         format doesn't define
        ///                                                                         value for specified
        ///                                                                         data {0}. or
        ///                                                                         TrimStart: Invalid
        ///                                                                         Text Attributes Value-
        ///                                                                         Pair or Text: Invalid
        ///                                                                         Text Length {0}. or
        ///                                                                         Text (TrimEnd) Format
        ///                                                                         doesn't define value
        ///                                                                         for specified data
        ///                                                                         {0}. or Text
        ///                                                                         (TrimEnd) Format
        ///                                                                         doesn't define value
        ///                                                                         for specified data
        ///                                                                         {0}. or Text: Invalid
        ///                                                                         Text Length {0}.. or
        ///                                                                         Text (Substring)
        ///                                                                         Format doesn't define
        ///                                                                         value for specified
        ///                                                                         data {0}. or Default:
        ///                                                                         Unknown Format
        ///                                                                         identifier {0}.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Transform data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="InvalidOperationException">     Thrown when the requested operation is
        ///                                                  invalid.</exception>
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        /// <exception cref="TeLMessageException">           Thrown when a Te L Message error condition
        ///                                                  occurs.</exception>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void TransformData(XmlNode node)
		{
			if (RequiredChildType != Enums.RequiredChildNode.Text)
				throw new InvalidOperationException("Text node required.");

			if (string.IsNullOrEmpty(Data))
				return;

			string format = Helper.TeLSessionXmlParser.GetAttributeString(node, "Format", false);
			if (string.IsNullOrEmpty(format))
				return;

			string[] parts = format.Split(':');
			switch (parts[0].ToLowerInvariant())
			{
				case "datetime":
					if (parts.Length != 2)
						throw new Exceptions.TeLInvalidDataFormatException("Invalid DateTime Format {0}.", format);

					DateTime ddata;
					if (!DateTime.TryParse(Data, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out ddata))
						throw new Exceptions.TeLInvalidDataFormatException("Invalid DateTime Data {0}.", node.OuterXml);

					Data = ddata.ToString(parts[1]);
					break;
				case "Numeric":
				  
					if (parts.Length != 2)
						throw new Exceptions.TeLInvalidDataFormatException("Invalid Numeric Format {0}.", format);

					decimal ddata2;
					string s1 = parts[1];
					if (!Decimal.TryParse(s1, out ddata2) )
						throw new Exceptions.TeLInvalidDataFormatException("Invalid Numeric Data {0} data [{1}].", node.OuterXml,parts[1]);

					// Gets a NumberFormatInfo associated with the en-US culture.
					NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

					// Displays the same value with four decimal digits.
					nfi.CurrencyDecimalDigits = 4;
					ddata2 = Convert.ToDecimal(string.Format("{0:F4}",parts[1]));
					Data = ddata2.ToString();
					break;
				case "bool":
					 if (parts.Length != 1 && parts.Length != 3)
										throw new Exceptions.TeLInvalidDataFormatException("Invalid Bool Format {0}.", format);
						switch (Data.ToLowerInvariant())
						{
							case "0":
							case "f":
							case "false":
									Data = parts.Length == 1 ? "N" : parts[2];
									break;
							case "1":
							case "t":
							case "true":
									Data = parts.Length == 1 ? "Y" : parts[1];
									break;
							default:
									throw new Exceptions.TeLInvalidDataFormatException("Invalid Bool Data {0}.", node.OuterXml);
						}
						break;

				case "text":
						{
							Dictionary<string, string> dic = new Dictionary<string, string>();
							for (int i = 1; i < parts.Length; i++)
							{
								int idx = parts[i].IndexOf('=');
								if (idx <= 0)
									throw new Exceptions.TeLInvalidDataFormatException("Text: Invalid Text Attributes Value-Pair('=') {0}.", format);

								string key = parts[i].Substring(0, idx);
								string value = parts[i].Substring(idx + 1);
								if (key.Length == 0 || dic.ContainsKey(key))
									throw new Exceptions.TeLInvalidDataFormatException("Text: Invalid Text Length {0}.", format);

								dic.Add(key, value);
							}

							if (!dic.ContainsKey(Data))
								throw new Exceptions.TeLInvalidDataFormatException("Text (text) Format doesn't define value for specified data {0}.", node.OuterXml);

							Data = dic[Data];
						}
						break;
				
				case "text_trimstart":
					{
						Dictionary<string, string> dic = new Dictionary<string, string>();
						for (int i = 1; i < parts.Length; i++)
						{
							int idx = parts[i].IndexOf('=');
							if (idx <= 0)
								throw new Exceptions.TeLInvalidDataFormatException("TrimStart: Invalid Text Attributes Value-Pair", format);

							string key = parts[i].Substring(0, idx);
							string value = parts[i].Substring(idx + 1).TrimStart();
							if (key.Length == 0 || dic.ContainsKey(key))
								throw new Exceptions.TeLInvalidDataFormatException("Text: Invalid Text Length {0}.", format);

							dic.Add(key, value);
						}

						if (!dic.ContainsKey(Data))
							throw new Exceptions.TeLInvalidDataFormatException("Text (TrimStart) format doesn't define value for specified data {0}.", node.OuterXml);

						Data = dic[Data];
					}
					break;
				case "text_trimend":
					{
						Dictionary<string, string> dic = new Dictionary<string, string>();
						for (int i = 1; i < parts.Length; i++)
						{
							int idx = parts[i].IndexOf('=');
							if (idx <= 0)
                                throw new Exceptions.TeLInvalidDataFormatException("TrimStart: Invalid Text Attributes Value-Pair", format);

							string key = parts[i].Substring(0, idx);
							string value = parts[i].Substring(idx + 1).TrimEnd();
							if (key.Length == 0 || dic.ContainsKey(key))
                                throw new Exceptions.TeLInvalidDataFormatException("Text: Invalid Text Length {0}.", format);

							dic.Add(key, value);
						}

						if (!dic.ContainsKey(Data))
							throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException("Text (TrimEnd) Format doesn't define value for specified data {0}.", node.OuterXml);

						Data = dic[Data];
					}
					break;
				case "text_substring":
					{
						Dictionary<string, string> dic = new Dictionary<string, string>();
						for (int i = 1; i < parts.Length; i++)
						{
							int idx = parts[i].IndexOf('=');
							if (idx <= 0)
                                throw new Exceptions.TeLInvalidDataFormatException("Text (TrimEnd) Format doesn't define value for specified data {0}.", format);

							string key = parts[i].Substring(0, idx);
							string value = parts[i].Substring(idx + 1).TrimEnd();
							if (key.Length == 0 || dic.ContainsKey(key))
                                throw new Exceptions.TeLInvalidDataFormatException("Text: Invalid Text Length {0}..", format);
							string xxxv = null;
							try
							{
								Helper.parseSubstring strx = new Helper.parseSubstring(value);
							
								xxxv = value.Substring(strx.OffsetStart, strx.OffsetLength);
							}
							catch (Exception ex)
							{
                                throw new Exceptions.TeLMessageException(string.Format("TeLSessionNode/{0} Error", "textSubstring"), ex);
                            }
                            finally
							{
								
							}


							dic.Add(key, xxxv);
						}

						if (!dic.ContainsKey(Data))
							throw new Exceptions.TeLInvalidDataFormatException("Text (Substring) Format doesn't define value for specified data {0}.", node.OuterXml);

						Data = dic[Data];
					}
					break;
				default:
					throw new Exceptions.TeLInvalidDataFormatException("Default: Unknown Format identifier {0}.", node.OuterXml);
			}
		}

        #region Documentation
        /// Returns a <see cref="System.String" /> that represents this instance.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \returns    A <see cref="System.String" /> that represents this instance.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a string that represents the current object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ///
        /// <seealso cref="M:System.Object.ToString()"/>
        ///-------------------------------------------------------------------------------------------------

        public override string ToString()
		{
            return TeLTag.ToString();
		}
	}
}
