SolarSystem-Saturn
==============

Contains all sources for the Windows 8 application and the Windows Phone 8 application for EPSILab.

You can download the application on:
- Windows 8 App: http://apps.microsoft.com/windows/fr-FR/app/epsilab/d238d3c6-fc4c-4190-961b-0dff8e6438e4
- Windows Phone App: http://www.windowsphone.com/fr-fr/store/app/epsilab/d5f38513-160d-49cb-99be-8d15bf9bb217

#HowTo
Clone this repository and open the solution file named "**Saturn.sln**".

#License
This project has a **LGPL** license.

#Required
- Windows 8 or higher
- .NET 4.5
- Visual Studio 2012 or higher
- Windows Phone 8.0 SDK

#Projects:
- **Saturn.Model**
 - Get all the informations from EPSILab web-service
 - Portable Class Library project
- **Saturn.ViewModel**
 - Contains all view-models for the Windows 8 and Windows Phone apps
 - Portable Class Library project
- **Saturn.Windows8**
 - The application dedicated to the Windows Store
 - Windows 8.0 project
- **Saturn.Windows8.BackgroundTasks**
 - Contains background tasks which update the Windows 8 application tile and display toast notifications if a new element has been published.
 - Windows 8.0 Class Library project
- **Saturn.Windows8.NotificationsFactory**
 - Contains all classes to update the Windows 8 application tile and to display toasts notifications when a new element is published
 - Windows 8.0 Class Library project
- **Saturn.WindowsPhone8**
 - The application dedicated to the Windows Phone Store for Windows Phone 8
 - Windows Phone 8.0 project
- **Saturn.WindowsPhone8.BackgroundTasks**
 - Contains background tasks which update the WP8 application tile and display toast notifications if a new element has been published.
 - Windows Phone 8.0 Scheduled Task Agent project
- **Saturn.WindowsPhone8.NotificationsFactory**
 - Contains all classes to update the WP8 application tile and display toast notifications if a new element has been published.
 - Windows Phone 8.0 Class Library project
 
#Toolkits
- MVVM Light Toolkit for PCL
- NotificationExtensions (for the Windows 8 app)
- BindableApplicationBar (for the Windows Phone app)
- Windows Phone Toolkit (for the Windows Phone app)

This project use **NuGet Package Manager** to retrieve all necessary packages and their dependencies.
