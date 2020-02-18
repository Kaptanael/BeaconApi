using BeaconApi.Dtos.Beacon;

namespace BeaconApi.Services
{
    public interface IBeaconService
    {
        bool Insert(BeaconForCreateDto beaconForCreateDto);
    }
}