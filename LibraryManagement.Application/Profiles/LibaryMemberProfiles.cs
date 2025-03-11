using AutoMapper;
using LibraryManagement.Application.Features.LibraryMembers.Dtos;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Profiles
{
    internal class LibaryMemberProfiles : Profile
    {
        public LibaryMemberProfiles()
        {
            //CreateMap<Member, GetAllLibraryMembersDto>()
            //    .ForMember(dest => dest.MemberId, options => options.MapFrom(src => src.Id))
            //    .ForMember(
            //        dest => dest.NoOfBooksBorrowed,
            //        options => options.MapFrom(src => src.NoOfBooksBorrowed)
            //    );

            //CreateMap<Member, GetLibraryMemberDto>()
            //    .ForMember(dest => dest.MemberId, options => options.MapFrom(src => src.Id))
            //    .ForMember(
            //        dest => dest.NoOfBooksBorrowed,
            //        options => options.MapFrom(src => src.NoOfBooksBorrowed)
            //    );

            //CreateMap<GetLibraryMemberDto, Member>()
            //    .ForMember(dest => dest.Id, options => options.MapFrom(src => src.MemberId))
            //    .ForMember(
            //        dest => dest.NoOfBooksBorrowed,
            //        options => options.MapFrom(src => src.NoOfBooksBorrowed)
            //    );
        }
    }
}
