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
            _configuration.AddConditionalObjectMapper().Conventions.Add(pair => pair.SourceType.Namespace != "Castle.Proxies");
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
        }
    }
}
