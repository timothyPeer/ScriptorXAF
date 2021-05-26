// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="XmlConvert.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using TelEnvyXmlLib.Exceptions;

namespace TelEnvyXmlLib.Helper
{


///-------------------------------------------------------------------------------------------------
/// <summary>   An XML convert. </summary>
///
/// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
///-------------------------------------------------------------------------------------------------

public static class XmlConvert
    {


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Serialize object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <typeparam name="T">    Generic type parameter.</typeparam>
        /// <param name="dataObject">   The data object.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string SerializeObject<T>(T dataObject)
        {
            if (dataObject == null)
            {
                return string.Empty;
            }
            try
            {
                using (StringWriter stringWriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stringWriter, dataObject);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("XmlConvert - Invalid Format to serialize object [{0}] to XML.\n", typeof(T).Name), ex);
                
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Deserialize object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <typeparam name="T">    Generic type parameter.</typeparam>
        /// <param name="xml">  The XML.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static T DeserializeObject<T>(string xml)
             where T : new()
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new T();
            }
            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                throw new TelEnvyXmlLib.Exceptions.TeLInvalidDataFormatException(string.Format("XmlConvert - Invalid Format to Deserialize XML to Object.\n{0}",xml),ex);
              /*  return new T();*/
            }
           
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets attribute safe. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node">     The node.</param>
        /// <param name="attrList"> A variable-length parameters list containing attribute list.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void SetAttrSafe(XmlNode node, params XmlAttribute[] attrList)
        {
            foreach (var attr in attrList)
            {
                if (node.Attributes[attr.Name] != null)
                {
                    node.Attributes[attr.Name].Value = attr.Value;
                }
                else
                {
                    node.Attributes.Append(attr);
                }
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Merge tel envy module. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="moduleXML">    The module XML.</param>
        /// <param name="loginInfoXML"> The login information XML.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void MergeTelEnvyModule(string moduleXML,string loginInfoXML)
        {

           
           



        }

    }

}