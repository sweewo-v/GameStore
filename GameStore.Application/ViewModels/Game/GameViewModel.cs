using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Application.Interfaces.Mapping;

namespace GameStore.Application.ViewModels.Game
{
    public class GameViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<GameCategoryViewModel> Categories { get; set; }

        public decimal Price { get; set; }

        public DateTime AddedDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.Game, GameViewModel>()
                .ForMember(e => e.Categories,
                    e => e.MapFrom(g => g.GameCategories));

            configuration.CreateMap<GameViewModel, Domain.Models.Game>();
        }
    }
}
