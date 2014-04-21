using System;

namespace ChatterBox.Core.Infrastructure
{
    public class UnitOfWorkProperties : IComparable<UnitOfWorkProperties>
    {
        public UnitOfWorkProperties()
        {
        }

        public UnitOfWorkProperties(Guid unitOfWorkId, int sequenceNumber, DateTimeOffset factTimestamp)
        {
            if (unitOfWorkId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "unitOfWorkId");

            UnitOfWorkId = unitOfWorkId;
            SequenceNumber = sequenceNumber;
            FactTimestamp = factTimestamp;
        }

        public Guid UnitOfWorkId { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTimeOffset FactTimestamp { get; private set; }

        public int CompareTo(UnitOfWorkProperties other)
        {
            if (other == null) 
                throw new ArgumentNullException("other");

            if (FactTimestamp < other.FactTimestamp) return -1;
            if (FactTimestamp > other.FactTimestamp) return 1;

            var uowIdComparison = UnitOfWorkId.CompareTo(other.UnitOfWorkId);
            if (uowIdComparison != 0) return uowIdComparison;

            return SequenceNumber.CompareTo(other.SequenceNumber);
        }
    }
}