using System;
using AutoMapper;
using GameStore.Application.Interfaces.Mapping;

namespace GameStore.Application.ViewModels.Category
{
    public class CategoryViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime AddedDateTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.Category, CategoryViewModel>()
                .ForMember(g => g.Name, m => m.MapFrom(gm => gm.Name));
        }
    }
}
