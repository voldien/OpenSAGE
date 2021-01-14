using Veldrid;

namespace OpenSage.Graphics
{
    public sealed class ConstantBuffer<T> : DisposableBase
        where T : unmanaged
    {
        public DeviceBuffer Buffer { get; }

        public T Value;

        public unsafe uint ElementSize {get { return (uint)sizeof(T); } }

        public unsafe ConstantBuffer(GraphicsDevice graphicsDevice, string name = null) : this(graphicsDevice, 1, name) { }

        public unsafe ConstantBuffer(GraphicsDevice graphicsDevice, uint count, string name){
            Buffer = AddDisposable(graphicsDevice.ResourceFactory.CreateBuffer(
                new BufferDescription(
                    count * (uint)sizeof(T),
                    BufferUsage.UniformBuffer | BufferUsage.Dynamic)));

            if (name != null)
            {
                Buffer.Name = name;
            }
        }

        public void Update(CommandList commandList)
        {
            commandList.UpdateBuffer(Buffer, 0, ref Value);
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.UpdateBuffer(Buffer, 0, ref Value);
        }
    }
}
