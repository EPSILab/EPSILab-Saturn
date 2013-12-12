using Microsoft.Phone.Tasks;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.WP8.Helpers.Tasks
{
    static class EmailTaskHelper
    {
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
