
using System;
using System.Linq;

namespace TelEnvyXmlLib.Helper
{
    #region Documentation
    /// Interface for terminal screen.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for terminal screen. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface ITerminalScreen
    {
        #region Documentation
        /// Gets or sets the columns
        ///
        /// \returns    The columns.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the columns. </summary>
        ///
        /// <value> The columns. </value>
        ///-------------------------------------------------------------------------------------------------

        int Columns { get; set; }

        #region Documentation
        /// Gets or sets the cursor left
        ///
        /// \returns    The cursor left.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the cursor left. </summary>
        ///
        /// <value> The cursor left. </value>
        ///-------------------------------------------------------------------------------------------------

        int CursorLeft { get; set; }

        #region Documentation
        /// Gets or sets the cursor top
        ///
        /// \returns    The cursor top.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the cursor top. </summary>
        ///
        /// <value> The cursor top. </value>
        ///-------------------------------------------------------------------------------------------------

        int CursorTop { get; set; }

        #region Documentation
        /// Gets or sets the rows
        ///
        /// \returns    The rows.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the rows. </summary>
        ///
        /// <value> The rows. </value>
        ///-------------------------------------------------------------------------------------------------

        int Rows { get; set; }

        #region Documentation
        /// Gets region text
        ///
        /// \param  row     The row.
        /// \param  column  The column.
        /// \param  width   The width.
        /// \param  height  (Optional) The height.
        ///
        /// \returns    An array of string.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets region text. </summary>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   (Optional) The height.</param>
        ///
        /// <returns>   An array of string. </returns>
        ///-------------------------------------------------------------------------------------------------

        string[] GetRegionText(int row, int column, int width, int height = 0);
    }
}
