using SolarSystem.Saturn.Model.ReadersService;
using System;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for show
    /// </summary>
    class SalonDesignViewModel : DetailsViewModel<Salon>
    {
        public SalonDesignViewModel()
        {
            Element = new Salon
                {
                    Date_Heure_Debut = DateTime.Now,
                    Date_Heure_Fin = DateTime.Now.AddHours(4),
                    Description = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris.",
                    Lieu = "Amphitheater",
                    Nom = "Pellentesque habitant morbi"
                };
        }
    }
}