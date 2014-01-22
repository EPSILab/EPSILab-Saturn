using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using Microsoft.Phone.Tasks;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Helpers.Tasks
{
    /// <summary>
    /// A helper to send a pre-made email
    /// </summary>
    static class EmailTaskHelper
    {
        /// <summary>
        /// Prepare an email to send
        /// </summary>
        /// <param name="element">Informations to prepare in the email</param>
        public static void Email(EmailableObject element)
        {
            EmailComposeTask task = new EmailComposeTask
                {
                    Body = element.Body,
                    Subject = element.Subject
                };

            task.Show();
        }
    }
}