using AutoMapper;
using LibraryManagement.Application.Features.Members.Dtos;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Profiles
{
    public class MemberProfiles : Profile
    {
        public MemberProfiles()
        {
            CreateMap<Member, GetAllMembersResponseDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.NoOfBooksBorrowed, options => options.MapFrom(src => src.Books.Count))
                .ForMember(dest => dest.FullName, options => options.MapFrom(src => src.FullName));

            CreateMap<Member, GetMemberResponseDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, options => options.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.nic, options => options.MapFrom(src => src.NIC))
                .ForMember(dest => dest.Telephone, options => options.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.dob, options => options.MapFrom(src => src.DOB));

            //.ForMember(dest => dest.MemberType, options => options.MapFrom(src => src.MemberType));
        }
    }
}
