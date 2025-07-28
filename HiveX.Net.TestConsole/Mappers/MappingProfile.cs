using AutoMapper;
using HiveX.Net.TestConsole.Entites;
using HiveX.Net.TestConsole.Models;


namespace HiveX.Net.TestConsole.Mappers
{

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            #region Dashboard

            CreateMap<Customer, CustomerModel>().ReverseMap(); 

            #endregion




        }

    }

}
