﻿using AutoMapper;
using $ext_safeprojectname$Services.Db.Entities;


namespace $safeprojectname$.Services.$ext_safeprojectname$StorageService.Mappers;


public class LogsMappingProfile : Profile
{
    private static readonly LogChangedDateValueResolver _logChangedDateValueResolver = new();


    public LogsMappingProfile()
    {
        CreateMap<MyAwesomeProduct, LogMyAwesomeProduct>()
            .ForMember(dst => dst.ChangedDate, opt => opt.MapFrom(_logChangedDateValueResolver));
    }
}


public class LogChangedDateValueResolver : IValueResolver<object, object, DateTime>
{
    public DateTime Resolve(object src, object dst, DateTime _, ResolutionContext context)
    {
        return DateTime.UtcNow;
    }
}