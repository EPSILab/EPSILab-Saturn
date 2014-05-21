using EPSILab.SolarSystem.Saturn.Model.ReadersService;

namespace EPSILab.SolarSystem.Saturn.ViewModel.DesignViewModel
{
    /// <summary>
    /// Design view-model for member informations
    /// </summary>
    class MemberDesignViewModel : DetailsViewModel<Member>
    {
        public MemberDesignViewModel()
        {
            Element = new Member
                {
                    LastName = "Vigneron",
                    FirstName = "Jean-Baptiste",
                    Promotion = new Promotion
                        {
                            GraduationYear = 2014,
                            Name = "I3"
                        },

                    Presentation = "Praesent tristique nisl tortor, et vulputate nisl vehicula ac. Donec eget venenatis dolor. In at iaculis elit. Aliquam porta consequat odio, non sagittis massa dapibus vitae. Pellentesque ac egestas neque, ac lacinia mauris."
                };
        }
    }
}