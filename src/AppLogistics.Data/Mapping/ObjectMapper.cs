using AppLogistics.Objects;
using AutoMapper;
using System.Collections.Generic;

namespace AppLogistics.Data.Mapping
{
    public class ObjectMapper
    {
        private readonly IMapperConfigurationExpression _configuration;

        private ObjectMapper(IMapperConfigurationExpression configuration)
        {
            configuration.ValidateInlineMaps = false;
            _configuration = configuration;
            //_configuration.AddConditionalObjectMapper().Conventions.Add(pair => pair.SourceType.Namespace != "Castle.Proxies");
        }

        public static void MapObjects()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => new ObjectMapper(configuration).Map());
        }

        private void Map()
        {
            MapRoles();
        }

        private void MapRoles()
        {
            _configuration.CreateMap<Role, RoleView>()
                .ForMember(role => role.Permissions, member => member.Ignore());
            _configuration.CreateMap<RoleView, Role>()
                .ForMember(role => role.Permissions, member => member.MapFrom(role => new List<RolePermission>()));

            _configuration.CreateMap<ServiceCreateEditView, Service>()
                .ForMember(dest => dest.Holdings, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceNovelties, opt => opt.Ignore());

            _configuration.CreateMap<Service, ServiceView>()
                .ForMember(dest => dest.UnifiedVehicleTypeName, opt => opt.MapFrom(src => src.VehicleTypeId.HasValue ? src.VehicleType.Name : src.Rate.VehicleType.Name ?? null));
        }
    }
}
