using Fossil;

namespace OnlineMultiplayerMod.Model
{
    public class Delta<T>
    {
        public long IterationNumber { get; private set; }
        public byte[] DeltaBytes { get; private set; }
        public byte[] OriginalBytes { get; private set; }

        public Delta(T state)
        {
            OriginalBytes = Serialize(state);
        }

        public Delta(byte[] state)
        {
            OriginalBytes = state;
        }

        public void IncrementDelta(T state)
        {
            byte[] newBytes = Serialize(state);

            DeltaBytes = Delta.Create(OriginalBytes, newBytes);
            OriginalBytes = Delta.Apply(OriginalBytes, DeltaBytes);

            IterationNumber++;
        }

        public void IncrementDelta(byte[] state)
        {
            DeltaBytes = Delta.Create(OriginalBytes, state);
            OriginalBytes = Delta.Apply(OriginalBytes, DeltaBytes);

            IterationNumber++;
        }

        public T ApplyTo(T state)
        {
            byte[] stateBaseBytes = Serialize(state);

            byte[] mergedBytes = Delta.Create(stateBaseBytes, DeltaBytes);

            return Deserialize(mergedBytes);
        }

        private static byte[] Serialize(T state)
        {
            return DynamicMessagePackSerializer.Get(state.GetType()).PackSingleObject(state);
        }

        private static T Deserialize(byte[] state)
        {
            return (T)DynamicMessagePackSerializer.Get(state.GetType()).UnpackSingleObject(state);
        }
    }
}