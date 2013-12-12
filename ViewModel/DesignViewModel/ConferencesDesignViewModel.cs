using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SolarSystem.Saturn.DataAccess.Webservice;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    internal class ConferencesDesignViewModel : MasterViewModel<Conference>
    {
        public ConferencesDesignViewModel()
        {
            Elements = new ObservableCollection<Conference>();

            for (int i = 0; i < 10; i++)
            {
                Elements.Add(new Conference
                    {
                        Date_Heure_Debut = DateTime.Now,
                        Date_Heure_Fin = DateTime.Now.AddHours(4),
                        Description =
                            "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris.",
                        Lieu = "Amphitheater",
                        Nom = "Pellentesque habitant morbi",
                        Ville = new Ville
                            {
                                Libelle = "Paris"
                            }
                    });
            }
        }
    }
}