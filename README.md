# Service Manager - Tray Application
Tray application for easy access to start/stop pinned services.

## The Idea
Whilst working for a previous employer, I found I had an issue with services that were running in the background of my laptop. Some of the services running in the background, such as SQL Server, were quite resource intensive. This led to situations where I was in meetings and the laptop fans would flare up because of high CPU/memory usage and the battery would drain long before it's should. I had to continually go back and fourth with the Task Manager and Services screens to start and stop these services when I was in a meeting or wanted to start developing again.

I wanted a quicker way of stopping/start these services I regularly toggled. Service Manager was born.

I started work on a C# WPF and Tray application to help achieve my goal. 

## Progress

At present, the application is capable of starting and stopping services however as such does not currently have the functionality to watch/bookmark services. I have been working on refactoring some of my existing code to be more reusable and to be more asynchronous to prevent blocking UI calls. 

To-Do:
- Add service watch functionality
- Add toggle ability to context menu for tray icon

Nice to Haves:
- CLI Interfaces
- Service grouping for toggling services
- Environmental rules for toggling services/groups such as:
  - Battery status
  - Scheduling
