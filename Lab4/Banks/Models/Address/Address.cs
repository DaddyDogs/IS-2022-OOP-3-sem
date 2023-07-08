using Banks.Exceptions;

namespace Banks.Models.Address;

public class Address
{
    private Address(string streetName, string buildingNumber, string? entranceNumber, int floorNumber, int flatNumber)
    {
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        EntranceNumber = entranceNumber;
        FloorNumber = floorNumber;
        FlatNumber = flatNumber;
    }

    public static IStreetBuilder Builder => new AddressBuilder();

    public string StreetName { get; }
    public string BuildingNumber { get; }
    public string? EntranceNumber { get; }
    public int FloorNumber { get; }
    public int FlatNumber { get; }
    private class AddressBuilder : IStreetBuilder, IBuildingBuilder, IAddressBuilder, IFlatBuilder
    {
        private string? _streetName;
        private string? _buildingNumber;
        private string? _entranceNumber;
        private int _floorNumber;
        private int _flatNumber;

        public IBuildingBuilder WithStreet(string streetName)
        {
            Validate(streetName);

            _streetName = streetName;
            return this;
        }

        public IFlatBuilder WithBuilding(string buildingNumber)
        {
            Validate(buildingNumber);

            _buildingNumber = buildingNumber;
            return this;
        }

        public IAddressBuilder WithEntrance(string entranceNumber)
        {
            Validate(entranceNumber);

            _entranceNumber = entranceNumber;
            return this;
        }

        public IAddressBuilder WithFloor(int floorNumber)
        {
            if (floorNumber < 1)
            {
                throw AddressException.InvalidNumberException("Floor", floorNumber);
            }

            _floorNumber = floorNumber;
            return this;
        }

        public IAddressBuilder WitFlat(int flatNumber)
        {
            if (flatNumber < 1)
            {
                throw AddressException.InvalidNumberException("Flat", flatNumber);
            }

            _flatNumber = flatNumber;
            return this;
        }

        public Address Build()
        {
            if (_streetName is null || _buildingNumber is null)
            {
                throw new ArgumentNullException();
            }

            return new Address(_streetName, _buildingNumber, _entranceNumber, _floorNumber, _flatNumber);
        }

        private static void Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                ArgumentNullException.ThrowIfNull(input);
            }
        }
    }
}