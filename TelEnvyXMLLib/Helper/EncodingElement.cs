// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-30-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-30-2018
// ***********************************************************************
// <copyright file="EncodingElement.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelEnvyXmlLib.Helper
{
    #region Documentation
    /// Helper class for working with Encoding combo box.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An encoding element. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class EncodingElement
    {
        #region Documentation
        /// Gets the code page.
        ///
        /// \returns    The code page.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the code page. </summary>
        ///
        /// <value> The code page. </value>
        ///-------------------------------------------------------------------------------------------------

        public int CodePage
        {
            get { return _encoding.CodePage; }
        }

        #region Documentation
        /// Gets the encoding.
        ///
        /// \returns    The encoding.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the encoding. </summary>
        ///
        /// <value> The encoding. </value>
        ///-------------------------------------------------------------------------------------------------

        public Encoding Encoding
        {
            get { return _encoding; }
        }
        /// <summary>   The encoding. </summary>
        private Encoding _encoding; /* The encoding */

        #region Documentation
        /// Initializes a new instance of the <see cref="EncodingElement" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  encodingName    Name of the encoding.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.EncodingElement class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="encodingName"> Name of the encoding.</param>
        ///-------------------------------------------------------------------------------------------------

        public EncodingElement(string encodingName)
        {
            _encoding = Encoding.GetEncoding(encodingName);
        }

        #region Documentation
        /// Returns a <see cref="System.String" /> that represents this instance.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \returns    A <see cref="System.String" /> that represents this instance.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a string that represents the current object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ///
        /// <seealso cref="M:System.Object.ToString()"/>
        ///-------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return string.Format("{0} {1}", _encoding.HeaderName, _encoding.EncodingName);
        }
    }
}
