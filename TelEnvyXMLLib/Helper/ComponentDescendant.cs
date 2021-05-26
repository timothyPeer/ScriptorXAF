

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TelEnvyXmlLib.Helper
{
    #region Documentation
    /// A component descendant.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A component descendant. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.ComponentModel.Component"/>
    ///-------------------------------------------------------------------------------------------------

    public class ComponentDescendant : Component
    {
        #region Documentation
        /// Default constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.ComponentDescendant class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public ComponentDescendant()
        {

        }

        #region Documentation
        /// Gets a value indicating whether the component can raise an event.
        ///
        /// \returns    true if the component can raise events; otherwise, false. The default is true.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the component can raise an event. </summary>
        ///
        /// <value> true if the component can raise events; otherwise, false. The default is true. </value>
        ///
        /// <seealso cref="P:System.ComponentModel.Component.CanRaiseEvents"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool CanRaiseEvents
        {
            get
            {
                return base.CanRaiseEvents;
            }
        }

        #region Documentation
        /// Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the
        /// <see cref="T:System.ComponentModel.Component" />.
        ///
        /// \returns
        /// The <see cref="T:System.ComponentModel.ISite" /> associated with the
        /// <see cref="T:System.ComponentModel.Component" />, or null if the
        /// <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an
        /// <see cref="T:System.ComponentModel.IContainer" />, the
        /// <see cref="T:System.ComponentModel.Component" /> does not have an
        /// <see cref="T:System.ComponentModel.ISite" /> associated with it, or the
        /// <see cref="T:System.ComponentModel.Component" /> is removed from its
        /// <see cref="T:System.ComponentModel.IContainer" />.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the
        ///             <see cref="T:System.ComponentModel.Component" />.
        ///             </summary>
        ///
        /// <value> The <see cref="T:System.ComponentModel.ISite" /> associated with the
        ///         <see cref="T:System.ComponentModel.Component" />, or null if the
        ///         <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an
        ///         <see cref="T:System.ComponentModel.IContainer" />, the
        ///         <see cref="T:System.ComponentModel.Component" /> does not have an
        ///         <see cref="T:System.ComponentModel.ISite" /> associated with it, or the
        ///         <see cref="T:System.ComponentModel.Component" /> is removed from its
        ///         <see cref="T:System.ComponentModel.IContainer" />.
        ///         </value>
        ///
        /// <seealso cref="P:System.ComponentModel.Component.Site"/>
        ///-------------------------------------------------------------------------------------------------

        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                base.Site = value;
            }
        }

        #region Documentation
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" />
        /// and optionally releases the managed resources.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  disposing   true to release both managed and unmanaged resources; false to release
        ///                     only unmanaged resources.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Releases the unmanaged resources used by the
        ///             <see cref="T:System.ComponentModel.Component" /> and optionally releases the
        ///             managed resources.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="disposing"> true to release both managed and unmanaged resources; false to
        ///                          release only unmanaged resources.</param>
        ///
        /// <seealso cref="M:System.ComponentModel.Component.Dispose(bool)"/>
        ///-------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Documentation
        /// Returns an object that represents a service provided by the
        /// <see cref="T:System.ComponentModel.Component" /> or by its
        /// <see cref="T:System.ComponentModel.Container" />.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  service A service provided by the <see cref="T:System.ComponentModel.Component" />.
        ///
        /// \returns
        /// An <see cref="T:System.Object" /> that represents a service provided by the
        /// <see cref="T:System.ComponentModel.Component" />, or null if the
        /// <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns an object that represents a service provided by the
        ///             <see cref="T:System.ComponentModel.Component" /> or by its
        ///             <see cref="T:System.ComponentModel.Container" />.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="service"> A service provided by the
        ///                        <see cref="T:System.ComponentModel.Component" />.</param>
        ///
        /// <returns>   An <see cref="T:System.Object" /> that represents a service provided by the
        ///             <see cref="T:System.ComponentModel.Component" />, or null if the
        ///             <see cref="T:System.ComponentModel.Component" /> does not provide the specified
        ///             service.
        ///             </returns>
        ///
        /// <seealso cref="M:System.ComponentModel.Component.GetService(Type)"/>
        ///-------------------------------------------------------------------------------------------------

        protected override object GetService(Type service)
        {
            return base.GetService(service);
        }
    }
}
