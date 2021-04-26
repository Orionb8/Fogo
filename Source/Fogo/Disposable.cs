using System;

namespace Fogo {

    public abstract class Disposable : IDisposable {

        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        protected bool _disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    DisposeManagedResources();
                }
                DisposeUnmanagedResources();
                _disposed = true;
            }
        }

        protected virtual void DisposeManagedResources() {
        }

        protected virtual void DisposeUnmanagedResources() {
        }

        ~Disposable() {
            /// Do not change this code. Put cleanup code in <see cref="Dispose(bool)"/> method.
            Dispose(false);
        }

        public void Dispose() {
            /// Do not change this code. Put cleanup code in <see cref="Dispose(bool)"/> method.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}