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
            MapReports();
        }

        private void MapRoles()
        {
            _configuration.CreateMap<Role, RoleView>()
                .ForMember(role => role.Permissions, member => member.Ignore());
            _configuration.CreateMap<RoleView, Role>()
                .ForMember(role => role.Permissions, member => member.MapFrom(role => new List<RolePermission>()));
        }

        private void MapReports()
        {
            _configuration.CreateMap<Employee, ServiceReportEmployeeExcelView>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeInternalCode, opt => opt.MapFrom(src => src.InternalCode))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name));

            _configuration.CreateMap<Service, ServiceReportExcelView>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Rate.Client.Name))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Rate.Activity.Name))
                .ForMember(dest => dest.VehicleTypeName, opt => opt.MapFrom(src => src.Rate.VehicleType.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Rate.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ServiceFullPrice, opt => opt.MapFrom(src => src.FullPrice))
                .ForMember(dest => dest.EmployeePercentage, opt => opt.MapFrom(src => src.Rate.EmployeePercentage))
                .ForMember(dest => dest.CarrierName, opt => opt.MapFrom(src => src.Carrier.Name))
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.VehicleNumber))
                .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sector.Name))
                .ForMember(dest => dest.CustomsInformation, opt => opt.MapFrom(src => src.CustomsInformation))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        }

    }
}
