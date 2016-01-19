using System.Threading;

/// <summary>
/// A thread-safe byte array, that will wait for lock before performing operations.
/// </summary>
namespace ScanWidget
{
    class AtomicByteArray
    {
        /// <summary>
        /// The byte data.
        /// </summary>
        private byte[] _bytes;

        /// <summary>
        /// The lock for thread safety.
        /// </summary>
        private int usingLock;

        /// <summary>
        /// Creates a thread-safe byte array.
        /// </summary>
        /// <param name="bytes"></param>
        public AtomicByteArray(byte[] bytes)
        {
            _bytes = bytes;
            usingLock = 0;
        }

        /// <summary>
        /// Returns the byte array.
        /// </summary>
        /// <returns>The byte array.</returns>
        public byte[] value()
        {
            byte[] ret;
            // Wait for lock
            while (Interlocked.Exchange(ref usingLock, 1) != 0) { }
            ret = _bytes;
            Interlocked.Exchange(ref usingLock, 0);
            return ret;
        }

        /// <summary>
        /// Sets the byte array.
        /// </summary>
        /// <param name="bytes">The new bytes to set as the current value.</param>
        public void set(byte[] bytes)
        {
            // wait for lock
            while (Interlocked.Exchange(ref usingLock, 1) != 0) { }
            _bytes = bytes;
            Interlocked.Exchange(ref usingLock, 0);
        }
    }
}
