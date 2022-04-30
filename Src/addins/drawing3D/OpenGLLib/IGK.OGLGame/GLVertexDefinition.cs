namespace IGK.OGLGame
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct GLVertexDefinition
    {
        private int size, sizeInByte, offset;
        private enuGLVertexUsage usage;
        private enuGLDataType m_dataType;
        public int SizeInByte { get { return sizeInByte; } }
        public int Size { get { return size; } }
        public int Offset { get { return offset; } }
        public enuGLVertexUsage Usage { get { return usage; } }
        public enuGLDataType DataType { get { return m_dataType; } }
        public GLVertexDefinition(int size, int sizeInByte, int offset, enuGLVertexUsage usage, enuGLDataType dataType)
        {
            this.size = size;
            this.sizeInByte = sizeInByte;
            this.offset = offset;
            this.usage = usage;
            this.m_dataType = dataType;
        }
    }

}