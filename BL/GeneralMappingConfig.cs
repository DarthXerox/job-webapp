using AutoMapper;
using BL.Entities.Dto;
using DAL.Entities;

namespace Bussiness
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
            config.CreateMap<JobOfferSkill, JobOfferSkillDto>().ReverseMap();
            config.CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            config.CreateMap<JobSeekerSkill, JobSeekerSkillDto>().ReverseMap();
            config.CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
