// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-03-2018
//
// Last Modified By : tim
// Last Modified On : 03-06-2018
// ***********************************************************************
// <copyright file="LoginInfo.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Globalization;
using System.IO;
using TelEnvyXmlLib.Exceptions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TelEnvyXmlLib.Helper
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Information about the login. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class LoginInfo
    {
   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Builds a directory. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLMessageException"> Thrown when a Te L Message error condition occurs.</exception>
        ///
        /// <param name="directoryPath">    Full pathname of the directory file.</param>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.Description("Builds the directory.")]
        private void BuildDirectory(string directoryPath)
        {
            System.IO.DirectoryInfo dInfoX = null;
            try
            {
                dInfoX = new System.IO.DirectoryInfo(directoryPath);
                if (!dInfoX.Exists)
                {
                    dInfoX.Create();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TeLMessageException(string.Format("Cannot create directory {0} elevate privileges and retry", directoryPath));
            }
            finally
            {
                dInfoX = null;
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.LoginInfo class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public LoginInfo()
        {
            LoadDefaultSettings();
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads default settings. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void LoadDefaultSettings()
        {
            if (string.IsNullOrEmpty(DomainSubDirectory)) DomainSubDirectory = "";
            else DomainSubDirectory = string.Format("{0}\\", DomainSubDirectory);
            TeLDBConnectionString = string.Empty;
            TeLHostPort = 23;
            TeLHostname = "tel_SVR";
           TeLHostUserName = "tel_UID";
           TeLHostPassword = "tel_PWD";
           TeLLogDirectory = string.Format ("c:\\envy\\{0}logs", DomainSubDirectory );
           TeLRecordingDirectory = string.Format( "c:\\envy\\{0}RecordFiles", DomainSubDirectory);
           TeLOutputDirectory = string.Format( "c:\\envy\\{0}output", DomainSubDirectory);
           TeLInputDirectory = string.Format( "c:\\envy\\{0}input", DomainSubDirectory );
           TeLDebugDirectory = string.Format( "c:\\envy\\{0}debug", DomainSubDirectory);
           TeLRecordingEnabled = true;
           TeLDebugEnabled = true;
            TeLLoggingEnabled = true;
            IsEncrypted = false;
            SetEncryption = false;
            TeLScreenLength = 24;
            TeLScreenWidth = 132;
            TeLLoggingLevel = Enums.LogLevel.Debug;
            CustomerID = 0;
            TeLServerPrompt = "tel_DCLPROMPT";
            TeLServerTimeout = 1000;
            SmtpHostName = "LocalHost";
            SmtpHostUsername = string.Empty;
            SmtpHostPassword = string.Empty;
            SmtpHostPort = 25;
            TeLLoggingLevel = Enums.LogLevel.Verbose;
            TelSysNotAvailableMessage = "The System Is Not Available At This Time, Try Again Later";
            BuildDirectory(TeLLogDirectory);
            BuildDirectory(TeLRecordingDirectory);
            BuildDirectory(TeLOutputDirectory);
            BuildDirectory(TeLInputDirectory);
            BuildDirectory(TeLDebugDirectory);
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.LoginInfo class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="json"> The JSON.</param>
        ///-------------------------------------------------------------------------------------------------

        public LoginInfo(string json)
        {
            LoadDefaultSettings();
            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject["logininfo"];
           
           

            SmtpHostPort = (int)jUser["SmtpHostPort"];
            SmtpHostPassword = (string)jUser["SmtpHostPassword"];
            SmtpHostUsername = (string)jUser["SmtpHostUsername"];
            SmtpHostName = (string)jUser["SmtpHostName"];
            TeLServerTimeout = (int)jUser["TeLServerTimeout"];
            TeLServerPrompt = (string)jUser["TeLServerPrompt"];
            CustomerID = (int)jUser["CustomerID"];
            TeLLoggingLevel = (Enums.LogLevel)(int)jUser["TeLLoggingLevel"];
            TeLScreenWidth = (int)jUser["TeLScreenWidth"];
            TeLScreenLength = (int)jUser["TeLScreenLength"];
            SetEncryption = (bool)jUser["SetEncryption"];
            IsEncrypted = (bool)jUser["IsEncrypted"];
            TeLLoggingEnabled = (bool)jUser["TeLLoggingEnabled"];
            TeLDebugEnabled = (bool)jUser["TeLDebugEnabled"];
            TeLRecordingEnabled = (bool)jUser["TeLRecordingEnabled"];

            TeLDebugDirectory = (string)jUser["TeLDebugDirectory"];
            TeLInputDirectory = (string)jUser["TeLInputDirectory"];

            TeLOutputDirectory = (string)jUser["TeLOutputDirectory"];
            TeLRecordingDirectory = (string)jUser["TeLRecordingDirectory"];
            TeLLogDirectory = (string)jUser["TeLLogDirectory"];
            TeLHostPassword = (string)jUser["TeLHostPassword"];
            TeLHostUserName = (string)jUser["TeLHostUserName"];
            TeLHostname = (string)jUser["TeLHostname"];
            TeLHostPort = (int)jUser["TeLHostPort"];
            TelSysNotAvailableMessage = (string)jUser["TelSysNotAvailableMessage"];


        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets login information asynchronous. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="loginFoXmlFilePath">   Full pathname of the login fo XML file.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool GetLoginInfoAsync (string loginFoXmlFilePath)
        {
            GetLoginInfo(loginFoXmlFilePath);
            return true;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets login information. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="loginFoXmlFilePath">   Full pathname of the login fo XML file.</param>
        ///-------------------------------------------------------------------------------------------------

        public void GetLoginInfo(string loginFoXmlFilePath)
        {

            XmlDocument doc = new XmlDocument();
          
            doc.Load(@loginFoXmlFilePath);

            string jsonstr = JsonConvert.SerializeXmlNode(doc);

            string dateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");

            
            JObject jObject = JObject.Parse(jsonstr);
          
            JToken jUser;
            var hasvalue = jObject.TryGetValue("LoginInfo", out jUser);
            if (!hasvalue) return;

            SmtpHostPort = jUser["SmtpHostPort"] != null ? (int) jUser["SmtpHostPort"] : 0; 

            SmtpHostPassword = jUser["SmtpHostPassword"] == null ? (string)jUser["SmtpHostPassword"] : string.Empty;

            SmtpHostUsername = jUser["SmtpHostUsername"] != null ? (string)jUser["SmtpHostUsername"] : string.Empty;

            SmtpHostName =  jUser["SmtpHostName"] != null ?   jUser["SmtpHostName"].ToString() : string.Empty ;

            TeLServerTimeout = jUser["TeLServerTimeout"] != null ? (int)jUser["TeLServerTimeout"] : 0;

            TeLServerPrompt =  jUser["TeLServerPrompt"] != null ? string.Empty : (string)jUser["TeLServerPrompt"];

            CustomerID = jUser["CustomerID"] != null ? (int)jUser["CustomerID"] : 0;

        //    TeLLoggingLevel = jUser["TeLLoggingLevel"] != null ? (LogLevel)(int)jUser["TeLLoggingLevel"] : (LogLevel)40;

            TeLScreenWidth = jUser["TeLScreenWidth"] != null ? (int)jUser["TeLScreenWidth"] : 0;

            TeLScreenLength = jUser["TeLScreenLength"] != null ? (int)jUser["TeLScreenLength"] : 0;

            SetEncryption = jUser["SetEncryption"] != null ? (bool)jUser["SetEncryption"] : false;

            IsEncrypted = jUser["IsEncrypted"] != null ? (bool)jUser["IsEncrypted"] : false;

            TeLLoggingEnabled = jUser["TeLLoggingEnabled"] != null ? (bool)jUser["TeLLoggingEnabled"] : false;

            TeLDebugEnabled = jUser["TeLDebugEnabled"] != null ? (bool)jUser["TeLDebugEnabled"] : false;

            TeLRecordingEnabled = jUser["TeLRecordingEnabled"] != null ? (bool)jUser["TeLRecordingEnabled"] : false;

            string tempTeLDebugDirectory = jUser["TeLDebugDirectory"] != null ? (string)jUser["TeLDebugDirectory"] : string.Empty;
            TeLDebugDirectory = Path.Combine(tempTeLDebugDirectory, createddate);
            if (!Directory.Exists(TeLDebugDirectory)) Directory.CreateDirectory(TeLDebugDirectory);

            string tempTeLInputDirectory = jUser["TeLInputDirectory"] != null ? (string)jUser["TeLInputDirectory"] : string.Empty;
            TeLInputDirectory = Path.Combine(tempTeLInputDirectory, createddate);
            if (!Directory.Exists(TeLInputDirectory)) Directory.CreateDirectory(TeLInputDirectory);

            string tempTeLOutputDirectory = jUser["TeLOutputDirectory"] != null ? (string)jUser["TeLOutputDirectory"] : string.Empty;
            TeLOutputDirectory = Path.Combine(tempTeLOutputDirectory, createddate);
            if (!Directory.Exists(TeLOutputDirectory)) Directory.CreateDirectory(TeLOutputDirectory);

            string tempTeLRecordingDirectory = jUser["TeLRecordingDirectory"] != null ? (string)jUser["TeLRecordingDirectory"] : string.Empty;
            TeLRecordingDirectory = Path.Combine(tempTeLRecordingDirectory, createddate);
            if (!Directory.Exists(TeLRecordingDirectory)) Directory.CreateDirectory(TeLRecordingDirectory);

            string tempTeLLogDirectory = jUser["TeLLogDirectory"] != null ? (string)jUser["TeLLogDirectory"] : string.Empty;
            TeLLogDirectory = Path.Combine(tempTeLLogDirectory, createddate);
            if (!Directory.Exists(TeLLogDirectory)) Directory.CreateDirectory(TeLLogDirectory);

            TeLHostPassword = jUser["TeLHostPassword"] != null ? (string)jUser["TeLHostPassword"] : string.Empty;

            TeLHostUserName = jUser["TeLHostUserName"] != null ? (string)jUser["TeLHostUserName"] : string.Empty;

            TeLHostname = jUser["TeLHostname"] != null ? (string)jUser["TeLHostname"] : string.Empty;
            TeLHostPort = jUser["TeLHostPort"] != null ? (int)jUser["TeLHostPort"] : 0;
            TelSysNotAvailableMessage = jUser["TelSystemNotAvailableMessage"] != null ? (string)jUser["TelSystemNotAvailableMessage"] : string.Empty;

            if (jUser["TeLLoggingLevel"].ToString().ToLower().Contains("off")) TeLLoggingLevel = Enums.LogLevel.Off;
            if (jUser["TeLLoggingLevel"].ToString().ToLower().Contains("debug")) TeLLoggingLevel = Enums.LogLevel.Debug;
            if (jUser["TeLLoggingLevel"].ToString().ToLower().Contains("error")) TeLLoggingLevel = Enums.LogLevel.Error;
            if (jUser["TeLLoggingLevel"].ToString().ToLower().Contains("info")) TeLLoggingLevel = Enums.LogLevel.Info;
            if (jUser["TeLLoggingLevel"].ToString().ToLower().Contains("verbose")) TeLLoggingLevel = Enums.LogLevel.Verbose;
         

        }

        // Fields...

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a message describing the tel system not available. </summary>
        ///
        /// <value> A message describing the tel system not available. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TelSysNotAvailableMessage { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the identifier of the customer. </summary>
        ///
        /// <value> The identifier of the customer. </value>
        ///-------------------------------------------------------------------------------------------------

        public int CustomerID { get; set;  }

     
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te ldb connection string. </summary>
        ///
        /// <value> The te ldb connection string. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLDBConnectionString { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l hostname. </summary>
        ///
        /// <value> The te l hostname. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLHostname { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l logging level. </summary>
        ///
        /// <value> The te l logging level. </value>
        ///-------------------------------------------------------------------------------------------------

        public Enums.LogLevel TeLLoggingLevel { get; set; }

      
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l host port. </summary>
        ///
        /// <value> The te l host port. </value>
        ///-------------------------------------------------------------------------------------------------

        public int TeLHostPort { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the te l host user. </summary>
        ///
        /// <value> The name of the te l host user. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLHostUserName { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l host password. </summary>
        ///
        /// <value> The te l host password. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLHostPassword { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l log directory. </summary>
        ///
        /// <value> The pathname of the te l log directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLLogDirectory { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l recording directory. </summary>
        ///
        /// <value> The pathname of the te l recording directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLRecordingDirectory { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l output directory. </summary>
        ///
        /// <value> The pathname of the te l output directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLOutputDirectory { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l input directory. </summary>
        ///
        /// <value> The pathname of the te l input directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLInputDirectory { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l debug directory. </summary>
        ///
        /// <value> The pathname of the te l debug directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLDebugDirectory { get;set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the te l recording is enabled. </summary>
        ///
        /// <value> True if te l recording enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool TeLRecordingEnabled { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the te l debug is enabled. </summary>
        ///
        /// <value> True if te l debug enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool TeLDebugEnabled { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the te l logging is enabled. </summary>
        ///
        /// <value> True if te l logging enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool TeLLoggingEnabled { get; set; }

      
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this LoginInfo is encrypted. </summary>
        ///
        /// <value> True if this LoginInfo is encrypted, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsEncrypted { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the encryption should be set. </summary>
        ///
        /// <value> True if set encryption, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool SetEncryption { set; get; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the length of the te l screen. </summary>
        ///
        /// <value> The length of the te l screen. </value>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.DefaultValue(24)]
        public int TeLScreenLength { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the width of the te l screen. </summary>
        ///
        /// <value> The width of the te l screen. </value>
        ///-------------------------------------------------------------------------------------------------

        [System.ComponentModel.DefaultValue(80)]
        public int TeLScreenWidth { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l server prompt. </summary>
        ///
        /// <value> The te l server prompt. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLServerPrompt { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the te l server timeout. </summary>
        ///
        /// <value> The te l server timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int TeLServerTimeout { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the SMTP host. </summary>
        ///
        /// <value> The name of the SMTP host. </value>
        ///-------------------------------------------------------------------------------------------------

        public string SmtpHostName { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP host port. </summary>
        ///
        /// <value> The SMTP host port. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SmtpHostPort { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP host username. </summary>
        ///
        /// <value> The SMTP host username. </value>
        ///-------------------------------------------------------------------------------------------------

        public string SmtpHostUsername { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP host password. </summary>
        ///
        /// <value> The SMTP host password. </value>
        ///-------------------------------------------------------------------------------------------------

        public string SmtpHostPassword { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP authentication mechanism. </summary>
        ///
        /// <value> The SMTP authentication mechanism. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SmtpAuthMechanism { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP time out. </summary>
        ///
        /// <value> The SMTP time out. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SmtpTimeOut { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP priority. </summary>
        ///
        /// <value> The SMTP priority. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SmtpPriority { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the domain sub directory. </summary>
        ///
        /// <value> The pathname of the domain sub directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string DomainSubDirectory { get; set; }




    }
}
