using MediatR;
using WeatherDemo.Domain.Entities;
using WeatherDemo.Domain.Repositories;

namespace WeatherDemo.Application.Requests.Location
{
    public class LocationRequestHandler :
        IRequestHandler<LocationListRequest, GeoLocation[]>,
        IRequestHandler<LocationItemRequest, GeoLocation>,
        IRequestHandler<LocationDeleteRequest, bool>
    {
        private readonly IGeoLocationRepository _geoRepo;

        public LocationRequestHandler(IGeoLocationRepository geoRepo)
        {
            _geoRepo = geoRepo;
        }

        public Task<GeoLocation[]> Handle(LocationListRequest request, CancellationToken cancellationToken)
        {
            var res = _geoRepo.GetList(request.Skip, request.Take);

            return Task.FromResult(res);
        }

        public Task<GeoLocation> Handle(LocationItemRequest request, CancellationToken cancellationToken)
        {
            var res = _geoRepo.GetById(request.Id);

            return Task.FromResult(res);
        }

        public Task<bool> Handle(LocationDeleteRequest request, CancellationToken cancellationToken)
        {
            var res = _geoRepo.Delete(request.Id);

            return Task.FromResult(res);
        }
    }
}
