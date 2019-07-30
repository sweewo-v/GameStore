using AutoMapper;
using GameStore.Application.Interfaces.Mapping;

namespace GameStore.Application.ViewModels.Game
{
    public class GameCategoryViewModel : ICustomMapping
    {
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.GameCategory, GameCategoryViewModel>()
                .ForMember(g => g.Name, m => m.MapFrom(gm => gm.Category.Name));
        }
    }
}
