using AutoMapper;
using DailyApp.API.DataModel;
using DailyApp.API.DTOs;

namespace DailyApp.API.AutoMappers;

public class AutoMapperSetting : Profile
{

    public AutoMapperSetting()
    {

        CreateMap<AccountInfoDto, AccountInfo>().ReverseMap();
    }

}
