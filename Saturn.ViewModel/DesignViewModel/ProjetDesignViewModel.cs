using EPSILab.SolarSystem.Saturn.Model.ReadersService;

namespace EPSILab.SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for projects
    /// </summary>
    class ProjectDesignViewModel : DetailsViewModel<Project>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectDesignViewModel()
        {
            Element = new Project
                {
                    Description = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris.",
                    Progression = 77,
                    Name = "Microsoft Office",
                    Campus = new Campus
                        {
                            Place = "Paris"
                        }
                };
        }
    }
}