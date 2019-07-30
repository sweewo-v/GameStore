using System;
using System.Collections.Generic;
using System.Text;
using GameStore.Application.ViewModels.Category;
using GameStore.Application.ViewModels.Game;

namespace GameStore.Application.ViewModels.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; } 
        public IEnumerable<CategoryViewModel> Categories { get; set; } 
    }
}
