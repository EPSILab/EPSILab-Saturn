using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for member informations
    /// </summary>
    class MembreDesignViewModel : DetailsViewModel<Membre>
    {
        public MembreDesignViewModel()
        {
            Element = new Membre
                {
                    Nom = "Vigneron",
                    Prenom = "Jean-Baptiste",
                    Classe = new Classe
                        {
                            Annee_Promo_Sortante = 2014,
                            Libelle = "I3"
                        },

                    Presentation = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris."
                };
        }
    }
}