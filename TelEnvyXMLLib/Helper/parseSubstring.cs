// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="parseSubstring.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelEnvyXmlLib.Exceptions;

namespace TelEnvyXmlLib.Helper
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A parse substring. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class parseSubstring
    {
        // Fields...
        // Fields...
        /// <summary>   The offset length. </summary>
        private int _OffsetLength;  /* Length of the offset */
        /// <summary>   The offset start. </summary>
        private int _OffsetStart;   /* The offset start */

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the offset start. </summary>
        ///
        /// <value> The offset start. </value>
        ///-------------------------------------------------------------------------------------------------

        public int OffsetStart
        {
            get { return _OffsetStart; }
            set
            {
                _OffsetStart = value;
            }
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the offset length. </summary>
        ///
        /// <value> The length of the offset. </value>
        ///-------------------------------------------------------------------------------------------------

        public int OffsetLength
        {
            get { return _OffsetLength; }
            set
            {
                _OffsetLength = value;
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.parseSubstring class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLMessageException"> Thrown when a Te L Message error condition occurs.</exception>
        ///
        /// <param name="originalString">   The original string.</param>
        ///-------------------------------------------------------------------------------------------------

        public parseSubstring(string originalString)
        {
            this.originalString = originalString;
            try
            {
                 process();
            }
            catch (Exception ex)
            {
                throw new TelEnvyXmlLib.Exceptions.TeLMessageException(string.Format("ParseSubstring failed for \n {0}",originalString),ex);
            }
            finally
            {
                
            }
           
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the original string. </summary>
        ///
        /// <value> The original string. </value>
        ///-------------------------------------------------------------------------------------------------

        public string originalString { get; set; }

   
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process this parseSubstring. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLMessageException"> Thrown when a Te L Message error condition occurs.</exception>
        ///-------------------------------------------------------------------------------------------------

        private void process()
        {
           

            string[] args = this.originalString.Split(':');
            int idxarg = originalString.IndexOf(':');
            if (idxarg <= 0)
                Console.WriteLine("The indexer is !zero");
            // strip the right and left brackets
          
            for (int j = 0; j < args.Length; j++)
            {

                bool hasRightBracket = (args[j].IndexOf(']') >= 0);
                bool hasLeftBracket = (args[j].IndexOf('[') >= 0);
                int xx = args[j].IndexOf(']');
                string xarg = args[j];
                string yarg = args[j];
                if (hasRightBracket)
                {
                    string argx = xarg.Substring(0, xarg.Length - 1);
                    try
                    {
                        //int safeConvert = 0;
                        //Int32.TryParse(argx, out safeConvert);
                        
                        OffsetLength = Convert.ToInt16(argx);
                    }
                    catch (Exception ex)
                    {
                        throw new TelEnvyXmlLib.Exceptions.TeLMessageException(string.Format("ParseSubstring::process failed to convert {0} to Int16", originalString), ex);
                    }
                    finally
                    {

                    }
                }
                if (hasLeftBracket)
                {
                    string argx = xarg.Substring(1, xarg.Length - 1);
                    try
                    {
                        OffsetStart = Convert.ToInt16(argx);
                    }
                    catch (Exception ex)
                    {
                        throw new TelEnvyXmlLib.Exceptions.TeLMessageException(string.Format("ParseSubstring::process failed to convert {0} to Int16 - Has Left Bracket", originalString), ex);
                    }
                    finally
                    {

                    }
                }

            }
        }
        

    }
}
