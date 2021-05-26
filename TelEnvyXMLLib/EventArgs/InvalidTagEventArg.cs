// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-28-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="TeXXmlToJsonEventArg.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelEnvyXmlLib.EventArgs
{
    #region Documentation
    /// Class TeXXmlToJsonEventArg.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.EventArgs    
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An invalid tag event argument. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class InvalidTagEventArg : System.EventArgs
    {
        // Fields...
        /// <summary>   The data. </summary>
        private string _Data;   /* The data */
         /// <summary>  The name. </summary>
         private string _Name;  /* The name */

        #region Documentation
        /// Gets or sets the name.
        ///
        /// \returns    The name.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }

        #region Documentation
        /// Gets or sets the data.
        ///
        /// \returns    The data.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the data. </summary>
        ///
        /// <value> The data. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
            }
        }
          
    }
}
