
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TelEnvyXmlLib.Logging
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A log. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class Log
    {
     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the filename of the log file. </summary>
        ///
        /// <value> The filename of the log file. </value>
        ///-------------------------------------------------------------------------------------------------

        public static string logFileName { get; private set; }

     
        //   public static readonly NLog.Logger log = LogManager.GetCurrentClassLogger();



         ///-------------------------------------------------------------------------------------------------
         /// <summary>  Initializes static members of the TelEnvyXmlLib.Logging.Log class. </summary>
         ///
         /// <remarks>  Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
         ///-------------------------------------------------------------------------------------------------

         static Log()
        {
            //#if DEBUG
            //            // Setup the logging view for Sentinel - http://sentinel.codeplex.com
            //            var PricingAndAtpCheckTarget = new NLogViewerTarget()
            //            {
            //                Name = "sentinal",
            //                Address = "udp://127.0.0.1:10041",
            //                IncludeNLogData = false
            //            };
            //            var sentinalRule = new LoggingRule("*", LogLevel.Trace, PricingAndAtpCheckTarget);
            //            LogManager.Configuration.AddTarget("sentinal", PricingAndAtpCheckTarget);
            //            LogManager.Configuration.LoggingRules.Add(sentinalRule);

            //            // Setup the logging view for Harvester - http://harvester.codeplex.com
            //            var harvesterTarget = new OutputDebugStringTarget()
            //            {
            //                Name = "harvester",
            //                Layout = "${log4jxmlevent:includeNLogData=false}"
            //            };
            //            var harvesterRule = new LoggingRule("*", LogLevel.Trace, harvesterTarget);
            //            LogManager.Configuration.AddTarget("harvester", harvesterTarget);
            //            LogManager.Configuration.LoggingRules.Add(harvesterRule);
            //#endif

          
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes the log. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="fileName"> Filename of the file.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void InitializeLog(string fileName)
        {
          //  DirectoryInfo d = new DirectoryInfo(@"c:\envy\logs\");
          //  FileInfo f = new FileInfo(string.Concat(d, string.Concat(fileName, ".log")));
          //  logFileName = f.FullName;
          //  var config = new LoggingConfiguration();
          //  
          //  
          //  var fileTarget = new FileTarget
          //  {
          //      //Header = Layout.FromString(header),
          //      CreateDirs = true,
          //      Layout = Layout.FromString("${date:format=yyyy-MM-dd HH\\:mm\\:ss.fffffff} ${level} ${message}"),
          //      FileName = logFileName,
          //      Encoding = System.Text.Encoding.GetEncoding(1251),
          //      ArchiveFileName = String.Format("{0}.{1}.log", logFileName, "{#####}"),
          //      ArchiveNumbering = ArchiveNumberingMode.Sequence,
          //      ArchiveEvery = FileArchivePeriod.None,
          //      ArchiveAboveSize = 10 * 1024 * 1024,
          //      AutoFlush = true,
          //      ConcurrentWrites = false,
          //      BufferSize = 10240
          //  };
          //  var asyncFileTarget = new AsyncTargetWrapper(fileTarget);
          //  config.AddTarget("file", asyncFileTarget);
          //
          //  var fileLoggingRule = new LoggingRule("*", LogLevel.Trace, asyncFileTarget);
          // 
          //  config.LoggingRules.Add(fileLoggingRule);
          //  
          //  LogManager.ThrowExceptions = true;
          //  LogManager.ReconfigExistingLoggers();

        }


    }
}
