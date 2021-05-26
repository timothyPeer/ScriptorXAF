
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyXmlLib.Helper
{
    #region Documentation
    /// Interface ILogEntry
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for log entry. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface ILogEntry
    {
        #region Documentation
        /// Gets or sets the classification
        ///
        /// \returns    The classification.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the classification. </summary>
        ///
        /// <value> The classification. </value>
        ///-------------------------------------------------------------------------------------------------

        string Classification { get; set; }

        #region Documentation
        /// Gets or sets the date time
        ///
        /// \returns    The date time.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the date time. </summary>
        ///
        /// <value> The date time. </value>
        ///-------------------------------------------------------------------------------------------------

        DateTime DateTime { get; set; }

        #region Documentation
        /// Gets or sets the description
        ///
        /// \returns    The description.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        string Description { get; set; }

        #region Documentation
        /// Gets or sets the source for the
        ///
        /// \returns    The source.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the source for the. </summary>
        ///
        /// <value> The source. </value>
        ///-------------------------------------------------------------------------------------------------

        string Source { get; set; }

        #region Documentation
        /// Gets or sets the system
        ///
        /// \returns    The system.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the system. </summary>
        ///
        /// <value> The system. </value>
        ///-------------------------------------------------------------------------------------------------

        string System { get; set; }

        #region Documentation
        /// Gets or sets the thread
        ///
        /// \returns    The thread.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the thread. </summary>
        ///
        /// <value> The thread. </value>
        ///-------------------------------------------------------------------------------------------------

        string Thread { get; set; }

        #region Documentation
        /// Gets or sets the type
        ///
        /// \returns    The type.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the type. </summary>
        ///
        /// <value> The type. </value>
        ///-------------------------------------------------------------------------------------------------

        string Type { get; set; }
    }
}
