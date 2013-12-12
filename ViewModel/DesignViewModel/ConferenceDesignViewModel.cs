using SolarSystem.Saturn.DataAccess.Webservice;
using System;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    class ConferenceDesignViewModel : DetailsViewModel<Conference>
    {
        public ConferenceDesignViewModel()
        {
            Element = new Conference
                {
                    Date_Heure_Debut = DateTime.Now,
                    Date_Heure_Fin = DateTime.Now.AddHours(4),
                    Description = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris.",
                    Lieu = "Amphitheater",
                    Nom = "Pellentesque habitant morbi",
                    Ville = new Ville
                        {
                            Libelle = "Paris"
                        }
                };
        }
    }
}
