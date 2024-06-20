using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Repositories;
using WebApplication4.Repositories;

public class PointsCalculationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmailSender _emailSender;
    public PointsCalculationService(IServiceProvider serviceProvider, IEmailSender emailSender)
    {
        _serviceProvider = serviceProvider;
        _emailSender = emailSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Initial delay before starting the loop
        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Run your periodic logic here
                using (var scope = _serviceProvider.CreateScope())
                {
                    var likesRepo = scope.ServiceProvider.GetRequiredService<LikesRepo>();
                    var likesByUser = new Dictionary<int, int>();
                    // Example: Calculate and add points
                    likesByUser = likesRepo.CalculateAndAddLikesAsPoints();
                    foreach (var entry in likesByUser)
                    {
                        int userId = entry.Key;
                        int totalLikes = entry.Value;
                        string email = UserRepo.FindUserById(userId).Email;
                        var subject = "Weekly point count";
                        var message = "This week your weekly contribution earned you: " + totalLikes + " points";

                        // Send the email using the EmailSender
                        _emailSender.SendEmailAsync(email, subject, message);
                    }
                }

                // Sleep for a week (adjust the duration as needed)
                await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
            }
            catch (Exception ex)
            {
                // Log any errors
                Console.WriteLine($"Error in PointsCalculationService: {ex.Message}");
            }
        }
    }
}