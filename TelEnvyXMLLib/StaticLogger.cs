using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace TelEnvyXmlLib
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A static logger. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class StaticLogger
    {


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes static members of the TelEnvyXmlLib.StaticLogger class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        static StaticLogger()
        {

        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs an information. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogInfo(Type declaringType, string text)
        {
            LogManager.GetLogger(declaringType.FullName).Info(text);
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs a debug. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogDebug(Type declaringType, string text)
        {
            LogManager.GetLogger(declaringType.FullName).Debug(text);
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs a trace. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogTrace(Type declaringType, string text)
        {
            LogManager.GetLogger(declaringType.FullName).Trace(text);
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs an error. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="e">                An Exception to process.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogError(Type declaringType, Exception e, string text)
        {
            LogManager.GetLogger(declaringType.FullName).Error(e, text);
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs an information. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogInfo(string declaringType, string text)
        {
            LogManager.GetLogger(declaringType).Info(text);
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs a debug. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogDebug(string declaringType, string text)
        {
            LogManager.GetLogger(declaringType).Debug(text);
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs a trace. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogTrace(string declaringType, string text)
        {
            LogManager.GetLogger(declaringType).Trace(text);
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs an error. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="declaringType">    Type of the declaring.</param>
        /// <param name="e">                An Exception to process.</param>
        /// <param name="text">             The text.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void LogError(string declaringType, Exception e, string text)
        {
            LogManager.GetLogger(declaringType).Error(e, text);
        }
    }
}
