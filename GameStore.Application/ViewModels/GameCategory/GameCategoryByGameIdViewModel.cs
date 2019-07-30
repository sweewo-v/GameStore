using AutoMapper;
using GameStore.Application.Interfaces.Mapping;

namespace GameStore.Application.ViewModels.GameCategory
{
    public class GameCategoryByGameIdViewModel : ICustomMapping
    {
        public int CategoryId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.GameCategory, GameCategoryByGameIdViewModel>()
                .ForMember(g => g.CategoryId,
                    m => m.MapFrom(gm => gm.CategoryId));
        }
    }
}
