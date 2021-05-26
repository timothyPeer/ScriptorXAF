// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TeXSpecialKeyNotSupported.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;

namespace TelEnvyXmlLib.EventArgs
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An argument out of cell range event argument. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ArgumentOutOfCellRangeEventArg : System.EventArgs
    {
     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the column. </summary>
        ///
        /// <value> The column. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Column { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the row. </summary>
        ///
        /// <value> The row. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Row { get; set; }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the level. </summary>
        ///
        /// <value> The level. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Level { get; private set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ArgumentOutOfCellRangeEventArg class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        ///-------------------------------------------------------------------------------------------------

        public ArgumentOutOfCellRangeEventArg(int row, int column)
        {
            Row = row;
            Column = column;
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ArgumentOutOfCellRangeEventArg class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public ArgumentOutOfCellRangeEventArg()
        {

        }

    }
}
