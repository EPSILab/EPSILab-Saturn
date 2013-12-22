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

#Layers:
- **DataAccess:** Get all the informations from the EPSILab web-service. (PCL project)
- **Model:** The Model layer from the MVVM pattern. (PCL project)
- **ViewModel:** The common View-Model layer from the MVVM pattern for both applications. (PCL project)
- **Win8:** Windows 8/RT project for Windows Store. Represents the View from the MVVM pattern.
- **WP8:** Windows Phone 8 project for Windows Phone. Represents the View from the MVVM pattern.

#Toolkits
- MVVM Light Toolkit for PCL
- NotificationExtensions (for the Windows 8 app)
- BindableApplicationBar (for the Windows Phone app)
- Windows Phone Toolkit (for the Windows Phone app)

This project use **NuGet Package Manager** to retrieve all necessary packages and their dependencies.
