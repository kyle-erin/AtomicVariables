using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ScanWidget
{
    /// <summary>
    /// A thread safe boolean variable.
    /// </summary>
    public class AtomicBool
    {
        private bool _value;
        private int usingLock;

        /// <summary>
        /// Creates thread safe boolean value.
        /// </summary>
        /// <param name="tBool">The starting boolean value.</param>
        public AtomicBool(bool tBool)
        {
            _value = tBool;
            usingLock = 0;
        }

        /// <returns>Returns the boolean value.</returns>
        public bool value()
        {
            bool ret = false;
            // Wait for lock
            while (Interlocked.Exchange(ref usingLock, 1) != 0) { }
            ret = _value;
            Interlocked.Exchange(ref usingLock, 0); // Release lock
            return ret;
        }

        /// <summary>
        /// Sets the boolean value.
        /// </summary>
        /// <param name="val">The value to set to.</param>
        public void set(bool val)
        {
            while (Interlocked.Exchange(ref usingLock, 1) != 0) { }
            _value = val;
            Interlocked.Exchange(ref usingLock, 0);
        }
    }
}
