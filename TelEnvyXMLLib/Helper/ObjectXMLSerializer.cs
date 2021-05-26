// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="ObjectXMLSerializer.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

using System.Text;
using System;
using System.Xml.Serialization; // For serialization of an object to an XML Document file.
using System.Runtime.Serialization.Formatters.Binary; // For serialization of an object to an XML Binary file.
using System.IO; // For reading/writing data to an XML file.
using System.IO.IsolatedStorage; // For accessing user isolated data.

namespace TelEnvyXmlLib.Helper
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An object for persisting object XML data. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter.</typeparam>
    ///-------------------------------------------------------------------------------------------------

        [System.Xml.Serialization.XmlInclude(typeof(LoginInfo))]
    public  class ObjectXMLSerializer<T>
        where T: class // Specify that T must be a class.
    {
        #region Load methods

        #region Documentation
        /// Loads an object from an XML file in Document format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  path                Path of the file to load the object from.
        /// \param  serializedFormat    XML serialized format used to load the object.
        ///
        /// \returns    Object loaded from an XML file in Document format.
        ///
        /// ### example <code>
        ///             serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml");
        ///             </code>
        /// \example    <code>
        ///             serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", SerializedFormat.Binary);
        ///             </code>
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the given file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="path">             Full pathname of the file.</param>
        /// <param name="serializedFormat"> The serialized format.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public  T Load(string path, TelEnvyXmlLib.Enums.SerializedFormat serializedFormat)
        {
            T serializableObject = null;

            switch (serializedFormat)
            {
                case TelEnvyXmlLib.Enums.SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(path, null);
                    break;

                case TelEnvyXmlLib.Enums.SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(null, path, null);
                    break;
            }

            return serializableObject;
        }

        #region Documentation
        /// Loads an object from an XML file in Document format, supplying extra data types to enable
        /// deserialization of custom types within the object.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  path        Path of the file to load the object from.
        /// \param  extraTypes  Extra data types to enable deserialization of custom types within the
        ///                     object.
        ///
        /// \returns    Object loaded from an XML file in Document format.
        ///
        /// ### example
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the given file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="path">         Full pathname of the file.</param>
        /// <param name="extraTypes">   List of types of the extras.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public  T Load(string path, System.Type[] extraTypes)
        {
            T serializableObject = LoadFromDocumentFormat(extraTypes, path, null);
            return serializableObject;
        }

        #region Documentation
        /// Loads an object from an XML file in Document format, located in a specified isolated storage
        /// area.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  fileName                    Name of the file in the isolated storage area to load the
        ///                                     object from.
        /// \param  isolatedStorageDirectory    Isolated storage area directory containing the XML file
        ///                                     to load the object from.
        ///
        /// \returns
        /// Object loaded from an XML file in Document format located in a specified isolated storage
        /// area.
        ///
        /// ### example
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the given file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public  T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            T serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        #region Documentation
        /// Loads an object from an XML file located in a specified isolated storage area, using a
        /// specified serialized format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  fileName                    Name of the file.
        /// \param  isolatedStorageDirectory    Isolated storage area directory containing the XML file
        ///                                     to load the object from.
        /// \param  serializedFormat            XML serialized format used to load the object.
        ///
        /// \returns
        /// Object loaded from an XML file located in a specified isolated storage area, using a
        /// specified serialized format.
        ///
        /// ### example
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), SerializedFormat.Binary);
        /// </code>
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the given file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        /// <param name="serializedFormat">         The serialized format.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public  T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory, TelEnvyXmlLib.Enums.SerializedFormat serializedFormat)
        {
            T serializableObject = null;

            switch (serializedFormat)
            {
                case TelEnvyXmlLib.Enums.SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(fileName, isolatedStorageDirectory);
                    break;

                case TelEnvyXmlLib.Enums.SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
                    break;
            }

            return serializableObject;
        }

        #region Documentation
        /// Loads an object from an XML file in Document format, located in a specified isolated storage
        /// area, and supplying extra data types to enable deserialization of custom types within the
        /// object.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  fileName                    Name of the file.
        /// \param  isolatedStorageDirectory    Isolated storage area directory containing the XML file
        ///                                     to load the object from.
        /// \param  extraTypes                  Extra data types to enable deserialization of custom
        ///                                     types within the object.
        ///
        /// \returns
        /// Object loaded from an XML file located in a specified isolated storage area, using a
        /// specified serialized format.
        ///
        /// ### example
        /// <code>
        /// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load("XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), new Type[] { typeof(MyCustomType) });
        /// </code>
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads the given file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        /// <param name="extraTypes">               List of types of the extras.</param>
        ///
        /// <returns>   A T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public  T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory, System.Type[] extraTypes)
        {
            T serializableObject = LoadFromDocumentFormat(null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        #endregion

        #region Save methods

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">   The serializable object.</param>
        /// <param name="path">                 Full pathname of the file.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path, null);
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves a tex file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">   The serializable object.</param>
        /// <param name="path">                 Full pathname of the file.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void SaveTexFile(T serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path, null);
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">   The serializable object.</param>
        /// <param name="path">                 Full pathname of the file.</param>
        /// <param name="serializedFormat">     The serialized format.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string path, TelEnvyXmlLib.Enums.SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case TelEnvyXmlLib.Enums.SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, path, null);
                    break;

                case TelEnvyXmlLib.Enums.SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, path, null);
                    break;
            }
        }
        //public static void Save(TeLFile serializableObject, string path)
        //{
        //       SaveToDocumentFormat(serializableObject, path, null);
        //}


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">   The serializable object.</param>
        /// <param name="path">                 Full pathname of the file.</param>
        /// <param name="extraTypes">           List of types of the extras.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string path, System.Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, extraTypes, path, null);
        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">       The serializable object.</param>
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">       The serializable object.</param>
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        /// <param name="serializedFormat">         The serialized format.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, TelEnvyXmlLib.Enums.SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case TelEnvyXmlLib.Enums.SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, fileName, isolatedStorageDirectory);
                    break;

                case TelEnvyXmlLib.Enums.SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
                    break;
            }
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">       The serializable object.</param>
        /// <param name="fileName">                 Filename of the file.</param>
        /// <param name="isolatedStorageDirectory"> Pathname of the isolated storage directory.</param>
        /// <param name="extraTypes">               List of types of the extras.</param>
        ///-------------------------------------------------------------------------------------------------

        public  void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, System.Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        #endregion

        #region Private

        #region Documentation
        /// Serializes the TelEnvyXmlLib file.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  texFile The TelEnvyXmlLib file.
        ///
        /// \returns    System.String.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Serialize te x coordinate file. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="texFile">  The tex file.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        private string SerializeTeXFile(TeLFile texFile)
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(TeLFile));
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            ser.Serialize(writer, texFile);
            return sb.ToString();
        }

        //private static TeLFile DeSerializeTeXFile(string data)
        //{
        //    XmlSerializer ser = new XmlSerializer(typeof(TeLFile));
        //    TextReader reader = new StringReader(data);
        //    return (TeLFile)ser.Deserialize(reader);
        //}

        #region Documentation
        /// Creates the file stream.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  isolatedStorageFolder   The isolated storage folder.
        /// \param  path                    The path.
        ///
        /// \returns    FileStream.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates file stream. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        ///
        /// <returns>   The new file stream. </returns>
        ///-------------------------------------------------------------------------------------------------

        private  FileStream CreateFileStream(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            FileStream fileStream = null;

            if (isolatedStorageFolder == null)
                fileStream = new FileStream(path, FileMode.OpenOrCreate);
            else
                fileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder);

            return fileStream;
        }

        #region Documentation
        /// Loads from binary format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  path                    The path.
        /// \param  isolatedStorageFolder   The isolated storage folder.
        ///
        /// \returns    T.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads from binary format. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="path">                     Full pathname of the file.</param>
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        ///
        /// <returns>   The data that was read from the binary format. </returns>
        ///-------------------------------------------------------------------------------------------------

        private  T LoadFromBinaryFormat(string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (FileStream fileStream = CreateFileStream(isolatedStorageFolder, path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                serializableObject = binaryFormatter.Deserialize(fileStream) as T;
            }

            return serializableObject;
        }

        #region Documentation
        /// Loads from document format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  extraTypes              The extra types.
        /// \param  path                    The path.
        /// \param  isolatedStorageFolder   The isolated storage folder.
        ///
        /// \returns    T.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads from document format. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="extraTypes">               List of types of the extras.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        ///
        /// <returns>   The data that was read from the document format. </returns>
        ///-------------------------------------------------------------------------------------------------

        private  T LoadFromDocumentFormat(System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T serializableObject = null;

            using (TextReader textReader = CreateTextReader(isolatedStorageFolder, path))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader) as T;
            }

            return serializableObject;
        }
        //private static TeLFile LoadFromDocumentFormat(string extraTypes)
        //{
        //    TeLFile serializableObject = null;
        //    FileInfo fInfo = new FileInfo(string.Format("{0}",extraTypes));
        //    if (!fInfo.Exists) return null;
        //   
        //    using (TextReader textReader = new StreamReader(fInfo.FullName))
        //    {

        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeLFile));
        //        textReader.ReadToEnd();
        //        serializableObject = xmlSerializer.Deserialize(textReader) as TeLFile;
        //    }

        //    return serializableObject;
        //}

        #region Documentation
        /// Creates the text reader.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  isolatedStorageFolder   The isolated storage folder.
        /// \param  path                    The path.
        ///
        /// \returns    TextReader.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates text reader. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        ///
        /// <returns>   The new text reader. </returns>
        ///-------------------------------------------------------------------------------------------------

        private  TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextReader textReader = null;

            if (isolatedStorageFolder == null)
                textReader = new StreamReader(path);
            else
                textReader = new StreamReader(new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder));

            return textReader;
        }

        #region Documentation
        /// Creates the text writer.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  isolatedStorageFolder   The isolated storage folder.
        /// \param  path                    The path.
        ///
        /// \returns    TextWriter.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates text writer. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        ///
        /// <returns>   The new text writer. </returns>
        ///-------------------------------------------------------------------------------------------------

        private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextWriter textWriter = null;

            if (isolatedStorageFolder == null)
                textWriter = new StreamWriter(path);
            else
                textWriter = new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));

            return textWriter;
        }

        #region Documentation
        /// Creates the XML serializer.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  extraTypes  The extra types.
        ///
        /// \returns    System.Xml.Serialization.XmlSerializer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates XML serializer. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="extraTypes">   List of types of the extras.</param>
        ///
        /// <returns>   The new XML serializer. </returns>
        ///-------------------------------------------------------------------------------------------------

        private  System.Xml.Serialization.XmlSerializer CreateXmlSerializer(System.Type[] extraTypes)
        {
            Type objectType = typeof(T);

           System.Xml.Serialization.XmlSerializer xmlSerializer = null;

            if (extraTypes != null)
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(objectType, extraTypes);
            else
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(objectType);

            return xmlSerializer;
        }

        #region Documentation
        /// Saves to document format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  serializableObject      The serializable object.
        /// \param  extraTypes              The extra types.
        /// \param  path                    The path.
        /// \param  isolatedStorageFolder   The isolated storage folder.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves to document format. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">       The serializable object.</param>
        /// <param name="extraTypes">               List of types of the extras.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        ///-------------------------------------------------------------------------------------------------

        private  void SaveToDocumentFormat(T serializableObject, System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (TextWriter textWriter = CreateTextWriter(isolatedStorageFolder, path))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }
        //private static void SaveToDocumentFormat(TeLFile serializableObject, string path, IsolatedStorageFile isolatedStorageFolder)
        //{
        //    using (TextWriter textWriter = CreateTextWriter(isolatedStorageFolder, path))
        //    {
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeLFile));
        //        xmlSerializer.Serialize(textWriter, serializableObject);
        //    }
        //}

        #region Documentation
        /// Saves to binary format.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  serializableObject      The serializable object.
        /// \param  path                    The path.
        /// \param  isolatedStorageFolder   The isolated storage folder.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves to binary format. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="serializableObject">       The serializable object.</param>
        /// <param name="path">                     Full pathname of the file.</param>
        /// <param name="isolatedStorageFolder">    Pathname of the isolated storage folder.</param>
        ///-------------------------------------------------------------------------------------------------

        private  void SaveToBinaryFormat(T serializableObject, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (FileStream fileStream = CreateFileStream(isolatedStorageFolder, path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, serializableObject);
            }
        }
   

        #endregion
    }
}
