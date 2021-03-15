# CodeChallengeGenerator

## Introduction
This console application is for the code challenge to read an Xml input report, perform a series of caluclations and aggregations (with reference data) then produce an Xml output report.

Following the spec closly the application monitors a directory for reports and processes them immediatly when a report is dropped in.  Configuration of these folders location is stored in a standard .NET Core appsettings.json file.

## Spec changes
- The Xml report format for the Total heat for each coal generator didn't fit well and a collection has been added.
- The system will archive the Report once it has been archived in a seperate folder.

## Development
The solution has been develped using .NET Core 3.1 in VS2019 community.

A number of nuget packages have been used to facilitate some functionality and tooling for the application:

### Production
- Microsoft.Extensions.Hosting - Version=5.0.0 (standard .NET core configuration)

### Testing
- Microsoft.NET.Test.Sdk Version=16.7.1
- Moq Version=4.16.1
- xunit Version=2.4.1
- xunit.runner.visualstudio Version=2.4.3
- coverlet.collector Version=1.3.0


