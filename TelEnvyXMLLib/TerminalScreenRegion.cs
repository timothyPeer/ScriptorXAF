
using System;
using System.Linq;

namespace TelEnvyXmlLib
{
   
    // <copyright file="TerminalScreenRegion.cs" company="MyCompany.com">
    // Copyright (c) 2019 MyCompany.com. All rights reserved.
    // </copyright>
    // <author>Timothy Peer, eNVy Systems Inc.</author>
    // <date>6/26/2019</date>
    // <summary>Implements the terminal screen region class</summary>
    ///-------------------------------------------------------------------------------------------------


    public class TerminalScreenRegion : IDisposable
    {
        /// <summary>   The cells. </summary>
        private TerminalCell[] _cells;  /* The cells */
        /// <summary>   The width. </summary>
        private int _width; /* The width */
        /// <summary>   The height. </summary>
        private int _height;    /* The height */
        /// <summary>   The counter offset. </summary>
        private int counterOffset = 0;  /* The counter offset */


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the buffer. </summary>
        ///
        /// <value> The buffer. </value>
        ///-------------------------------------------------------------------------------------------------

        public string buffer { get; set; }

  
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads a buffer. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="_buffer">  The buffer.</param>
        ///-------------------------------------------------------------------------------------------------

        public void LoadBuffer(string _buffer)
        {
            buffer = _buffer;
            int i = counterOffset;
            foreach (char item in _buffer)
            {
                var c = new TerminalCell() { Character = item };
                _cells.SetValue(c, i);
                i++;
                counterOffset++;
                if (counterOffset >= _maxCellBufferSize) break;     // this denotes the capacity of the screen region - do not exceed this region
                                                                    // to do... add pagination capability buffer support
            }
            // Write(_buffer);
        }

  
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the cells. </summary>
        ///
        /// <value> The cells. </value>
        ///-------------------------------------------------------------------------------------------------

        public TerminalCell[] Cells { get { return _cells; } }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the width. </summary>
        ///
        /// <value> The width. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Width { get { return _width; } }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the height. </summary>
        ///
        /// <value> The height. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Height { get { return _height; } }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        ///
        /// <returns>   The indexed item. </returns>
        ///-------------------------------------------------------------------------------------------------

        public TerminalCell this[int column, int row] { get { return GetCell(column, row); } }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a cell. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        ///
        /// <returns>   The cell. </returns>
        ///-------------------------------------------------------------------------------------------------

        public TerminalCell GetCell(int row, int column)
        {
            CheckCoords(row, column);
            return _cells[row * _width + column];
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check coordinates. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside
        ///                                                the required range.</exception>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        ///-------------------------------------------------------------------------------------------------

        private void CheckCoords(int row, int column)
        {
            if (column < 0 || column >= _width)
                throw new ArgumentOutOfRangeException(string.Format("CheckCoords-Width Out of Range @[{0}:{1}]", row, column));

            if (row < 0 || row >= _height)
                throw new ArgumentOutOfRangeException(string.Format("CheckCoords-Height Out of Range @[{0}:{1}]", row, column));
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TerminalScreenRegion class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="height">   The height.</param>
        /// <param name="width">    The width.</param>
        ///-------------------------------------------------------------------------------------------------

        internal TerminalScreenRegion(int height, int width)
        {
            _width = width;
            _height = height;
            _cells = new TerminalCell[width * height];
            _maxCellBufferSize = _cells.Length;
        }
        //private void Write (string s)
        //{
        //    _cells.Clear();
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        TerminalCell c = new TerminalCell() { Character = s.ToCharArray()[i] };
        //        _cells.Add(c);
        //    }
        //}

        #region IDisposable Support
        /// <summary>   True to disposed value. </summary>
        private bool disposedValue = false; /* True to disposed value */
        /// <summary>   The maximum size of the cell buffer. </summary>
        private int _maxCellBufferSize = 0; /* The maximum size of the cell buffer */

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Performs application-defined tasks associated with freeing, releasing, or
        ///             resetting unmanaged resources.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="disposing"> True to release both managed and unmanaged resources; false to
        ///                          release only unmanaged resources.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cells = null;//.Clear();
                    GC.WaitForPendingFinalizers();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TerminalScreenRegion() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Performs application-defined tasks associated with freeing, releasing, or
        ///             resetting unmanaged resources.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        ///-------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
