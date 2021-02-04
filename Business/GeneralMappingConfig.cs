using AutoMapper;
using Business.DTOs;
using Bussiness.Dto;
using DAL.Entities;

namespace Business
{
    public class GeneralMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Company, CompanyDto>().ReverseMap();
            config.CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
            config.CreateMap<JobApplicationAnswer, JobApplicationAnswerDto>().ReverseMap();
            config.CreateMap<JobOffer, JobOfferDto>().ReverseMap();
            config.CreateMap<JobOfferQuestion, JobOfferQuestionDto>().ReverseMap();
            config.CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            config.CreateMap<User, UserRegisterDto>().ReverseMap();
            config.CreateMap<User, UserShowDto>().ReverseMap();
        }
    }
}
