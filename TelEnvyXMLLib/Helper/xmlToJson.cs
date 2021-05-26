// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-28-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-30-2018
// ***********************************************************************
// <copyright file="xmlToJson.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Xml;
using Newtonsoft.Json;
using TelEnvyXmlLib.EventArgs;
using TelEnvyXmlLib.Exceptions;
using Formatting = Newtonsoft.Json.Formatting;

namespace TelEnvyXmlLib.Helper
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An XML to json. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class xmlToJson
    {
  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the invalid tag action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        An InvalidTagEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnInvalidTag(object sender, InvalidTagEventArg e)
        {
            EventHandler<InvalidTagEventArg> handler = InvalidTag;
            if (handler != null)
                handler(sender, e);
        }
        /// <summary>   Occurs when [invalid tag]. </summary>
        public event EventHandler<InvalidTagEventArg> InvalidTag;   /*Event queue for all listeners interested in InvalidTag events.*/  /* Occurs when Invalid Tag. */

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts the given document. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLXMLInvalidTagException"> Thrown when a Te LXML Invalid Tag error condition
        ///                                              occurs.</exception>
        ///
        /// <param name="xmlDoc">   The XML document.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Convert(string xmlDoc)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlDoc);
                var json = JsonConvert.SerializeXmlNode(doc,Formatting.None);
                return json;
            }
            catch (Exception ex)
            {
                string message = string.Format("{0}\n{1}", string.Format("xmlToJson::TeXXMLInvalidTag:\n An invalid tag found.\nExpected <Session> as a document root node and found {0}", doc.DocumentType.Name), ex.InnerException.Message);
               // writeErrorToLog(4100, "TeLXMLInvalidTagException", message);
                throw new TeLXMLInvalidTagException(message);
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts the given document. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLXMLInvalidTagException"> Thrown when a Te LXML Invalid Tag error condition
        ///                                              occurs.</exception>
        ///
        /// <param name="doc">  The document.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Convert(XmlDocument doc)
        {
            try
            {
                return JsonConvert.SerializeXmlNode(doc, Formatting.Indented);
            }
            catch (Exception ex)
            {
                string message =
                    $"{string.Format("xmlToJson::TeXXMLInvalidTag:\n An invalid tag found.\nExpected <Session> as a document root node and found {0}", doc.DocumentType.Name)}\n{ex.InnerException.Message}";
               // writeErrorToLog(4100, "TeLXMLInvalidTagException", message);
                throw new TeLXMLInvalidTagException(message);
            }
        }
    }
}
