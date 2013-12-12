using SolarSystem.Saturn.DataAccess.Webservice;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    class ProjetDesignViewModel : DetailsViewModel<Projet>
    {
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
