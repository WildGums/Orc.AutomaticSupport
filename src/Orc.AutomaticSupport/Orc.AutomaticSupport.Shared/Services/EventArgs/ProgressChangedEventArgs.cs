// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressChangedEventArgs.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.AutomaticSupport
{
    using System;

    /// <summary>
    /// Event args for when the download progress changes.
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="remainingTime">The remaining time.</param>
        public ProgressChangedEventArgs(int progress, TimeSpan remainingTime)
        {
            Progress = progress;
            RemainingTime = remainingTime;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets the remaining time.
        /// </summary>
        /// <value>The remaining time.</value>
        public TimeSpan RemainingTime { get; set; }
        #endregion
    }
}