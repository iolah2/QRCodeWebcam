﻿using System;

namespace OpenFileWithQRReader.Abstractions
{
    /// <summary>
    /// Event arguments for the event when file picking was cancelled, either
    /// by the user or when an exception occured
    /// </summary>
    public class FilePickerCancelledEventArgs : EventArgs
    {
        /// <summary>
        /// Exception that occured that led to cancelling file picking; may be
        /// null when file picking was cancelled by the user
        /// </summary>
        public Exception Exception { get; set; }
    }
}
