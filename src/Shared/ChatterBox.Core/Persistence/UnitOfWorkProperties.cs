using System;

namespace ChatterBox.Core.Persistence
{
    public class UnitOfWorkProperties : IComparable<UnitOfWorkProperties>
    {
        public UnitOfWorkProperties()
        {
        }

        public UnitOfWorkProperties(Guid unitOfWorkId, int sequenceNumber, DateTimeOffset factTimestamp)
        {
            UnitOfWorkId = unitOfWorkId;
            SequenceNumber = sequenceNumber;
            FactTimestamp = factTimestamp;
        }

        public Guid UnitOfWorkId { get; set; }

        public int SequenceNumber { get; set; }

        public DateTimeOffset FactTimestamp { get; set; }

        public int CompareTo(UnitOfWorkProperties other)
        {
            if (FactTimestamp < other.FactTimestamp) return -1;
            if (FactTimestamp > other.FactTimestamp) return 1;

            var uowIdComparison = UnitOfWorkId.CompareTo(other.UnitOfWorkId);
            if (uowIdComparison != 0) return uowIdComparison;

            return SequenceNumber.CompareTo(other.SequenceNumber);
        }
    }
}