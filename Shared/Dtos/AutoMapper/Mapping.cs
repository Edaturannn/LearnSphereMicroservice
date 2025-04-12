using AutoMapper;
using Dtos.Dtos.CatalogDtos.CategoryDtos;
using Dtos.Dtos.CatalogDtos.CourseTagDtos;
using Dtos.Dtos.CatalogDtos.SubCategoryDtos;
using Dtos.Dtos.CatalogDtos.TagDtos;
using Dtos.Dtos.EnrollmentDtos.EnrollmentDtos;
using Dtos.Dtos.EnrollmentDtos.EnrollmentHistoryDtos;
using Dtos.Dtos.PaymentDtos.PaymentDtos;

using Dtos.Dtos.CourseDtos.CourseDtos;
using Dtos.Dtos.CourseDtos.InstructorDtos;
using Dtos.Dtos.CommentDtos.CommentDtos;

using Entities.Concrete.CourseService;
using Entities.Concrete.EnrollmentService;
using Entities.Concrete.CommentService;
using Entities.Concrete.PaymentService;
using Entities.Concrete.CatalogService;



namespace Dtos.AutoMapper;

public class Mapping : Profile
{
    public Mapping()
    {
        //CourseService Mikroservise
        CreateMap<Dtos.CourseDtos.CategoryDtos.CreateCategoryDto, Entities.Concrete.CourseService.Category>().ReverseMap();
        CreateMap<Dtos.CourseDtos.CategoryDtos.CreateCategoryDto, Entities.Concrete.CourseService.Category>().ReverseMap();
        
        CreateMap<CreateCourseDto, Course>().ReverseMap();
        CreateMap<UpdateCourseDto, Course>().ReverseMap();

        CreateMap<CreateInstructorDto, Instructor>().ReverseMap();
        CreateMap<UpdateInstructorDto, Instructor>().ReverseMap();


        //CatalogService Mikroservise
        CreateMap<Dtos.CatalogDtos.CategoryDtos.CreateCategoryDto, Entities.Concrete.CatalogService.Category>().ReverseMap();
        CreateMap<Dtos.CatalogDtos.CategoryDtos.UpdateCategoryDto, Entities.Concrete.CatalogService.Category>().ReverseMap();


        CreateMap<CreateCourseTagDto, CourseTag>().ReverseMap();
        CreateMap<UpdateCourseTagDto, CourseTag>().ReverseMap();


        CreateMap<CreateSubCategoryDto, SubCategory>().ReverseMap();
        CreateMap<UpdateSubCategoryDto, SubCategory>().ReverseMap();

                
        CreateMap<CreateTagDto, Tag>().ReverseMap();
        CreateMap<UpdateTagDto, Tag>().ReverseMap();


        //EnrollmentService Mikroservisi

        CreateMap<CreateEnrollmentDto, Enrollment>().ReverseMap();
        CreateMap<UpdateEnrollmentDto, Enrollment>().ReverseMap();

        CreateMap<CreateEnrollmentHistoryDto, EnrollmentHistory>().ReverseMap();
        CreateMap<UpdateEnrollmentHistoryDto, EnrollmentHistory>().ReverseMap();


        //CommentService Mikroservisi

        CreateMap<CreateCommentDto, Comment>().ReverseMap();
        CreateMap<UpdateCommentDto, Comment>().ReverseMap();

        //PaymentService Mikroservisi

        CreateMap<CreatePaymentDto, Payment>().ReverseMap();
        CreateMap<UpdatePaymentDto, Payment>().ReverseMap();

    }
}