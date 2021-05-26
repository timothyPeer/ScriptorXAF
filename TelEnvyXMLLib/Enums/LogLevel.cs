

using System;
using System.Linq;

namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Log level - specifies the levels of severity. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum LogLevel
    {
        /// <summary>
        /// Be verbose - log everything.
        /// </summary>
        Verbose = 10,   /* . */

        /// <summary>
        /// Log all messages useful for debugging purposes.
        /// </summary>
        Debug = 20, /* An enum constant representing the debug option */

        /// <summary>
        /// Only log informative messages.
        /// </summary>
        Info = 30,  /* An enum constant representing the Information option */

        /// <summary>
        /// Only log errors.
        /// </summary>
        Error = 40, /* An enum constant representing the error option */

        /// <summary>
        /// The Off level designates a higher level than all the rest.
        /// </summary>
        Off = Int32.MaxValue,   /* An enum constant representing the off option */
    }
}
