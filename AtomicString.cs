using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScanWidget
{
    /// <summary>
    /// Thread safe String.
    /// </summary>
    public class AtomicString
    {
        private String _value;
        private int _usingLock;
        
        /// <summary>
        /// Creates a thread safe string.
        /// </summary>
        /// <param name="str">The default string value.</param>
        public AtomicString(String str)
        {
            _value = str;
            _usingLock = 0;
        }

        /// <summary>
        /// Returns the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public String value()
        {
            String ret = "";
            while (Interlocked.Exchange(ref _usingLock, 1) != 0) { }
            ret = _value;
            Interlocked.Exchange(ref _usingLock, 0);
            return ret;
        }


        /// <summary>
        /// Sets the string value.
        /// </summary>
        /// <param name="str">The string value to set to.</param>
        public void set(String str)
        {
            while (Interlocked.Exchange(ref _usingLock, 1) != 0) { }
            _value = str;
            Interlocked.Exchange(ref _usingLock, 0);
        }
    }
}
