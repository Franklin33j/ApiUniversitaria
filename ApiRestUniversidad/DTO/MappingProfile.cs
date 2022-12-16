
using ApiRestUniversidad.Data;
using AutoMapper;

namespace ApiRestUniversidad.DTO
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CareerDTO, Career>();
            CreateMap<StudentDTO, Student>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<RatingDTO, Rating>();
            CreateMap<InscriptionTeacherDTO, Recordinscriptionteacher>();
            CreateMap<InscriptionStudentDTO, Recordinscriptionstudent>();        }
    }
}
