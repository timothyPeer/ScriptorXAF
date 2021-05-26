
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace TelEnvyXmlLib
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Serializable) a terminal cell. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public struct TerminalCell
    {
      
        //      private ConsoleUnderline _underline;



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the character. </summary>
        ///
        /// <value> The character. </value>
        ///-------------------------------------------------------------------------------------------------

        public char Character { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the color of the foreground. </summary>
        ///
        /// <value> The color of the foreground. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ForeColor { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the color of the back. </summary>
        ///
        /// <value> The color of the back. </value>
        ///-------------------------------------------------------------------------------------------------

        public int BackColor { get; set; }

        ///// <summary>
        ///// Gets or sets the underline style.
        ///// </summary>
        ///// <value>Underline style.</value>
        //public ConsoleUnderline Underline
        //{
        //    get { return _underline; }
        //    set { _underline = value; }
        //}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the italic. </summary>
        ///
        /// <value> True if italic, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Italic { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the bold. </summary>
        ///
        /// <value> True if bold, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Bold { get; set; }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the blink. </summary>
        ///
        /// <value> True if blink, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Blink { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the ignore end of line. </summary>
        ///
        /// <value> True if ignore end of line, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        internal bool IgnoreEndOfLine { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the fully qualified type name of this instance. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A <see cref="T:System.String" /> containing a fully qualified type name. </returns>
        ///
        /// <seealso cref="M:System.ValueType.ToString()"/>
        ///-------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return Character.ToString();
        }

    }
}
