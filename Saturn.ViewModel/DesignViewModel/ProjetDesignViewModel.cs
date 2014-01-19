using EPSILab.SolarSystem.Saturn.Model.ReadersService;

namespace EPSILab.SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for projects
    /// </summary>
    class ProjetDesignViewModel : DetailsViewModel<Projet>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjetDesignViewModel()
        {
            Element = new Projet
                {
                    Description = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris.",
                    Avancement = 77,
                    Nom = "Microsoft Office",
                    Ville = new Ville
                        {
                            Libelle = "Paris"
                        }
                };
        }
    }
}