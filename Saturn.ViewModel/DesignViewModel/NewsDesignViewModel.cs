using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System;

namespace EPSILab.SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for news
    /// </summary>
    class NewsDesignViewModel : DetailsViewModel<News>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsDesignViewModel()
        {
            Element = new News
                {
                    Date_Heure = DateTime.Now,
                    Image = "",
                    Membre = new Membre
                        {
                            Nom = "Dupont",
                            Prenom = "John"
                        },
                    Titre = "Lorem ipsum dolor sit amet.",
                    Texte_Long = "<strong>Lorem ipsum</strong> dolor sit amet, <a href=\"#\">consectetur</a> adipiscing elit. Vivamus nec lacinia felis. Morbi et pharetra tortor. Donec sit amet pellentesque nunc, at scelerisque est. Aliquam vehicula lectus ipsum, a dapibus ipsum venenatis eu. Nulla imperdiet lectus nec felis eleifend, in ornare purus gravida. Curabitur dolor ligula, tempus id accumsan eget, gravida sit amet sapien. Curabitur egestas purus sed felis aliquet vehicula. Cras in mauris iaculis ligula imperdiet laoreet. Mauris eget sapien sed lorem vulputate tincidunt at nec lectus. Fusce in consectetur odio. Sed sit amet posuere eros. Aliquam sit amet sagittis justo. Etiam congue, dui id bibendum tempor, lorem dolor rhoncus lectus, ut ultricies orci quam id mi. Maecenas id fringilla metus."
                };
        }
    }
}