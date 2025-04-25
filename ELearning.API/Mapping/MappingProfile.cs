using AutoMapper;
using ELearning.API.DTOs.Course;
using ELearning.API.DTOs.User;
using ELearning.API.DTOs.Quiz;
using ELearning.API.DTOs.Category;
using ELearning.Core.Domain;

namespace ELearning.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.Name))
                .ForMember(dest => dest.EnrolledStudentsCount, opt => opt.MapFrom(src => src.EnrolledStudents.Count))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0))
                .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));

            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.InstructedCoursesCount, opt => opt.MapFrom(src => src.InstructedCourses.Count))
                .ForMember(dest => dest.EnrolledCoursesCount, opt => opt.MapFrom(src => src.EnrolledCourses.Count));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateProfileDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            // Quiz mappings
            CreateMap<Quiz, QuizDto>()
                .ForMember(dest => dest.ModuleTitle, opt => opt.MapFrom(src => src.Module.Title))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.Name))
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count))
                .ForMember(dest => dest.AttemptsCount, opt => opt.MapFrom(src => src.Attempts.Count))
                .ForMember(dest => dest.AverageScore, opt => opt.MapFrom(src => 
                    src.Attempts.Any() ? src.Attempts.Average(a => a.Score) : 0));

            CreateMap<CreateQuizDto, Quiz>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<CreateOptionDto, Option>();

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
