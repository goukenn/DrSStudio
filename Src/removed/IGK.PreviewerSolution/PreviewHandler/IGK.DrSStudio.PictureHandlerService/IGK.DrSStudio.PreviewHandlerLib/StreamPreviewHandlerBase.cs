

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StreamPreviewHandlerBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:StreamPreviewHandlerBase.cs
*/
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
namespace IGK.PrevHandlerLib
{
    public abstract class StreamPreviewHandlerBase : PreviewHandler, IInitializeWithStream
    {
        private IStream _stream;
        private uint _streamMode;
        void IInitializeWithStream.Initialize(IStream pstream, uint grfMode)
        {
            _stream = pstream;
            _streamMode = grfMode;
        }
        protected override void Load(PreviewHandlerControl c)
        {
            c.Load(new ReadOnlyIStreamStream(_stream));
        }
        private class ReadOnlyIStreamStream : Stream
        {
            private IStream _stream;
            public ReadOnlyIStreamStream(IStream stream)
            {
                if (stream == null) throw new ArgumentNullException("stream");
                _stream = stream;
            }
            protected override void Dispose(bool disposing)
            {
                _stream = null;
                base.Dispose(disposing);
            }
            private void ThrowIfDisposed() { if (_stream == null) throw new ObjectDisposedException(GetType().Name); }
            public unsafe override int Read(byte[] buffer, int offset, int count)
            {
                ThrowIfDisposed();
                if (buffer == null) throw new ArgumentNullException("buffer");
                if (offset < 0) throw new ArgumentNullException("offset");
                if (count < 0) throw new ArgumentNullException("count");
                int bytesRead = 0;
                if (count > 0)
                {
                    IntPtr ptr = new IntPtr(&bytesRead);
                    if (offset == 0)
                    {
                        if (count > buffer.Length) throw new ArgumentOutOfRangeException("count");
                        _stream.Read(buffer, count, ptr);
                    }
                    else
                    {
                        byte[] tempBuffer = new byte[count];
                        _stream.Read(tempBuffer, count, ptr);
                        if (bytesRead > 0) Array.Copy(tempBuffer, 0, buffer, offset, bytesRead);
                    }
                }
                return bytesRead;
            }
            public override bool CanRead { get { return _stream != null; } }
            public override bool CanSeek { get { return _stream != null; } }
            public override bool CanWrite { get { return false; } }
            public override long Length
            {
                get
                {
                    ThrowIfDisposed();
                    const int STATFLAG_NONAME = 1;
                    STATSTG stats;
                    _stream.Stat(out stats, STATFLAG_NONAME);
                    return stats.cbSize;
                }
            }
            public override long Position
            {
                get
                {
                    ThrowIfDisposed();
                    return Seek(0, SeekOrigin.Current);
                }
                set
                {
                    ThrowIfDisposed();
                    Seek(value, SeekOrigin.Begin);
                }
            }
            public override unsafe long Seek(long offset, SeekOrigin origin)
            {
                ThrowIfDisposed();
                long pos = 0;
                IntPtr posPtr = new IntPtr((void*)&pos);
                _stream.Seek(offset, (int)origin, posPtr);
                return pos;
            }
            public override void Flush() { ThrowIfDisposed(); }
            public override void SetLength(long value)
            {
                ThrowIfDisposed();
                throw new NotSupportedException();
            }
            public override void Write(byte[] buffer, int offset, int count)
            {
                ThrowIfDisposed();
                throw new NotSupportedException();
            }
        }
    }
}

